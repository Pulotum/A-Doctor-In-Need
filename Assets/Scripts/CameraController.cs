using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float dis;
    public float error;

    [Header("Positions")]
    public Transform Player;
    public Transform Target;
    public Vector3 playerPos;
    public Vector3 cameraPos;

    public string moveTo;
    public string lockTo;
    public float travelSpeed;

    bool hasReset;

	// Use this for initialization
	void Start () {
        Player = FindObjectOfType<Player>().transform;
        playerPos = Player.position;
        cameraPos = transform.position;
       

	}
	
	// Update is called once per frame
	void Update () {
        playerPos = Player.position;
        cameraPos = transform.position;
        

        if (moveTo == "player") {
            transform.SetParent(GameObject.Find("WORLD").transform);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerPos.x, playerPos.y, cameraPos.z), travelSpeed * Time.deltaTime);
            if ((cameraPos.x > Player.transform.position.x - error & cameraPos.x < Player.transform.position.x + error) && (cameraPos.y > Player.transform.position.y - error & cameraPos.y < Player.transform.position.y + error)) {
                lockTo = "player";
                moveTo = "";
            }
        }
        else if (moveTo == "target") {
            transform.SetParent(GameObject.Find("WORLD").transform);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(Target.transform.position.x, Target.transform.position.y, cameraPos.z), travelSpeed * Time.deltaTime);
            if ((cameraPos.x > Target.transform.position.x - error & cameraPos.x < Target.transform.position.x + error) && (cameraPos.y > Target.transform.position.y - error & cameraPos.y < Target.transform.position.y + error)) {
                lockTo = "target";
                moveTo = "";
            }
        }
        else if (moveTo == "world") {
            transform.SetParent(GameObject.Find("WORLD").transform);
            lockTo = "world";
            moveTo = "";
        }

        if (lockTo == "player") {
            transform.SetParent(Player);
            transform.localPosition = new Vector3(0, 0, cameraPos.z);
            resetSwitches();
        }
        else if (lockTo == "target") {
            transform.SetParent(Target);
            transform.localPosition = new Vector3(0, 0, cameraPos.z);
            resetSwitches();
        }
        else if (lockTo == "world") {
            transform.localPosition = cameraPos;
            resetSwitches();
        }
        else {
            transform.SetParent(GameObject.Find("WORLD").transform);
        }

    }

    public void MoveToPlayer() {
        hasReset = false;
        lockTo = "";
        moveTo = "player";
    }
    public void MoveToTarget() {
        hasReset = false;
        lockTo = "";
        moveTo = "target";
    }
    public void MoveToWorld() {
        hasReset = false;
        lockTo = "";
        moveTo = "world";
    }

    void resetSwitches() {
        if (!hasReset) {
            hasReset = true;
            foreach (GameObject swatch in GameObject.FindGameObjectsWithTag("switcher")) {
                swatch.GetComponent<SwitchController>().hasFocus = false;
            }
        }
    }

}
