using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlightDetails : MonoBehaviour
{
    public FlightDetails flightDetailsObject; // Reference to the GameObject with TextMeshPro component
    private TextMeshProUGUI flightDetailsText; // Reference to the TextMeshPro component
    private void Awake()
    {
        // Get the TextMeshPro component attached to the flightDetailsObject
        if (flightDetailsObject != null)
        {
            flightDetailsText = flightDetailsObject.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogWarning("FlightDetails GameObject is not assigned.");
        }
    }
}
