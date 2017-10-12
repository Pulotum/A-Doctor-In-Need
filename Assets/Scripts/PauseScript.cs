using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

    public Canvas pausedCanvas;
    public Button resumeButton;
    public Button quitButton;
    AudioSource sound;
    public AudioClip click;

    public bool isPaused = false;

    public GameObject[] menu;
    public int current;

    GlobalVariables globals;

    // Use this for initialization
    void Start() {

        pausedCanvas = pausedCanvas.GetComponent<Canvas>();
        sound = GetComponent<AudioSource>();

        GameObject globalObj = GameObject.Find("Globals");
        globals = globalObj.GetComponent<GlobalVariables>();

        Hide();

    }

    void Update() {
        if (isPaused) {
            Show();
            Time.timeScale = 0f;

            if (Input.GetKeyUp(KeyCode.UpArrow)) {
                current -= 1;
            }
            if (Input.GetKeyUp(KeyCode.DownArrow)) {
                current += 1;
            }

            for (int i = 0; i < menu.Length; i++) {
                menu[i].GetComponent<ButtonScript>().display = false;
            }
            if (current > menu.Length - 1) {
                current = 0;
            }
            if (current < 0) {
                current = menu.Length - 1;
            }
            if (current < menu.Length && current >= 0) {
                menu[current].GetComponent<ButtonScript>().display = true;
            }

        }
        else {
            Hide();
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            isPaused = !isPaused;
        }
    }

    //show menu
    public void Show() {
        pausedCanvas.enabled = true;
        resumeButton.enabled = true;
        quitButton.enabled = true;
    }
    //hide menu
    public void Hide() {
        pausedCanvas.enabled = false;
        resumeButton.enabled = false;
        quitButton.enabled = false;
    }

    //quit to the main menu
    public void QuitPress() {
        StartCoroutine(ButtonClick(2));
    }

    //resume game
    public void ResumePress() {
        StartCoroutine(ButtonClick(1));
    }

    private IEnumerator ButtonClick(int choice) {
        sound.PlayOneShot(click, globals.op.option_sound);
        if (choice == 1) {
            isPaused = false;
        }
        else if (choice == 2) {
            isPaused = false;
            globals.soundSwitch = "menu";
            globals.NextLevel = "_S_MENU";
            SceneManager.LoadScene("_Scenes/_S_MENU");
        }
        else {
            isPaused = false;
            globals.soundSwitch = "menu";
            globals.NextLevel = "_S_MENU";
            SceneManager.LoadScene("_Scenes/_S_MENU");
        }
        yield return new WaitForSeconds(0);
    }


}
