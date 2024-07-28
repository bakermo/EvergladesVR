using UnityEngine;
using UnityEngine.InputSystem;

public class CanoeMovement : MonoBehaviour
{
    public Rigidbody canoeRigidbody;
    public Transform canoeTransform; // Reference to the canoe's transform
    public Animator leftPaddleAnimator; // Reference to the left paddle's animator
    public Animator rightPaddleAnimator; // Reference to the right paddle's animator
    public float speed = 5f;
    public float drag = 2f; // Drag to slow down the canoe over time

    private Vector2 moveInput;
    private InputAction moveAction;

    private void Start()
    {
        var actionMap = new InputActionMap("XRI LeftHand Locomotion");
        moveAction = actionMap.AddAction("Move", binding: "<XRController>{LeftHand}/thumbstick");

        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;

        moveAction.Enable();
    }

    private void OnDestroy()
    {
        if (moveAction != null)
        {
            moveAction.performed -= OnMove;
            moveAction.canceled -= OnMove;
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        bool isRowing = moveInput != Vector2.zero;

        // Update paddle animations
        UpdatePaddleAnimations(isRowing);
    }

    private void UpdatePaddleAnimations(bool isRowing)
    {
        if (leftPaddleAnimator != null)
        {
            leftPaddleAnimator.SetBool("isRowing", isRowing);
        }
        
        if (rightPaddleAnimator != null)
        {
            rightPaddleAnimator.SetBool("isRowing", isRowing);
        }
    }

    private void FixedUpdate()
    {
        if (canoeRigidbody != null && canoeTransform != null)
        {
            if (moveInput != Vector2.zero)
            {
                // Use the canoe's forward direction to move
                Vector3 moveDirection = (canoeTransform.forward * moveInput.y + canoeTransform.right * moveInput.x) * speed * Time.fixedDeltaTime;
                canoeRigidbody.AddForce(moveDirection, ForceMode.VelocityChange);
            }
            else
            {
                // Apply drag to simulate drift
                Vector3 dragForce = -canoeRigidbody.velocity * drag;
                canoeRigidbody.AddForce(dragForce, ForceMode.Acceleration);
            }
        }
    }
}
