using UnityEngine;
using System.Collections;

public class SwitchController : MonoBehaviour {

    [Header("Switch Type")]
    public bool timer;
    public bool canMove;
    public bool toTarget;
    public bool toWorld;
    public bool toPlayer;


    [Header("If Timer")]
    public bool destroyAfter;
    public float time;
    public float current;

    [Header("If Target")]
    public Transform Target;
    public bool hasFocus;

    CameraController camController;
    Player player;

	// Use this for initialization
	void Start () {
        camController = FindObjectOfType<CameraController>();
        player = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        if (hasFocus) {

            if (!canMove) {
                player.canMove = false;
            }
            
            if(camController.lockTo != "" && toTarget) {
                camController.Target = Target;
                camController.MoveToTarget();
            }
            else if (camController.lockTo != "" && toPlayer) {
                camController.MoveToPlayer();
            }
            else if (camController.lockTo != "" && toWorld) {
                camController.MoveToWorld();
            }


            if (timer) {
                current += Time.deltaTime;
                if(current >= time) {
                    camController.MoveToPlayer();
                    player.canMove = true;
                    if (destroyAfter) {
                        Destroy(gameObject, 1f);
                        Destroy(Target.gameObject, 1f);
                    }

                }                
            }

        }
	}

    void OnDrawGizmosSelected() {
        if (toTarget) {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, Target.position);
        }
    }
}
