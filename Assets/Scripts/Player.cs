using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour {
    
    [Header("Player Stats")]
	public float accelerationTimeAirborne = .2f;
	public float accelerationTimeGrounded = .1f;
    public float MoveSpeed = 4;
    public float SprintSpeed = 6;
	public float gravity = -50;
    public float minJumpVelocity = 5f;
    public float maxJumpVelocity = 20f;

    public bool gunAuto;
    public float gunSurvival;
    public float gunDamage;

    Vector3 velocity;
	float velocityXSmoothing;

    Vector2 input;

    public bool MoveFoward = false;
    public bool canMove = true;
    public bool canShoot = true;
    public bool invinsible = false;

    Controller2D controller;

    [Header("Audio Clips")]
    AudioSource sound;
    public AudioClip audioJump;
    public AudioClip audioEnd;
    public AudioClip audioCoin;
    public AudioClip audioEnemy;
    public AudioClip audioOver;
    public AudioClip audioCrouch;

    [Header("Rotator")]
    public GameObject rotor;
    public GameObject shield;
    public GameObject point;
    GameObject shild;
    public GameObject bullet;
    public float bul_speed;
    public float reloadTime;
    public float curTime;


    bool sound_OVER = false;
    bool sound_CROUCH = false;
    bool sound_COIN = false;
    bool sound_ENEMY = false;
    bool sound_END = false;

    GlobalVariables globals;
    GoalsScript goals;
    StartTimer startTimer;

    void Start() {

        //get required game objects
		controller = GetComponent<Controller2D> ();
        sound = GetComponent<AudioSource>();

        GameObject globalObj = GameObject.Find("Globals");
        globals = globalObj.GetComponent<GlobalVariables>();
        goals = globalObj.GetComponent<GoalsScript>();
        GameObject timerObj = GameObject.Find("Start Timer");
        startTimer = timerObj.GetComponent<StartTimer>();

        //set size of player
        transform.localScale = new Vector3(1, 1, 1);

    }

    //force recheck of character ray casting
    //NOTE:added this so when chrouches, rays changes with him
    void ReCheck() {
        controller.UpdateRaycastOrigins();
        controller.CalculateRaySpacing();
    }

    void Update() {
        
        if(transform.position.y <= -10) {
            Camera.main.transform.position = new Vector3(0, 0, -10);
        }

        //check if hitting block from above or below
        if (controller.collisions.above || controller.collisions.below) {
            velocity.y = 0;
        }

        //check if player is hitting 'end' block
        if (controller.collisions.end) {
            if (sound_END == false) {
                
                GameObject.Find("Main Camera").GetComponent<CameraController>().MoveToWorld();

                sound_END = true;
                canMove = false;
                MoveFoward = true;
                goals.Increment("end_level", 1);
                
                if(globals.StageLimit[globals.info.stage - 1] < globals.info.level + 1) {
                    globals.info.stage += 1;
                    globals.info.level = 1;
                }
                else {
                    globals.info.level += 1;
                }
                
                StartCoroutine(End());
            }
        }

        //check if player is hitting 'switcher' block
        if (controller.collisions.switcher) {
            //
        }

        //check if player is hitting instant kill
        if (controller.collisions.death) {
            if(globals.info.health > 0) {
                globals.info.health = 0;
                globals.Save_Player();
            }
        }

        //check if player is hitting 'coin' block
        if (controller.collisions.coin) {
            if (sound_COIN == false) {
                sound_COIN = true;
                globals.info.score += 100;
                globals.Save_Player();
                StartCoroutine(Coin());
            }
        }

        //check if player is hitting 'enemy' block
        if (controller.collisions.enemy && !invinsible) {
            GetHit();
        }

        //check number of health
        if(globals.info.health <= 0) {
            canMove = false;
            if (sound_OVER == false) {

                globals.info.lives -= 1;
                sound_OVER = true;

                if (globals.info.lives > 0) {
                    globals.info.health = 5;
                    startTimer.dead = true;
                    StartCoroutine(GameOverReset(2));
                }
                else{
                    globals.info.score = 0;
                    globals.info.lives = 0;
                    globals.info.health = 0;
                    startTimer.over = true;
                    StartCoroutine(GameOver());
                }


                
            }
            if(globals.info.lives <= 0) {
                
            }
            
        }


        //check if start timer is done counting
        if (startTimer.intro == false) {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            
            //check if nothing has paused movement
            if (canMove) {

                curTime -= Time.deltaTime;
                if(curTime <= 0) {
                    curTime = 0;
                }

                //deploy shield
                if (Input.GetMouseButtonDown(1)) {
                    shild = (GameObject)Instantiate(shield, rotor.transform.position, Quaternion.identity);
                    shild.transform.SetParent(rotor.transform);
                    shild.transform.rotation = rotor.transform.rotation;
                }
                if (Input.GetMouseButtonUp(1)) {
                    Destroy(shild);
                }

                //change rotation of gun
                Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
                Vector3 dir = (Input.mousePosition - pos).normalized;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                rotor.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                if (canShoot) {
                    if (!gunAuto) {
                        if (Input.GetMouseButtonDown(0)) {
                            if (curTime <= 0) {
                                curTime = reloadTime;
                                GameObject bul = (GameObject)Instantiate(bullet, point.transform.position + 2 * transform.forward, rotor.transform.rotation);
                                bul.GetComponent<Rigidbody2D>().AddForce(dir * bul_speed * 10);
                            }
                        }
                    }
                    else {
                        if (Input.GetMouseButton(0)) {
                            if (curTime <= 0) {
                                curTime = reloadTime;
                                GameObject bul = (GameObject)Instantiate(bullet, point.transform.position + 2 * transform.forward, rotor.transform.rotation);
                                bul.GetComponent<Rigidbody2D>().AddForce(dir * bul_speed * 10);
                            }
                        }
                    }
                }

                //initiate jump if player is on ground
                if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below) {
                    goals.Increment("jump", 1);
                    sound.PlayOneShot(audioJump, globals.op.option_sound);
                    velocity.y = maxJumpVelocity;
                }
                if (Input.GetKeyUp(KeyCode.Space) && !controller.collisions.below) {
                    if(velocity.y > minJumpVelocity) {
                        velocity.y = minJumpVelocity;
                    }
                }

                //crouch player if on ground
                if (Input.GetKey(KeyCode.DownArrow) && controller.collisions.below) {
                    //NOTE: disbaled crouching 
                    //transform.localScale = new Vector3(1, .5f, 1);
                    if (sound_CROUCH == false) {
                        sound_CROUCH = true;
                        //NOTE: disabled crouching
                        //StartCoroutine(Crouch());
                    }
                    ReCheck();
                }

                //returns to normal size when chrouch ends
                if (Input.GetKeyUp(KeyCode.DownArrow)) {
                    sound_CROUCH = false;
                    //NOTE: disabled crouching
                    //transform.Translate(new Vector3(0, .5f, 0));
                    transform.localScale = new Vector3(1, 1, 1);
                    ReCheck();
                }

                float targetVelocityX = input.x * MoveSpeed;
                if (Input.GetKey(KeyCode.LeftShift)) {
                    targetVelocityX = input.x * SprintSpeed;
                }
                velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
                velocity.y += gravity * Time.deltaTime;
                controller.Move(velocity * Time.deltaTime);
            }
            else {
                velocity.x = 0;
                velocity.y += gravity * Time.deltaTime;
                controller.Move(velocity * Time.deltaTime);
            }


            //forced movement forward
            if (MoveFoward) {
                velocity.x = MoveSpeed;
                controller.Move(velocity * Time.deltaTime);
            }

        }  
	}

    public void GetHit() {
        if (sound_OVER == false && sound_ENEMY == false) {
            StartCoroutine(Invinsible(1));
            goals.Increment("hit", 1);
            globals.info.health -= 1;
            globals.Save_Player();
            if (globals.info.health > 0) {
                StartCoroutine(Enemy());
            }
        }
    }

    //turn player invinsible for x seconds
    private IEnumerator Invinsible(float seconds) {
        invinsible = true;
        Color32 tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = 50;
        GetComponent<SpriteRenderer>().color = tmp;
        yield return new WaitForSeconds(seconds);
        tmp.a = 255;
        GetComponent<SpriteRenderer>().color = tmp;
        invinsible = false;
    }

    //play end music then move scene
    private IEnumerator End() {
        sound.PlayOneShot(audioEnd, globals.op.option_sound);
        yield return new WaitForSeconds(audioEnd.length);
        //SceneManager.LoadScene("_Scenes/_S_" + LEVEL);
        globals.NextLevel = "_S_GAME_" + globals.info.stage + "_" + globals.info.level;
        SceneManager.LoadScene("_Scenes/_S_LOADING");
    }

    //play coin music
    private IEnumerator Coin() {
        sound.PlayOneShot(audioCoin, globals.op.option_sound);
        yield return new WaitForSeconds(0);
        sound_COIN = false;
    }
    
    //play enemy music
    private IEnumerator Enemy() {
        sound.PlayOneShot(audioEnemy, globals.op.option_sound);
        yield return new WaitForSeconds(0);
        sound_ENEMY = false;
    }

    //play gameOver music
    private IEnumerator GameOver() {
        sound.PlayOneShot(audioOver, globals.op.option_sound);
        yield return new WaitForSeconds(0);
    }

    //play gameOver music
    private IEnumerator GameOverReset(float tim) {
        sound.PlayOneShot(audioOver, globals.op.option_sound);
        yield return new WaitForSeconds(tim);
        globals.NextLevel = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("_Scenes/_S_LOADING");
    }
    
    //play chrouch music
    private IEnumerator Crouch() {
        sound.PlayOneShot(audioCrouch, globals.op.option_sound);
        yield return new WaitForSeconds(0);
    }
}
