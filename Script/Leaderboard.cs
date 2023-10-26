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
    public string textPrefix = "player";
    public TextMeshProUGUI playerscore;
    public TextMeshProUGUI playerrank;

    public UnityEvent<string, int, string> submitScoreEvent;

    private string publicLeaderboardKey = "6c3aafdd5f037297bc202dbda1db7c77df98e15e980ef623e0e3cacf99ca7efe";

    private void Awake()
    {
        string playerName = PlayerPrefs.GetString("playerName");

        if (!string.IsNullOrEmpty(playerName))
        {
            // If a player name is found in PlayerPrefs, display it
            inputName.text = playerName;
        }
        else
        {
            // If no player name is found, generate a random name
            int randomValue = Random.Range(1, int.MaxValue);
            playerName = textPrefix + randomValue.ToString();
            inputName.text = playerName;

            // Save the generated name in PlayerPrefs for future use
            PlayerPrefs.SetString("playerName", playerName);

        }

        GetLeaderboard();
    }

        private void Start()
    {
        string playerID = PlayerPrefs.GetString("playerID");
        print(playerID);

        if (string.IsNullOrEmpty(playerID)) // Check if player is new or not
        {
            // If a player unique id is not found in PlayerPrefs, generate a random unique id
            generateUniqueID();
        }

        inputName.onEndEdit.AddListener(OnNameChanged);
        StartCoroutine(DelayedSubmitScore());
    }
    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
            int loopLength = (msg.Length);
            string playerID = PlayerPrefs.GetString("playerID");
            for (int i = 0; i < loopLength; ++i)
            {
                if (msg[i].Rank > 10)
                {
                    if (/*PlayerPrefs.GetString("playerName") == msg[i].Username*/ playerID == msg[i].Extra)
                    {
                        playerrank.text = msg[i].Rank + ". " + msg[i].Username;
                        playerscore.text = msg[i].Score.ToString();
                    }
                }
                else
                {
                    names[i].text = msg[i].Rank + ". " + msg[i].Username;
                    scores[i].text = msg[i].Score.ToString();
                    if (playerID == msg[i].Extra)
                    {
                        names[i].color = Color.green;
                        scores[i].color = Color.green;
                    }
                    if (/*PlayerPrefs.GetString("playerName") == msg[i].Username*/ playerID == msg[i].Extra)
                    {
                        playerrank.text = msg[i].Rank + ". " + msg[i].Username;
                        playerscore.text = msg[i].Score.ToString();
                    }
                }
            }
        }));
        
    }

    public void SetLeaderboardEntry(string username, int score, string extra)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, extra, ((msg) =>
        {
            /*if (System.Array.IndexOf(badWords, name) != -1) return;*/
            GetLeaderboard();
        }));
    }
    /*public void SubmitScore()
    {
        *//*int randomScore = Random.Range(100, 1001);*//* // Generate a random integer between 100 and 1000.
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
    }*/

    public void generateUniqueID()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            int loopLength = msg.Length; //13
            for (int i = 0; i < loopLength+1; i++) //0-12
            {
                if (i.ToString() != msg[i].Extra)
                {
                    // i != 13
                    print("leaderboard" + msg[i].Extra);
                    print("id " + i);
                    PlayerPrefs.SetString("playerID", i.ToString());
                }
                else
                {
                    print("duplicates " + i);
                    print("DUPLICATES!");
                }
            }

        }));
    }

    
    private IEnumerator DelayedSubmitScore()
    {
        yield return new WaitForSeconds(0f);
        SubmitScore();
    }

    private void OnNameChanged(string newName)
    {
        // Save the new name in PlayerPrefs when it's changed
        PlayerPrefs.SetString("playerName", newName);
    }

    public void SubmitScore()
    {
        int score = int.Parse(inputScore.text);
        if (score >= 0) {

            submitScoreEvent.Invoke(inputName.text, int.Parse(inputScore.text), PlayerPrefs.GetString("playerID"));
    
        }
    }
}
