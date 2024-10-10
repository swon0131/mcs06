using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using IATK;

public class ControllerRaycaster : MonoBehaviour
{
    public CSVDataSource csvDataSource;
    public InputActionProperty rightTriggerAction; // Assign this in the Inspector
    public float raycastDistance = 10f;
    public LayerMask pointLayerMask; // Assign a specific layer for the point colliders
    public FlightDetails flightDetailsObject; // Reference to the FlightDetails GameObject
    public TextMeshProUGUI flightDetailsText; // Reference to the TextMeshPro component
    private Camera mainCamera;
    private bool isTriggerHeld = false;

    // The latest point interacted with
    private string pointName;

    void Start()
    {
        mainCamera = Camera.main;
        csvDataSource = FindObjectOfType<CSVDataSource>();

        // Debug check for rightTriggerAction
        if ( rightTriggerAction.action !=null)
        {
            Debug.Log("ControllerRaycaster.cs: RightTriggerAction bound to script successfully.");
        }
        else
        {
            Debug.LogError("ControllerRaycaster.cs: Right trigger action not bound to script. Select the 'GenericXR/Right Trigger Pressed' reference in the inspector");
        }

        // Debug check for ScatterplotVisualisation
        if (pointLayerMask != 0)
        {
            Debug.Log("ButtonHandler.cs: Collision filter initialized as 'PointLayer'.");

        }
        else
        {
            Debug.LogError("ButtonHandler.cs: Collision filter uninitialized. Select the 'PointLayer' filter in the inspector");
        }
        // Subscribe to the right trigger action
        rightTriggerAction.action.started += OnRightTriggerStarted;
        rightTriggerAction.action.canceled += OnRightTriggerReleased;
        flightDetailsObject = FindObjectOfType<FlightDetails>();
    
        // Check if the target GameObject is assigned
        if (flightDetailsObject != null)
        {
            // Get all components attached to the GameObject
            var components = flightDetailsObject.GetComponents<Component>();

            // Iterate through and log the names of the components
            foreach (var component in components)
            {
                if (component is TextMeshProUGUI tmpComponent)
                {
                    flightDetailsText = tmpComponent; // Now it's a TextMeshProUGUI component
                }
            }
        }
        else
        {
            Debug.LogWarning("Target GameObject is not assigned.");
        }
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
        // update flight details if a point was interacted with
        if (pointName != "") { UpdateFlightDetails(pointName); }
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
                pointName = hit.collider.gameObject.name;  
            }
        }
    }

    private void UpdateFlightDetails(string objectName)
    {

        // Find the position of the last underscore in the string
        int underscoreIndex = objectName.LastIndexOf('_');
        int index;

        // Extract the substring starting from the character after the underscore to the end of the string
        index = int.Parse(objectName.Substring(underscoreIndex + 1));

        string callsign = csvDataSource.pointsInfoList[index].pointCallsign;
        float longitude = csvDataSource.pointsInfoList[index].pointLongitude;
        float latitude = csvDataSource.pointsInfoList[index].pointLatitude;
        int altitude = csvDataSource.pointsInfoList[index].pointAltitude;
        string utc = csvDataSource.pointsInfoList[index].utc;
        int speed = csvDataSource.pointsInfoList[index].speed;
        int direction = csvDataSource.pointsInfoList[index].pointDirection;

        // Trigger the interaction logic here
        flightDetailsText.text = string.Format(
            "Callsign   : {0,-10}\n" +
            "Longitude  : {1, -10}\n" +
            "Latitude   : {2, -10}\n" +
            "Altitude   : {3, -10}\n" +
            "Timestamp  : {4, -10}\n" +
            "Speed      : {5, -10}\n" +
            "Direction  : {6, -10}",
            callsign, longitude, latitude, altitude, utc, speed, direction
        );

    }

    void OnDestroy()
    {
        rightTriggerAction.action.started -= OnRightTriggerStarted;
        rightTriggerAction.action.canceled -= OnRightTriggerReleased;
    }
}