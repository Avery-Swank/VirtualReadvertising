using UnityEngine;

public class Camera360Viewing : MonoBehaviour {

    private Vector3 change;
    private float keySpeed = 40f;

    private float mouseSensitivity = 120f;
    private Vector3 rot;
    private float rotY;
    private float rotX;

    void Start()
    {
        change = Vector3.zero;
        ResetMouseRotation();
    }

    void Update()
    {
        // Mouse click down determines choice of camera rotation
        // either keys or mouse
        if (!ViewingManager.menuActive)
        {
            if (Input.GetMouseButton(0))
            {
                MouseControls();
            }
            else
            {
                KeyControls();
            }
        }
    }

    private void ResetMouseRotation()
    {
        rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    private void MouseControls()
    {
        ResetMouseRotation();

        // Get Mouse movemnt
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        // Apply movement to global variable
        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

        // Apply mouse sensitivity to camera rotation
        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;

        // Set current euler angles to "change" variable for KeyControls so the 
        // current rotation is carried across movement systems
        change = transform.localRotation.eulerAngles;
    }

    private void KeyControls()
    {
        // Add a small change in euler angles to camera rotation based
        // on what keys are being pressed
        if (Input.GetKey(KeyCode.UpArrow))
        {
            change += new Vector3(-keySpeed * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            change += new Vector3(keySpeed * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            change += new Vector3(0f, -keySpeed * Time.deltaTime, 0f);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            change += new Vector3(0f, keySpeed * Time.deltaTime, 0f);
        }

        // Apply keystroke Vector3 changes to rotate camera with a locked z axis
        Vector3 final = new Vector3(change.x, change.y, 0f);
        transform.rotation = Quaternion.Euler(final);
    }
}

