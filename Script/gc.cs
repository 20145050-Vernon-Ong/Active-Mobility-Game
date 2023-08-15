using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gc : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;
    private SimpleFlash sf;
    public GameObject Player;

    public TextMeshProUGUI ValueText;
    public TextMeshProUGUI lifeValueText;
    public TextMeshProUGUI summaryText;

    public int totalpoints;
    private bool isTouch;
    int lifetotalpoints = 3;
    void Awake()
    {
        // Store currentscore in prefs
        isTouch = false;
        lifeValueText.text = lifetotalpoints.ToString();
        ValueText.text = totalpoints.ToString();
        PlayerPrefs.SetString("currentScore", "0");
        PlayerPrefs.SetString("summary", "");
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        sf = GetComponent<SimpleFlash>();
    }

    void Update()
    {
        StartCoroutine(RestartCurrentlevel());
    }

    void OnTriggerEnter2D(Collider2D other)  
    {

        if (other.CompareTag("macetag") || other.CompareTag("maceTrafficTag"))
        {
            lifetotalpoints -= 1;
            lifeValueText.text = lifetotalpoints.ToString();
            sf.Flash();
            if (other.CompareTag("macetag"))
            {
                summaryText.text = "Be on the correct lane to avoid conflicts!";
                Debug.Log("BOMB!");

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
            StartCoroutine(RestartCurrentlevel());
        }
    }

    public IEnumerator RestartCurrentlevel()
    {
        //|| isTouch
        if (lifetotalpoints <= 0 || isTouch)
            {
                yield return new WaitForSeconds(0.01f);
                PlayerPrefs.SetString("currentScore", ValueText.text);
                PlayerPrefs.SetString("summary", summaryText.text);
                SceneManager.LoadScene("LoseScreen");
        }
    }

}
