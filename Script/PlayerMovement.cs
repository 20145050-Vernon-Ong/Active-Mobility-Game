using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public bool canMove;

    public GameObject amUser;
    public GameObject pedestrian;

    [SerializeField]
    private FixedJoystick joystick;
    [SerializeField]
    private float movespeed;

    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;
    void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        amUser.SetActive(false);
    }
    // Update is called once per frame 
    void Update()
    {
        if (!canMove)
        {
            myRigidBody.velocity = Vector2.zero;
            return;
        }
        change = Vector3.zero;
        /*change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");*/
        Debug.Log(change);
        //myRigidBody.velocity = new Vector2(joystick.Vertical * movespeed, myRigidBody.velocity.y);
        //myRigidBody.velocity = new Vector2(joystick.Horizontal * movespeed, myRigidBody.velocity.x);
        change.x = new Vector2(joystick.Horizontal* movespeed, myRigidBody.velocity.x).x;
        change.y = new Vector2(joystick.Vertical * movespeed, myRigidBody.velocity.y).x;
        UpdateAnimationAndMove();
        
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        myRigidBody.MovePosition(transform.position + speed * Time.deltaTime * change);
    }

    public void OnTriggerEnter2D(Collider2D popupBox)
    {
        if (popupBox.CompareTag("trafficBox"))
        {
            canMove = false;
        }
    }

    public void TrafficStopLook()
    {
        canMove = true;
    }

    public void TrafficProceed()
    {
        canMove = true;
    }

}

