using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GC : MonoBehaviour
{

    // public GameObject panel;
    // public GameObject gover;
    public RectTransform Phone;

    // private Vector2 initialPosition;

    public int totalpoints;
    public float maxDistanceBeforeDamage = 1f;

    // public GameObject Phone;

    public TextMeshProUGUI ValueText;
    public TextMeshProUGUI summaryText;
    public TextMeshProUGUI learningPoints;
    public TextMeshProUGUI learningPoints2;
    public TextMeshProUGUI learningPoints3;

    public GameObject check1;
    public GameObject check2; 
    public GameObject check3;
    public GameObject phoneinfo;

    public bool isPhone;

    public Image imageToChangeColor;
    public Image imageToChangeColor2;
    public Image imageToChangeColor3;
    public Color newColor = Color.red;

    private SimpleFlash sf;
    private Vector3 pos;
    private Vector3 initialPosition;
    private bool isInDamageZone = false;
    private bool hasHealthDecremented = false;
    private bool isTouch;
    private float _timeColliding;
    private readonly float timeThreshold = 2f;
    private int points;
    private int isGreen;
    private int isGreen2;
    private int isGreen3;
    private int addPoints = 0;


    // private int additionalPoints = 0;
    private PlayerMovement pm;
    private HealthManager hm;
    private Animator animator;
    // public gcs_menu gcmenu;
    void Awake()
    {
        // Store currentscore in prefs
        sf = GetComponent<SimpleFlash>();
        pm = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        hm = FindObjectOfType<HealthManager>();
    }

    void Start()
    {
        initialPosition = Phone.anchoredPosition;
        isTouch = false;
        pos = Phone.transform.localPosition;
        ValueText.text = totalpoints.ToString();
        isGreen = 1;
        isGreen2 = 1;
        isGreen3 = 1;
        points = PlayerPrefs.GetInt("Points", 0);
        UpdatePointsDisplay();
    }

    void Update()
    {

            // if (check1.activeSelf)
            // {
            //     Debug.Log("check 1 is active");
            // }
            //  if (check2.activeSelf)
            // {
            //     Debug.Log("check 2 is active");
            // }
            //  if (check3.activeSelf)
            // {
            //     Debug.Log("check 3 is active");
            // }

        if (isPhone == true && animator.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
        {
            if (!hasHealthDecremented)
            {
                // summaryText.text = "Do not use your phone when walking";
                isGreen3 = 0;
                check3.SetActive(false);
                // learningPoints3.color = new Color(255, 0, 0, 255);
                if (HealthManager.health > 0)
                {
                    // gcmenu.PlayHurt();
                    HealthManager.health -= 1;
                    sf.Flash();
                    imageToChangeColor3.color = newColor;
                }
                hasHealthDecremented = true;
            }
        }
        else
        {
            hasHealthDecremented = false; // Reset the flag when the conditions are not met
        }

        StartCoroutine(RestartCurrentlevel());
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

        if (other.CompareTag("macetag") || other.CompareTag("maceTrafficTag"))
        {
            // summaryText.text = "Be on the correct lane to avoid conflicts!";
            HealthManager.health--;
            // gcmenu.PlayHurt();
            sf.Flash();
            PlayerPrefs.SetString("distance", pm.distanceLeft.ToString());
            if (other.CompareTag("maceTrafficTag"))
            {
                isGreen2 = 0;
                check2.SetActive(false);
                _timeColliding = 0f;
                // learningPoints2.color = new Color(255, 0, 0, 255);
                imageToChangeColor2.color = newColor;

                // if (HealthManager.health <= 0)
                // {
                //     summaryText.text = "Stop and look out for traffic before crossing";
                // }
            }
            StartCoroutine(RestartCurrentlevel());
        }
        else if (other.CompareTag("coinTag"))
        {
            Destroy(other.gameObject);
            // gcmenu.PlayCoin();
            totalpoints++;
            ValueText.text = totalpoints.ToString();
            AddPoints(1);
            // Debug.Log("hello my name"+totalpoints);
        }
        else if (other.CompareTag("car"))
        {
            sf.Flash();
            isGreen = 0;
            isGreen2 = 0;
            check2.SetActive(false);
            check1.SetActive(false);
            // learningPoints.color = new Color(255, 0, 0, 255);
            // learningPoints2.color = new Color(255, 0, 0, 255);

            imageToChangeColor.color = newColor;
            imageToChangeColor2.color = newColor;

            HealthManager.health = 0;
            // gcmenu.PlayHurt();
            summaryText.text = "Be sure to lookout for moving vehicles";
            PlayerPrefs.SetString("distance", pm.distanceLeft.ToString("0"));
            //StartCoroutine(RestartCurrentlevel());
        }
        else if (other.CompareTag("questPoint"))
        {
            isTouch = true;

            // gcmenu.PlayFinish();

            summaryText.text = "You have completed the game!";
            //addPoints = totalpoints + hm.GetPoints();
            //ValueText.text = addPoints.ToString();
            //PlayerPrefs.SetString("distance", pm.differenceY.ToString("0"));
            //AddPoints(HealthManager.points);


            

            if (check1.activeSelf)
            {
                totalpoints += 10;
                AddPoints(10);
            }
            if (check2.activeSelf)
            {
                totalpoints += 10;
                AddPoints(10);
            }
            if (check3.activeSelf)
            {
                totalpoints += 10;
                AddPoints(10);
            }



            if (HealthManager.health == 3)
            {
                totalpoints += 15;
                AddPoints(15);
            }
            else if (HealthManager.health == 2)
            {
                totalpoints += 10;
                AddPoints(10);
            }
            else if (HealthManager.health == 1)
            {
                totalpoints += 5;
                AddPoints(5);
            }



            Debug.Log("this is your health :" + HealthManager.health);

            // Update the UI text

            ValueText.text = totalpoints.ToString();


            //StartCoroutine(RestartCurrentlevel());
        }
        else if (other.CompareTag("gemTag"))
        {
            // Add points to the totalpoints variable
            Destroy(other.gameObject);
            // gcmenu.PlayGem();
            AddPoints(10);
            // Update the UI text
            totalpoints += 10;
            ValueText.text = totalpoints.ToString();
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
                summaryText.text = "Do not walk on the roads";
                isGreen = 0;
                check2.SetActive(false);

                // learningPoints.color = new Color(255, 0, 0, 255);
                imageToChangeColor.color = newColor;

                // Stop tracking position to prevent further damage
                isInDamageZone = false;
                //StartCoroutine(RestartCurrentlevel());
            }
        }

        if (other.CompareTag("cyclingTag"))
        {
            if (_timeColliding < timeThreshold)
            {
                _timeColliding += Time.deltaTime;
            }
            else
            {
                sf.Flash();
                HealthManager.health--;
                // gcmenu.PlayHurt();
                isGreen = 0;
                check1.SetActive(false);
                summaryText.text = "Do not walk on cycling paths";

                // learningPoints.color = new Color(255, 0, 0, 255);
                imageToChangeColor.color = newColor;

                _timeColliding = 0f;
            }
            // Time is over theshold, player takes damag
            // Reset timer
            PlayerPrefs.SetString("distance", pm.distanceLeft.ToString("0"));
            //StartCoroutine(RestartCurrentlevel());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("roadTag"))
        {
            // Player exited the damage zone, stop tracking position
            isInDamageZone = false;
            Debug.Log("damage" + isInDamageZone);
        }
    }

    IEnumerator RestartCurrentlevel()
    {
        if (HealthManager.health == 0 || isTouch)
        {
            
            if (HealthManager.health == 0) 
            {
                // gcmenu.PlayHurt();
                // panel.SetActive(true);
                // gover.SetActive(true);
                PlayerPrefs.SetInt("enableImageInLoseScene", 0);
            }
            else
            {
                PlayerPrefs.SetInt("enableImageInLoseScene", 1);
                yield return new WaitForSeconds(3f);
            }

            
            PlayerPrefs.SetString("currentScore", ValueText.text);
            PlayerPrefs.SetString("summary", summaryText.text);
            PlayerPrefs.SetString("learningPoint1", learningPoints.text);
            PlayerPrefs.SetString("learningPoint2", learningPoints2.text);
            PlayerPrefs.SetString("learningPoint3", learningPoints3.text);
            PlayerPrefs.SetInt("tick1", isGreen);
            PlayerPrefs.SetInt("tick2", isGreen2);
            PlayerPrefs.SetInt("tick3", isGreen3);
            AsyncOperation operation = SceneManager.LoadSceneAsync("LoseScreen");
            while (!operation.isDone)
            {
                yield return null;
            }
        }
    }

    // public void OpenPhone()
    // {
    //     if (Phone.transform.localPosition.y == 280)
    //     {
    //         isPhone = false;
    //         Phone.transform.localPosition = new Vector3(pos.x, pos.y, 0);
    //     } else if (Phone.transform.localPosition.y == pos.y)
    //     {
    //         isPhone = true;
    //         Phone.transform.localPosition = new Vector3(pos.x, 280, 0);
    //     }  
    // }
    

    public void OpenPhone()
    {
        if (Phone.anchoredPosition.y == 700f)
        {
            // Close the phone
            isPhone = false;
            phoneinfo.SetActive(false);
            Phone.anchoredPosition = initialPosition;
        }
        else
        {
            // Open the phone
            isPhone = true;
            phoneinfo.SetActive(true);
            Phone.anchoredPosition = new Vector2(initialPosition.x, 700f);
        }
    }

    public void UpdatePointsDisplay()
    {
        Debug.Log(points);
    }

    public void AddPoints(int amount)
    {
        points += amount;

        PlayerPrefs.SetInt("Points", points);
        PlayerPrefs.Save();
        UpdatePointsDisplay();
    }

    public void SetTotalPoints(int newValue)
    {
        totalpoints = newValue;
        ValueText.text = totalpoints.ToString();
    }



}