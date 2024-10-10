using UnityEngine;
using TMPro; // Ensure this namespace is included for TMP components
using System.Collections.Generic;

public class DropdownPopulator : MonoBehaviour
{
    public TMP_Dropdown dropdown; // Use TMP_Dropdown for TextMesh Pro
    public CSVReader csvReader;

    void Start()
    {
        PopulateDropdown();
    }

    void PopulateDropdown()
    {
        HashSet<string> uniqueCallsigns = csvReader.ReadUniqueCallsigns();
        dropdown.ClearOptions();

        // Create a list to hold the options
        List<string> options = new List<string>();

        // Add the "Default" option
        options.Add("Default");

        // Add unique callsigns to the list
        options.AddRange(uniqueCallsigns); // Convert HashSet to List for TMP_Dropdown

        // Log each option to the console
        foreach (string option in options)
        {
            Debug.Log("Dropdown option: " + option);
        }

        // Add all options to the dropdown
        dropdown.AddOptions(options);
    }

}
