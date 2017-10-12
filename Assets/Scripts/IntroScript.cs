using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour {

    public bool display;

    [Header("Times")]
    public float current;
    public float timeBefore;
    public float timeDuring;
    public float timeAfter;

    [Header("Objects")]
    public Image logo;
    public Image overlay;
    public Text warning;
    Color32 colorAfter;
    Color32 colorBefore;

    // Use this for initialization
    void Start () {
        colorBefore = overlay.color;
        colorAfter = new Color32(colorBefore.r, colorBefore.g, colorBefore.b, 0);

        logo.enabled = display;

    }
	
	// Update is called once per frame
	void Update () {
        current += Time.deltaTime * 0.5f ;

        if(current < timeBefore) {
            warning.enabled = true;
        }

        if(current >= timeBefore) {
            warning.enabled = false;
            overlay.color = Color32.Lerp(colorBefore, colorAfter, (current - timeBefore) / timeDuring);
        }
        
        if(current >= timeBefore + timeDuring + timeAfter) {
            SceneManager.LoadScene("_S_MENU");
        }

	}
}
