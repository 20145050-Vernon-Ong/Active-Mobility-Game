using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public class QuestClass
    {
        public string QuestDetail;
        public float QuestTime;
        public float resetTime;
        public string genre;
        public string difficulty;
        public bool isFinished;

        public QuestClass(string QuestDetail, float QuestTime, float resetTime, string genre, string difficulty)
        {
            this.QuestDetail = QuestDetail;
            this.QuestTime = QuestTime + 1;
            this.resetTime = resetTime + 1;
            this.genre = genre;
            this.difficulty = difficulty;
            isFinished = false;
        }

        public string GetQuestDetail() { return QuestDetail; }

        public float GetQuestTime() { return QuestTime; }

        public float GetResetTime() { return resetTime; }

        public string GetGenre() { return genre; }

        public string GetDifficulty() { return difficulty; }

        public bool IsFinished() { return isFinished; }       
    }

    readonly QuestClass quest1 = new("Travel to 7-Eleven in 1 Minute 30 Seconds", 90, 90, "", "easy");
    readonly QuestClass quest2 = new("Walk to the park in 2 minutes", 120, 120, "", "easy");
    readonly QuestClass quest3 = new("Walk to the cemetery in 1 minute", 60, 60, "", "medium");
    readonly QuestClass quest4 = new("Travel to the Adventure Bridge in 3 minutes", 180, 180, "", "hard");
    
    public GameObject windowDetails;
    public GameObject maxPhone;
    public GameObject failpopup;
    public GameObject finishpopup;

    public Button confirmBtn;

    public TMP_Text obj;
    public TMP_Text countDown;
    public TMP_Text active;
    public TMP_Text failText;
    public TMP_Text diffText;
    public TMP_Text finishText;
    public TMP_Text distanceText;

    bool time = false;
    bool isQuest = false;

    private float distance;

    private readonly List<QuestClass> quests = new();
    public List<Transform> pointList = new();
    public List<TMP_Text> textList = new();
    public List<Button> missionList = new();

    private PlayerMovement p;
    private Animator animator;
    // Start is called before the first frame update
    private void Awake()
    {
        quests.Add(quest1);
        quests.Add(quest2);
        quests.Add(quest3);
        quests.Add(quest4);
        for (int i = 0; i < pointList.Count; i++)
        {
            textList[i].text = quests[i].GetQuestDetail();
            pointList[i].transform.gameObject.SetActive(false);
        }
        finishpopup.SetActive(false);
        windowDetails.SetActive(false);
        failpopup.SetActive(false);
        finishpopup.SetActive(false);
        p = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        OpenQuestDetails();
    }
    // Update is called once per frame
    void Update()
    {
        Mission();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("questPoint"))
        {
            p.canMove = false;
            FinishPopup();
        }
    }

    public void Mission()
    {
        active.text = "Active Quest: \n";
        for (int i = 0; i < quests.Count; i++)
        {
            if (isQuest == true && obj.text == quests[i].GetQuestDetail())
            {
                QuestSystem();
                 active.text += quests[i].GetQuestDetail();
                if (pointList[i].gameObject.activeInHierarchy)
                {
                    distance = (pointList[i].transform.position - transform.position).magnitude;
                    distanceText.text = "Distance: " + distance.ToString("F1") + " meters";
                }
            } 
        }
    }

    public void MaximumPhone()
    {
         maxPhone.transform.position = new Vector3(900, 200, 0);
    }

    public void MinimumPhone()
    {
        maxPhone.transform.position = new Vector3(900, -125, 0);
    }

    private void OpenQuestDetails()
    {
        for (int i = 0; i < missionList.Count; i++)
        {
            missionList[i].onClick.AddListener(() => OpenDetail());
            confirmBtn.onClick.AddListener(() => QuestSystem());
            if (textList[i].text.Equals(quest1.GetQuestDetail()))
            {
                missionList[i].onClick.AddListener(() => obj.text = quest1.GetQuestDetail());
                missionList[i].onClick.AddListener(() => diffText.text = "Difficulty: " + quest1.GetDifficulty());
            } else if (textList[i].text.Equals(quest2.GetQuestDetail()))
            {
                missionList[i].onClick.AddListener(() => obj.text = quest2.GetQuestDetail());
                missionList[i].onClick.AddListener(() => diffText.text = "Difficulty: " + quest2.GetDifficulty());
            } else if (textList[i].text.Equals(quest3.GetQuestDetail()))
            {
                missionList[i].onClick.AddListener(() => obj.text = quest3.GetQuestDetail());
                missionList[i].onClick.AddListener(() => diffText.text = "Difficulty: " + quest3.GetDifficulty());
            } else if (textList[i].text.Equals(quest4.GetQuestDetail()))
            {
                missionList[i].onClick.AddListener(() => obj.text = quest4.GetQuestDetail());
                missionList[i].onClick.AddListener(() => diffText.text = "Difficulty: " + quest4.GetDifficulty());
            }
        }
    }

    private void QuestSystem()
    {
        MinimumPhone();
        StartTime();
        countDown.text = "Time Remaining: ";
        for (int i = 0; i < quests.Count; i++)
        {
            if (time == true && windowDetails.activeInHierarchy == true)
            {
                isQuest = true;
                if (obj.text == quests[i].GetQuestDetail())
                {
                    countDown.text += ((int)quests[i].GetQuestTime());
                    pointList[i].gameObject.SetActive(true);
                    if (quests[i].GetQuestTime() > 0)
                    {
                        quests[i].QuestTime -= Time.deltaTime;
                        if (finishpopup.activeInHierarchy)
                        {
                            CloseTime();
                            Time.timeScale = 0;
                            quests[i].isFinished = true;
                            animator.SetBool("moving", false);
                        }
                    }
                    else if (quests[i].GetQuestTime() <= 0)
                    {
                        isQuest = false;
                        TimeEnd();
                        quests[i].QuestTime = quests[i].GetResetTime();
                        pointList[i].gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    public void Restart()
    {
        failpopup.SetActive(false);
        if (isQuest == false)
        {
            QuestSystem();
        }
    }
    public void Cancel()
    {
        failpopup.SetActive(false);
        windowDetails.SetActive(false);
        countDown.text = string.Empty;
        MaximumPhone();          
    }

    public void OpenDetail()
    {
        windowDetails.SetActive(true);
    }

    public void CloseDetail()
    {
        windowDetails.SetActive(false);
    }

    private void TimeEnd()
    {
        time = false;
        failText.text = "You fail to complete the quest! Try again or cancel?";
        failpopup.SetActive(true);
    }

    private void StartTime()
    {
        time = true;
    }

    private void CloseTime()
    {
        time = false;
    }

    public void FinishPopup()
    {
        finishText.text = "Congratulations! You have completed the quest";
        finishpopup.SetActive(true);
    }

    public void Finish()
    {
        windowDetails.SetActive(false);
        finishpopup.SetActive(false);
        p.canMove = true;
        countDown.text = "";
        distanceText.text = "Distance:";
        isQuest = false;
        for (int i = 0; i < pointList.Count; i++)
        {
            if (pointList[i].gameObject.activeInHierarchy)
            {
                GameObject.Destroy(pointList[i].gameObject);
                GameObject.Destroy(missionList[i].gameObject);
                missionList.Remove(missionList[i]);
                textList.Remove(textList[i]);
                pointList.Remove(pointList[i]);
                quests.Remove(quests[i]);
            }
        }

    }

}
