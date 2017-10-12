using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

[System.Serializable]
public class GoalList {
    public string image;
    public string title;
    public string desc;
    public string checker;
    public float got;
    public float goal;
    public string date;

    public GoalList(string _title, string _desc, string _checker, float _goal) {
        title = _title;
        desc = _desc;
        checker = _checker;
        got = 0;
        goal = _goal;
        date = "";
    }
}

public class GoalsScript : MonoBehaviour {

    public bool reCheck;

    public GoalList[] List;
      

	// Use this for initialization
	void Start () {

        if (GameObject.FindGameObjectsWithTag("goal").Length > 1) {
            Destroy(this.gameObject);
            return;
        }

        Load_Goals();
        
        DontDestroyOnLoad(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        if (reCheck) {
            ReCheck();
        }
	}

    public void Increment(string type, float i) {
        foreach(GoalList GOAL in List) {
            if(GOAL.checker == type) {
                GOAL.got += i;
            }
        }
        ReCheck();
        Save_Goals();
    }

    void ReCheck() {
        int i = 0;
        foreach (GoalList GOAL in List) {
            if (GOAL.got >= GOAL.goal && GOAL.date == "") {
                GOAL.date = System.DateTime.Now.ToString("yyyy/MM/dd");
                Display(i);
            }
            i++;
        }
    }

    public void Display(int i) {
        GameObject uiObj = GameObject.Find("General UI");
        UiScript ui = uiObj.GetComponent<UiScript>();
        ui.Display(List[i].title, List[i].desc);
    }


    //File management
    public void Save_Goals() {
        Debug.Log("Goals Saved");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/goals.gd");
        bf.Serialize(file, List);
        file.Close();
    }
    public void Delete_Goals() {
        Debug.Log("Goals Deleted");
        List = new GoalList[1];
        if (File.Exists(Application.persistentDataPath + "/goals.gd")) {
            File.Delete(Application.persistentDataPath + "/goals.gd");
        }
    }
    public bool Load_Goals() {
        Debug.Log("Goals Loaded");
        if (File.Exists(Application.persistentDataPath + "/goals.gd")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenRead(Application.persistentDataPath + "/goals.gd");
            List = (GoalList[])bf.Deserialize(file);
            file.Close();
            return true;
        }
        else {
            return false;
        }
    }
    public void Reset_Goals() {
        Debug.Log("Goals Reset");
        List = new GoalList[6];
        List[0] = new GoalList("Mario Man In Training", "Jump 10 Times", "jump", 10);
        List[1] = new GoalList("OK We Get It", "Jump 20 Times", "jump", 20);
        List[2] = new GoalList("Now Your Just Showing Off", "Jump 100 Times", "jump", 100);
        List[3] = new GoalList("Ouch", "Hit By Enemy", "hit", 1);
        List[4] = new GoalList("You Gotta Try Harder Than That", "Hit By Enemy 10 Times", "hit", 10);
        List[5] = new GoalList("Just Starting", "Finished First Level", "end_level", 1);
    }
}
