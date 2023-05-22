using UnityEngine;
using TMPro;
public class ItemSystem : MonoBehaviour
{
    public static ItemSystem instance;
    public static ItemSystem instanceB;

    public TMP_Text coinText;
    public TMP_Text bombText;
    public int coinAmt = 0;

    void Awake()
    {
        instance = this;
        instanceB = this;
       
        
    }
    // Start is called before the first frame update
    void Start()
    {
        coinText.text = "      " + coinAmt.ToString();
    }

    // Update is called once per frame
    public void CoinCollection(int c)
    {
        coinAmt += c;
        coinText.text = "      " + coinAmt.ToString();
    }

    public void BombCollection(int b)
    {
        coinAmt -= b;
        coinText.text = "      " + coinAmt.ToString();
    }

}
