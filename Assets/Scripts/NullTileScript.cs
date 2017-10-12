using UnityEngine;
using System.Collections;

public class NullTileScript : MonoBehaviour {

    public int value;
    public Color32 color;
    [Range(0f, 255f)]
    public float alpha;
    public bool solid;
    public bool visible;

    int layer;

	// Use this for initialization
	void Start () {
        layer = gameObject.layer;
	}
	
	// Update is called once per frame
	void Update () {

        if (solid) {
            gameObject.tag = "tile";
            gameObject.layer = layer;
        }
        else {
            gameObject.tag = "Untagged";
            gameObject.layer = 0;
        }

        if (visible) {
            color.a = (byte)alpha;
        }
        else {
            color.a = 0;
        }

        GetComponent<SpriteRenderer>().color = color;


    }
}
