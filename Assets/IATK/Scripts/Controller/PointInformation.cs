using UnityEngine;
using System.Collections.Generic;


public class PointInformation
{
    // Public fields or properties to hold additional information
    public int pointId;
    public long timeStamp;
    public string utc;  // Coordinated Universal Time
    public string pointCallsign;
    public int pointAltitude;
    public float pointLatitude;
    public float pointLongitude;
    public int pointDirection;
    public Vector3 pointData;
    public int speed;

    public PointInformation(int speed_, long timeStamp_, string utc_, string pointCallsign_, int pointAltitude_, int pointDirection_, float pointLatitude_, float pointLongitude_)
    {
        timeStamp = timeStamp_;
        utc = utc_;
        pointCallsign = pointCallsign_;
        pointAltitude = pointAltitude_;
        pointDirection = pointDirection_;
        pointLatitude = pointLatitude_;
        pointLongitude = pointLongitude_;
        speed = speed_;
    }

}
