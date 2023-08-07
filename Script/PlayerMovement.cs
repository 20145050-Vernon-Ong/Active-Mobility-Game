using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.IO;
using System.Collections;
using System;
using NUnit.Framework;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    protected string text = " ";
    string word;
    string[] textList;

    public float movespeed;
    public float speed;
    private float totalDistance = 0;
    readonly private float time = 0.001f;
    private float left;
    private float right;
    private float playerPos;

    public bool canMove = true;
    private bool isTutor;
    private bool record = true;

    public TMP_Text okayBtn;
    public TMP_Text distanceText;
    public TMP_Text sqlText;

    private Vector2 startTouch;
    private Vector2 endTouch;
    private Vector3 startPosition;
    private Vector3 previousLoc;
    private Vector3 change;
    
    public GameObject amUser;
    public GameObject pedestrian;
    public GameObject resumeBtn;
    public GameObject trafficCross;
    public GameObject tutorialpopup1;
    public GameObject tutorialInterface;
    public GameObject cycleLane;
    public GameObject footLane;
    public GameObject roadLane;

    public Button pauseBtn;

    public FixedJoystick joystick;
    private Rigidbody2D myRigidBody;
    private Animator animator;
    protected FileInfo source = null;
    protected StreamReader reader = null;
    [Obsolete]
    void Awake()
    {
        left = cycleLane.transform.localPosition.x;
        right = roadLane.transform.localPosition.x;

    }

    void Start()
    {
        startPosition = pedestrian.transform.position;
        Debug.Log(SystemInfo.deviceType);
        Debug.Log(Input.touchSupported);
        GetComponent<Collider2D>().isTrigger = true;
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        amUser.SetActive(false);
        resumeBtn.SetActive(false);
        isTutor = true;
        change.y = 1;
    }
    // Update is called once per frame 
    /*void Update()
    {
        if (!canMove)
        {
            myRigidBody.velocity = Vector2.zero;
            return;
        }
    change = Vector3.zero;
    if (Screen.width == 1920 || Screen.width == 2560 || Screen.width == 3840 && !(Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight))
    {
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        joystick.gameObject.SetActive(false);
        pauseBtn.gameObject.SetActive(false);
    } else if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight){
        change.x = new Vector2(joystick.Horizontal * movespeed, myRigidBody.velocity.x).x;
        change.y = new Vector2(joystick.Vertical * movespeed, myRigidBody.velocity.y).x;
        joystick.gameObject.SetActive(true);
        pauseBtn.gameObject.SetActive(true);
    }        
    if (!resumeBtn.activeInHierarchy && !EventSystem.current.IsPointerOverGameObject())
    {
        if (GameObject.FindWithTag("Player"))
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startTouch = touch.position;
                        break;
                    case TouchPhase.Moved:
                        if (startTouch.x < 1300)
                        {
                            change.x = -1;
                        }
                        else if (startTouch.x > 1300)
                        {
                            change.x = 1;
                        }
                        break;
                    case TouchPhase.Ended:
                        if (endTouch.x < startTouch.x)
                        {

                            Invoke(nameof(MoveLeftRight), time);
                        }
                        else if (endTouch.x > startTouch.x)
                        {

                            Invoke(nameof(MoveLeftRight), time);
                        }
                        break;
                }

            }

            if (Input.GetKey(KeyCode.Space))
            {
                Time.timeScale = 0;
                canMove = false;
            } else if (canMove == false)
            {
                canMove = true;
                Time.timeScale = 1;
            }
            if (tutorialpopup1.IsDestroyed())
            {
                change.y = 1;
            }
            change.x = Input.GetAxisRaw("Horizontal");
        }



        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouch = Input.GetTouch(0).position;
            Debug.Log(startTouch);
            if (startTouch.x < 1300 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                change.x = -1;
            }
            else if (startTouch.x > 1300 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                change.x = 1;
            }
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouch = Input.GetTouch(0).position;
            if (endTouch.x < startTouch.x)
            {

                Invoke(nameof(MoveLeftRight), time);
            }
            else if (endTouch.x > startTouch.x)
            {

                Invoke(nameof(MoveLeftRight), time);
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {

            change.x = -1;
            Invoke(nameof(MoveLeftRight), time);

        }
        else if (Input.GetKey(KeyCode.D))
        {

            change.x = 1;
            Invoke(nameof(MoveLeftRight), time);
        }
    }
         UpdateAnimationAndMove();
    }*/
    void Update()
    {
        playerPos = pedestrian.transform.localPosition.y;
        if (!resumeBtn.activeInHierarchy && !EventSystem.current.IsPointerOverGameObject())
        {
            if (GameObject.FindWithTag("Player") && record)
            {
                RecordDistance();
                //change.x = Input.GetAxisRaw("Horizontal");
                //change.y = Input.GetAxisRaw("Vertical");
                if (Input.GetKey(KeyCode.Space))
                {
                    Time.timeScale = 0;
                    canMove = false;
                }
                else if (canMove == false)
                {
                    canMove = true;
                    Time.timeScale = 1;
                }
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            startTouch = touch.position;
                            break;
                        case TouchPhase.Moved:
                            if (startTouch.x < 1300)
                            {
                                change.x = -1;
                            }
                            else if (startTouch.x > 1300)
                            {
                                change.x = 1;
                            }
                            break;
                        case TouchPhase.Ended:
                            if (endTouch.x < startTouch.x)
                            {

                                Invoke(nameof(MoveLeftRight), time);
                            }
                            else if (endTouch.x > startTouch.x)
                            {

                                Invoke(nameof(MoveLeftRight), time);
                            }
                            break;
                    }

                }

            }
        }
        UpdateAnimationAndMove();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("trafficBox"))
        {
            trafficCross.SetActive(true);       
        } 

        if (collision.CompareTag("playertag"))
        {

            tutorialpopup1.SetActive(true);
            Time.timeScale = 0;
        }

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("footTag"))
        {
            if (Input.GetKey(KeyCode.A))
            {
                pedestrian.transform.localPosition = new Vector3(cycleLane.transform.localPosition.x, playerPos, 0);
            } else if (Input.GetKey(KeyCode.D)) 
            {
                pedestrian.transform.localPosition = new Vector3(roadLane.transform.localPosition.x, playerPos, 0);
            }
        } else if(collision.CompareTag("cyclingTag"))
        {
            if (Input.GetKey(KeyCode.D))
            {
                pedestrian.transform.localPosition = new Vector3(footLane.transform.localPosition.x, playerPos, 0);
            }
        } else if (collision.CompareTag("RoadTag"))
        {
            if (Input.GetKey(KeyCode.A))
            {
                pedestrian.transform.localPosition = new Vector3(footLane.transform.localPosition.x, playerPos, 0);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("trafficBox"))
        {
            trafficCross.SetActive(false);
        }
    }

    void RecordDistance()
    {
        totalDistance += Vector3.Distance(pedestrian.transform.position, previousLoc);
        previousLoc = transform.position;
        distanceText.text = "Distance " + previousLoc.ToString();
    }

    void ToggleRecord() => record = !record;

    public void CloseTutorial()
    {

        for (int i = 0; i < textList.Length; i++)
        {
            
        }
        
    }
    void MoveLeftRight()
    {
        change.x = 0;
    }

    public void StopMove()
    {
        change = Vector3.zero;
        change.x = 0;
        change.y = 0;
        resumeBtn.SetActive(true);
    }

    public void ResumeMove()
    {
        change = Vector3.up;
        change.x = 0;
        resumeBtn.SetActive(false);
    }
    void UpdateAnimationAndMove()
    {
        
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    public void StopWalking() {
        animator.SetBool("moving", false);
    }

    void MoveCharacter()
    {
        myRigidBody.MovePosition(transform.position + speed * Time.deltaTime * change);
    }

    public void TrafficStopLook()
    {
        canMove = true;
    }

    public void TrafficProceed()
    {
        canMove = true;
    }


}

