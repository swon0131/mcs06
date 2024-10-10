using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IATK;

public class ButtonHandler : MonoBehaviour
{

    [SerializeField] public GetValueFromDropdown dropdownHandler; // Reference to GetValueFromDropdown script
    [SerializeField] private ScatterplotVisualisation scatterplotPointVisualisation;
    bool viewBuilderSet = false;
    public CSVDataSource csvDataSource;
    private ViewBuilder viewBuilder;
    // Start is called before the first frame update
    void Start()
    {
        // Debug check for GetValueFromDropDown
        if (dropdownHandler != null)
        {
            Debug.Log("ButtonHandler.cs: GetValueFromDropdown initialized.");
        }
        else
        {
            Debug.LogError("ButtonHandler.cs: GetValueFromDropdown uninitialized. Drag an instance into the Dropdown Handler field in the inspector");
        }

        // Debug check for ScatterplotVisualisation
        if (scatterplotPointVisualisation != null)
        {
            Debug.Log("ButtonHandler.cs: ScatterplotVisualisation initialized.");
        }
        else
        {
            Debug.LogError("ButtonHandler.cs: ScatterplotVisualisation uninitialized. Drag an instance into the Scatterplot Point Visualisation field in the inspector");
        }
        csvDataSource = FindObjectOfType<CSVDataSource>();
        if (csvDataSource != null)
        {
            Debug.Log("ButtonHandler.cs: CSVDataSource is initialized.");
        }
        else
        {
            Debug.LogError("ButtonHandler.cs: CSVDataSource uninitialized. Ensure the GameObject name 'CSVDataSource' on line 36 matches the class name in Assets/IATK/Scripts/Model/CSVDataSource");
        }
    }
    // Method to set the ViewBuilder instance
    public void SetViewBuilder(ViewBuilder vb)
    {
        if (vb != null)
        {
            viewBuilder = vb;
            if (viewBuilder != null)
            {
                if (!viewBuilderSet)
                {
                    viewBuilderSet = true;
                    Debug.Log("ButtonHandler.cs:View builder has been set");
                }
            }
        }
    }
    // This function will be called when the button is pressed
    public void OnButtonClick()
    {
        Debug.Log("Button was clicked!");
        if (viewBuilder == null)
        {
            Debug.LogError("ButtonHandler.cs: ViewBuilder not initialized before OnButtonClick. Check if the the ScatterPlotVisualisation.cs file has called the SetViewBuilder() method in CreateSimpleVisualisation()");
            return;
        }


        // Call the method from GetValueFromDropdown
        if (dropdownHandler != null)
        {
            string value = dropdownHandler.GetDropdownValue();

            // Execute Point Visualisation filter of 3D model
            foreach (View view in scatterplotPointVisualisation.viewList)
            {
                Color[] colors = view.GetColors();

                for (int i = 0; i < colors.Length; i++)
                {
                    string callsign = csvDataSource.pointsInfoList[i].pointCallsign;
                    if (callsign == value || value == "Default")
                    {
                        colors[i].a = 1f;  // make it opaque
                        viewBuilder.pointColliderList[i].enabled = true; // enable the collider
                    }
                    else
                    {
                        colors[i].a = 0f; // make it transparent
                        viewBuilder.pointColliderList[i].enabled = false; // disable the collider
                    }
                }
                view.SetColors(colors);  // Apply the new transparent colors
            }
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
