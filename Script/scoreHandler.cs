using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class scoreHandler : MonoBehaviour
{
    public GameObject currentScore;
    //public GameObject highScore;
    public GameObject summary;
    public GameObject distance;
    public GameObject learningPoint1;
    public GameObject learningPoint2;
    public GameObject learningPoint3;
    public GameObject check1;
    public GameObject check2;
    public GameObject check3;

    private TextMeshProUGUI currentScoreText;
    //private TextMeshProUGUI highScoreText;
    private TextMeshProUGUI summaryText;
    private TextMeshProUGUI distanceText;
    private TextMeshProUGUI learningPoint1Text;
    private TextMeshProUGUI learningPoint2Text;
    private TextMeshProUGUI learningPoint3Text;

    private AsyncOperation operation;
    
    private float current;
    private int highscore;
    private int isTicked;
    private int isTicked2;
    private int isTicked3;

    private HealthManager hm;
    //private int high;
    
    // Start is called before the first frame update
    void Awake()
    {
        //target text mesh
        learningPoint1Text = learningPoint1.GetComponent<TextMeshProUGUI>();
        learningPoint2Text = learningPoint2.GetComponent<TextMeshProUGUI>();
        learningPoint3Text = learningPoint3.GetComponent<TextMeshProUGUI>();
        distanceText = distance.GetComponent<TextMeshProUGUI>();
        currentScoreText = currentScore.GetComponent<TextMeshProUGUI>();
        //highScoreText = highScore.GetComponent<TextMeshProUGUI>();
        summaryText = summary.GetComponent<TextMeshProUGUI>();
        learningPoint1Text.text = PlayerPrefs.GetString("learningPoint1", learningPoint1Text.text);
        learningPoint2Text.text = PlayerPrefs.GetString("learningPoint2", learningPoint2Text.text);
        learningPoint3Text.text = PlayerPrefs.GetString("learningPoint3", learningPoint3Text.text);
        summaryText.text = PlayerPrefs.GetString("summary", summaryText.text);
        // currentscore;
        currentScoreText.text = PlayerPrefs.GetString("currentScore");
        // retrieve and convert currentscore into current
        current = float.Parse(PlayerPrefs.GetString("currentScore"));
        //high = int.Parse(PlayerPrefs.GetString("highScore"));
        // if current score is 100, set 100 to highscore mesh. set < to restart
        PlayerPrefs.SetString("highScore", System.Convert.ToString(current));
        if (HealthManager.health > 0)
        {
            distanceText.text = "Distance Covered: " + PlayerPrefs.GetString("distance") + "m";
        } else
        {
            distanceText.text = "Distance Left: " + PlayerPrefs.GetString("distance") + "m";
        }     
        isTicked = PlayerPrefs.GetInt("tick1", isTicked);
        isTicked2 = PlayerPrefs.GetInt("tick2", isTicked2);
        isTicked3 = PlayerPrefs.GetInt("tick3", isTicked3);
        if (isTicked == 1)
        {
            check1.SetActive(true);
        }
        else
        {
            check1.SetActive(false);
        }
        if (isTicked2 == 1)
        {
            check2.SetActive(true);
        } else
        {
            check2.SetActive(false);
        }
        if (isTicked3 == 1)
        {
            check3.SetActive(true);
        } else
        {
            check3.SetActive(false);
        }
    }

    public IEnumerator LoadGame()
    {
        operation = SceneManager.LoadSceneAsync("MainScene");
        while (!operation.isDone)
        {
            yield return null;
        }
    }

    public void RestartGame()
    {
        StartCoroutine(LoadGame());
    }

    public void HighScore()
    {

        highscore = PlayerPrefs.GetInt("PlayerHighScore");

        if (current > highscore)
        {
            highscore = (int)current;
            PlayerPrefs.SetInt("PlayerHighScore", highscore);
            PlayerPrefs.Save();
            Debug.Log(highscore);
        }

    }

}
