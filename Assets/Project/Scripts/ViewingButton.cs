using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is responsible for holding an index in the 
// ViewingElements list so when a user clicks on the button
// it opens the viewingelement that corresponds to it.
public class ViewingButton : MonoBehaviour {

    private int viewingElementIndex;

    public void SetIndex(int x)
    {
        viewingElementIndex = x;
    }

    public int GetIndex()
    {
        return viewingElementIndex;
    }
}
