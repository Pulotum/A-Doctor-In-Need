using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour {

    public Text text;
    public GameObject loader;
    public float speed;
    public float angle;
    public float wait;
    public float current;
    bool loadScene = false;
    
    GlobalVariables globals;

    void Start() {
        GameObject globalObj = GameObject.Find("Globals");
        globals = globalObj.GetComponent<GlobalVariables>();

        text.text = globals.info.stage + " - " + globals.info.level; 

        if(globals.info.health <= 0) {
            globals.info.health = 5;
        }

    }
    
	void Update () {
        
        globals.NextLevel = globals.NextLevel.Replace("_Scenes/", "");

        angle -= Time.deltaTime * speed;
        if (Mathf.Abs(angle) >= 360) {
            angle = 0;
        }
        loader.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        current += Time.deltaTime;
        if (current >= wait) {
            current = wait;
        }

        if (!loadScene && current >= wait) {
            loadScene = true;
            StartCoroutine(LoadNextScene());
        }

	}

    IEnumerator LoadNextScene() {

        AsyncOperation async;

        if (globals.info.stage > globals.StageLimit.Length) {
            globals.info.stage = 1;
            globals.info.level = 1;
            globals.soundSwitch = "menu";
            async = SceneManager.LoadSceneAsync("_Scenes/_S_MENU");
        }
        else {
            async = SceneManager.LoadSceneAsync("_Scenes/" + globals.NextLevel);
        }

        while (!async.isDone) {
            yield return null;
        }

    }
}
