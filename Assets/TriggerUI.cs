using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class VRRaycastInteraction : MonoBehaviour
{
    public XRController controller;
    public LineRenderer lineRenderer;
    public float rayLength = 10f;
    public LayerMask interactableLayer;
    public GameObject uiPopup;
    public float inputCooldown = 0.1f; // Cooldown period to prevent rapid toggling

    private GameObject currentObject;
    private bool isUIPopupActive = false;
    private bool isRayActive = false;
    private float lastTriggerTime = 0f;

    public InputActionReference rightTriggerActionReference;

    private void OnEnable()
    {
        if (rightTriggerActionReference != null)
        {
            rightTriggerActionReference.action.Enable();
        }
        else
        {
            Debug.LogError("RightTriggerActionReference is not assigned.");
        }
    }

    private void OnDisable()
    {
        if (rightTriggerActionReference != null)
        {
            rightTriggerActionReference.action.Disable();
        }
    }

    private void Update()
    {
        if (lineRenderer == null || uiPopup == null)
        {
            Debug.LogError("LineRenderer or UIPopup is not assigned.");
            return;
        }

        // Get the start and end positions from the LineRenderer
        Vector3 rayOrigin = lineRenderer.GetPosition(0);
        Vector3 rayDirection = (lineRenderer.GetPosition(1) - rayOrigin).normalized;

        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayLength, interactableLayer))
        {
            currentObject = hit.collider.gameObject;
            lineRenderer.material.color = Color.green; // Change color to indicate hit
            isRayActive = true; // Ray is hitting an object
        }
        else
        {
            currentObject = null;
            lineRenderer.material.color = Color.red; // Change color to indicate no hit
            isRayActive = false; // Ray is not hitting an object
        }

        // Handle the input action only if the ray is active and debounce input
        if (isRayActive && rightTriggerActionReference != null)
        {
            bool isTriggerPressed = rightTriggerActionReference.action.ReadValue<float>() > 0.1f;
            if (isTriggerPressed && Time.time - lastTriggerTime > inputCooldown)
            {
                HandleTriggerPress();
                lastTriggerTime = Time.time; // Update last trigger time
            }
        }
    }

    private void HandleTriggerPress()
    {
        if (currentObject != null)
        {
            isUIPopupActive = !isUIPopupActive;
            uiPopup.SetActive(isUIPopupActive);
        }
    }
}
