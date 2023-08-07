using UnityEngine;

public class TrafficLightCar : MonoBehaviour
{
    private Animator animator;
    public GameObject cross;
    public GameObject cross2;
    // Start is called before the first frame update
    void Start()
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
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("TrafficLightGreen"))
        {
            animator.SetBool("isGreen", true);
            animator.SetBool("isRed", false);
            cross.SetActive(false);
            cross2.SetActive(false);
        }
        else if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("TrafficLightAmber"))
        {
            animator.SetBool("isAmber", true);
            animator.SetBool("isGreen", false);
            cross.SetActive(false);
            cross2.SetActive(false);
        }
        else if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("TrafficLightRed"))
        {
            animator.SetBool("isRed", true);
            animator.SetBool("isAmber", false);
            cross.SetActive(true);
            cross2.SetActive(true);
        }
    }
}
