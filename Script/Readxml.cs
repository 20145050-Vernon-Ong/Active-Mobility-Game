using System.Collections;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using SystemInfo = UnityEngine.Device.SystemInfo;
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
    private int count;
    private string data;
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
    private readonly List<PopNotifi> popNotifiList = new();
    public TextAsset xmlFile;
    private PlayerMovement pm;
    /*IEnumerator GetRequest(string url)
    {
        using UnityWebRequest webrequest = UnityWebRequest.Get(url);
        yield return webrequest.SendWebRequest();
        if (webrequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(webrequest.error);
            Debug.LogError(webrequest.downloadHandler.error);
            Debug.Log(webrequest.downloadHandler.text);
        } else
        {
            xmldoc.Load(@url);
            root = xmldoc.DocumentElement;
            nodes = root.SelectNodes("/popupNotify/popNoti");

            foreach (XmlNode node in nodes)
            {
                PopNotifi popNoti = new(node["text"].InnerText, node["colliderName"].InnerText, node["type"].InnerText, node["duration"].InnerText);
                popNotifiList.Add(popNoti);
            }
        }
    }*/

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

    /*private async Task<string> AsyncGetRequest()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SendWebRequest();
        while (!request.isDone)
        {
            await Task.Yield();

        }

        if (request.result != UnityWebRequest.Result.ConnectionError)
        {
            xmldoc.Load(@url);
            root = xmldoc.DocumentElement;
            nodes = root.SelectNodes("/popupNotify/popNoti");

            foreach (XmlNode node in nodes)
            {
                PopNotifi popNoti = new(node["text"].InnerText, node["colliderName"].InnerText, node["type"].InnerText, node["duration"].InnerText);
                popNotifiList.Add(popNoti);

            }
        }
        return request.url;
    }*/

    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        //StartCoroutine(ReadXML("https://raw.githubusercontent.com/20145050-Vernon-Ong/Active-Mobility-Game/main/popupNotifi.xml"));
        //var result = await AsyncGetRequest();
        data = xmlFile.text;
        ParseXmlFile(data);
        count = 0;
        for (int i = 0; i < popNotifiList.Count; i++)
        {
            if (popNotifiList[i].GetType().Equals("startPopup"))
            {
                tutorText.text = popNotifiList[i].GetText();
                pm.change = Vector3.zero;
                count++;
            }
        }
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
    void ParseXmlFile(string xmlData)
    {
        xmldoc.LoadXml(xmlData);
        string xmlPathPattern = "/popupNotify/popNoti";
        nodes = xmldoc.SelectNodes(xmlPathPattern);
        foreach (XmlNode node in nodes)
        {
            if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Windows)
            {
                PopNotifi popNoti = new(node["text"].InnerText, node["colliderName"].InnerText, node["type"].InnerText, node["duration"].InnerText);
                popNotifiList.Add(popNoti);
            } else if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Other)
            {
                PopNotifi popNoti = new(node["mobileText"].InnerText, node["colliderName"].InnerText, node["type"].InnerText, node["duration"].InnerText);
                popNotifiList.Add(popNoti);
            }
            
        }
    }
    /*IEnumerator ReadXML(string url)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();
        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error");
        } else
        {
            xmldoc.Load(@url);
            root = xmldoc.DocumentElement;
            nodes = root.SelectNodes("/popupNotify/popNoti");
            foreach (XmlNode node in nodes)
            {
                if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Windows)
                {
                    PopNotifi popNoti = new(node["text"].InnerText, node["colliderName"].InnerText, node["type"].InnerText, node["duration"].InnerText);
                    popNotifiList.Add(popNoti);
                }
                else if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Other)
                {
                    PopNotifi popNoti = new(node["mobileText"].InnerText, node["colliderName"].InnerText, node["type"].InnerText, node["duration"].InnerText);
                    popNotifiList.Add(popNoti);
                }
            }
        }
        
    }*/

    void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < popNotifiList.Count; i++)
        {
            if (collision.CompareTag(popNotifiList[i].GetCollider()))
            {
                Debug.Log(popNotifiList[i].GetCollider());
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
}
