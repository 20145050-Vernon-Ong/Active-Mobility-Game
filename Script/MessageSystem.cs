using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSystem : MonoBehaviour
{

    public GameObject msg;
    // Start is called before the first frame update
    void Start()
    {
        msg.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMsg()
    {
        msg.SetActive(true);
    }

    public void CloseMsg() 
    { 
        msg.SetActive(false);
    }

}
