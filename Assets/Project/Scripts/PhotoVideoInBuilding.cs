using UnityEngine;
using UnityEngine.Video;

// This class manages the 360 photos and videos that are downloaded
// to the machine because the client wants to view photos in this building
public class PhotoVideoInBuilding : MonoBehaviour {

    public Material[] pictureMaterials;

    public VideoClip[] videoClips;

    public static ViewingElement[] viewingElements;

    void Start()
    {
        FillViewingElements();
    }

    // Fill up the static list with element objects with either a photo or video
    public void FillViewingElements()
    {
        viewingElements = new ViewingElement[pictureMaterials.Length + videoClips.Length];
        
        // Create empty objects
        for(int i = 0; i < viewingElements.Length; i++)
        {
            viewingElements[i] = new ViewingElement();
        }

        // Add photos to list
        for(int i = 0; i < pictureMaterials.Length; i++)
        {
            ViewingElement el = viewingElements[i];
            el.AddInfo(pictureMaterials[i], null);
            viewingElements[i] = el;
        }
        
        // Add videos to list
        for (int i = pictureMaterials.Length; i < videoClips.Length; i++)
        {
            ViewingElement el = viewingElements[i];
            el.AddInfo(null, videoClips[i - pictureMaterials.Length]);
            viewingElements[i] = el;
        }
    }

    // Clear the elements when the user exits the building or picks another building
    public void ClearElements()
    {
        viewingElements = null;
    }

}
