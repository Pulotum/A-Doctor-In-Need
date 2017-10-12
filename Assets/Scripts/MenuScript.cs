using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    AudioSource sound;
    public AudioClip click;
    GlobalVariables globals;

    public GameObject[] menu;
    public int current;

	// Use this for initialization
	void Start () {

        GameObject globalObj = GameObject.Find("Globals");
        globals = globalObj.GetComponent<GlobalVariables>();
        sound = GetComponent<AudioSource>();
	}

    void Update() {

        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            current -= 1;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow)) {
            current += 1;
        }

        if (globals.info.stage == 1 && globals.info.level == 1) {
            menu[0].GetComponent<Text>().text = "New Game";
        }
        else {
            menu[0].GetComponent<Text>().text = "Continue";
        }


        for (int i = 0; i < menu.Length; i++) {
            menu[i].GetComponent<ButtonScript>().display = false;
        }
        if(current > menu.Length-1) {
            current = 0;
        }
        if(current < 0) {
            current = menu.Length - 1;
        }
        if(current < menu.Length && current >= 0) {
            menu[current].GetComponent<ButtonScript>().display = true;
        }
    }
	
	public void ExitPress() {
        StartCoroutine(ButtonClick(2));
    }

    public void StartPress() {
        StartCoroutine(ButtonClick(1));
    }

    public void OptionPress() {
        StartCoroutine(ButtonClick(3));
    }

    public void CreditsPress() {
        StartCoroutine(ButtonClick(4));
    }

    public void GoalsPress() {
        StartCoroutine(ButtonClick(5));
    }

    private IEnumerator ButtonClick(int choice) {
        sound.PlayOneShot(click, globals.op.option_sound);
        if(choice == 1) {
            globals.soundSwitch = "game";
            globals.NextLevel = "_S_GAME_" + globals.info.stage + "_" + globals.info.level;
            SceneManager.LoadScene("_Scenes/_S_LOADING");
            
        }
        else if(choice == 2) {
            Application.Quit();
        }
        else if (choice == 3) {
            globals.NextLevel = "_S_OPTIONS";
            SceneManager.LoadScene("_Scenes/_S_OPTIONS");
        }
        else if (choice == 4) {
            globals.soundSwitch = "credit";
            globals.NextLevel = "_S_CREDITS";
            SceneManager.LoadScene("_Scenes/_S_CREDITS");
        }
        else if (choice == 5) {
            globals.NextLevel = "_S_GOALS";
            SceneManager.LoadScene("_Scenes/_S_GOALS");
        }
        else {
            Application.Quit();
        }
        yield return new WaitForSeconds(0);
    }

}
