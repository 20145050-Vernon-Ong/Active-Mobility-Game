using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class gc : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;
    private SimpleFlash sf;
    public GameObject Player;
    public GameObject bomb;

    public TextMeshProUGUI ValueText;
    public TextMeshProUGUI lifeValueText;

    public int totalpoints;
    int lifetotalpoints = 3;

    void Start()
    {
        // Store currentscore in prefs
        lifeValueText.text = lifetotalpoints.ToString();
        ValueText.text = totalpoints.ToString();
        PlayerPrefs.SetString("currentScore", "0");
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
                GameObject.Destroy(bomb);
            }
            StartCoroutine(RestartCurrentlevel());
        } 

        if(other.CompareTag("cointag"))
        {
            other.gameObject.SetActive(false);
            totalpoints += 1 ;
            ValueText.text = totalpoints.ToString();
        }
    }

    public IEnumerator RestartCurrentlevel()
    {
        
        if (lifetotalpoints <= 0)
            {
                yield return new WaitForSeconds(1);
                PlayerPrefs.SetString("currentScore", ValueText.text);
                SceneManager.LoadScene("LoseScreen");
        }
    }

}
