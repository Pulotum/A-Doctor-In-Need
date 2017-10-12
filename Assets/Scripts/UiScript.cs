using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiScript : MonoBehaviour {

    public Text lives;
    public Text score;
    public Text health;

    public Image background;
    public Text title;
    public Text desc;

    GlobalVariables globals;

	// Use this for initialization
	void Start () {

        lives.text = "";
        score.text = "";

        background.enabled = false;
        title.enabled = false;
        desc.enabled = false;

        GameObject globalObj = GameObject.Find("Globals");
        globals = globalObj.GetComponent<GlobalVariables>();

    }
	
	// Update is called once per frame
	void Update () {
        lives.text = "Lives:  " + globals.info.lives.ToString();
        score.text = "Score:  " + globals.info.score.ToString();
        health.text = "Health: " + globals.info.health.ToString();
    }
    public void Display(string title, string disc) {
        StartCoroutine(show(title, disc));
    }

    IEnumerator show(string text, string disc) {
        background.enabled = true;
        title.enabled = true;
        desc.enabled = true;
        title.text = text;
        desc.text = disc;
        yield return new WaitForSeconds(3);
        background.enabled = false;
        title.enabled = false;
        desc.enabled = false;
    }

}
