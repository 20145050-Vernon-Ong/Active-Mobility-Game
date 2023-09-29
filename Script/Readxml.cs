using System.Collections;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using UnityEngine.Networking;

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
    private bool popupsDisabled;
    public GameObject notifpop;
    public GameObject tutorPopup;
    public GameObject popup;
    public GameObject zebraInterface;
    public GameObject trafficInterface;
    public TMP_Text notifText;
    public TMP_Text tutorText;
    public TMP_Text popupText;
    public XmlDocument xmldoc = new();
    public XmlNodeList nodes;
    public XmlElement root;
    public List<PopNotifi> popNotifiList = new();
    private PlayerMovement pm;
    private PopupManager pop;
    
    void Awake()
    {
        pm = GetComponent<PlayerMovement>();
        pop = GetComponent<PopupManager>();
        StartCoroutine(ReadXML("https://raw.githubusercontent.com/20145050-Vernon-Ong/Active-Mobility-Game/main/popupNotifi.xml"));
    }

    void Update()
    {
        if (tutorPopup.activeInHierarchy || popup.activeInHierarchy)
        {
            pm.change = Vector3.zero;
            pm.keyDisabled = false;
        } else
        {
            pm.keyDisabled = true;
        }
    }

    IEnumerator ReadXML(string url)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();
        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error");
        }
        else
        {
            xmldoc.LoadXml(@webRequest.downloadHandler.text);
            root = xmldoc.DocumentElement;
            nodes = root.SelectNodes("/popupNotify/popNoti");
            for (int i = 0; i < nodes.Count; i++)
            {
                if (PlayerPrefs.GetInt("mobile") == 0)
                {
                    PopNotifi popNoti = new(nodes[i]["text"].InnerText, nodes[i]["colliderName"].InnerText, nodes[i]["type"].InnerText, nodes[i]["duration"].InnerText);
                    popNotifiList.Add(popNoti);
                }
                else 
                {
                    PopNotifi popNoti = new(nodes[i]["mobileText"].InnerText, nodes[i]["colliderName"].InnerText, nodes[i]["type"].InnerText, nodes[i]["duration"].InnerText);
                    popNotifiList.Add(popNoti);
                }
                Checking(i);
                if (nodes[i]["type"].InnerText.Equals("startPopup"))
                {
                    tutorText.text = nodes[i]["text"].InnerText;
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < popNotifiList.Count; i++)
        {
            if (collision.CompareTag(popNotifiList[i].GetCollider()))
            {
                if (popNotifiList[i].GetType().Equals("popup"))
                {
                    popupText.text = popNotifiList[i].GetText();
                    pm.change = Vector3.zero;
                    popup.SetActive(true);
                    if (float.Parse(popNotifiList[i].GetDuration()) > 0)
                    {
                        popNotifiList[i].SetType("notification");
                    }
                    else if (popNotifiList[i].GetCollider().Equals("coinTag"))
                    {
                        popNotifiList.RemoveAt(i);
                    }
                    else if (popNotifiList[i].GetCollider().Equals("zebraTag"))
                    {
                        Destroy(zebraInterface);
                    }
                    else if (popNotifiList[i].GetCollider().Equals("trafficTag"))
                    {
                        Destroy(trafficInterface);
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

    void Checking(int i)
    {
        if (pop.isChecked)
        {
            if (popNotifiList[i].GetType().Equals("popup"))
            {
                if (popNotifiList[i].GetCollider().Equals("cyclingTag") || popNotifiList[i].GetCollider().Equals("roadTag"))
                {
                    popNotifiList[i].SetType("notification");
                } else
                {
                    popNotifiList.RemoveAt(i);
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
        else if (popup.activeInHierarchy)
        {
            popup.SetActive(false);
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
