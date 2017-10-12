using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour {

    public bool touching = false;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        if (touching) {
            DestroyObject(this.gameObject);
            Debug.Log("destroyed");
        }
	}


}
