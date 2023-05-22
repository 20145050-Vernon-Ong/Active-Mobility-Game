using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject warningBox;
    readonly float timeDis = 3;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        warningBox.SetActive(false);
       
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D c2d)
    {
        //Destroy the coin if Object tagged Player comes in contact with it
        if (c2d.CompareTag("Player"))
        {
            warningBox.SetActive(true);
            Destroy(warningBox, timeDis);

        }

    }

    
}
