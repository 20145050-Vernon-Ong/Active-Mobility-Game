using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputScore;
    [SerializeField] private TMP_InputField inputName;

    public UnityEvent<string, int> submitScoreEvent;

    public void SubmitScore()
    {
        /*int randomScore = Random.Range(100, 1001);*/ // Generate a random integer between 100 and 1000.
        submitScoreEvent.Invoke(inputName.text, int.Parse(inputScore.text));
    }
}