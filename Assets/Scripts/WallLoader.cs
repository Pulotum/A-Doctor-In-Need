using UnityEngine;
using System.Collections;

public class WallLoader : MonoBehaviour {

    public GameObject[] walls;
    GameObject wall;
    float width, height;

	// Use this for initialization
	void Start () {

        width = 28 * walls[0].transform.localScale.x;
        height = 21 * walls[0].transform.localScale.y;

        int i = 0;
        int j;
        while(i < 10) {

            int ran = walls.Length + (walls.Length / 2);
            int pre = walls.Length / 2;
            Debug.Log("Ran:" + ran + " Pre:" + pre);

            j = Random.Range(0, ran);
            Debug.Log("Pre Check J:" + j);
            if(j < pre) {
                j = 0;
            }
            else {
                j -= pre;
            }

            if(i == 0) {
                Debug.Log("J:" + j);
                Instantiate(walls[0], new Vector3(15 + (width * i), 17, 10), Quaternion.identity);
                Instantiate(walls[0], new Vector3(15 + (width * i), 17 - height, 10), Quaternion.identity);
                i++;
            }
            else {
                Debug.Log("J:" + j);
                Instantiate(walls[j], new Vector3(-10 + (width * i), 17, 10), Quaternion.identity);
                Instantiate(walls[0], new Vector3(-10 + (width * i), 17 - height, 10), Quaternion.identity);
                i++;
            }

            
        }
	}
	
}
