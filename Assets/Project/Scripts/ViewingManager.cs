using UnityEngine;
using UnityEngine.Video;

// This class pulls and manages the "PhotosandVideos" that the PhotosandVideos.cs
// script stores and manages. This scripts changes the user skybox materials,
// implements menu functions, etc.
public class ViewingManager : MonoBehaviour {

    // Video prefab and VideoPlayer already in scene
    public GameObject videoPrefab;

    // Menu canvas access for enabling and disabling
    public GameObject menuCanvas;

    // menuActive is a static variable so it can be accessed by Camera360Viewing.cs
    // when determining camera movement with an active menu
    public static bool menuActive;

    void Start()
    {
        menuActive = false;

        // Keep video still until player wants to view video
        SetVideoPlayback(0f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EnableMenu();
        } else
        {
            SetVideoPlayback(1f);
        }
    }

    private void EnableMenu()
    {
        menuCanvas.SetActive(!menuCanvas.activeSelf);
        menuActive = menuCanvas.activeSelf;

        // Pause video if it exists
        SetVideoPlayback(0f);
    }

    // "EnablePicture" and "EnableVideo" are public functions
    //  that can be accessed by buttons in the bitmpas gameobject
    public void EnablePicture(Transform button)
    {
        SetVideoPlayback(0f);
        videoPrefab.SetActive(false);

        int index = button.GetComponent<ViewingButton>().GetIndex();
        SetSkybox(PhotoVideoInBuilding.viewingElements[index].GetMaterial());
    }

    public void EnableVideo(Transform button)
    {
        videoPrefab.SetActive(true);
        SetVideoPlayback(1f);

        int index = button.GetComponent<ViewingButton>().GetIndex();
        videoPrefab.GetComponent<VideoPlayer>().clip = PhotoVideoInBuilding.viewingElements[index].GetVideoClip();
    }

    private void SetSkybox(Material mat)
    {
        menuCanvas.SetActive(false);
        menuActive = menuCanvas.activeSelf;
        RenderSettings.skybox = mat;
    }

    private void SetVideoPlayback(float x)
    {
        videoPrefab.GetComponent<VideoPlayer>().playbackSpeed = x;
    }
}
