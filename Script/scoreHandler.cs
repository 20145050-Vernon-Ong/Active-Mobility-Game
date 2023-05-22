using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scoreHandler : MonoBehaviour
{
    public GameObject currentScore;
    public GameObject highScore;

    private TextMeshProUGUI currentScoreText;
    private TextMeshProUGUI highScoreText;

    private int current;
    private int high;
    
    // Start is called before the first frame update
    void Start()
    {
        //target text mesh
        currentScoreText = currentScore.GetComponent<TextMeshProUGUI>();
        highScoreText = highScore.GetComponent<TextMeshProUGUI>();
        // currentscore
        currentScoreText.text = PlayerPrefs.GetString("currentScore");
        // retrieve and convert currentscore into current
        current = int.Parse(PlayerPrefs.GetString("currentScore"));
        high = int.Parse(PlayerPrefs.GetString("highScore"));

        // if current score is 100, set 100 to highscore mesh. set < to restart
        if(current > high)
        {
            PlayerPrefs.SetString("highScore", System.Convert.ToString(current));
        }

        highScoreText.text = PlayerPrefs.GetString("highScore");
    }
}
