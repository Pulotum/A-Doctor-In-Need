using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GoalsPageScript : MonoBehaviour {

    AudioSource sound;
    public AudioClip click;

    public Scrollbar scroll;
    public GameObject wall;
    public GameObject goal_temp;
    public float wallHeight;

    public List<Transform> listCanvas;
    public List<Transform> listArray;

    Vector2 currentPos;
    float finalPos;

    GlobalVariables globals;
    GoalsScript goals;
    public GameObject[] menu;
    public int current;

    // Use this for initialization
    void Start () {
        GameObject globalObj = GameObject.Find("Globals");
        globals = globalObj.GetComponent<GlobalVariables>();
        goals = globalObj.GetComponent<GoalsScript>();
        sound = GetComponent<AudioSource>();

        //generate goals
        int i = 0;
        foreach(GoalList GOAL in goals.List) {
            GameObject obj = (GameObject)Instantiate(goal_temp, new Vector3(0, 0, 0), Quaternion.identity);
            obj.transform.SetParent(wall.transform);
            obj.transform.position = obj.transform.parent.transform.position - new Vector3(0, 120 * i, 0);

            obj.GetComponent<PersonalGoalScript>().img.color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
            obj.GetComponent<PersonalGoalScript>().title.text = GOAL.title;
            obj.GetComponent<PersonalGoalScript>().desc.text = GOAL.desc;
            obj.GetComponent<PersonalGoalScript>().date.text = GOAL.date;
            if(GOAL.got >= GOAL.goal) {
                obj.GetComponent<PersonalGoalScript>().got.text = GOAL.goal + " / " + GOAL.goal;
            }
            else {
                obj.GetComponent<PersonalGoalScript>().got.text = GOAL.got + " / " + GOAL.goal;
            }


            i++;
        }

        //GENERATED LISTS
        foreach (Transform child in transform) {
            listCanvas.Add(child);
        }
        foreach (Transform child in listCanvas[0]) {
            listArray.Add(child);
            wallHeight += 120;
        }

        currentPos = wall.transform.position;
        finalPos = wallHeight - 100;

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetAxis("Mouse ScrollWheel") > 0f) {
            scroll.value -= 0.1f;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetAxis("Mouse ScrollWheel") < 0f) {
            scroll.value += 0.1f;
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

        wall.transform.position = currentPos + new Vector2(0, finalPos * scroll.value);
    }

    public void BackPress() {
        StartCoroutine(ButtonClick());
    }

    public IEnumerator ButtonClick() {
        sound.PlayOneShot(click, globals.op.option_sound);
        yield return new WaitForSeconds(click.length);
        globals.NextLevel = "_S_MENU";
        SceneManager.LoadScene("_Scenes/_S_MENU");
    }

}
