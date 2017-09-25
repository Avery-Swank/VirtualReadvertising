using UnityEngine;
using UnityEngine.Video;

// This class stores either 360 photo or 360 video
// information that can be managed in one massive list
public class ViewingElement : MonoBehaviour {

	public enum VIEWINGTYPE { Photo, Video }

    public VIEWINGTYPE type;

    public Material photoMaterial;

    public VideoClip videoClip;

    public void AddInfo(Material mat, VideoClip clip)
    {
        photoMaterial = mat;
        videoClip = clip;

        // Determines whether this viewing element is a photo or video based on which is null
        if ((photoMaterial == null && videoClip == null) || (photoMaterial != null && videoClip != null))
        {
            Debug.Log("Both objects are null or both objects are not null.");
        } else
        {
            if (photoMaterial == null)
                type = VIEWINGTYPE.Video;
            else
                type = VIEWINGTYPE.Photo;
        }
    }

    public void ClearInfo()
    {
        photoMaterial = null;
        videoClip = null;
        //Destroy(gameObject);
    }

    public VIEWINGTYPE GetTYPE()
    {
        return type;
    }

    public Material GetMaterial()
    {
        return photoMaterial;
    }

    public VideoClip GetVideoClip()
    {
        return videoClip;
    }


}
