using UnityEngine;
using TMPro;
public class ItemSystem : MonoBehaviour
{
    public static ItemSystem instance;
    public static ItemSystem instanceB;


    public int coinAmt;

    void Awake()
    {
        coinAmt = 0;
        instance = this;
        instanceB = this;
    }

    // Update is called once per frame
    public void CoinCollection(int c)
    {
        coinAmt += c;
    }

    public void BombCollection(int b)
    {
        coinAmt -= b;
    }

}
