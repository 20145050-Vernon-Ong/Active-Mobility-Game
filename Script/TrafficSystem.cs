using UnityEngine;

public class TrafficSystem : MonoBehaviour
{
    private Animator animator;

    public GameObject bomb;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Traffic();
    }
    void Traffic()
    {
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("TrafficLightRed"))
        {
            animator.SetBool("isRed", true);
            animator.SetBool("isGreen", false);
            bomb.SetActive(true);
        }
        else if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("TrafficLightGreen"))
        {
            animator.SetBool("isGreen", true);
            animator.SetBool("isRed", false);
            bomb.SetActive(false);
        }
        
    }
}
