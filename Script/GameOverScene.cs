using TMPro;
using UnityEngine;

public class GameOverScene : MonoBehaviour
{
    public TMP_Text score;

    private gc GC;
    // Start is called before the first frame update
    void Start()
    {
        GC = GetComponent<gc>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text += "Score: " + GC.totalpoints.ToString();
    }
}
