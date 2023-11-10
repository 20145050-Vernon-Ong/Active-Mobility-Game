using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
// using MongoDB.Bson.Serialization.Serializers;

public class scoreHandler : MonoBehaviour
{
    public GameObject currentScore;
    public GameObject summary;
    public GameObject distance;
    public GameObject learningPoint1;
    public GameObject learningPoint2;
    public GameObject learningPoint3;
    public GameObject check1;
    public GameObject check2;
    public GameObject check3;

    public TextMeshProUGUI checboxscore;
    public TextMeshProUGUI checboxscore1;
    public TextMeshProUGUI checboxscore2;

    private TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;
    private TextMeshProUGUI summaryText;
    private TextMeshProUGUI distanceText;
    private TextMeshProUGUI learningPoint1Text;
    private TextMeshProUGUI learningPoint2Text;
    private TextMeshProUGUI learningPoint3Text;
    public TextMeshProUGUI howmanyboxes;

    private AsyncOperation operation;
    
    private int current;
    private int isTicked;
    private int isTicked2;
    private int isTicked3;

    public int howmanybox = 0; 
    public int howmanypoints = 0; 

    private readonly HealthManager hm;
    private int highscore;
    
    // Start is called before the first frame update
    void Awake()
    {
        //target text mesh
        learningPoint1Text = learningPoint1.GetComponent<TextMeshProUGUI>();
        learningPoint2Text = learningPoint2.GetComponent<TextMeshProUGUI>();
        learningPoint3Text = learningPoint3.GetComponent<TextMeshProUGUI>();
        distanceText = distance.GetComponent<TextMeshProUGUI>();
        currentScoreText = currentScore.GetComponent<TextMeshProUGUI>();

        checboxscore = checboxscore.GetComponent<TextMeshProUGUI>();
        checboxscore1 = checboxscore1.GetComponent<TextMeshProUGUI>();
        checboxscore2 = checboxscore2.GetComponent<TextMeshProUGUI>();

        int points = PlayerPrefs.GetInt("Points", 0);
        highScoreText.text = points.ToString();
        summaryText = summary.GetComponent<TextMeshProUGUI>();
        learningPoint1Text.text = PlayerPrefs.GetString("learningPoint1", learningPoint1Text.text);
        learningPoint2Text.text = PlayerPrefs.GetString("learningPoint2", learningPoint2Text.text);
        learningPoint3Text.text = PlayerPrefs.GetString("learningPoint3", learningPoint3Text.text);
        summaryText.text = PlayerPrefs.GetString("summary", summaryText.text);
        // currentscore;
        currentScoreText.text = PlayerPrefs.GetString("currentScore");
        // retrieve and convert currentscore into current
        current = int.Parse(PlayerPrefs.GetString("currentScore"));
        // if current score is 100, set 100 to highscore mesh. set < to restart
        PlayerPrefs.SetString("highScore", System.Convert.ToString(current));


        if (HealthManager.health == 3)
        {
            distanceText.text = "x " + HealthManager.health + ": " + "<color=green>+15 coins</color>";
        }
        else if (HealthManager.health == 2)
        {
            distanceText.text = "x " + HealthManager.health + ": " + "<color=green>+10 coins</color>";
        }
        else if (HealthManager.health == 1)
        {
            distanceText.text = "x " + HealthManager.health + ": " + "<color=green>+5 coins</color>";
        }
        else
        {
            distanceText.text = "Left: " + HealthManager.health;  
            // health
        }
        
        isTicked = PlayerPrefs.GetInt("tick1", isTicked);
        isTicked2 = PlayerPrefs.GetInt("tick2", isTicked2);
        isTicked3 = PlayerPrefs.GetInt("tick3", isTicked3);
        
        if(HealthManager.health != 0)
        {
            if (isTicked == 1)
            {
                check1.SetActive(true);
                checboxscore.text = "+10 coins";
                checboxscore.color = Color.green;
                howmanybox += 1;
                howmanypoints += 10;
            }
            else
            {
                check1.SetActive(false);
            }
            if (isTicked2 == 1)
            {
                check2.SetActive(true);
                checboxscore1.text = "+10 coins";
                checboxscore1.color = Color.green;
                howmanybox += 1;
                howmanypoints += 10;
            } else
            {
                check2.SetActive(false);
            }
            if (isTicked3 == 1)
            {
                check3.SetActive(true);
                checboxscore2.text = "+10 coins";
                checboxscore2.color = Color.green;
                howmanybox += 1;
                howmanypoints += 10;
            } else
            {
                check3.SetActive(false);
            }
            
             howmanyboxes.text = "x " + howmanybox.ToString() + ": <color=green>+" + howmanypoints + " coins</color>";

            
        }

        if(HealthManager.health == 0)
        {
            if (isTicked == 1)
            {
                check1.SetActive(true);
                howmanybox += 1;
                // checboxscore.text = "+10 coins";
                // checboxscore.color = Color.green;
            }
            else
            {
                check1.SetActive(false);
            }
            if (isTicked2 == 1)
            {
                check2.SetActive(true);
                howmanybox += 1;
                // checboxscore1.text = "+10 coins";
                // checboxscore1.color = Color.green;
            } else
            {
                check2.SetActive(false);
            }
            if (isTicked3 == 1)
            {
                check3.SetActive(true);
                howmanybox += 1;
                // checboxscore2.text = "+10 coins";
                // checboxscore2.color = Color.green;
            } else
            {
                check3.SetActive(false);
            }

            howmanyboxes.text = ": " + howmanybox.ToString();
        }
    }

    public IEnumerator LoadGame()
    {
        operation = SceneManager.LoadSceneAsync("newscene");
        while (!operation.isDone)
        {
            yield return null;
        }
    }
    public void RestartGame()
    {
        StartCoroutine(LoadGame());
    }
}
