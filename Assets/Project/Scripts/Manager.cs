using UnityEngine;
using Wrld;
using Wrld.Space;

public class Manager : MonoBehaviour {

    private Api api;
    private Camera camera;

    public CameraControls camControls;
    public GameObject poiPrefab;

    // POI system for storing all interest points of photos and videos
    [System.Serializable]
    public class PointsOfInterest { public double latitude;
                                    public double longitude;
                                    public double altitude; }
    public PointsOfInterest[] poi;

    // Latitude-Longitude that the player wakes up to in the application
    private double startLatitude = 37.7858;
    private double startLongitude = -122.401;

    // Max altitude at which POI can start spawning into the scene
    private float cameraMaxSpawningAltitude = 1000f;
    // Rate at which to check for POI in the area
    private float cameraRefreshRate = 2.0f;

    private bool foundCoordinates;

	void Start ()
    {
        api = Api.Instance;
        camera = Camera.main;
        foundCoordinates = false;

        InvokeRepeating("UpdatePOISpawning", 0f, cameraRefreshRate);
	}

    private void UpdatePOISpawning()
    {
        if(camControls.latitude == 0 && camControls.longitude == 0)
        {
            Debug.Log("Main Camera has not updated Latitude-Longitude in Manager.cs.");
            return;
        } else
        {
            for (int i = 0; i < poi.Length; i++)
            {
                PointsOfInterest currentPOI = poi[i];
                LatLongAltitude poiLatLong = LatLongAltitude.FromDegrees(currentPOI.latitude, currentPOI.longitude, currentPOI.altitude);

                //Debug.Log(poiLatLong.GetLatitude());

                GameObject photo = (GameObject)Instantiate(poiPrefab);
                PhotoObject photoObject = photo.GetComponent<PhotoObject>();
                photoObject.SetPOILocation(poiLatLong);

                //Vector3 screenSpacePoint = api.CameraApi.GeographicToScreenPoint(poiLatLong, camera);
                //Debug.Log(camera.pixelWidth + " by " + camera.pixelHeight + "   " + screenSpacePoint);
            }
        }

    }
}
