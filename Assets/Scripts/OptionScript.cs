using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionScript : MonoBehaviour {

    public Button backButton;
    public Slider musicSlider;
    public Slider soundSlider;

    AudioSource sound;
    public AudioClip click;
    GlobalVariables globals;

    public GameObject[] menu;
    public int current;
    GameObject globalObj;

    // Use this for initialization
    void Start () {

        globalObj = GameObject.Find("Globals");
        globals = globalObj.GetComponent<GlobalVariables>();

        backButton = backButton.GetComponent<Button>();
        musicSlider = musicSlider.GetComponent<Slider>();
        soundSlider = soundSlider.GetComponent<Slider>();

        sound = GetComponent<AudioSource>();

        musicSlider.value = globals.op.option_music;
        soundSlider.value = globals.op.option_sound;

    }
	
	// Update is called once per frame
	void Update () {

        musicSlider.value = globals.op.option_music;
        soundSlider.value = globals.op.option_sound;

        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            current -= 1;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow)) {
            current += 1;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow)) {
            if (menu[current].transform.name == "music") {
                globals.op.option_music = globals.op.option_music + .1f;
                musicSlider.value = globals.op.option_music;
                globals.Save_Option();
            }
            if (menu[current].transform.name == "sound") {
                globals.op.option_sound = globals.op.option_sound + .1f;
                soundSlider.value = globals.op.option_sound;
                globals.Save_Option();
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            if (menu[current].transform.name == "music") {
                globals.op.option_music = globals.op.option_music - .1f;
                musicSlider.value = globals.op.option_music;
                globals.Save_Option();
            }
            if (menu[current].transform.name == "sound") {
                globals.op.option_sound = globals.op.option_sound - .1f;
                soundSlider.value = globals.op.option_sound;
                globals.Save_Option();
            }
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

    public void BackPress() {
        StartCoroutine(ButtonClick(1));
    }

    public void MusicSlider() {
        globals.op.option_music = musicSlider.value;
        globals.Save_Option();
    }

    public void SoundSlider() {
        globals.op.option_sound = soundSlider.value;
        globals.Save_Option();
    }

    public void EraseGoals() {
        StartCoroutine(ButtonClick(10));
    }
    public void EraseInfo() {
        StartCoroutine(ButtonClick(9));
    }


    private IEnumerator ButtonClick(int choice) {
        sound.PlayOneShot(click, globals.op.option_sound);
        yield return new WaitForSeconds(click.length);
        if(choice == 1) {
            globals.NextLevel = "_S_MENU";
            SceneManager.LoadScene("_Scenes/_S_MENU");
        }
        else if (choice == 9) {
            globals.Delete_Player();
            globals.Reset_Player();
        }
        else if(choice == 10) {
            GoalsScript goals = globalObj.GetComponent<GoalsScript>();
            goals.Delete_Goals();
            goals.Reset_Goals();
        }
    }
}
