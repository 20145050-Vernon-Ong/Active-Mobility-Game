using UnityEngine;

public class gcs_menu : MonoBehaviour
{

    public AudioSource soundPlayer;
    public AudioClip phone;
    public AudioClip hurt;
    public AudioClip health;
    public AudioClip compass;
    public AudioClip die;
    public AudioClip click;
    public AudioClip coin;
    public AudioClip gem;
    public AudioClip finish;

    // public void PlayHover()
    // {
    //     soundPlayer.PlayOneShot(hover);
    // }

    public void PlayClick()
    {
        soundPlayer.PlayOneShot(click);
    }

    public void PlayPhone()
    {
        soundPlayer.PlayOneShot(phone);
    }
    public void PlayHurt()
    {
        soundPlayer.PlayOneShot(hurt);
    }
    public void PlayHealth()
    {
        soundPlayer.PlayOneShot(health);
    }
    public void PlayCompass()
    {
        soundPlayer.PlayOneShot(compass);
    }
    public void PlayDie()
    {
        soundPlayer.PlayOneShot(die);
    }
    public void PlayCoin()
    {
        soundPlayer.PlayOneShot(coin);
    }
    public void PlayGem()
    {
        soundPlayer.PlayOneShot(gem);
    }
    public void PlayFinish()
    {
        soundPlayer.PlayOneShot(finish);
    }

}
