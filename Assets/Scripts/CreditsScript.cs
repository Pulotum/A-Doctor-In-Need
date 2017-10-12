using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Words {
    public string title;
    public string[] names;

    public Words(string _title, string[] _names) {
        title = _title;
        names = _names;
    }
}

public class CreditsScript : MonoBehaviour {
    
    public float speed;
    public bool display;

    public Color32 colorOrg;
    public Color32 colorNew;
    public float range;

    GlobalVariables globals;

    public GameObject movingWall;
    public float nextHeight;
    public Text textPrefab;
    public bool displayTitle;

    [Header("Font Specifics")]
    public Font fontMain;
    public Font fontName;
    public float titleSize;
    public float nameSize;
    public string underline;
    public Color32 fontColor;
    public float spaceEach;
    public float spaceUnderline;
    public float spaceTitle;
    public float spaceNext;

    [Header("Names")]
    public Words[] Wall;
    Text textThanks;

    // Use this for initialization
    void Start () {
        display = true;

        GameObject globalObj = GameObject.Find("Globals");
        globals = globalObj.GetComponent<GlobalVariables>();

        Wall = new Words[11];
        Wall[0] = new Words("Director",         new string[] { "Josh Larminay" });
        Wall[1] = new Words("Producer",         new string[] { "Josh Larminay" });
        Wall[2] = new Words("Project Manager",  new string[] { "Josh Larminay" });
        Wall[3] = new Words("Designers",        new string[] { "Josh Larminay" });
        Wall[4] = new Words("Level Designers",  new string[] { "Josh Larminay" });
        Wall[5] = new Words("Programmers",      new string[] { "Josh Larminay" });
        Wall[6] = new Words("Artists",          new string[] { "Josh Larminay" });
        Wall[7] = new Words("Animators",        new string[] { "Josh Larminay" });
        Wall[8] = new Words("Writers",          new string[] { "Josh Larminay" });
        Wall[9] = new Words("QA Testers",       new string[] { "Josh Larminay" });
        Wall[10] = new Words("Special Thanks",  new string[] { "Josh Larminay" });




        if (displayTitle) {
            Text textMain = (Text)Instantiate(textPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            textMain.transform.SetParent(movingWall.transform);
            textMain.transform.position = new Vector3(movingWall.transform.position.x, movingWall.transform.position.y - nextHeight, 0);
            textMain.font = fontMain;
            textMain.color = fontColor;
            textMain.fontSize = 70;
            textMain.text = "A Doctor In Need";
            nextHeight += 300;
        }

        foreach (Words coll in Wall) {
            Text textTitle = (Text)Instantiate(textPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            textTitle.transform.SetParent(movingWall.transform);
            textTitle.transform.position = new Vector3(movingWall.transform.position.x, movingWall.transform.position.y - nextHeight, 0);
            textTitle.font = fontName;
            textTitle.color = fontColor;
            textTitle.fontSize = (int)titleSize;
            textTitle.text = coll.title;
            nextHeight += spaceUnderline;

            Text textUnderline = (Text)Instantiate(textPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            textUnderline.transform.SetParent(movingWall.transform);
            textUnderline.transform.position = new Vector3(movingWall.transform.position.x, movingWall.transform.position.y - nextHeight, 0);
            textUnderline.font = fontName;
            textUnderline.color = fontColor;
            textUnderline.fontSize = (int)titleSize;
            textUnderline.text = underline;

            nextHeight += spaceTitle;

            foreach (string name in coll.names) {
                Text textName = (Text)Instantiate(textPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                textName.transform.SetParent(movingWall.transform);
                textName.transform.position = new Vector3(movingWall.transform.position.x, movingWall.transform.position.y - nextHeight, 0);
                textName.font = fontName;
                textName.color = fontColor;
                textName.fontSize = (int)nameSize;
                textName.text = name;
                nextHeight += spaceEach;
            }

            nextHeight += spaceNext;
        }
        nextHeight += 200;
        textThanks = (Text)Instantiate(textPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        textThanks.transform.SetParent(movingWall.transform);
        textThanks.transform.position = new Vector3(movingWall.transform.position.x, movingWall.transform.position.y - nextHeight, 0);
        textThanks.font = fontName;
        textThanks.color = fontColor;
        textThanks.fontSize = 70;
        textThanks.text = "Thanks For Playing";

    }
	
	// Update is called once per frame
	void Update () {

        if (display) {
            if (Input.anyKey) {
                globals.soundSwitch = "menu";
                globals.NextLevel = "_S_MENU";
                SceneManager.LoadScene("_Scenes/_S_MENU");
            }
            
            if(textThanks.transform.position.y >= (Screen.height/2) + 35) {
                textThanks.transform.position = new Vector3(Screen.width / 2, (Screen.height/2) + 35, 0);
                speed = 0;
            }

            Camera.main.backgroundColor = Color32.Lerp(colorOrg, colorNew, (movingWall.transform.position.y / nextHeight));
            range = movingWall.transform.position.y / nextHeight;
            if(range >= 1) {
                range = 1;
            }

            movingWall.transform.position += new Vector3(0, Time.deltaTime * speed, 0);
        }
	}

}
