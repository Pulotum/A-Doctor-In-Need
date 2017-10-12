using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    public float damage;

    [Header("Tags")]
    public string enemyTag;
    public string tileTag;

    GlobalVariables globals;

    void Start() {
        damage = FindObjectOfType<Player>().gunDamage;
        globals = FindObjectOfType<GlobalVariables>();
    }

    void Update() {
        if (gameObject.GetComponent<SpriteRenderer>().isVisible == false) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == enemyTag) {

            if (collision.gameObject.GetComponent<EnemyController>()) {
                collision.gameObject.GetComponent<EnemyController>().DoDamage(damage);
            }
            else {
                Destroy(collision.gameObject);
            }
            
            globals.info.score += 10;
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Player") {
            //
        }

        //catch all to destroy
        else {
            Destroy(gameObject);
        }
    }
}
