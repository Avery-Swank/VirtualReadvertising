using UnityEngine;
using Wrld;
using Wrld.Space;

public class PhotoObject : MonoBehaviour {
    
    private CameraControls camControls;

    private Vector3 offset = new Vector3(35f, 200f, 0f);

    // Latitude-Longitude of the photo object on Earth
    private double photoLatitude;
    private double photoLongitude;
    private double photoAltitude;

    // Coordinate frame attributes
    private UnityWorldSpaceCoordinateFrame frame;
    public LatLongAltitude latLongAlt;

    // Boolean logic gates
    private bool foundCoordinates;
    private bool foundLatLong;
    private bool set;

    void Start()
    {
        camControls = Camera.main.transform.GetComponent<CameraControls>();

        foundCoordinates = false;
        foundLatLong = false;
        set = false;


        // Setting coordinates for New York City
        //SetPOILocation(40.7128, -74.0059, 100);
        // Setting coordinates for Prior Lake Home
        //SetPOILocation(44.705821, -93.453626, 100);
        // Setting coordinates for San Francisco
        SetPOILocation(37.7749, -122.4194, 100);
    }

    void Update()
    {
        if (foundLatLong)
        {
            UpdatePhotoLocation();
        }
    }


    // Assigning Latitude-Longitude values to the POI component
    public void SetPOILocation(LatLongAltitude latlon)
    {
        SetPOILocation(latlon.GetLatitude(), latlon.GetLongitude(), latlon.GetAltitude());
    }

    // Assigning Latitude-Longitude values to the POI component
    public void SetPOILocation(double lat, double lon, double alt)
    {
        photoLatitude = lat;
        photoLongitude = lon;
        photoAltitude = alt;
        latLongAlt = LatLongAltitude.FromDegrees(photoLatitude,
                                                 photoLongitude,
                                                 photoAltitude);
        foundLatLong = true;
    }

    private void UpdatePhotoLocation()
    {
        // It takes a few frames for the camera to update its latitude and longitude on start up so this logic gate
        // is here to make sure the coordinate frame is using legitamate coordinates
        if (camControls.latitude == 0 && camControls.longitude == 0)
        {
            Debug.Log("Main Camera has not updated Latitude-Longitude.");
            return;
        }
        else
        {
            foundCoordinates = true;
        }

        // The unity 3D coordinate frame based around the Main Camera's current location
        frame = new UnityWorldSpaceCoordinateFrame(LatLong.FromDegrees(camControls.latitude, camControls.longitude));

        if (foundCoordinates == true && set == false)
        {
            // Convert the requested Latitude-Longitude to unity3D coordinates
            transform.position = frame.LatLongAltitudeToLocalSpace(latLongAlt) + offset;
            Debug.Log("Positon Set");
            set = true;
        }
    }

    public UnityWorldSpaceCoordinateFrame getFrame()
    {
        return frame;
    }

}
