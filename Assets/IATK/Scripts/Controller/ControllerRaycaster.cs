using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerRaycaster : MonoBehaviour
{
    public InputActionProperty rightTriggerAction; // Assign this in the Inspector
    public float raycastDistance = 10f;
    public LayerMask pointLayerMask; // Assign a specific layer for the point colliders

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        // Subscribe to the right trigger action
        rightTriggerAction.action.performed += OnRightTriggerPressed;
    }

    void Update()
    {
        RaycastFromController();
    }

    private void RaycastFromController()
    {
        // Assuming the controller is oriented with a forward direction
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Check if the ray hits any object with the PointCollider layer
        if (Physics.Raycast(ray, out hit, raycastDistance, pointLayerMask))
        {
            PointsInteraction pointHandler = hit.collider.GetComponent<PointsInteraction>();
            if (pointHandler != null)
            {
                Debug.Log("Pointing at: " + hit.collider.gameObject.name);
                // You can add more logic here, such as highlighting the point, etc.
            }
        }
    }

    private void OnRightTriggerPressed(InputAction.CallbackContext context)
    {
        // Perform a raycast to check if the right trigger press is pointing at a point
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, pointLayerMask))
        {
            PointsInteraction pointHandler = hit.collider.GetComponent<PointsInteraction>();
            if (pointHandler != null)
            {
                pointHandler.OnTriggerPressed(context);
            }
        }
    }

    void OnDestroy()
    {
        rightTriggerAction.action.performed -= OnRightTriggerPressed;
    }
}
