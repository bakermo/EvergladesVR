using UnityEngine;

public class CanoeInitialAlignment : MonoBehaviour
{
    public Transform vrCamera; // Reference to the VR camera

    void Start()
    {
        if (vrCamera != null)
        {
            AlignWithVRCamera();
        }
    }

    private void AlignWithVRCamera()
    {
        Vector3 cameraForward = vrCamera.forward;
        cameraForward.y = 0; // Keep the rotation on the horizontal plane
        transform.forward = cameraForward;
    }
}
