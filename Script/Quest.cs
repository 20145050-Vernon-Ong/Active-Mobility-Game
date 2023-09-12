using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

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
        public int reward;

        public QuestClass(string QuestDetail, float QuestTime, float resetTime, string genre, string difficulty, int reward)
        {
            this.QuestDetail = QuestDetail;
            this.QuestTime = QuestTime + 1;
            this.resetTime = resetTime + 1;
            this.genre = genre;
            this.difficulty = difficulty;
            isFinished = false;
            this.reward = reward;
        }

        public string GetQuestDetail() { return QuestDetail; }

        public float GetQuestTime() { return QuestTime;}

        public float GetResetTime() { return resetTime; }

        public string GetGenre() { return genre; }

        public string GetDifficulty() { return difficulty; }

        public bool IsFinished() { return isFinished; }
        
        public int GetReward() { return reward; }

        
    }

    readonly QuestClass quest1 = new("Travel to 7-Eleven in 1 Minute 30 Seconds", 90, 90, "", "easy", 10);
    readonly QuestClass quest2 = new("Walk to the park in 2 minutes", 120, 120, "", "easy", 15);
    readonly QuestClass quest3 = new("Walk to the cemetery in 1 minute", 60, 60, "", "medium", 25);
    readonly QuestClass quest4 = new("Travel to the Adventure Bridge in 3 minutes", 180, 180, "", "hard", 40);
    readonly QuestClass quest5 = new("Wait for the taxi at the carpark", 50, 50, "", "medium", 30);
    
    public GameObject windowDetails;
    public GameObject maxPhone;
    public GameObject failpopup;
    public GameObject finishpopup;
    public GameObject pointer;

    public Button confirmBtn;

    private Vector3 originalPos;
    private Vector3 pointerPos;

    public TMP_Text obj;
    public TMP_Text countDown;
    public TMP_Text active;
    public TMP_Text failText;
    public TMP_Text diffText;
    public TMP_Text finishText;
    public TMP_Text distanceText;
    public TMP_Text rewardText;

    bool time = false;
    bool isQuest = false;
    bool isTracking = false;

    private float distance;
    public float hideDistance;

    private readonly List<QuestClass> quests = new();
    public List<Transform> pointList = new();
    public List<TMP_Text> textList = new();
    public List<Button> missionList = new();

    private PlayerMovement p;
    private Animator animator;
    private GC addPoint;
    // Start is called before the first frame update
    void Awake()
    {
        quests.Add(quest1);
        quests.Add(quest2);
        quests.Add(quest3);
        quests.Add(quest4);
        quests.Add(quest5);
        for (int i = 0; i < pointList.Count; i++)
        {
            missionList[i].gameObject.SetActive(true);
            textList[i].text = quests[i].GetQuestDetail();
            pointList[i].transform.gameObject.SetActive(false);
        }
        pointerPos = pointer.transform.position;
        pointer.SetActive(false);
        finishpopup.SetActive(false);
        windowDetails.SetActive(false);
        failpopup.SetActive(false);
        finishpopup.SetActive(false);
        p = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        addPoint = GetComponent<GC>();
    }
    void Start()
    {
        OpenQuestDetails();  

    }
    // Update is called once per frame
    void Update()
    {
        Mission();
        if (isTracking) {

        }
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
                var dir = pointList[i].position - transform.position;
                if (pointList[i].gameObject.activeInHierarchy)
                {
                    distance = (pointList[i].transform.position - transform.position).magnitude;
                    distanceText.text = "Distance: " + distance.ToString("F1") + " meters";
                    isTracking = true;
                    if (isTracking)
                    {
                        pointer.SetActive(true);
                        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    }
                } 
            }
        }
    }

    public void MaximumPhone()
    {
         maxPhone.transform.localPosition = new Vector3(5, -50, 0);
    }

    public void MinimumPhone()
    {
        maxPhone.transform.localPosition = new Vector3(-5, -464.6f, 0);
    }

    private void OpenQuestDetails()
    {
        confirmBtn.onClick.AddListener(() => QuestSystem());
        for (int i = 0; i < missionList.Count; i++)
        {
            if (textList[i].text.Equals(quest1.GetQuestDetail()))
            {
                missionList[i].onClick.AddListener(() => obj.text = quest1.GetQuestDetail());
                missionList[i].onClick.AddListener(() => diffText.text = "Difficulty: " + quest1.GetDifficulty());
                missionList[i].onClick.AddListener(() => rewardText.text = "Reward: " + quest1.GetReward() + " points");
            }
            else if (textList[i].text.Equals(quest2.GetQuestDetail()))
            {
                missionList[i].onClick.AddListener(() => obj.text = quest2.GetQuestDetail());
                missionList[i].onClick.AddListener(() => diffText.text = "Difficulty: " + quest2.GetDifficulty());
                missionList[i].onClick.AddListener(() => rewardText.text = "Reward: " + quest2.GetReward() + " points");
            }
            else if (textList[i].text.Equals(quest3.GetQuestDetail()))
            {
                missionList[i].onClick.AddListener(() => obj.text = quest3.GetQuestDetail());
                missionList[i].onClick.AddListener(() => diffText.text = "Difficulty: " + quest3.GetDifficulty());
                missionList[i].onClick.AddListener(() => rewardText.text = "Reward: " + quest3.GetReward() + " points");
            }
            else if (textList[i].text.Equals(quest4.GetQuestDetail()))
            {
                missionList[i].onClick.AddListener(() => obj.text = quest4.GetQuestDetail());
                missionList[i].onClick.AddListener(() => diffText.text = "Difficulty: " + quest4.GetDifficulty());
                missionList[i].onClick.AddListener(() => rewardText.text = "Reward: " + quest4.GetReward() + " points");
            }
            else if (textList[i].text.Equals(quest5.GetQuestDetail()))
            {
                missionList[i].onClick.AddListener(() => obj.text = quest5.GetQuestDetail());
                missionList[i].onClick.AddListener(() => diffText.text = "Difficulty: " + quest5.GetDifficulty());
                missionList[i].onClick.AddListener(() => rewardText.text = "Reward: " + quest5.GetReward() + " points");
            }
            missionList[i].onClick.AddListener(() => OpenDetail());
        }
    }

    public void Pos()
    {
        originalPos = p.pedestrian.transform.position;
    }

    private void QuestSystem()
    {
        StartTime();
        windowDetails.SetActive(false);
        countDown.text = "Time Remaining: ";
        for (int i = 0; i < quests.Count; i++)
        {
            missionList[i].enabled = false;
            if (obj.text == quests[i].GetQuestDetail())
            {
                isQuest = true;
                countDown.text += (int)quests[i].GetQuestTime();
                pointList[i].gameObject.SetActive(true);
                StartTime();
                if (quests[i].GetQuestTime() > 0)
                {
                    quests[i].QuestTime -= Time.deltaTime;

                    if (finishpopup.activeInHierarchy && time == false)
                    {
                        Time.timeScale = 0;
                        quests[i].isFinished = true;
                        //animator.SetBool("moving", false);
                    }
                }
                else if (quests[i].GetQuestTime() <= 0)
                {
                    p.canMove = false;
                    isQuest = false;
                    animator.SetBool("moving", false);
                    TimeEnd();
                    quests[i].QuestTime = quests[i].GetResetTime();
                    pointList[i].gameObject.SetActive(false);
                }




            }
        }
    }

    public void Restart()
    {
        p.canMove = true;
        animator.SetBool("moving", true);
        failpopup.SetActive(false);
        p.pedestrian.transform.position = originalPos;
        if (isQuest == false)
        {
            QuestSystem();
        }

    }
    public void Cancel()
    {
        p.canMove = true;
        animator.SetBool("moving", true);
        failpopup.SetActive(false);
        windowDetails.SetActive(false);
        countDown.text = string.Empty;
        distanceText.text = "Distance: ";
        for (int i = 0; i < missionList.Count; i++)
        {
            missionList[i].enabled = true;
        }
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
        CloseTime();
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
        //pointer.transform.position = pointerPos;
        Time.timeScale = 1;
        windowDetails.SetActive(false);
        finishpopup.SetActive(false);
        p.canMove = true;
        countDown.text = string.Empty;
        distanceText.text = "Distance:";
        isQuest = false;
        isTracking = false;
        for (int i = 0; i < pointList.Count; i++)
        {
            missionList[i].enabled = true;
            if (pointList[i].gameObject.activeInHierarchy)
            {
                
                if (!isTracking)
                {
                    pointer.SetActive(false);
                    //addPoint.totalpoints += quests[i].GetReward();
                    //addPoint.ValueText.text = addPoint.totalpoints.ToString();
                    GameObject.Destroy(pointList[i].gameObject);
                    GameObject.Destroy(missionList[i].gameObject);
                    GameObject.Destroy(textList[i].gameObject);
                    pointList.RemoveAt(i);
                    missionList.RemoveAt(i);
                    textList.RemoveAt(i);
                    quests.Remove(quests[i]);
                }
            }
        }

    }

}
