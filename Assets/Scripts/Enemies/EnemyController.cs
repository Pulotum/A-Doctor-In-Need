using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public bool canBeKilled;
    public float healthTotal;
    public float healthCurrent;
    public GameObject bar;
    GlobalVariables globals;
	
    void Start() {
        healthCurrent = healthTotal;
        var tmp = bar.GetComponent<SpriteRenderer>().color;
        tmp.a = 0;
        bar.GetComponent<SpriteRenderer>().color = tmp;
        globals = FindObjectOfType<GlobalVariables>();
    }

	// Update is called once per frame
	void Update () {
        if (canBeKilled) {
            if (healthCurrent <= 0) {
                globals.info.score += 100;
                Destroy(gameObject, 1f);
            }

            var tmp = healthCurrent / healthTotal;
            bar.transform.localScale = new Vector3(tmp, 0.15f, 1);
        }
    }

    public void DoDamage(float dam) {
        if (canBeKilled) {
            StartCoroutine(display());
        }
        healthCurrent -= dam;
    }

    IEnumerator display() {
        var tmp = bar.GetComponent<SpriteRenderer>().color;
        tmp.a = 255;
        bar.GetComponent<SpriteRenderer>().color = tmp;
        yield return new WaitForSeconds(2);
        tmp.a = 0;
        bar.GetComponent<SpriteRenderer>().color = tmp;
    }

}
