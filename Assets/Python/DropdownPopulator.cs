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
        dropdown.AddOptions(new List<string>(uniqueCallsigns)); // Convert HashSet to List for TMP_Dropdown
    }
}
