using UnityEngine;
using System.Collections;

public class Enemy_Buzz_Spawner : MonoBehaviour {

    public bool canShoot;
    public float bullSpeed;
    public float speed;
    public float current;
    public GameObject bullet;
    public GameObject point;

    // Update is called once per frame
    void Update () {

        if (FindObjectOfType<StartTimer>().intro == false && canShoot) {
            current += Time.deltaTime;
            if (current >= speed) {
                current = 0;
                GameObject bull = (GameObject)Instantiate(bullet, point.transform.position, Quaternion.identity);
                bull.GetComponent<Bullet_Buzz>().speed = bullSpeed;
            }
        }
	}
}
