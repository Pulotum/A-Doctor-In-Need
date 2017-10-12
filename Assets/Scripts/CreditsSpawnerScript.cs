using UnityEngine;
using System.Collections;

public class CreditsSpawnerScript : MonoBehaviour {

    public GameObject star;

    public float spawnSpeedMin;
    public float spawnSpeedMax;
    public float spawnSpeedCurrent;
    public float spawnCounter;

    public int MasterCounter;

    float minX;
    float maxX;
    float minY;
    float maxY;

    // Use this for initialization
    void Start () {
        var VertExtent = Camera.main.orthographicSize;
        var HorzExtent = VertExtent * Screen.width / Screen.height;

        minX = -Mathf.Floor(HorzExtent);
        maxX = Mathf.Floor(HorzExtent);
        minY = -Mathf.Floor(VertExtent);
        maxY = Mathf.Floor(VertExtent);
    }
	
	// Update is called once per frame
	void Update () {
	    if(spawnCounter <= 0) {
            Spawn();
            spawnSpeedCurrent = Random.Range(spawnSpeedMin, spawnSpeedMax);
            spawnCounter = spawnSpeedCurrent;
        }

        spawnCounter -= Time.deltaTime;

        MasterCounter = GameObject.FindGameObjectsWithTag("firework").Length;


    }

    void Spawn() {
        int x = (int)Random.Range(minX, maxX);
        int y = (int)Random.Range(minY, maxY);
        
        Instantiate(star, new Vector2(x, y), Quaternion.identity);
    }
}
