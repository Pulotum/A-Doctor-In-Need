using UnityEngine;
using System.Collections;

public class Aim_Enemy : MonoBehaviour {
    
    void OnCollisionEnter2D(Collision2D collider) {
        Debug.Log("enter:" + collider);
    }

    void OnCollisionExit2D(Collision2D collider) {
        Debug.Log("exit:" + collider);
    }

}
