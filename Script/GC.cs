using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GC : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;
    private SimpleFlash sf;
    public GameObject Player;

    public TextMeshProUGUI ValueText;
    public TextMeshProUGUI lifeValueText;
    public TextMeshProUGUI summaryText;
    public TextMeshProUGUI learningPoints;
    public TextMeshProUGUI learningPoints2;

    public int totalpoints;
    private bool isTouch;
    public int lifetotalpoints = 3;
    private PlayerMovement pm;
    void Awake()
    {
        // Store currentscore in prefs
        isTouch = false;
        lifeValueText.text = lifetotalpoints.ToString();
        ValueText.text = totalpoints.ToString();
        PlayerPrefs.SetString("currentScore", "0");
        PlayerPrefs.SetString("summary", "");
        PlayerPrefs.SetString("learningPoint1", "");
        PlayerPrefs.SetString("learningPoint2", "");
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        sf = GetComponent<SimpleFlash>();
        pm = GetComponent<PlayerMovement>();
    }

    private void Update()
    {

    }

 
    void OnTriggerEnter2D(Collider2D other)  
    {

        if (other.CompareTag("macetag") || other.CompareTag("maceTrafficTag"))
        {
            lifetotalpoints--;
            lifeValueText.text = lifetotalpoints.ToString();
            sf.Flash();
            if (other.CompareTag("macetag"))
            {
                summaryText.text = "Be on the correct lane to avoid conflicts!";
            }
            else if (other.CompareTag("maceTrafficTag"))
            {
                summaryText.text = "Be sure to cross only when the green man is flashing!";
            }
            StartCoroutine(RestartCurrentlevel());
        }
        else if (other.CompareTag("coinTag") || other.CompareTag("cointag"))
        {
            other.gameObject.SetActive(false);
            totalpoints += 1 ;
            ValueText.text = totalpoints.ToString();
        }
        else if (other.CompareTag("car"))
        {
            isTouch = true;
            summaryText.text = "Be sure to lookout for moving vehicles.";
            StartCoroutine(RestartCurrentlevel());
        } else if (other.CompareTag("questPoint"))
        {
            isTouch = true;
            summaryText.text = "You have completed the game!";
            StartCoroutine(RestartCurrentlevel());
        }
    }

    private IEnumerator CycleLane()
    {
        yield return new WaitForSeconds(3);
        lifetotalpoints -= 1;
    }

    public IEnumerator RestartCurrentlevel()
    {
        if (lifetotalpoints <= 0 || isTouch)
        {
            yield return new WaitForSeconds(1);
            PlayerPrefs.SetString("currentScore", ValueText.text);
            PlayerPrefs.SetString("summary", summaryText.text);
            PlayerPrefs.SetString("distance", pm.distanceText.text);
            PlayerPrefs.SetString("learningPoint1", learningPoints.text);
            PlayerPrefs.SetString("learningPoint2", learningPoints2.text);
            PlayerPrefs.SetString("learningPoint3", learningPoints2.text);
            SceneManager.LoadScene("LoseScreen");
        }
    }

}
