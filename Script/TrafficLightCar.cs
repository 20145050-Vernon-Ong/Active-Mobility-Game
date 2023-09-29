using TMPro;
using UnityEngine;

public class TrafficLightCar : MonoBehaviour
{
    private Animator animator;
    public GameObject cross;
    public GameObject cross2;
    public GameObject bomb;
    public GameObject timer;
    public TMP_Text timerText;
    private float time = 12;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        TrafficCar();
    }
    void TrafficCar()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("TrafficLightGreen"))
        {
            animator.SetBool("isGreen", true);
            animator.SetBool("isRed", false);
            cross.SetActive(false);
            cross2.SetActive(false);
            bomb.SetActive(true);
            timer.SetActive(false);
            time = 12;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("TrafficLightAmber"))
        {
            animator.SetBool("isAmber", true);
            animator.SetBool("isGreen", false);
            cross.SetActive(false);
            cross2.SetActive(false);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("TrafficLightRed"))
        {
            animator.SetBool("isRed", true);
            animator.SetBool("isAmber", false);
            cross.SetActive(true);
            cross2.SetActive(true);
            bomb.SetActive(false);
            timer.SetActive(true);
            timerText.text = "Cross now (" + (time -= 1 * Time.deltaTime).ToString("0") + ")";
        }
    }
}
