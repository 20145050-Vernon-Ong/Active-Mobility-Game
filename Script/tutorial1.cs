using UnityEngine;

public class tutorial1 : MonoBehaviour
{
    
    public GameObject tutorialpopup1;
    public GameObject tutorial1interact;
    public GameObject controlinteract;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.transform.tag == "playertag")
        {
            tutorialpopup1.SetActive(true);

            


        }
        
    }

    public void removepop()
    {
        // tutorialpopup1.SetActive(false);
        Destroy(tutorialpopup1);

        controlinteract.SetActive(true);
    }

    public void removecontrol()
    {
        Destroy(controlinteract);
        
        tutorial1interact.SetActive(false);
    }
    
 
}
