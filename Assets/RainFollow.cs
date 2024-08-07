using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainFollow : MonoBehaviour
{
    public Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraTransform.position;

        // Override the rotation to always point downward
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
