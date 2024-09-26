using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetValueFromDropdown : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown dropdown;

    public string GetDropdownValue()
    {
        int pickedEntryIndex = dropdown.value;
        string selectedOption = dropdown.options[pickedEntryIndex].text;
        Debug.Log(selectedOption);

        return selectedOption;
    }

}
