using UnityEngine;
using System.Collections;

public class SwitchScripter : MonoBehaviour {

    [Header("Choice CutScene")]
    public int choice;

    [Header("Choice 1")]
    public float time1;
    public float time2;

    [Header("Default Values")]
    public GameObject target;
    public bool playing = false;
    public bool done = false;
    public float current;
    

    //main code
    void Update() {
        if (playing) {
            /*  ********** ********** **********
             * Choice 1
             * -description
             *  ********** ********** **********
             */
            if (choice == 0) {
                done = false;
            }
            /*  ********** ********** **********
            * Choice 1
            * -description
            *  ********** ********** **********
            */
                if (choice == 1) {
                    current += Time.deltaTime;

                if (current <= time1) {
                    return;
                }

                if (current < time1 + time2) {
                    target.transform.position += new Vector3(Time.deltaTime *2, 0, 0);
                    return;    
                }

                //end code
                done = true;
            }
            /* ********** ********** **********
             * Else
             * ********** ********** **********
             */
            else {
                done = true;
            }
        }
    }
}
