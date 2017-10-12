using UnityEngine;
using System.Collections;

public class Bullet_Enemy : MonoBehaviour {

    string playerTag = "Player";
    string shieldTag = "shield";

    // Use this for initialization
    void Start () {
    }

    void Update() {
        if (!gameObject.GetComponent<Renderer>().isVisible) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == playerTag) {
            FindObjectOfType<Player>().GetHit();
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == shieldTag) {
            Destroy(gameObject);
        }

        //catch all to destroy
        else {
            Destroy(gameObject);
        }
    }
}
