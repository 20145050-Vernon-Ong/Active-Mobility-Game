using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    public float speed;
    public float exitSpeed;
    private Vector3 PosX;
    public GameObject spawn;
    public GameObject spawn2;
    private Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        exitSpeed = speed;
        PosX = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.z == 0)
        {
            rigid.MovePosition(transform.position + speed * Time.deltaTime * Vector3.up);
        }
        else
        {
            rigid.MovePosition(transform.position + speed * Time.deltaTime * Vector3.down);
        }

        if (GameObject.Find("pauseMenuUI"))
        {
            Time.timeScale = 0;
        } else
        {
            Time.timeScale = 1;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("car") || collision.CompareTag("Stop"))
        {
            speed = 0;
        }
        else if (collision.CompareTag("resetCarPos"))
        {
            if (transform.rotation.z == 0)
            {
                transform.position = new Vector3(PosX.x, spawn.transform.position.y, 0);
            }
            else
            {
                transform.position = new Vector3(PosX.x, spawn2.transform.position.y, 0);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("car") || collision.CompareTag("Stop"))
        {
            speed = exitSpeed;
        } 
    }

}

