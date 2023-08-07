using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class summaryscript : MonoBehaviour
{
    public TextMeshProUGUI samepletext;
    
    // Start is called before the first frame update
    void Start()
    {
       
        samepletext.text = PlayerPrefs.GetString("summary");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
