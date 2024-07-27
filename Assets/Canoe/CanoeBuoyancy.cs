using UnityEngine;

public class GridBuoyancy : MonoBehaviour
{
    public float buoyancyForce = 10f; // Adjust this value to control the buoyancy force
    public float waterLevel = 0f; // Set this to match your water plane's y-coordinate
    public float damping = 0.5f; // Increased damping to reduce rocking
    public float stabilizationFactor = 1f; // Additional stabilization force factor
    public int gridResolutionX = 5; // Number of points along the X-axis
    public int gridResolutionZ = 10; // Number of points along the Z-axis
    public Vector3 gridSize = new Vector3(2f, 0f, 4f); // Size of the grid on the canoe

    private Rigidbody rb;
    private Vector3[] buoyancyPoints;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GenerateBuoyancyPoints();
    }

    void FixedUpdate()
    {
        ApplyBuoyancy();
        ApplyStabilization();
    }

    void GenerateBuoyancyPoints()
    {
        // Create an array to store buoyancy points
        buoyancyPoints = new Vector3[gridResolutionX * gridResolutionZ];

        // Calculate spacing between points
        Vector3 spacing = new Vector3(gridSize.x / (gridResolutionX - 1), 0, gridSize.z / (gridResolutionZ - 1));

        // Generate points in a grid pattern
        int index = 0;
        for (int x = 0; x < gridResolutionX; x++)
        {
            for (int z = 0; z < gridResolutionZ; z++)
            {
                // Calculate the position of each point relative to the canoe
                Vector3 point = new Vector3(-gridSize.x / 2 + x * spacing.x, 0, -gridSize.z / 2 + z * spacing.z);
                buoyancyPoints[index++] = point;
            }
        }
    }

    void ApplyBuoyancy()
    {
        // Apply buoyancy to each point
        foreach (Vector3 point in buoyancyPoints)
        {
            Vector3 worldPoint = transform.TransformPoint(point); // Convert local point to world coordinates
            float displacement = waterLevel - worldPoint.y;

            if (displacement > 0)
            {
                Vector3 buoyantForce = Vector3.up * displacement * buoyancyForce / buoyancyPoints.Length;
                rb.AddForceAtPosition(buoyantForce, worldPoint, ForceMode.Acceleration);

                Vector3 dampingForce = -rb.velocity * damping / buoyancyPoints.Length;
                rb.AddForceAtPosition(dampingForce, worldPoint, ForceMode.Acceleration);
            }
        }
    }

    void ApplyStabilization()
    {
        Vector3 angularVelocity = rb.angularVelocity;
        Vector3 stabilizationTorque = -angularVelocity * stabilizationFactor;
        rb.AddTorque(stabilizationTorque, ForceMode.Acceleration);
    }

    void OnDrawGizmos()
    {
        if (buoyancyPoints == null) return;

        // Draw gizmos for each buoyancy point
        Gizmos.color = Color.red;
        foreach (Vector3 point in buoyancyPoints)
        {
            Vector3 worldPoint = transform.TransformPoint(point);
            Gizmos.DrawSphere(worldPoint, 0.05f);
        }
    }

    public void RecalculateBuoyancyPoints()
    {
        Debug.Log("RECALCULATING");
        GenerateBuoyancyPoints();
    }
}
