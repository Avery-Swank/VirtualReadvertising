using UnityEngine;
using UnityEngine.UI;

public class CameraBlackScreen : MonoBehaviour {

    // Boolean logic to keep image alpha rotating in an update loop
    public static bool apear;
    public static bool disapear;

    private Image blackBackground;
    private float fadeSpeed = .5f;

	void Start()
    {
        // Keep black screen functional across all scenes for smooth fade in fade out effects
        DontDestroyOnLoad(gameObject);
        apear = true;
        disapear = true;

        blackBackground = transform.GetChild(0).GetComponent<Image>();
    }

    void Update()
    {
        if (apear)
        {
            Apear();
        } else if (disapear)
        {
            Disapear();
        } else
        {
            Debug.Log("ERROR: appear and disapear are both false.");
        }
    }

    public void Disapear()
    {
        float alpha = blackBackground.color.a;
        alpha -= fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp(alpha, 0f, 255f);

        blackBackground.color = new Color(0f, 0f, 0f, alpha);
    }

    public void Apear()
    {
        float alpha = blackBackground.color.a;
        alpha += fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp(alpha, 0f, 255f);

        blackBackground.color = new Color(0f, 0f, 0f, alpha);
    }
}
