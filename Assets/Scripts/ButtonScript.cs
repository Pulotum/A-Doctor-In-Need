using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

    public Image left;
    public Image right;
    public bool display;
    public int num;
    Rect rect;
    GameObject[] array;


    void Start() {
        rect = new Rect(0, 0, 110, 35);
        rect.center = new Vector2(transform.position.x, transform.position.y);

        if (gameObject.transform.parent.gameObject.GetComponent<MenuScript>() != null) {
            array = gameObject.transform.parent.gameObject.GetComponent<MenuScript>().menu;
        }
        if (gameObject.transform.parent.gameObject.GetComponent<PauseScript>() != null) {
            array = gameObject.transform.parent.gameObject.GetComponent<PauseScript>().menu;
        }
        if (gameObject.transform.parent.gameObject.GetComponent<OptionScript>() != null) {
            array = gameObject.transform.parent.gameObject.GetComponent<OptionScript>().menu;
        }
        if (gameObject.transform.parent.gameObject.GetComponent<GoalsPageScript>() != null) {
            array = gameObject.transform.parent.gameObject.GetComponent<GoalsPageScript>().menu;
        }

        int i = 0;
        foreach(GameObject obj in array) {
            if(obj == gameObject) {
                num = i;
            }
            i++;
        }

    }

	// Update is called once per frame
	void Update () {
        
        if (rect.Contains(Input.mousePosition)) {
            if (gameObject.transform.parent.gameObject.GetComponent<MenuScript>() != null) {
                gameObject.transform.parent.gameObject.GetComponent<MenuScript>().current = num;
            }
            if (gameObject.transform.parent.gameObject.GetComponent<OptionScript>() != null) {
                gameObject.transform.parent.gameObject.GetComponent<OptionScript>().current = num;
            }
            if (gameObject.transform.parent.gameObject.GetComponent<PauseScript>() != null) {
                gameObject.transform.parent.gameObject.GetComponent<PauseScript>().current = num;
            }
            if (gameObject.transform.parent.gameObject.GetComponent<GoalsPageScript>() != null) {
                gameObject.transform.parent.gameObject.GetComponent<GoalsPageScript>().current = num;
            }

        }

        if (display) {
            left.enabled = true;
            right.enabled = true;

            if (Input.GetButtonUp("Enter")) {
                this.GetComponent<Button>().onClick.Invoke();
            }

        }
        else {
            left.enabled = false;
            right.enabled = false;
        }
	}

}
