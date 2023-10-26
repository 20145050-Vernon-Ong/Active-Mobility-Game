using System.Collections;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using UnityEngine.Networking;
using UnityEngine.Assertions.Must;

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

        public void SetType(string type) { this.type = type; }

    }
    public GameObject notifpop;
    public GameObject tutorPopup;
    public GameObject tutorialPage;
    public GameObject panel;
    public TMP_Text notifText;
    public TMP_Text tutorText;
    public gcs_menu gcs;

    private bool popupsDisabled;
    private XmlNodeList nodes;
    private XmlElement root;
    private readonly XmlDocument xmldoc = new();
    private readonly List<PopNotifi> popNotifiList = new();
    private PopupManager pop;

    IEnumerator ReadXML(string url)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            xmldoc.LoadXml(@webRequest.downloadHandler.text);
            root = xmldoc.DocumentElement;
            nodes = root.SelectNodes("/popupNotify/popNoti");
            for (int i = 0; i < nodes.Count; i++)
            {
                if (pop.isChecked)
                {
                    if (!nodes[i]["colliderName"].InnerText.Equals("coinTag") && !nodes[i]["colliderName"].InnerText.Equals("startPopup"))        
                    {
                        nodes[i]["type"].InnerText = "notification";
                        if (float.Parse(nodes[i]["duration"].InnerText) == 0)
                        {
                            nodes[i]["duration"].InnerText = "3";
                        }
                        PopNotifi popNoti2 = new(nodes[i]["text"].InnerText, nodes[i]["colliderName"].InnerText, nodes[i]["type"].InnerText, nodes[i]["duration"].InnerText);
                        popNotifiList.Add(popNoti2);
                    }
                }
                else
                {
                    if (nodes[i]["type"].InnerText.Equals("startPopup"))
                    {
                        if (PlayerPrefs.GetInt("mobile") == 0)
                        {
                            tutorText.text = nodes[i]["text"].InnerText;
                        }
                        else
                        {
                            tutorText.text = nodes[i]["mobileText"].InnerText;
                        }
                    } else
                    {
                        PopNotifi popNoti = new(nodes[i]["text"].InnerText, nodes[i]["colliderName"].InnerText, nodes[i]["type"].InnerText, nodes[i]["duration"].InnerText);
                        popNotifiList.Add(popNoti);
                    }
                }
            }
        }
        else
        {
            Debug.Log("Server is unreachable");
        }
    }

    IEnumerator WaitBeforeShow(float time)
    {
        yield return new WaitForSeconds(0.5f);
        notifpop.SetActive(true);
        yield return new WaitForSeconds(time);
        notifpop.SetActive(false);
    }

    void Awake()
    {
        pop = GetComponent<PopupManager>();
        StartCoroutine(ReadXML("https://raw.githubusercontent.com/20145050-Vernon-Ong/Active-Mobility-Game/main/Xml/popupNotifi.xml"));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < popNotifiList.Count; i++)
        {
            if (collision.CompareTag(popNotifiList[i].GetCollider()))
            {
                if (popNotifiList[i].GetType().Equals("popup"))
                {
                    tutorText.text = popNotifiList[i].GetText();
                    tutorPopup.SetActive(true);
                    if (float.Parse(popNotifiList[i].GetDuration()) > 0)
                    {
                        popNotifiList[i].SetType("notification");
                    }
                    else if (popNotifiList[i].GetCollider().Equals("coinTag") || popNotifiList[i].GetCollider().Equals("zebraTag") || popNotifiList[i].GetCollider().Equals("trafficTag"))
                    {
                        popNotifiList.RemoveAt(i);          
                    } 
                }
                else if (popNotifiList[i].GetType().Equals("notification"))
                {
                    gcs.PlayNotif();
                    notifText.text = popNotifiList[i].GetText();
                    StartCoroutine(WaitBeforeShow(float.Parse(popNotifiList[i].GetDuration())));
                }
            }
        }
    }

    public void ClosePopup()
    {
        if (tutorPopup.activeInHierarchy)
        {
            tutorPopup.SetActive(false);
        }
        else if (tutorialPage.activeInHierarchy)
        {
            tutorialPage.SetActive(false);
            panel.SetActive(false);
            tutorPopup.SetActive(true);
        }
    }

    public void DisableAllPopupsPermanently()
    {
        if (popupsDisabled)
        {
            Debug.Log("on pops");
            // Restore the initial states
            // tutorPopup = initialTutorPopupState;
            // popup = initialPopupState;
            // notifpop = initialNotifpopState;
            // Set the flag to false to enable popups
            popupsDisabled = false;
        }
        else
        {
            Debug.Log("off pops");
            popupsDisabled = true;
            // Disable the popups and set the flag to true to disable popups permanently
            // tutorPopup.SetActive(false);
            // popup.SetActive(false);
            // notifpop.SetActive(false);
        }
    }
}
