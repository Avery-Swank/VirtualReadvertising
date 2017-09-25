using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
        
public class PhotoCanvas : MonoBehaviour {

    public Text canvasDescription;
    private bool counting;

    private float bigCountDown;
    private float countDown = 3f;
    private Transform child;
    
    // Client texts prompts
    private string idleString = "Would you like to see?";
    private string waitingString = "Loading your photo in: ";

    // Building info call. When the player wants to select the canvas for
    // a particular building, this will add all of the photos and videos
    // in that building to the static list that will be used in the next scene
    private PhotoVideoInBuilding buildingInfo;

    void Start()
    {
        child = transform.GetChild(0);
        child.gameObject.SetActive(false);
        counting = false;
        bigCountDown = countDown;

        buildingInfo = transform.parent.GetComponent<PhotoVideoInBuilding>();
    }
	
    void Update()
    {
        if (ClickingPhotos.animatingCamera)
            child.gameObject.SetActive(true);

        if (child.gameObject.activeSelf)
        {
            child.LookAt(Camera.main.transform.position, Vector3.up);
        }

        if (counting)
        {
            countDown -= Time.deltaTime;
            canvasDescription.text = waitingString + countDown.ToString("0.00");
        } else
        {
            countDown = bigCountDown;
            canvasDescription.text = idleString;
        }

    }

    public void CancelLoadScene()
    {
        counting = false;
    }

    public void LoadPhotoScene()
    {
        buildingInfo.FillViewingElements();
        StartCoroutine("FadeScene");
        counting = true;
    }

    public IEnumerator FadeScene()
    {
        Debug.Log("Before");
        yield return new WaitForSeconds(countDown);
        Debug.Log("After");

        if(counting)
            SceneManager.LoadScene(1);
    }
}
