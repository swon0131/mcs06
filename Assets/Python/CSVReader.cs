using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public string filePath; // The relative path to the CSV file within the Assets folder

    public HashSet<string> ReadUniqueCallsigns()
    {
        HashSet<string> uniqueCallsigns = new HashSet<string>();

        // Ensure the file path is correct; adjust if necessary
        string path = Path.Combine(Application.dataPath, filePath);
        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            for (int i = 1; i < lines.Length; i++) // Skip the header line
            {
                string[] columns = lines[i].Split(',');
                if (columns.Length > 2) // Check if there are enough columns
                {
                    string callsign = columns[2]; // Assuming the callsign is in the third column
                    uniqueCallsigns.Add(callsign);
                }
            }
        }
        else
        {
            Debug.LogError("CSV file not found at: " + path);
        }

        return uniqueCallsigns;
    }
}
