using UnityEngine;
using System.Collections;

public class Bullet_Buzz : MonoBehaviour {

    public LayerMask layer;
    public bool isGrounded;
    public string dir;
    public float speed;

	// Use this for initialization
	void Start () {
        Vector3 playerPos = FindObjectOfType<Player>().gameObject.transform.position;
        Vector3 buzzPos = transform.position;
        if(playerPos.x > buzzPos.x) {
            dir = "right";
        }
        else {
            dir = "left";
        }

	}
	
	// Update is called once per frame
	void Update () {

        if (gameObject.GetComponent<SpriteRenderer>().isVisible == false) {
            Destroy(gameObject);
        }

        if (dir == "right" && isGrounded) {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        if (dir == "left" && isGrounded) {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 8) {
            isGrounded = true;
        }
    }

}
