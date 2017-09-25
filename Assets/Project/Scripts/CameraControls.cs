using UnityEngine;
using Wrld;
using Wrld.Space;

public class CameraControls : MonoBehaviour {

    public static Transform self;

    // Latitude-Longitude attributes
    public float latitude;
    public float longitude;

    public static bool goodCoordinates;

    private Api api;
    private Camera camera;
    private Vector3 screenSpacePoint;
    private LatLongAltitude latLongAltitude;


    void Start()
    {
        self = transform;

        api = Api.Instance;
        camera = Camera.main;

        api.CameraApi.SetControlledCamera(camera);

        goodCoordinates = false;
    }

	void Update()
    {
        // Update Latitude-Longitude at the point the camera is pointing on the Earth.
        //screenSpacePoint = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, camera.nearClipPlane);
        screenSpacePoint = camera.WorldToScreenPoint(Vector3.zero);
        latLongAltitude = api.CameraApi.ScreenToGeographicPoint(screenSpacePoint, camera);
        //latLongAltitude = api.CameraApi.ViewportToGeographicPoint(screenSpacePoint, camera);

        // Update attributes
        latitude = (float)latLongAltitude.GetLatitude();
        longitude = (float)latLongAltitude.GetLongitude();

        if(latitude != 0 && longitude != 0)
        {
            goodCoordinates = true;
        }

    }
}
