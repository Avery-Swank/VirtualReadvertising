using UnityEngine;

// This class manages the UI and coordinate system that is pulled
// from the PhotosandVideos.cs. This does math on where photos
// and videos are relative to each other with simple geometry,
// 
public class BitmapManager : MonoBehaviour {

    public Transform bitmap;

    public Transform photoButtonPrefab;
    public Transform videoButtonPrefab;


    void Start()
    {
        FillUI();
    }

    // Fill the UI map with buttons that correspond
    // to ViewingElements
    private void FillUI()
    {
        ViewingElement[] elements = PhotoVideoInBuilding.viewingElements;

        if (elements == null)
        {
            Debug.Log("No elements list.");
            return;
        }

        for(int i = 0; i < elements.Length; i++)
        {
            Debug.Log(i + " " + elements[i].GetTYPE());
        }

        for (int i = 0; i < elements.Length; i++)
        {
            if(elements[i].GetTYPE() == ViewingElement.VIEWINGTYPE.Photo)
            {
                Transform button = (Transform)Instantiate(photoButtonPrefab, bitmap);
                button.GetComponent<ViewingButton>().SetIndex(i);
            } else
            {
                Transform video = (Transform)Instantiate(videoButtonPrefab, bitmap);
                video.GetComponent<ViewingButton>().SetIndex(i);
            }
        }
    }
}
