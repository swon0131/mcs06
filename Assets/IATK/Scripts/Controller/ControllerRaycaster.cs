using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerRaycaster : MonoBehaviour
{
    public InputActionProperty rightTriggerAction; // Assign this in the Inspector
    public float raycastDistance = 10f;
    public LayerMask pointLayerMask; // Assign a specific layer for the point colliders

    private Camera mainCamera;
    private bool isTriggerHeld = false;

    void Start()
    {
        mainCamera = Camera.main;

        // Subscribe to the right trigger action
        rightTriggerAction.action.started += OnRightTriggerStarted;
        rightTriggerAction.action.canceled += OnRightTriggerReleased;
    }

    void Update()
    {
        if (isTriggerHeld)
        {
            RaycastFromController();
        }
    }

    private void OnRightTriggerStarted(InputAction.CallbackContext context)
    {
        // The trigger has been pressed, now consider it being held
        isTriggerHeld = true;
    }

    private void OnRightTriggerReleased(InputAction.CallbackContext context)
    {
        // The trigger has been released
        isTriggerHeld = false;
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
                // Trigger the interaction logic here
            }
        }
    }

    void OnDestroy()
    {
        rightTriggerAction.action.started -= OnRightTriggerStarted;
        rightTriggerAction.action.canceled -= OnRightTriggerReleased;
    }
}