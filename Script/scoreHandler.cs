using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class scoreHandler : MonoBehaviour
{
    public GameObject currentScore;
    //public GameObject highScore;
    public GameObject summary;
    public GameObject distance;
    public GameObject learningPoint1;
    public GameObject learningPoint2;
    public GameObject learningPoint3;

    private TextMeshProUGUI currentScoreText;
    //private TextMeshProUGUI highScoreText;
    private TextMeshProUGUI summaryText;
    private TextMeshProUGUI distanceText;
    private TextMeshProUGUI learningPoint1Text;
    private TextMeshProUGUI learningPoint2Text;
    private TextMeshProUGUI learningPoint3Text;
    
    private int current;
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
        current = int.Parse(PlayerPrefs.GetString("currentScore"));
        //high = int.Parse(PlayerPrefs.GetString("highScore"));

        // if current score is 100, set 100 to highscore mesh. set < to restart
        PlayerPrefs.SetString("highScore", System.Convert.ToString(current));
        distanceText.text = PlayerPrefs.GetString("distance");
        /*if (current > high)
        {
            
        } else
        {
            
        }*/
        //highScoreText.text = PlayerPrefs.GetString("highScore");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
