using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class CanoeSnapTurnProvider : MonoBehaviour
{
    public Transform canoeTransform;
    public XRRig xrRig;
    public Rigidbody canoeRigidbody;
    public Collider canoeCollider;
    public GridBuoyancy buoyancyScript;
    public float turnAmount = 45.0f;
    public float snapTurnDuration = 0.1f;

    private Vector2 turnInput;
    private InputAction turnAction;
    private bool isTurning = false;

    private void Start()
    {
        var actionMap = new InputActionMap("XRI RightHand Locomotion");
        turnAction = actionMap.AddAction("Turn", binding: "<XRController>{RightHand}/thumbstick");

        turnAction.performed += OnTurn;
        turnAction.canceled += OnTurn;

        turnAction.Enable();
    }

    private void OnDestroy()
    {
        if (turnAction != null)
        {
            turnAction.performed -= OnTurn;
            turnAction.canceled -= OnTurn;
        }
    }

    private void OnTurn(InputAction.CallbackContext context)
    {
        if (!isTurning)
        {
            turnInput = context.ReadValue<Vector2>();
            if (turnInput.x != 0)
            {
                isTurning = true;
                PerformSnapTurn(turnInput.x);
            }
        }
    }

    private void PerformSnapTurn(float direction)
    {
        float snapTurnAmount = direction > 0 ? turnAmount : -turnAmount;

        if (buoyancyScript != null)
        {
            buoyancyScript.enabled = false;
        }

        if (canoeRigidbody != null)
        {
            canoeRigidbody.useGravity = false;
            canoeRigidbody.isKinematic = true;
        }

        if (canoeCollider != null)
        {
            canoeCollider.enabled = false;
        }

        if (xrRig != null)
        {
            xrRig.RotateAroundCameraUsingOriginUp(snapTurnAmount);
        }

        if (canoeTransform != null)
        {
            StartCoroutine(RotateCanoe(snapTurnAmount));
        }

        StartCoroutine(ReactivateComponents());
    }

    private IEnumerator RotateCanoe(float snapTurnAmount)
    {
        float elapsedTime = 0f;
        Quaternion startRotation = canoeTransform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, snapTurnAmount, 0);

        while (elapsedTime < snapTurnDuration)
        {
            canoeTransform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / snapTurnDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canoeTransform.rotation = endRotation;
        isTurning = false;
    }

    private IEnumerator ReactivateComponents()
    {
        yield return new WaitForSeconds(snapTurnDuration);

        if (canoeRigidbody != null)
        {
            canoeRigidbody.isKinematic = false;
            canoeRigidbody.useGravity = true;
        }

        if (canoeCollider != null)
        {
            canoeCollider.enabled = true;
        }

        if (buoyancyScript != null)
        {
            buoyancyScript.enabled = true;
        }
    }
}
