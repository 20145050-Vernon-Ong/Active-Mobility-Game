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
    public GameObject notifpop;
    public GameObject tutorPopup;
    public GameObject tutorialPage;
    public GameObject panel;
    public TMP_Text notifText;
    public TMP_Text tutorText;
    private bool popupsDisabled;
    private bool isCount = false;
    private bool counting = false;
    private float timer = 1.00f;
    private float countDown;
    private string phoneTime;
    private string phoneWarning;
    private XmlNodeList nodes;
    private XmlNodeList nodeP;
    private XmlElement root;
    private readonly XmlDocument xmldoc = new();
    private readonly List<PopNotifi> popNotifiList = new();
    private PopupManager pop;
    private Animator animator;
    private GC gc;

    private bool isPhoneNotificationVisible = false;


    IEnumerator ReadXML(string url)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            xmldoc.LoadXml(@webRequest.downloadHandler.text);
            root = xmldoc.DocumentElement;
            nodes = root.SelectNodes("/popupNotify/popNoti");
            nodeP = root.SelectNodes("/popupNotify/notiPhone");
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i]["colliderName"].InnerText.Equals("footTag"))
                {
                    countDown = float.Parse(nodes[i]["timer"].InnerText);
                }
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
                    PopNotifi popNoti = new(nodes[i]["text"].InnerText, nodes[i]["colliderName"].InnerText, nodes[i]["type"].InnerText, nodes[i]["duration"].InnerText);
                    popNotifiList.Add(popNoti);
                }
            }
            for (int i = 0; i < nodeP.Count; i++)
            {
                phoneWarning = nodeP[i]["text"].InnerText;
                phoneTime = nodeP[i]["duration"].InnerText;
            }
            
        }
        else
        {
            Debug.Log("Server is unreachable");
        }
    }

    IEnumerator WaitBeforeShow(float time)
    {
        yield return new WaitForSeconds(0);
        notifpop.SetActive(true);
        yield return new WaitForSeconds(time);
        notifpop.SetActive(false);
    }

    IEnumerator FootTimer()
    {
        for (int i = 0; i < popNotifiList.Count; i++)
        {
            if (popNotifiList[i].GetCollider().Equals("footTag"))
            {
                if (timer >= 0.00f)
                {
                    timer += Time.deltaTime;
                    if (timer >= countDown && timer <= (countDown + 0.01))
                    {
                        yield return new WaitForSeconds(0.01f);
                        notifText.text = popNotifiList[i].GetText();
                        notifpop.SetActive(true);
                        yield return new WaitForSeconds(float.Parse(popNotifiList[i].GetDuration()));
                        notifpop.SetActive(false);
                        yield return new WaitForSeconds(0.00f);
                        timer = 1.00f;
                    }
                    
                }
            }
        }
    }

    void Awake()
    {
        pop = GetComponent<PopupManager>();
        animator = GetComponent<Animator>();
        gc = GetComponent<GC>();
        StartCoroutine(ReadXML("https://raw.githubusercontent.com/20145050-Vernon-Ong/Active-Mobility-Game/main/Xml/popupNotifi.xml"));
    }
    

    void Update()
    {
        if (counting)
        {
            isCount = true;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
        {
            if (isCount)
            {
                StartCoroutine(FootTimer());
                Debug.Log(timer.ToString("0"));
            }

            if (gc.isPhone)
            {
                isCount = false;
                notifText.text = phoneWarning;
                notifpop.SetActive(true);
                isPhoneNotificationVisible = false;
            } 
            else
            {
                
            }
   
        }

        if (!gc.isPhone) {
            if (!isPhoneNotificationVisible) {
                notifpop.SetActive(false);
            }
        }
  
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < popNotifiList.Count; i++)
        {
            if (!popNotifiList[i].GetCollider().Equals("footTag"))
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
                        else if (popNotifiList[i].GetCollider().Equals("coinTag") || popNotifiList[i].GetCollider().Equals("trafficTag"))
                        {
                            popNotifiList.RemoveAt(i);
                        }
                    }
                    else if (popNotifiList[i].GetType().Equals("notification"))
                    {
                        isPhoneNotificationVisible = true;
                        notifText.text = popNotifiList[i].GetText();
                        StartCoroutine(WaitBeforeShow(float.Parse(popNotifiList[i].GetDuration())));
                    }
                }
            }
        }

        if (collision.CompareTag("footTag"))
        {
            counting = true;
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
