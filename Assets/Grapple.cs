using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField] private Transform firePoint; // The origin point of the raycasts
    [SerializeField] private float maxDistance = 10f; // Max distance of the raycasts
    [SerializeField] private int raycastCount = 12; // Number of raycasts

    [SerializeField, Range(0f, 180f)] private float angleRange = 120f; // Total angle of the spread, e.g., 120 degrees


    [Header("Grapple Variables")]
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody rb; // Player rigidbody
    [SerializeField] private float maxGrappleLength = 10.0f, spring = 4.5f, damper = 7.0f; // Player rigidbody
    private Vector3 raycastHitPoint;
    private SpringJoint springJoint;


    private LineRenderer lineRenderer;


    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse click to fire
        {
            ShootRaycasts();
        } else if(Input.GetMouseButtonDown(1)) {
            DetachSpring();
        }

        DrawGrapple();
    }

    private void ShootRaycasts()
    {
        // Semi-circle spans angleRange;
        float angleStep = angleRange / (raycastCount - 1);
        float startAngle = -angleRange / 2.0f;

        RaycastHit hit;
        for (int i = 0; i < raycastCount; i++)
        {
            // Calculate the current angle and direction
            float angle = startAngle + (i * angleStep);
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;

            // Shoot the raycast
            if (Physics.Raycast(firePoint.position, direction, out hit, maxDistance))
            {
                Debug.DrawRay(firePoint.position, direction * maxDistance, Color.blue, 10f);

                raycastHitPoint = hit.point;
                AttachSpring();
                // Handle the hit (e.g., attach hook, pull object, etc.)
                break; // Stop after the first hit
            }

            // Optional: Draw raycast in editor for visualization
            Debug.DrawRay(firePoint.position, direction * maxDistance, Color.red, 10f);
        }
    }

    private void AttachSpring() {
        if (springJoint != null) 
        {
            Destroy(springJoint);
        }

        springJoint = player.gameObject.AddComponent<SpringJoint>();
        springJoint.autoConfigureConnectedAnchor = false;
        springJoint.connectedAnchor = raycastHitPoint; // The point we're attaching to

        float distanceFromPoint = Vector3.Distance(player.transform.position, firePoint.position);


        springJoint.spring = spring;
        springJoint.damper = damper;
        springJoint.maxDistance = distanceFromPoint * 0.8f;
        springJoint.minDistance = distanceFromPoint * 0.25f;
        springJoint.massScale = 4.5f;
        
        lineRenderer.positionCount = 2;
    }

    private void DetachSpring()
    {
        if (springJoint != null) 
        {
            lineRenderer.positionCount = 0;
            Destroy(springJoint);
        }
    }

    private void DrawGrapple() {
        if(springJoint == null) return; 

        lineRenderer.SetPosition(0, firePoint.transform.position);
        lineRenderer.SetPosition(1, raycastHitPoint);
    }
}
