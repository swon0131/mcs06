using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{

    [SerializeField] private GetValueFromDropdown dropdownHandler; // Reference to GetValueFromDropdown script

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // This function will be called when the button is pressed
    public void OnButtonClick()
    {
        Debug.Log("Button was clicked!");
        // Call the method from GetValueFromDropdown
        if (dropdownHandler != null)
        {
            dropdownHandler.GetDropdownValue();
        }
        else
        {
            Debug.LogError("DropdownHandler reference is missing.");
        }
        // Add your logic here
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
