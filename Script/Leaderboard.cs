using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using UnityEngine.Events;

public class Leaderboard : MonoBehaviour
{

    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> scores;
    [SerializeField] private TextMeshProUGUI inputScore;
    [SerializeField] private TMP_InputField inputName;
    public TextMeshProUGUI playerscore;
    public TextMeshProUGUI playerrank;

    public UnityEvent<string, int> submitScoreEvent;

    private string publicLeaderboardKey = "6c3aafdd5f037297bc202dbda1db7c77df98e15e980ef623e0e3cacf99ca7efe";

    private void Start()
    {
        GetLeaderboard();
    }
    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            int loopLength = (msg.Length);
            for (int i = 0; i < loopLength; ++i)
            {
                if (msg[i].Rank > 10)
                {
                    if (PlayerPrefs.GetString("playerName") == msg[i].Username)
                    {
                        playerrank.text = msg[i].Rank + ". " + msg[i].Username;
                        playerscore.text = msg[i].Score.ToString();
                    }
                }
                else
                {
                    names[i].text = msg[i].Rank + ". " + msg[i].Username;
                    scores[i].text = msg[i].Score.ToString();
                    if (PlayerPrefs.GetString("playerName") == msg[i].Username)
                    {
                        playerrank.text = msg[i].Rank + ". " + msg[i].Username;
                        playerscore.text = msg[i].Score.ToString();
                    }
                }
            }
        }));
        
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((msg) =>
        {
            /*if (System.Array.IndexOf(badWords, name) != -1) return;*/
            GetLeaderboard();
        }));
    }
    public void SubmitScore()
    {
        /*int randomScore = Random.Range(100, 1001);*/ // Generate a random integer between 100 and 1000.
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            string exist = "no";
            int loopLength = (msg.Length);
            for (int i = 0; i < loopLength; ++i)
            {
                if (inputName.text != (msg[i].Username))
                {
                    //inputName not inside leaderboard yet
                    print(i);
                    exist = "no";
                }
                else if (PlayerPrefs.GetString("playerName") == msg[i].Username)
                {
                    //playerPrefs same as leaderboard
                    print(i);
                    exist = "no";
                }
                else
                {
                    exist = "yes";
                    print("Name exist! Please enter a new name!");
                    break;
                    //Make a popup
                }
            }
            if (exist == "no")
            {
                PlayerPrefs.SetString("playerName", inputName.text);
                submitScoreEvent.Invoke(inputName.text, int.Parse(inputScore.text));
                print("Name successfully added!");
            }
        }));
    }

}
