using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using SystemInfo = UnityEngine.Device.SystemInfo;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float movespeed;
    public float speed;
    private float totalDistance = 0;

    public bool canMove = true;
    public bool keyDisabled;

    public TextMeshProUGUI distanceText;

    private Touch touch;

    private Vector2 startTouch;
    private Vector3 startPosition;
    public Vector3 change;

    public GameObject pedestrian;
    public GameObject trafficCross;
    public GameObject zebraCross;
    public GameObject zebraCross2;
    public GameObject pauseMenu;

    public FixedJoystick joystick;
    private Rigidbody2D myRigidBody;
    [SerializeField]
    private Animator animator;
    private Readxml rl;
    void Awake()
    {
        Debug.Log(SystemInfo.operatingSystemFamily);
        Debug.Log(Screen.height / 2);
        startPosition = pedestrian.transform.position;
        Debug.Log(Input.touchSupported);
        GetComponent<Collider2D>().isTrigger = true;
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        rl = GetComponent<Readxml>();
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
        totalDistance += Vector3.Distance(pedestrian.transform.localPosition, startPosition);
        startPosition = pedestrian.transform.localPosition;
        distanceText.text = totalDistance.ToString("0") + " Metre";
        if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Other)
        {
            TouchInput();
        }
        else if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Windows)
        {
            ControlKey();
            rl.panelLeft.SetActive(false);
            rl.panelRight.SetActive(false);
            rl.middleline.SetActive(false);
            rl.panelTop.SetActive(false);
            rl.middleline2.SetActive(false);
        }
        UpdateAnimationAndMove();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("trafficBox"))
        {
            trafficCross.SetActive(true);
            zebraCross.SetActive(true);
            zebraCross2.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("trafficBox"))
        {
            zebraCross.SetActive(false);
            zebraCross2.SetActive(false);
        }
    }
    void ControlKey()
    {
        if (keyDisabled)
        {
            if (Input.GetKey(KeyCode.A))
            {
                change.y = 0;
                change.x = -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                change.y = 0;
                change.x = 1;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                change.y = 1;
                change.x = 0;
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                canMove = false;
                change = Vector3.zero;
            }
        } 
    }
    void TouchInput()
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
                        Debug.Log(startTouch);
                        StartCoroutine(DoubleTap());
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
                        }
                        break;
                    case TouchPhase.Ended:
                        break;
                }
            } 
        }
    }

    private IEnumerator DoubleTap()
    {
        yield return new WaitForSeconds(0.1f);
        if (touch.tapCount == 2)
        {
            change = Vector3.zero;
        } 
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


