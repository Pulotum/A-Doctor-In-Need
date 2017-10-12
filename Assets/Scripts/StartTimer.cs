using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartTimer : MonoBehaviour {

    public Canvas startTimer;
    public Text three;
    public Text two;
    public Text one;
    public Text go;
    public Text gameover;
    public Text died;

    AudioSource sound;
    public AudioClip sound_three;
    public AudioClip sound_two;
    public AudioClip sound_one;
    public AudioClip sound_go;

    public bool display_three;
    public bool display_two;
    public bool display_one;
    public bool display_go;

    public bool intro = true;
    public bool over = false;
    public bool dead = false;

    GlobalVariables globals;

	// Use this for initialization
	void Start () {

        GameObject globalObj = GameObject.Find("Globals");
        globals = globalObj.GetComponent<GlobalVariables>();
        sound = GetComponent<AudioSource>();

        //display none
        three.enabled = false;
        two.enabled = false;
        one.enabled = false;
        go.enabled = false;
        gameover.enabled = false;
        died.enabled = false;

        display_three = true;
        display_two = false;
        display_one = false;
        display_go = false;

    }

    void Update() {

        if (over) {
            gameover.enabled = true;
        }
        if (dead) {
            died.enabled = true;
        }

        if (display_three) {
            display_three = false;
            StartCoroutine(Display_3());
        }
        if (display_two) {
            display_two = false;
            StartCoroutine(Display_2());
        }
        if (display_one) {
            display_one = false;
            StartCoroutine(Display_1());
        }
        if (display_go) {
            display_go = false;
            StartCoroutine(Display_go());
        }

    }

    IEnumerator Display_3() {
        display_three = false;
        three.enabled = true;
        sound.PlayOneShot(sound_three, globals.op.option_sound);
        yield return new WaitForSeconds(1);
        display_two = true;
        three.enabled = false;
    }

    IEnumerator Display_2() {
        display_two = false;
        two.enabled = true;
        sound.PlayOneShot(sound_two, globals.op.option_sound);
        yield return new WaitForSeconds(1);
        display_one = true;
        two.enabled = false;
    }

    IEnumerator Display_1() {
        display_one = false;
        one.enabled = true;
        sound.PlayOneShot(sound_one, globals.op.option_sound);
        yield return new WaitForSeconds(1);
        display_go = true;
        one.enabled = false;
    }

    IEnumerator Display_go() {
        display_go = false;
        go.enabled = true;
        sound.PlayOneShot(sound_go, globals.op.option_sound);
        yield return new WaitForSeconds(1);
        intro = false;
        go.enabled = false;
    }

}
