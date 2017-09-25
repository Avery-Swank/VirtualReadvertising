using System.Collections;
using Wrld;
using Wrld.Space;
using UnityEngine;

public class ClickingPhotos : MonoBehaviour
{
    private Api api;
    private Camera camera;

    public static bool animatingCamera;

    private Vector3 mouseDownPosition;
    private float clickRange = 8.0f;

    private PhotoObject currentPhoto;

    private bool animating;
    private float startHeading = 0f;
    private float headingRate = .3f;

    void Start()
    {
        api = Api.Instance;
        camera = Camera.main;
        animating = false;
        animatingCamera = false;

    }

    void Update()
    {
        // If the mouse button is clicked down
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPosition = Input.mousePosition;
            // Cancel the rotation
            animating = false;
        }

        UpdateMouseHover();

        // If the mouse is clicked up and the distance between the click down and click up is within a certain range
        if (Input.GetMouseButtonUp(0) && Vector3.Distance(mouseDownPosition, Input.mousePosition) < clickRange)
            UpdateMouseClick();

        // Control when to constantly rotate the camera around the current PhotoObject
        if (animating)
        {
            AnimateCamera();
            animatingCamera = true;
        } else
        {
            animatingCamera = false;
        }
    }

    private void UpdateMouseHover()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit h;
        // Check the hit point on the world
        if (Physics.Raycast(r, out h))
        {
            // If the player clicked on a photo
            if (h.transform.GetComponent<PhotoObject>() != null)
            {
                // Mouse is hovering a PhotoObject
            }
        }
    }

    private void UpdateMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Check the hit point on the world
        if (Physics.Raycast(ray, out hit))
        {
            // If the player clicked on a photo
            if (hit.transform.GetComponent<PhotoObject>() != null)
            {
                // Mouse has clicked a PhotoObject
                currentPhoto = hit.transform.GetComponent<PhotoObject>();
                animating = true;
                //Vector3 screenSpacePoint = api.CameraApi.GeographicToScreenPoint(hit.transform.GetComponent<PhotoObject>().latLongAlt, camera);
                //Debug.Log(camera.pixelWidth + " by " + camera.pixelHeight + "   " + screenSpacePoint);
            }
        }
    }

    private void AnimateCamera()
    {
       // Add to the heading degress so the animation feels like a rotation around the objcet
       startHeading += headingRate;
       api.CameraApi.AnimateTo(currentPhoto.latLongAlt.GetLatLong(), 800f, startHeading, null, .1f, true);
    }
}
