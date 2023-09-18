using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to a Text component where you want to display the score.

    void Start()
    {
        // Retrieve the score from PlayerPrefs.
        int playerScore = PlayerPrefs.GetInt("PlayerHighScore");

        // Display the score.
        scoreText.text = "Highscore: " + playerScore.ToString();
    }
}
