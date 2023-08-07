using UnityEngine;

public class InvisibleBomb : MonoBehaviour
{

    private readonly int bombs = 1;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        //Destroy the coin if Object tagged Player comes in contact with it
        if (c2d.CompareTag("Player"))
        {
            ItemSystem.instance.BombCollection(bombs);
            Destroy(gameObject);
        }

    }
}
