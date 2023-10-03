using UnityEngine;

public class gcs_menu : MonoBehaviour
{

    public AudioSource soundPlayer;
    public AudioClip hover;
    public AudioClip click;

    public void PlayHover()
    {
        soundPlayer.PlayOneShot(hover);
    }

    public void PlayClick()
    {
        soundPlayer.PlayOneShot(click);
    }

}
