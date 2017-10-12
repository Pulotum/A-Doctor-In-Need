using UnityEngine;
using System.Collections;

public class Enemy_Ball : MonoBehaviour {

    public int range;
    public float angle;
    public float speed;
    public GameObject rotor;
    public GameObject rope;
    public GameObject ball;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        rope.transform.localScale = new Vector3(0.2f, range, 1);
        rope.transform.localPosition = new Vector3(0, range / 2, 0.39f);
        ball.transform.localPosition = new Vector3(0, range, 0.39f);


        angle -= Time.deltaTime * speed;
        if(Mathf.Abs(angle) >= 360) {
            angle = 0;
        }
        

        rotor.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


	}

    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }


}
