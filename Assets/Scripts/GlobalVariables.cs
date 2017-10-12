using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class PlayerInfo {
    public int lives;
    public int health;
    public int score;
    public int stage;
    public int level;
}
[System.Serializable]
public class OptionInfo {
    public float option_music;
    public float option_sound;
}

public class GlobalVariables : MonoBehaviour {

    [Header("Force Resets")]
    public bool resetPlayer;
    public bool resetOption;
    public bool resetGoals;

    [Header("Meta Data")]
    public string NextLevel;

    [Header("Sound Settings")]
    public string soundSwitch;
    
    AudioSource sound;
    public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioClip creditMusic;
    public bool menuMusic_playing;
    public bool gameMusic_playing;
    public bool creditMusic_playing;

    public bool inMenu;
    public bool inGame;
    public bool inCredit;

    public PlayerInfo info;
    public OptionInfo op;

    public int[] StageLimit = {  1, 
                            1};

    void Start() {

        if(GameObject.FindGameObjectsWithTag("global").Length > 1) {
            Destroy(this.gameObject);
            return;
        }
        
        DontDestroyOnLoad(this.gameObject);

        Load_Player();
        Load_Option();

        sound = GetComponent<AudioSource>();

        inMenu = true;
        inGame = false;
        inCredit = false;
        menuMusic_playing = false;
        gameMusic_playing = false;
        creditMusic_playing = false;
    }

    void Update() {

        //check for forced resets
        if (resetPlayer) {
            Reset_Player();
            Save_Player();
            resetPlayer = false;
        }
        if (resetOption) {
            Reset_Option();
            Save_Option();
            resetOption = false;
        }
        if (resetGoals) {
            GetComponent<GoalsScript>().Reset_Goals();
            GetComponent<GoalsScript>().Save_Goals();
            resetGoals = false;
        }


        if (soundSwitch != "") {
            Switch(soundSwitch);
        }

        if (op.option_music > 1) {
            op.option_music = 1;
        }
        if (op.option_music < 0) {
            op.option_music = 0;
        }
        if (op.option_sound > 1) {
            op.option_sound = 1;
        }
        if (op.option_sound < 0) {
            op.option_sound = 0;
        }

        sound.volume = op.option_music;

        if (inMenu) {
            if(info.lives <= 0) {
                Reset_Player();
            }
            StartCoroutine(MusMenu());
        }
        if (inGame) {
            StartCoroutine(MusGame());
        }
        if (inCredit) {
            StartCoroutine(CreGame());
        }
    }

    public void Switch(string Choice) {
        soundSwitch = "";
        sound.Stop();
        inMenu = false;
        inGame = false;
        inCredit = false;
        menuMusic_playing = false;
        gameMusic_playing = false;
        creditMusic_playing = false;
        if (Choice == "menu") {
            inMenu = true;
        }
        if (Choice == "game") {
            inGame = true;
        }
        if (Choice == "credit") {
            inCredit = true;
        }
    }

    private IEnumerator MusMenu() {
        if (menuMusic_playing == false) {
            menuMusic_playing = true;
            sound.clip = menuMusic;
            sound.volume = op.option_music;
            sound.Play();
            yield return new WaitForSeconds(menuMusic.length);
            menuMusic_playing = false;
        }
        else {
            yield return new WaitForSeconds(0);
        }
    }
    private IEnumerator MusGame() {
        if (gameMusic_playing == false) {
            gameMusic_playing = true;
            sound.clip = gameMusic;
            sound.volume = op.option_music;
            sound.Play();
            yield return new WaitForSeconds(gameMusic.length);
            gameMusic_playing = false;
        }
        else {
            yield return new WaitForSeconds(0);
        }
    }
    private IEnumerator CreGame() {
        if (creditMusic_playing == false) {
            creditMusic_playing = true;
            sound.clip = creditMusic;
            sound.volume = op.option_music;
            sound.Play();
            yield return new WaitForSeconds(creditMusic.length);
            creditMusic_playing = false;
        }
        else {
            yield return new WaitForSeconds(0);
        }
    }

    //player
    public void Save_Player() {
        Debug.Log("Player Info Saved");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.gd");
        bf.Serialize(file, info);
        file.Close();
    }
    public void Delete_Player() {
        Debug.Log("Player Info Deleted");
        info = new PlayerInfo();
        if (File.Exists(Application.persistentDataPath + "/playerInfo.gd")) {
            File.Delete(Application.persistentDataPath + "/playerInfo.gd");
        }
    }
    public void Load_Player() {
        Debug.Log("Player Info Loaded");
        if (File.Exists(Application.persistentDataPath + "/playerInfo.gd")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenRead(Application.persistentDataPath + "/playerInfo.gd");
            info = (PlayerInfo)bf.Deserialize(file);
            file.Close();
        }
        else {
            Reset_Player();
        }
    }
    public void Reset_Player() {
        Debug.Log("Player Info Reset");
        info = new PlayerInfo();
        info.lives = 3;
        info.health = 5;
        info.score = 0;
        info.stage = 1;
        info.level = 1;
        Save_Player();
    }

    //option
    public void Save_Option() {
        Debug.Log("Option Info Saved");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/optionInfo.gd");
        bf.Serialize(file, op);
        file.Close();
    }
    public void Delete_Option() {
        Debug.Log("Option Info Deleted");
        op = new OptionInfo();
        if (File.Exists(Application.persistentDataPath + "/optionInfo.gd")) {
            File.Delete(Application.persistentDataPath + "/optionInfo.gd");
        }
    }
    public void Load_Option() {
        Debug.Log("Option Info Loaded");
        if (File.Exists(Application.persistentDataPath + "/optionInfo.gd")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenRead(Application.persistentDataPath + "/optionInfo.gd");
            op = (OptionInfo)bf.Deserialize(file);
            file.Close();
        }
        else {
            Reset_Option();
        }
    }
    public void Reset_Option() {
        Debug.Log("Option Info Reset");
        op = new OptionInfo();
        op.option_music = 0.5f;
        op.option_sound = 0.5f;
        Save_Option();
    }

}
