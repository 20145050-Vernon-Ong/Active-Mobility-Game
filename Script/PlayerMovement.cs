using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{

    private readonly string distanceLeftKey = "DistanceLeft";
    private float initialYPosition; // Store the initial Y position
    // Start is called before the first frame update
    public int differenceY;
    public float movespeed;
    public float speed;
    public float distanceLeft = 0;

    public bool canMove = true;
    public bool keyDisabled;

    public TextMeshProUGUI distanceText;

    /*private Touch touch;
    private Vector2 startTouch;*/
    public Vector3 change;

    public Transform pedestrian;
    public GameObject trafficCross;
    public GameObject pauseMenu;
    public GameObject endPoint;
    public GameObject DPad;

    // public FixedJoystick joystick;
    private Rigidbody2D myRigidBody;
    public Animator animator;
    public Canvas canvas;
    public List<GameObject> trafficCrossList = new();
    void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        trafficCrossList.Add(GameObject.FindWithTag("trafficBox"));
    }

    void Start()
    {
        change = Vector3.zero;
        if (PlayerPrefs.HasKey("InitialYPosition"))
        {
            initialYPosition = PlayerPrefs.GetFloat("InitialYPosition");
        }
        else
        {
            // If initial Y position doesn't exist in PlayerPrefs, set it to the current Y position
            initialYPosition = transform.position.y;
            // Save the initial Y position to PlayerPrefs so you can use it in the future
            PlayerPrefs.SetFloat("InitialYPosition", initialYPosition);
        }
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
    if (SystemInfo.operatingSystemFamily == operatingSystemFamily.Window))
    {
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        joystick.gameObject.SetActive(false);
        pauseBtn.gameObject.SetActive(false);
    } else if (SystemInfo.operatingSystemFamily == operatingSystemFamily.Other){
        change.x = new Vector2(joystick.Horizontal * movespeed, myRigidBody.velocity.x).x;
        change.y = new Vector2(joystick.Vertical * movespeed, myRigidBody.velocity.y).x;
        joystick.gameObject.SetActive(true);
        pauseBtn.gameObject.SetActive(true);
    }        
         UpdateAnimationAndMove();
    }*/
    void Update()
    {
        if (PlayerPrefs.GetInt("mobile") == 1)
        {
            speed = 3;
            DPad.SetActive(true);
        }
        else
        {
            ControlKey();
            DPad.SetActive(false);
        }
        if (PlayerPrefs.HasKey(distanceLeftKey))
        {
            distanceLeft = PlayerPrefs.GetFloat(distanceLeftKey);
            // Calculate the difference along the Y-axis between the initial Y position and the current Y position
            float currentYPosition = transform.position.y;
            differenceY = Mathf.CeilToInt(Mathf.Abs(currentYPosition - initialYPosition));
            // You can use 'differenceY' as an integer in your script
            // For example, you can print it to the console:
        }
        distanceLeft = Vector3.Distance(endPoint.transform.position, pedestrian.position);
        distanceText.text = distanceLeft.ToString("0") + " m";
        // Save distanceLeft in PlayerPrefs
        PlayerPrefs.SetFloat(distanceLeftKey, distanceLeft);
        UpdateAnimationAndMove();
    }

    void ControlKey()
    {
        if (keyDisabled)
        {
            change.y = Input.GetAxisRaw("Vertical");
            change.x = Input.GetAxisRaw("Horizontal");
            if (Input.GetKey(KeyCode.S))
            {
                change.y = 0;
            } else if (Input.GetKey(KeyCode.Space))
            {

            }
        } 
    }
    /*void TouchInput()
    {
        if (Input.touchCount == 1 && keyDisabled)
        {
            touch = Input.GetTouch(0);
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startTouch = touch.position;
                        break;
                    case TouchPhase.Stationary:
                        
                        if (startTouch.y < (Screen.height / 2) && startTouch.x < (Screen.width / 2))
                        {
                            change.y = 0;
                            change.x = -1;
                        }
                        else if (startTouch.y < (Screen.height / 2) && startTouch.x > (Screen.width / 2))
                        {
                            change.y = 0;
                            change.x = 1;
                        } 
                        else if (startTouch.y > (Screen.height / 2))
                        {
                            change.y = 1;
                            change.x = 0;
                        } else if (startTouch.y > (Screen.height / 2) && startTouch.x < (Screen.width / 2))
                        {
                            change.y = 1;
                            change.x = -1;
                        } else if (startTouch.y > (Screen.height / 2) && startTouch.x < (Screen.width / 2))
                        {
                            change.y = 1;
                            change.x = 1;
                        }
                        break;
                    case TouchPhase.Ended:
                        change.y = 0;
                        change.x = 0;
                        break;
                }
            } 
        }
    }*/

    public void GoUp()
    {
        if (keyDisabled)
        {
            change.y = 1;
            change.x = 0;
        }        
    }
    public void GoLeft()
    {
        if (keyDisabled)
        {
            change.y = 0;
            change.x = -1;
        }
    }

    public void GoRight()
    {
        if (keyDisabled)
        {
            change.y = 0;
            change.x = 1;
        }    
    }

    public void Stop()
    {
        change = Vector3.zero;
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

    void MoveCharacter()
    {
        myRigidBody.MovePosition(transform.position + speed * Time.deltaTime * change);
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

}