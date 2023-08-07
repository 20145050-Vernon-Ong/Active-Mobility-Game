using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class notif : MonoBehaviour
{

    public GameObject notifpop;
    public TMP_Text notifText;
    // Start is called before the first frame update
    private void Start()
    {
        notifText.text = "Remember to stay on the correct lane and keep a lookout for vehicle";
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.transform.tag == "Player")
        {
            
            StartCoroutine(WaitBeforeShow());
            


        }
        
    }

    // public void notifshow()
    // {
    //     notifpop.SetActive(false);

    //     StartCoroutine(WaitBeforeShow());
    // }

    private IEnumerator WaitBeforeShow()
    {

        

        // for(;;) 
        // {
        // execute block of code here
        yield return new WaitForSeconds(1);
        notifpop.SetActive(true);
        yield return new WaitForSeconds(5);
        notifpop.SetActive(false);

        yield return new WaitForSeconds(.1f);
        // }

    }
}
