using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Include this if using TextMeshPro

public class FlightTrailGenerator : MonoBehaviour
{
    public string filePath = "Assets/Python/2_aircraft_data_mod.csv";
    public GameObject buttonPrefab;
    public GameObject contentArea;

    private Dictionary<string, List<Vector3>> flightTrails = new Dictionary<string, List<Vector3>>();

    void Start()
    {
        LoadFlightData();
        PopulateButtons();
    }

    void LoadFlightData()
    {
        string[] lines = File.ReadAllLines(filePath);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');

            if (values.Length < 8)
            {
                Debug.LogWarning($"Skipping line {i + 1} because it doesn't have enough columns: {lines[i]}");
                continue;
            }

            string flightNumber = values[2];
            float latitude, longitude, altitude;

            if (!float.TryParse(values[6], out latitude) ||
                !float.TryParse(values[7], out longitude))
            {
                Debug.LogWarning($"Skipping line {i + 1} because it contains invalid float values: {lines[i]}");
                continue;
            }

            altitude = values.Length > 3 ? float.Parse(values[3]) : 0f;

            Vector3 flightPoint = new Vector3(longitude, latitude, altitude);

            if (!flightTrails.ContainsKey(flightNumber))
            {
                flightTrails[flightNumber] = new List<Vector3>();
            }

            flightTrails[flightNumber].Add(flightPoint);
        }
    }

    void PopulateButtons()
    {
        if (buttonPrefab == null)
        {
            Debug.LogError("Button prefab is not assigned!");
            return;
        }

        if (contentArea == null)
        {
            Debug.LogError("Content area is not assigned!");
            return;
        }

        foreach (Transform child in contentArea.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var flightNumber in flightTrails.Keys)
        {
            GameObject btn = Instantiate(buttonPrefab);
            btn.transform.SetParent(contentArea.transform, false);

            // Handle both Text and TMP_Text
            Text btnText = btn.GetComponentInChildren<Text>();
            TMP_Text btnTMPText = btn.GetComponentInChildren<TMP_Text>();

            if (btnText != null)
            {
                btnText.text = flightNumber;
            }
            else if (btnTMPText != null)
            {
                btnTMPText.text = flightNumber;
            }
            else
            {
                Debug.LogError("Button prefab is missing a Text or TMP_Text component!");
            }

            Button buttonComponent = btn.GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() => OnFlightSelected(flightNumber));
            }
            else
            {
                Debug.LogError("Button prefab is missing a Button component!");
            }
        }
    }

    void OnFlightSelected(string flightNumber)
    {
        Debug.Log($"Selected flight: {flightNumber}");
    }
}
