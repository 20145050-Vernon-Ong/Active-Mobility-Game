using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GC : MonoBehaviour
{
    private SimpleFlash sf;
    public GameObject Player;
    public GameObject tick1;
    public GameObject tick2;
    public GameObject tick3;

    public TextMeshProUGUI ValueText;
    public TextMeshProUGUI lifeValueText;
    public TextMeshProUGUI summaryText;
    public TextMeshProUGUI learningPoints;
    public TextMeshProUGUI learningPoints2;
    public TextMeshProUGUI learningPoints3;

    private float totalpoints;
    private float _timeColliding;
    private readonly float timeThreshold = 2f;
    private bool isTouch;
    private int isGreen;
    private int isGreen2;
    private int isGreen3;
    private float lifetotalpoints = 3;
    private PlayerMovement pm;
    void Awake()
    {
        // Store currentscore in prefs
        isTouch = false;
        sf = GetComponent<SimpleFlash>();
        pm = GetComponent<PlayerMovement>();
        lifeValueText.text = lifetotalpoints.ToString();
        ValueText.text = totalpoints.ToString();
        PlayerPrefs.SetString("currentScore", "0");
        PlayerPrefs.SetString("summary", "");
        PlayerPrefs.SetString("learningPoint1", "");
        PlayerPrefs.SetString("learningPoint2", "");
        PlayerPrefs.SetInt("tick1", isGreen);
        PlayerPrefs.SetInt("tick2", isGreen2);
        PlayerPrefs.SetInt("tick3", isGreen3);
        learningPoints.color = new Color(0, 241, 17, 255);
        learningPoints3.color = new Color(0, 241, 17, 255);
        tick1.SetActive(true);
        tick3.SetActive(true);
        isGreen = 1;
        isGreen2 = 0;
        isGreen3 = 1;
    }

    void OnTriggerEnter2D(Collider2D other)  
    {

        if (other.CompareTag("macetag") || other.CompareTag("maceTrafficTag"))
        {
            summaryText.text = "Be on the correct lane to avoid conflicts!";
            lifetotalpoints --;
            lifeValueText.text = lifetotalpoints.ToString();
            sf.Flash();
            if (other.CompareTag("maceTrafficTag"))
            {
                _timeColliding = 0f;
                summaryText.text = "Be sure to cross only when the green man is flashing!";
            }
            StartCoroutine(RestartCurrentlevel());
        }
        else if (other.CompareTag("coinTag") || other.CompareTag("cointag"))
        {
            other.gameObject.SetActive(false);
            totalpoints++;
            ValueText.text = totalpoints.ToString();
        }
        else if (other.CompareTag("car"))
        {
            isTouch = true;
            summaryText.text = "Be sure to lookout for moving vehicles.";
            StartCoroutine(RestartCurrentlevel());
        }
        else if (other.CompareTag("questPoint"))
        {
            isTouch = true;
            summaryText.text = "You have completed the game!";
            StartCoroutine(RestartCurrentlevel());
        } else if (other.CompareTag("checkTag"))
        {
            learningPoints2.color = new Color(0, 241, 17, 255);
            tick2.SetActive(true);
            isGreen2 = 1;
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
                // Time is over theshold, player takes damage
                sf.Flash();
                if (other.CompareTag("roadTag"))
                {
                    lifetotalpoints = 0;
                } else if (other.CompareTag("cyclingTag"))
                {
                    lifetotalpoints--;
                }
                if (lifetotalpoints >= 0)
                {
                    if (other.CompareTag("roadTag") || other.CompareTag("cyclingTag"))
                    {
                        summaryText.text = "Avoid walking across the wrong path!";
                        learningPoints.color = new Color(255, 0, 0, 255);
                        tick1.SetActive(false);
                        isGreen = 0;
                    }
                    lifeValueText.text = lifetotalpoints.ToString();
                }
                // Reset timer
                _timeColliding = 0f;
            }

            StartCoroutine(RestartCurrentlevel());
        }
    }
    public IEnumerator RestartCurrentlevel()
    {
        if (lifetotalpoints == 0 || isTouch)
        {
            yield return new WaitForSeconds(1);
            PlayerPrefs.SetString("currentScore", ValueText.text);
            PlayerPrefs.SetString("summary", summaryText.text);
            PlayerPrefs.SetString("distance", pm.distanceText.text);
            PlayerPrefs.SetString("learningPoint1", learningPoints.text);
            PlayerPrefs.SetString("learningPoint2", learningPoints2.text);
            PlayerPrefs.SetString("learningPoint3", learningPoints3.text);
            PlayerPrefs.SetInt("tick1", isGreen);
            PlayerPrefs.SetInt("tick2", isGreen2);
            PlayerPrefs.SetInt("tick3", isGreen3);
            SceneManager.LoadScene("LoseScreen");
        }
    }

}
