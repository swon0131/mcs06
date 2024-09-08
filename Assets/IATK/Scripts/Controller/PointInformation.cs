using UnityEngine;
using System.Collections.Generic;


public class PointInformation : MonoBehaviour
{
    // Public fields or properties to hold additional information
    public string pointName;
    public int pointId;
    public int timeStamp;
    public string utc;  // Coordinated Universal Time
    public string pointCallsign;
    public int pointAltitude;
    public int pointLatitide;
    public int pointLongitude;
    public int pointDirection;
    public Vector3 pointData;
    public string FUCK_THIS_PROJECT_AHHHHHHHHH;
    public List<Vector3> positions;                            // The positions of the data
    public List<int> indices;                                  // The indices for the positions
    public List<Color> colours;                                // The colour at each position
    public List<Vector3> normals;                              // 
    public List<Vector3> uvs;                                  // Holds extra data such as id, size
    public List<Vector3> uv2s;                                 // Holds extra data such as id, size

    public List<int> lineLength;                               // contains the size of each line when 
    // making a line topology mesh
    public List<int> chunkSizeList;                    // size of each block of indices when the
    // mesh contains more than 65k vertices
    
    public int numberOfDataPoints;                             // number of points in the dataset
    // You can also add methods or additional logic here if needed
    public string Name;

}
