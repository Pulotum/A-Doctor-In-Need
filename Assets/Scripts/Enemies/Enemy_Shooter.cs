using UnityEngine;
using System.Collections;

public class Enemy_Shooter : MonoBehaviour {

    public GameObject bullet;
    public GameObject rotor;
    public GameObject point;
    public GameObject aim;
    public GameObject shootingBar;

    public float range;
    public float shootSpeed;
    public float current;
    public float bulletSpeed;

    GameObject player;
    Vector3 playerPos;
    StartTimer startTimer;

	// Use this for initialization
	void Start () {
	    player = GameObject.Find("player");

        GameObject timerObj = GameObject.Find("Start Timer");
        startTimer = timerObj.GetComponent<StartTimer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (startTimer.intro == false) {
            playerPos = player.transform.position;
            float distance = Vector3.Distance(playerPos, transform.position);

            aim.transform.localScale = new Vector3(0.1f, distance, 0.1f);
            aim.transform.localPosition = new Vector3(0, distance / 2, 0);

            if(distance <= range) {

                Color32 tmp = aim.GetComponent<SpriteRenderer>().color;
                tmp.a = 25;
                aim.GetComponent<SpriteRenderer>().color = tmp;

                Vector3 dir = (playerPos - transform.position).normalized;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                rotor.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle - 90, Vector3.forward), 1f);

                if (current >= shootSpeed) {
                    current = 0;
                    GameObject bull = (GameObject)Instantiate(bullet, point.transform.position, Quaternion.identity);
                    bull.GetComponent<Rigidbody2D>().AddForce(dir * bulletSpeed * 10);
                }
                else {
                    current += Time.deltaTime;
                    shootingBar.transform.localScale = new Vector3(1, current/shootSpeed, 1);
                    shootingBar.transform.localPosition = new Vector3(0, -0.5f + ((current / shootSpeed)/2), -0.2f);
                }
            }
            else {

                current += Time.deltaTime;
                shootingBar.transform.localScale = new Vector3(1, current / shootSpeed, 1);
                shootingBar.transform.localPosition = new Vector3(0, -0.5f + ((current / shootSpeed) / 2), -0.2f);
                if (current >= shootSpeed) {
                    current = shootSpeed;
                }

                Color32 tmp = aim.GetComponent<SpriteRenderer>().color;
                tmp.a = 0;
                aim.GetComponent<SpriteRenderer>().color = tmp;

                rotor.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(0, Vector3.forward), 1f);
            }
        }
	}

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
