using System.Collections;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System;

public class Readxml : MonoBehaviour
{
    public class PopNotifi
    {
        public string text;
        public string colliderName;
        public string type;
        public string duration;
        public PopNotifi(string text, string colliderName, string type, string duration)
        {
            this.text = text;
            this.colliderName = colliderName;
            this.type = type;
            this.duration = duration;
        }
        public string GetText() { return text; }

        public string GetCollider() { return colliderName; }

        public new string GetType() { return type; }

        public string GetDuration() { return duration; }

        public void SetDuration(string duration) { this.duration = duration; }

        public void SetType(string type) { this.type = type; }
    
    }

    private int count;
    public GameObject notifpop;
    public GameObject tutorPopup;
    public TMP_Text notifText;
    public TMP_Text tutorText;
    public XmlDocument xmldoc = new();
    public XmlNodeList nodes;
    public XmlElement root;
    private readonly List<PopNotifi> popNotifiList = new();
    private string[] textList;
    void Awake()
    {
        ReadXML();
        count = 0;
        for (int i = 0; i < popNotifiList.Count; i++)
        {
            if (popNotifiList[i].GetType().Equals("startPopup"))
            {
                textList = popNotifiList[i].GetText().Split("\n", StringSplitOptions.RemoveEmptyEntries);
                tutorText.text = textList[count];
                tutorPopup.SetActive(true);
                Time.timeScale = 0;
                count++;
            }           
        }
    }

    private void ReadXML()
    {
        xmldoc.Load(@"https://raw.githubusercontent.com/20145050-Vernon-Ong/Active-Mobility-Game/main/popupNotifi.xml");
        root = xmldoc.DocumentElement;
        nodes = root.SelectNodes("/popupNotify/popNoti");
        foreach (XmlNode node in nodes)
        {
            PopNotifi popNoti = new(node["text"].InnerText, node["colliderName"].InnerText, node["type"].InnerText, node["duration"].InnerText);
            popNotifiList.Add(popNoti);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < popNotifiList.Count; i++)
        {
            if (collision.CompareTag(popNotifiList[i].GetCollider()))
            {
                Debug.Log(popNotifiList[i].GetCollider());
                if (popNotifiList[i].GetType().Equals("popup") || popNotifiList[i].GetType().Equals("popupNotification"))
                {
                    tutorText.text = popNotifiList[i].GetText();
                    tutorPopup.SetActive(true);
                    Time.timeScale = 0;
                    if (popNotifiList[i].GetType().Equals("popupNotification"))
                    {
                        popNotifiList[i].SetType("notification");
                    }
                }
                else if (popNotifiList[i].GetType().Equals("notification"))
                {
                    notifText.text = popNotifiList[i].GetText();
                    StartCoroutine(WaitBeforeShow(float.Parse(popNotifiList[i].GetDuration())));
                } 
            }
        }
    }

    private IEnumerator WaitBeforeShow(float time)
    {
        // for(;;) 
        // {
        // execute block of code here
        yield return new WaitForSeconds(0.5f);
        notifpop.SetActive(true);
        yield return new WaitForSeconds(time);
        notifpop.SetActive(false);
        yield return new WaitForSeconds(.1f);
        // }
    }

    public void ClosePopup()
    {
        if (count == 1)
        {
            tutorText.text = textList[count];
            count++;
        } else if (count == 2)
        {
            tutorPopup.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
