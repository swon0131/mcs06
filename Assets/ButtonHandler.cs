using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IATK;

public class ButtonHandler : MonoBehaviour
{

    [SerializeField] public GetValueFromDropdown dropdownHandler; // Reference to GetValueFromDropdown script
    [SerializeField] private ScatterplotVisualisation scatterplotPointVisualisation;
    public CSVDataSource csvDataSource;
    private ViewBuilder viewBuilder;
    // Start is called before the first frame update
    void Start()
    {
        csvDataSource = FindObjectOfType<CSVDataSource>();
    }
    // Method to set the ViewBuilder instance
    public void SetViewBuilder(ViewBuilder vb)
    {
        viewBuilder = vb;
    }
    // This function will be called when the button is pressed
    public void OnButtonClick()
    {
        Debug.Log("Button was clicked!");
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
                        colors[i].a = 1f;  // Set alpha to 0 for transparency
                        viewBuilder.pointColliderList[i].enabled = true; // disable the collider
                    }
                    else
                    {
                        colors[i].a = 0f; // make it opaque
                        viewBuilder.pointColliderList[i].enabled = false; // enable the collider
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
