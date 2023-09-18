using TMPro;
using UnityEngine;

public class TrafficSystem : MonoBehaviour
{
    private Animator animator;
    public GameObject bomb;
    public GameObject bomb2;
    public GameObject arrowUp;
    public GameObject arrowLeftRight;
    public GameObject arrowUp2;
    public GameObject arrowLeftRight2;
    private float currentTime = 0;
    private float currentTime2 = 0;
    private readonly float startingTime = 12;
    private readonly float startingTime2 = 12;
    public float moveDistance;   // Total distance to move up and down.
    public float moveSpeed;      // Speed of the movement.
    public TextMeshPro ValueText;//fortext
    public TextMeshPro ValueText2;//fortext
    private void StartTime()
    {
        currentTime = startingTime;
    }
    
    private void StartTime2()
    {  
        currentTime2 = startingTime2;
    }
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        currentTime2 -= 1 * Time.deltaTime;
        if (currentTime <= 0)
        {
            currentTime = 12;
        } 
        if (currentTime2 <= 0)
        {
            currentTime2 = 12;
        }
        ValueText.text = "Cross now (" + currentTime.ToString("0") + ")";
        ValueText2.text = "Cross now (" + currentTime2.ToString("0") + ")";
        Traffic();
        TrafficJunction();
    }

    void Traffic()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("TrafficLightSideRed"))
        {
            animator.SetBool("isGreen", false);
            animator.SetBool("isRed", true);
            currentTime = 0;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("TrafficLightSideGreen"))
        {
            animator.SetBool("isRed", false);
            animator.SetBool("isGreen", true);
        }/* else if (animator.GetCurrentAnimatorStateInfo(0).IsName("TrafficLightAmberPedestrian"))
        {
            animator.SetBool("isAmber", true);
            animator.SetBool("isRed", false);
            animator.SetBool("isGreen", false);
            bomb.SetActive(true);
            bomb2.SetActive(true);
        }*/
        
    }

    void TrafficJunction()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("TrafficJunctionRed") || animator.GetCurrentAnimatorStateInfo(0).IsName("TrafficJunctionRed[Right]"))
        {
            StartTime();
            animator.SetBool("isGreen", false);
            animator.SetBool("isRed", true);
            bomb.SetActive(true);
            bomb2.SetActive(false);
            arrowUp.SetActive(false);
            arrowUp2.SetActive(false);
            arrowLeftRight.SetActive(true);
            arrowLeftRight2.SetActive(true);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("TrafficJunctionGreen") || animator.GetCurrentAnimatorStateInfo(0).IsName("TrafficJunctionGreen[Right]"))
        {
            StartTime2();
            animator.SetBool("isRed", false);
            animator.SetBool("isGreen", true);
            bomb.SetActive(false);
            bomb2.SetActive(true);
            arrowUp.SetActive(true);
            arrowUp2.SetActive(true);
            arrowLeftRight.SetActive(false);
            arrowLeftRight2.SetActive(false);
        } 
    }

}

