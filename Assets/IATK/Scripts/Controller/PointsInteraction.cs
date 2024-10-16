using UnityEngine;
using UnityEngine.InputSystem;
using IATK;
public class PointsInteraction : MonoBehaviour
{
    private InputAction inputAction;

    public CSVDataSource csvDataSource;

    public void Start()
    {
        csvDataSource = FindObjectOfType<CSVDataSource>();        
    }

    // Assign the input action dynamically
    public void AssignInputAction(InputActionReference actionReference)
    {
        inputAction = actionReference.action;

        // Register the action's performed callback
        inputAction.performed += OnTriggerPressed;
        inputAction.Enable();
    }

    public void OnTriggerPressed(InputAction.CallbackContext context)
    {
        // Handle the trigger press event
        Debug.Log("Trigger Pressed on " + gameObject.name);
    }

    private void OnDestroy()
    {
        if (inputAction != null)
        {
            inputAction.performed -= OnTriggerPressed;
        }
    }
}
