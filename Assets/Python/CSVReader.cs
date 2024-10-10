//using System.Collections.Generic;
//using UnityEngine;

//public class CSVReader : MonoBehaviour
//{
//    public TextAsset data; // Drag and drop your CSV file here in the Unity Inspector

//public HashSet<string> ReadUniqueCallsigns()
//{
//    HashSet<string> uniqueCallsigns = new HashSet<string>();

//    if (data != null)
//    {
//        string[] lines = data.text.Split(new char[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
//        for (int i = 1; i < lines.Length; i++) // Skip the header line
//        {
//            string[] columns = lines[i].Split(',');
//            if (columns.Length > 2) // Check if there are enough columns
//            {
//                string callsign = columns[2]; // Assuming the callsign is in the third column
//                uniqueCallsigns.Add(callsign);
//            }
//        }
//    }
//    else
//    {
//        Debug.LogError("CSV data asset is not assigned.");
//    }

//    return uniqueCallsigns;
//}
//}

using System.Collections.Generic;
using UnityEngine;
using System;

public class CSVReader : MonoBehaviour
{
    public TextAsset data; // Drag and drop your CSV file here in the Unity Inspector

    private bool is3DDataInvalid; // Declare this as a class-level variable
    private bool callsignSequenceValid;

    void Start()
    {
        ValidateCallsignSequence();
    }

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
            Debug.LogError("CSVReader.cs: CSV data asset is not assigned.");
        }

        return uniqueCallsigns;
    }

    public void ValidateCallsignSequence()
    {
        if (data != null)
        {
            string[] lines = data.text.Split(new char[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
            // Store the previous callsign, timestamp, and closed callsigns
            string previousCallsign = null;
            HashSet<string> closedCallsigns = new HashSet<string>();
            DateTime previousTimestamp = DateTime.MinValue;

            for (int i = 1; i < lines.Length; i++) // Skip the header line
            {
                string[] columns = lines[i].Split(',');
                if (columns.Length > 3) // Ensure there are enough columns (adjust index if necessary)
                {
                    string callsign = columns[2]; // Assuming the callsign is in the third column
                    string timestampString = columns[1]; // Assuming the timestamp is in the second column

                    // Convert timestamp to DateTime object
                    DateTime currentTimestamp;
                    if (!DateTime.TryParse(timestampString, out currentTimestamp))
                    {
                        Debug.LogError($"CSVReader.cs: Invalid timestamp format on line {i + 1}: '{timestampString}'");
                        callsignSequenceValid = false;
                    }

                    // Interleaving validation: check if the callsign has reappeared after being closed
                    if (closedCallsigns.Contains(callsign))
                    {
                        Debug.LogError($"CSVReader.cs: Interleaving detected at line {i + 1}. Callsign '{callsign}' reappears after being closed.");
                        callsignSequenceValid = false;

                    }

                    // Check if the callsign changes from the previous one
                    if (previousCallsign != null && previousCallsign != callsign)
                    {
                        // Add the previous callsign to the closed set
                        closedCallsigns.Add(previousCallsign);
                        // Reset the previous timestamp since we're now dealing with a new callsign block
                        previousTimestamp = DateTime.MinValue;
                    }

                    // Timestamp validation: Ensure the timestamps are increasing for the same callsign
                    if (previousCallsign == callsign && currentTimestamp <= previousTimestamp)
                    {
                        Debug.LogError($"CSVReader.cs: Timestamp out of order for callsign '{callsign}' at line {i + 1}. Current timestamp: '{timestampString}', " +
                            $"Previous timestamp: '{previousTimestamp.ToString("o")}'");
                        callsignSequenceValid = false;

                    }

                    // Validate altitude, latitude, and longitude using the new Validate3DData() method
                    Validate3DData(i + 1, columns);



                    // Update the previous callsign and timestamp
                    previousCallsign = callsign;
                    previousTimestamp = currentTimestamp;
                }
                else
                {
                    Debug.LogError($"CSVReader.cs: Invalid number of columns on line {i + 1}");
                }
            }
            // If 3D data is valid, log the success message
            if (!is3DDataInvalid)
            {
                Debug.Log("Latitude, longitude, and altitude are all valid.");
            }
            if (callsignSequenceValid)
            {
                // If no errors, CSV data is valid
                Debug.Log("CSVReader.cs: CSV data is valid.");
            }
        }
        else
        {
            Debug.LogError("CSVReader.cs: CSV data asset is not assigned.");
        }
    }

    // Method to validate altitude, latitude, and longitude
    private void Validate3DData(int lineNumber, string[] columns)
    {
        // Altitude, latitude, and longitude are assumed to be in columns 4, 7, and 8, respectively
        string altitudeString = columns[3]; // Assuming altitude is in the fourth column
        string latitudeString = columns[6]; // Assuming latitude is in the seventh column
        string longitudeString = columns[7]; // Assuming longitude is in the eighth column

        // Altitude validation
        if (!float.TryParse(altitudeString, out float altitude) || altitude < 0)
        {
            Debug.LogError($"CSVReader.cs: Invalid altitude on line {lineNumber}: '{altitudeString}'. Altitude must be >= 0.");
            is3DDataInvalid = true;
        }

        // Latitude validation
        if (!float.TryParse(latitudeString, out float latitude) || latitude < -90 || latitude > 90)
        {
            Debug.LogError($"CSVReader.cs: Invalid latitude on line {lineNumber}: '{latitudeString}'. Latitude must be between -90 and +90.");
            is3DDataInvalid = true;
        }

        // Longitude validation
        if (!float.TryParse(longitudeString, out float longitude) || longitude < -180 || longitude > 180)
        {
            Debug.LogError($"CSVReader.cs: Invalid longitude on line {lineNumber}: '{longitudeString}'. Longitude must be between -180 and +180.");
            is3DDataInvalid = true;
        }
    }
}
