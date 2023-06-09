using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gcs_menu : MonoBehaviour
{

    public AudioSource soundPlayer;
    public AudioClip hover;
    public AudioClip click;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayHover()
    {
        soundPlayer.PlayOneShot(hover);
    }

    public void PlayClick()
    {
        soundPlayer.PlayOneShot(click);
    }

}
