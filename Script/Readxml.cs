using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class Readxml : MonoBehaviour
{
    public GameObject tutorPopup;
    public TMP_Text tutorText;
    public TMP_Text notification;
    private int count = 0;
    string[] textList;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://raw.githubusercontent.com/20145050-Vernon-Ong/Active-Mobility-Game/main/textStorage.txt");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        } else
        {
            string[] seperate = { "\n" };
            textList = www.downloadHandler.text.Split(seperate, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < textList.Length; i++)
            {
                Debug.Log(textList[i]);
                tutorText.text = textList[0];
                count = 1;
                
            }
            
        }
    }

    public void ChangeText()
    {
        if (count == 1)
        {
            tutorText.text = textList[1];
            count = 2;
        }
        else if (count == 2)
        {
            tutorPopup.SetActive(false);
            Time.timeScale = 1;
            count = 3;
        }
    }

}
