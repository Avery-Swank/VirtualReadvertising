using UnityEngine;
using UnityEngine.UI;

// This class is responsible for holding an index in the 
// ViewingElements list so when a user clicks on the button
// it opens the viewingelement that corresponds to it.
public class ViewingButton : MonoBehaviour {

    // Boolean to justify either a photoButton or a videoButton
    public bool photoButton;
    private int viewingElementIndex;

    private ViewingManager manager;


    void Start()
    {
        manager = GameObject.FindObjectOfType<ViewingManager>();

        if (photoButton)
        {
            UnityEngine.Events.UnityAction action = () => { manager.EnablePicture(transform); };
            GetComponent<Button>().onClick.AddListener(action);
        }
        else
        {
            UnityEngine.Events.UnityAction action = () => { manager.EnableVideo(transform); };
            GetComponent<Button>().onClick.AddListener(action);
        }
    }

    public void SetIndex(int x)
    {
        viewingElementIndex = x;
    }

    public int GetIndex()
    {
        return viewingElementIndex;
    }
}
