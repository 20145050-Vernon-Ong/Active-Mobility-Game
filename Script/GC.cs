using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GC : MonoBehaviour
{
    private SimpleFlash sf;
    public GameObject Player;
    public GameObject Phone;
    private Vector3 pos;
    private Vector3 PhonePos;
    public TextMeshProUGUI ValueText;
    public TextMeshProUGUI summaryText;
    public TextMeshProUGUI learningPoints;
    public TextMeshProUGUI learningPoints2;
    public TextMeshProUGUI learningPoints3;
    public TextMeshProUGUI distanceCoveredText;

    public float totalpoints;
    private float _timeColliding;
    private readonly float timeThreshold = 2f;
    private bool isTouch;
    private int isGreen;
    private int isGreen2;
    private int isGreen3;
    //private float lifetotalpoints = 3;
    private PlayerMovement pm;
    private Animator animator;
    void Awake()
    {
        // Store currentscore in prefs
        isTouch = false;
        pos = Phone.transform.localPosition;
        sf = GetComponent<SimpleFlash>();
        pm = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        //lifeValueText.text = lifetotalpoints.ToString();
        ValueText.text = totalpoints.ToString();
        isGreen = 1;
        isGreen2 = 1;
        isGreen3 = 1;
    }

    void Update()
    {
        if (Phone.transform.localPosition.y == 17 && animator.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
        {
            Debug.Log("Player has moved while the mobilephone is active.");
            isGreen3 = 0;
            learningPoints3.color = new Color(255, 0, 0, 255);
        } else
        {
            Debug.Log("Player stop moving while the mobilephone is active.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)  
    {
        if (other.CompareTag("macetag") || other.CompareTag("maceTrafficTag"))
        {
            summaryText.text = "Be on the correct lane to avoid conflicts!";
            //lifetotalpoints --;
            HealthManager.health--;
            sf.Flash();
            PlayerPrefs.SetString("distance", pm.distanceLeft.ToString());
            if (other.CompareTag("maceTrafficTag"))
            {
                isGreen2 = 0;
                _timeColliding = 0f;
                learningPoints.color = new Color(255, 0, 0, 255);
                summaryText.text = "Be sure to cross only when the green man is flashing!";
            }
            //lifeValueText.text = lifetotalpoints.ToString();
            StartCoroutine(RestartCurrentlevel());
        }
        else if (other.CompareTag("coinTag"))
        {
            Destroy(other.gameObject);
            totalpoints++;
            ValueText.text = totalpoints.ToString();
        }
        else if (other.CompareTag("car"))
        {
            sf.Flash();
            isGreen = 0;
            isGreen2 = 0;
            HealthManager.health = 0;
            learningPoints.color = new Color(255, 0, 0, 255);
            learningPoints2.color = new Color(255, 0, 0, 255);
            summaryText.text = "Be sure to lookout for moving vehicles.";
            PlayerPrefs.SetString("distance", pm.distanceLeft.ToString("0"));
            StartCoroutine(RestartCurrentlevel());
        }
        else if (other.CompareTag("questPoint"))
        {
            isTouch = true;
            summaryText.text = "You have completed the game!";
            PlayerPrefs.SetString("distance", pm.totalDistance.ToString("0"));
            StartCoroutine(RestartCurrentlevel());
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("roadTag") || other.CompareTag("cyclingTag"))
        {
            if (_timeColliding < timeThreshold)
            {
                _timeColliding += Time.deltaTime;
            }
            else
            {
                sf.Flash();
                if (other.CompareTag("roadTag"))
                {
                    HealthManager.health = 0;
                }
                else if (other.CompareTag("cyclingTag"))
                {
                    //lifetotalpoints--;
                    HealthManager.health--;
                }
                //lifeValueText.text = lifetotalpoints.ToString();
                if (other.CompareTag("roadTag") || other.CompareTag("cyclingTag"))
                {
                    isGreen = 0;
                    summaryText.text = "Avoid walking across the wrong path!";
                    learningPoints.color = new Color(255, 0, 0, 255);
                }
                _timeColliding = 0f;
            }
            // Time is over theshold, player takes damag
            // Reset timer
            PlayerPrefs.SetString("distance", pm.distanceLeft.ToString("0"));
            StartCoroutine(RestartCurrentlevel());
        }
    }

    public IEnumerator RestartCurrentlevel()
    {
        if (HealthManager.health == 0 || isTouch)
        {
            yield return new WaitForSeconds(0.5f);
            PlayerPrefs.SetString("currentScore", ValueText.text);
            PlayerPrefs.SetString("summary", summaryText.text);
            PlayerPrefs.SetString("learningPoint1", learningPoints.text);
            PlayerPrefs.SetString("learningPoint2", learningPoints2.text);
            PlayerPrefs.SetString("learningPoint3", learningPoints3.text);
            PlayerPrefs.SetInt("tick1", isGreen);
            PlayerPrefs.SetInt("tick2", isGreen2);
            PlayerPrefs.SetInt("tick3", isGreen3);
            SceneManager.LoadScene("LoseScreen");
        }
    }

    public void OpenPhone()
    {
        if (Phone.transform.localPosition.y == 17)
        {
            Phone.transform.localPosition = new Vector3(pos.x, pos.y, 0);
        } else
        {
            Phone.transform.localPosition = new Vector3(pos.x, 17, 0);
        }  
    }
}
