using System.Collections.Generic;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public TextAsset data; // Drag and drop your CSV file here in the Unity Inspector

    public HashSet<string> ReadUniqueCallsigns()
    {
        HashSet<string> uniqueCallsigns = new HashSet<string>();

        if (data != null)
        {
            string[] lines = data.text.Split(new char[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
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
            Debug.LogError("CSV data asset is not assigned.");
        }

        return uniqueCallsigns;
    }
}
