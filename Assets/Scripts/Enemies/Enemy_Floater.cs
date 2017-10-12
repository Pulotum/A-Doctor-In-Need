using UnityEngine;
using System.Collections;



public class Enemy_Floater : MonoBehaviour {

    public enum Direction_Bat {
        left, right
    }

    public float height;
    public float heightSpeed;
    public float speed;
    public Direction_Bat dir;
    bool goingUp;
    float curHeight;

    StartTimer startTimer;
    SpriteRenderer sprite;

    void Start() {
        GameObject timerObj = GameObject.Find("Start Timer");
        startTimer = timerObj.GetComponent<StartTimer>();
        sprite = GetComponent<SpriteRenderer>();


        if (Random.value >= 0.5) {
            goingUp = true;
        }
        else {
            goingUp = false;
        }

    }

    // Update is called once per frame
    void Update() {

        if(dir == Direction_Bat.left) {
            sprite.flipX = false;
        }
        else {
            sprite.flipX = true;
        }


        if (startTimer.intro == false) {

            if (curHeight > height) {
                goingUp = false;
            }
            if (curHeight < -height) {
                goingUp = true;
            }

            if (dir == Direction_Bat.left) {
                if (goingUp) {
                    curHeight += heightSpeed * Time.deltaTime;
                }
                else {
                    curHeight -= heightSpeed * Time.deltaTime;
                }
                transform.position -= new Vector3(speed * Time.deltaTime, curHeight, 0);
            }
            if (dir == Direction_Bat.right) {
                if (goingUp) {
                    curHeight += heightSpeed * Time.deltaTime;
                }
                else {
                    curHeight -= heightSpeed * Time.deltaTime;
                }
                transform.position += new Vector3(speed * Time.deltaTime, curHeight, 0);
            }
        }
    }
}
