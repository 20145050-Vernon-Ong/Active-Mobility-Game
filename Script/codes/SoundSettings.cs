using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour
    {
    [SerializeField] Slider soundSlider;
    [SerializeField] AudioMixer masterMixer;
    public GameObject soundMute;
    public GameObject soundunMute;
    // Start is called before the first frame update
    private void Start()
    {
        if (soundMute == true && PlayerPrefs.GetFloat("SavedMasterVolume", 100) > .001f)
        {
            soundMute.SetActive(false);
            soundunMute.SetActive(true);
            Debug.Log("unmute");
        }
        if(PlayerPrefs.GetFloat("SavedMasterVolume", 100) == .001f)
        {
            soundMute.SetActive(true);
            soundunMute.SetActive(false);
            Debug.Log("mute");
        }
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
    }

    public void SetVolume(float _value)
    {
        if(Mathf.Approximately(_value , 0))
        {
            _value = .001f;
            soundMute.SetActive(true);
            soundunMute.SetActive(false);
            Debug.Log("mute");
        }
        if(soundMute == true && _value > .001f)
        {
            soundMute.SetActive(false);
            soundunMute.SetActive(true);
            Debug.Log("unmute");
        }

        RefreshSlider(_value);
        PlayerPrefs.SetFloat("SavedMasterVolume", _value);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(_value / 100) * 20f);

    }    

    public void SetVolumeFromSlider()
    {
        SetVolume(soundSlider.value);
    }

    public void RefreshSlider(float _value)
    {
        soundSlider.value = _value;
    }

}
