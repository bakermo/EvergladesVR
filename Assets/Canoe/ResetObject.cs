using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ResettableObject : MonoBehaviour
{
    public Transform targetTransform; // The target transform to move the object to
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        if (targetTransform == null)
        {
            Debug.LogError("Target Transform is not assigned.");
            return;
        }

        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectExited.AddListener(OnSelectExited);
        }
        else
        {
            Debug.LogError("XRGrabInteractable component is missing.");
        }
    }

    void OnSelectExited(SelectExitEventArgs args)
    {
        if (targetTransform != null)
        {
            // Debugging logs
            Debug.Log($"Moving {gameObject.name} to target position: {targetTransform.position}, rotation: {targetTransform.rotation}");
            
            // Move and rotate the object to the target position and rotation
            transform.position = targetTransform.position;
            transform.rotation = targetTransform.rotation;
        }
        else
        {
            Debug.LogWarning("Target Transform is not assigned. Cannot reset position.");
        }
    }

    void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.RemoveListener(OnSelectExited);
        }
    }
}
