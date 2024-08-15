using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FlightTrailGenerator : MonoBehaviour
{
    public string filePath = "Assets/Python/2_aircraft_data_mod.csv"; // Path to the CSV file
    public Dropdown flightDropdown; // Reference to the dropdown UI element
    public Button viewButton; // Reference to the view button
    public float lineWidth = 0.1f;
    public float animationSpeed = 0.05f; // Speed of the animation
    private List<Vector3> flightData = new List<Vector3>();
    private LineRenderer lineRenderer;
    private Dictionary<string, List<Vector3>> flightTrails = new Dictionary<string, List<Vector3>>();

    void Start()
    {
        LoadFlightData();       // Load the flight data from the CSV file
        flightDropdown = GameObject.Find("FlightDropdown").GetComponent<Dropdown>();
        PopulateDropdown();     // Populate the dropdown with flight numbers
        viewButton.onClick.AddListener(VisualizeSelectedFlight); // Add button click listener 
    }

    void LoadFlightData()
    {
        string[] lines = File.ReadAllLines(filePath);

        for (int i = 1; i < lines.Length; i++) // Assuming the first line is a header
        {
            string[] values = lines[i].Split(',');
            string flightNumber = values[2]; // Assuming flight number is in the 3rd column
            float latitude = float.Parse(values[7]);
            float longitude = float.Parse(values[8]);
            float altitude = float.Parse(values[3]);

            Vector3 flightPoint = new Vector3(longitude, latitude, altitude);

            if (!flightTrails.ContainsKey(flightNumber))
            {
                flightTrails[flightNumber] = new List<Vector3>();
            }

            flightTrails[flightNumber].Add(flightPoint);
        }
    }

    void PopulateDropdown()
    {
        flightDropdown.ClearOptions(); // Clear existing options
        List<string> flightNumbers = new List<string>(flightTrails.Keys); // Extract flight numbers
        flightDropdown.AddOptions(flightNumbers); // Add flight numbers to dropdown
    }

    void VisualizeSelectedFlight()
    {
        string selectedFlight = flightDropdown.options[flightDropdown.value].text; // Get selected flight number

        if (lineRenderer != null)
        {
            Destroy(lineRenderer.gameObject); // Clear previous flight trail
        }

        flightData = flightTrails[selectedFlight]; // Get data for the selected flight
        GenerateFlightTrail(); // Generate the flight trail
        StartCoroutine(AnimateTrail()); // Animate the flight trail
    }

    void GenerateFlightTrail()
    {
        if (flightData.Count == 0) return;

        GameObject trailObject = new GameObject("FlightTrail");
        lineRenderer = trailObject.AddComponent<LineRenderer>();

        lineRenderer.positionCount = 0;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Material lineMaterial = new Material(Shader.Find("Standard"));
        lineRenderer.material = lineMaterial;
        lineRenderer.material.color = Color.red;
    }

    IEnumerator AnimateTrail()
    {
        for (int i = 0; i < flightData.Count; i++)
        {
            lineRenderer.positionCount = i + 1;
            lineRenderer.SetPosition(i, flightData[i]);
            yield return new WaitForSeconds(animationSpeed);
        }
    }
}
