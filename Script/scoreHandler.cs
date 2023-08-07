
using UnityEngine;
using TMPro;

public class scoreHandler : MonoBehaviour
{

    public GameObject currentScore;
    public GameObject highScore;
    public GameObject summary;

    private TextMeshProUGUI currentScoreText;
    private TextMeshProUGUI highScoreText;
    private TextMeshProUGUI summaryText;

    private int current;
    private int high;
    

    // Start is called before the first frame update
    void Start()
    {
        //target text mesh
        currentScoreText = currentScore.GetComponent<TextMeshProUGUI>();
        highScoreText = highScore.GetComponent<TextMeshProUGUI>();
        summaryText = summary.GetComponent<TextMeshProUGUI>();
        summaryText.text = PlayerPrefs.GetString("summary", summaryText.text);
        // currentscore;
        currentScoreText.text = PlayerPrefs.GetString("currentScore");
        // retrieve and convert currentscore into current
        current = int.Parse(PlayerPrefs.GetString("currentScore"));
        high = int.Parse(PlayerPrefs.GetString("highScore"));

        // if current score is 100, set 100 to highscore mesh. set < to restart
        if(current > high)
        {
            PlayerPrefs.SetString("highScore", System.Convert.ToString(current));
        } else
        {
            
        }

        highScoreText.text = "High Score: " + PlayerPrefs.GetString("highScore");


    }


}
