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

    // Example property to hold information about the point
    public string pointInformation;

    // Optionally, you could also have a method to get more complex information
    public string GetPointInformation()
    {
        return pointInformation;
    }

    void Start()
    {
        mainCamera = Camera.main;
        csvDataSource = FindObjectOfType<CSVDataSource>();
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
                    Debug.Log(tmpComponent.GetType().Name); // Log the type of each component
                    flightDetailsText = tmpComponent; // Now it's a TextMeshProUGUI component
                    Debug.Log(flightDetailsText.text); // Log the current text
                }
            }
        }
        else
        {
            Debug.LogWarning("Target GameObject is not assigned.");
        }

        // TESTING PURPOSES
        int index = 10;
        string callsign = csvDataSource.pointsInfoList[index].pointCallsign;
        float longitude = csvDataSource.pointsInfoList[index].pointLongitude;
        float latitude = csvDataSource.pointsInfoList[index].pointLatitude;
        int altitude = csvDataSource.pointsInfoList[index].pointAltitude;
        long timestamp = csvDataSource.pointsInfoList[index].timeStamp;
        int speed = csvDataSource.pointsInfoList[index].speed;
        int direction = csvDataSource.pointsInfoList[index].pointDirection;

        // Trigger the interaction logic here
        flightDetailsText.text = $"Callsign: {callsign}\n" +
                                 $"Longitude: {longitude}\n" +
                                 $"Latitude: {latitude}\n" +
                                 $"Altitude: {altitude}\n" +
                                 $"Timestamp: {timestamp}\n" +
                                 $"Speed: {speed}\n" +
                                 $"Direction: {direction}";

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
                string objectName = hit.collider.gameObject.name;

                // Find the position of the last underscore in the string
                int underscoreIndex = objectName.LastIndexOf('_');
                int index;
                if (underscoreIndex != -1 && underscoreIndex < objectName.Length - 1)
                {
                    // Extract the substring starting from the character after the underscore to the end of the string


                    index = int.Parse(objectName.Substring(underscoreIndex + 1));
                    string callsign = csvDataSource.pointsInfoList[index].pointCallsign;
                    float longitude = csvDataSource.pointsInfoList[index].pointLongitude;
                    float latitude = csvDataSource.pointsInfoList[index].pointLatitude;
                    int altitude = csvDataSource.pointsInfoList[index].pointAltitude;
                    long timestamp = csvDataSource.pointsInfoList[index].timeStamp;
                    int speed = csvDataSource.pointsInfoList[index].speed;
                    int direction = csvDataSource.pointsInfoList[index].pointDirection;

                    // Trigger the interaction logic here
                    flightDetailsText.text = $"Callsign: {callsign}\n" +
                                             $"Longitude: {longitude}\n" +
                                             $"Latitude: {latitude}\n" +
                                             $"Altitude: {altitude}\n" +
                                             $"Timestamp: {timestamp}\n" +
                                             $"Speed: {speed}\n" +
                                             $"Direction: {direction}";
                    // Log the extracted index
                    Debug.Log("Extracted index: " + index);
                }
                else
                {
                    Debug.LogWarning("No underscore found or nothing after the underscore in the object name.");
                }


                // We have to output the information into the FlightInformation TextMeshPro


            }
        }
    }

    void OnDestroy()
    {
        rightTriggerAction.action.started -= OnRightTriggerStarted;
        rightTriggerAction.action.canceled -= OnRightTriggerReleased;
    }
}