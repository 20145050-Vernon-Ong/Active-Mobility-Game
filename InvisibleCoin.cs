using UnityEngine;

public class InvisibleCoin : MonoBehaviour
{

    public int coins;
    //Keep track of total picked coins (Since the value is static, it can be accessed at "SC_2DCoin.totalCoins" from any script)
    void Start()
    {
        //Make Collider2D as trigger 
        GetComponent<Collider2D>().isTrigger = true;

        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy the coin if Object tagged Player comes in contact with it
        if (collision.gameObject.CompareTag("Player"))
        {
                   
            ItemSystem.instance.CoinCollection(coins);
            Destroy(gameObject);

        }
        
    }

}