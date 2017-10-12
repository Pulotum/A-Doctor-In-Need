using UnityEngine;
using System.Collections;

public class EnemyAnimator : MonoBehaviour {

    [Header("Animator Controller")]
    public Animator an;
    public bool jump;
    public bool walking;
    public bool hit;
    public bool dead;
    public bool attack;

    void Start() {
        an = GetComponent<Animator>();
    }

    void Update() {
        if (an != null) {
            if (jump) {
                Reset();
                StartCoroutine(anJump());
            }
            if (walking) {
                Reset();
                StartCoroutine(anWalking());
            }
            if (hit) {
                Reset();
                StartCoroutine(anHit());
            }
            if (dead) {
                Reset();
                StartCoroutine(anDead());
            }
            if (attack) {
                Reset();
                StartCoroutine(anAttack());
            }
        }
    }

    void Reset() {
        jump = false;
        walking = false;
        hit = false;
        dead = false;
        attack = false;
    }

    IEnumerator anJump() {
        an.SetBool("Jump", true);
        Debug.Log("Jump");
        yield return new WaitForSeconds(0.1f);
        an.SetBool("Jump", false);
    }
    IEnumerator anWalking() {
        an.SetBool("Walking", true);
        Debug.Log("Walking");
        yield return new WaitForSeconds(0.1f);
        an.SetBool("Walking", false);
    }
    IEnumerator anHit() {
        an.SetBool("Hit", true);
        Debug.Log("Hit");
        yield return new WaitForSeconds(0.1f);
        an.SetBool("Hit", false);
    }
    IEnumerator anAttack() {
        an.SetBool("Attack", true);
        Debug.Log("Attack");
        yield return new WaitForSeconds(0.1f);
        an.SetBool("Attack", false);
    }
    IEnumerator anDead() {
        an.SetBool("Dead", true);
        Debug.Log("Dead");
        yield return new WaitForSeconds(0.1f);
        an.SetBool("Dead", false);
    }

}
