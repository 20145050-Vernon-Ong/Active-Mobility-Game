using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GC2 : MonoBehaviour
{
    private SimpleFlash sf;
    public GameObject Player;
    public GameObject Phone;
    public Vector3 pos;
    private Vector3 PhonePos;
    public TextMeshProUGUI ValueText;
    public TextMeshProUGUI summaryText;
    public TextMeshProUGUI learningPoints;
    public TextMeshProUGUI learningPoints2;
    public TextMeshProUGUI learningPoints3;
    public TextMeshProUGUI distanceCoveredText;

    public TextMeshProUGUI pointsText; // Reference to the Text GameObject displaying points
    private int points;

    public int totalpoints;
    private float _timeColliding;
    private readonly float timeThreshold = 2f;
    private bool isTouch;
    private bool isPhone;
    private int isGreen;
    private int isGreen2;
    private int isGreen3;
    private int addPoints = 0;
    //private float lifetotalpoints = 3;
    private PlayerMovement pm;
    private HealthManager hm;
    private Animator animator;


    private bool isInDamageZone = false;
    private Vector3 initialPosition;
    public float maxDistanceBeforeDamage = 1f;


    void Awake()
    {
        // Store currentscore in prefs
        isTouch = false;
        sf = GetComponent<SimpleFlash>();
        pm = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        hm = FindObjectOfType<HealthManager>();
        if (Screen.width / Screen.height == 2)
        {
            Phone.transform.localPosition = new Vector3(718, -650, 0);
        }
        else if (Screen.width / Screen.height == 1)
        {
            Phone.transform.localPosition = new Vector3(718, -754, 0);
        }
        pos = Phone.transform.localPosition;
        //lifeValueText.text = lifetotalpoints.ToString();
        ValueText.text = totalpoints.ToString();
        isGreen = 1;
        isGreen2 = 1;
        isGreen3 = 1;
        points = PlayerPrefs.GetInt("Points", 0);
        UpdatePointsDisplay();
    }

    void Update()
    {
        if (isPhone == true && animator.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
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
        if (other.CompareTag("roadTag"))
        {
            // Player entered the damage zone, start tracking position
            isInDamageZone = true;
            initialPosition = transform.position;
            Debug.Log("damage" + isInDamageZone);
        }

        else if (other.CompareTag("macetag") || other.CompareTag("maceTrafficTag"))
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
                learningPoints2.color = new Color(255, 0, 0, 255);
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
            AddPoints(1);
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
            addPoints = totalpoints + hm.GetPoints();
            ValueText.text = addPoints.ToString();
            PlayerPrefs.SetString("distance", pm.differenceY.ToString("0"));
            StartCoroutine(RestartCurrentlevel());
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("roadTag") && isInDamageZone)
        {
            // Calculate the distance the player has moved inside the zone
            float distanceMoved = Vector3.Distance(initialPosition, transform.position);

            if (distanceMoved >= maxDistanceBeforeDamage)
            {
                // Player has moved the maximum allowed distance, lose all health
                HealthManager.health = 0;
                sf.Flash();

                // Stop tracking position to prevent further damage
                isInDamageZone = false;

                StartCoroutine(RestartCurrentlevel());
            }
        }

        else if (other.CompareTag("cyclingTag"))
        {
            if (_timeColliding < timeThreshold)
            {
                _timeColliding += Time.deltaTime;
            }
            else
            {
                sf.Flash();
                
                if (other.CompareTag("cyclingTag"))
                {
                    //lifetotalpoints--;
                    HealthManager.health--;
                }
                //lifeValueText.text = lifetotalpoints.ToString();
                if (other.CompareTag("cyclingTag"))
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
        if (Phone.transform.localPosition.y == -109)
        {
            isPhone = false;
            Phone.transform.localPosition = new Vector3(pos.x, pos.y, 0);
        } else
        {
            isPhone = true;
            Phone.transform.localPosition = new Vector3(pos.x, -109, 0);
        }  
    }

    public void SetTotalPoints(int newValue)
    {
        totalpoints = newValue;
        ValueText.text = totalpoints.ToString();
    }

    public void UpdatePointsDisplay()
    {
        // Update the Text component to display the current points
        pointsText.text = points.ToString();
        Debug.Log(points);
    }

    public void AddPoints(int amount)
    {
        // Add points to the current total
        points += amount;

        // Update the PlayerPrefs
        PlayerPrefs.SetInt("Points", points);
        PlayerPrefs.Save(); // Save PlayerPrefs to disk (optional but recommended)

        // Update the points display
        UpdatePointsDisplay();
    }




    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("roadTag"))
        {
            // Player entered the damage zone, start tracking position
            isInDamageZone = true;
            initialPosition = transform.position;
            Debug.Log("damage" + isInDamageZone);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("roadTag") && isInDamageZone)
        {
            // Calculate the distance the player has moved inside the zone
            float distanceMoved = Vector3.Distance(initialPosition, transform.position);

            if (distanceMoved >= maxDistanceBeforeDamage)
            {
                // Player has moved the maximum allowed distance, lose all health
                HealthManager.health = 0;

                // Stop tracking position to prevent further damage
                isInDamageZone = false;
            }
        }
    }*/

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("roadTag"))
        {
            // Player exited the damage zone, stop tracking position
            isInDamageZone = false;
            Debug.Log("damage" + isInDamageZone);
        }
    }


}
