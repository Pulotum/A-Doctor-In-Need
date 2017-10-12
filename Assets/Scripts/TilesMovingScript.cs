using UnityEngine;
using System.Collections;

public enum Direction_Tile {
    vertical, horizontal
}

public class TilesMovingScript : MonoBehaviour {

    public Direction_Tile dir;
    public int range;
    public float speed;
    public bool showTrack;

    public bool goingUp;

    public GameObject tile;
    public GameObject track;

    // Use this for initialization
    void Start() {
        if (showTrack) {
            if (dir == Direction_Tile.vertical) {
                track.transform.localScale = new Vector3(0.1f / transform.localScale.x, range * 2, 0);
                track.transform.localPosition = new Vector3(0, range, 0.01f);
            }
            if (dir == Direction_Tile.horizontal) {
                track.transform.localScale = new Vector3(range / 2.5f, 0.1f / transform.localScale.y, 0);
                track.transform.localPosition = new Vector3((range/2.5f) / 2, 0, 0.01f);
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (dir == Direction_Tile.vertical) {
            if ((transform.position.y - tile.transform.position.y) <= -(range * 2))  {
                goingUp = false;
            }
            else if ((transform.position.y - tile.transform.position.y) >= 0) {
                goingUp = true;
            }

            if (goingUp) {
                tile.transform.position += new Vector3(0, speed * Time.deltaTime, 0);
            }
            if (!goingUp) {
                tile.transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
            }
        }
        if (dir == Direction_Tile.horizontal) {
            if ((transform.position.x - tile.transform.position.x) <= -(range * 2)) {
                goingUp = false;
            }
            else if ((transform.position.x - tile.transform.position.x) >= 0) {
                goingUp = true;
            }

            if (goingUp) {
                tile.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            }
            if (!goingUp) {
                tile.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            }
        }

        
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        if(dir == Direction_Tile.vertical) {
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, range * 2, 0));
        }
        if (dir == Direction_Tile.horizontal) {
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(range * 2, 0, 0));
        }

    }
}
