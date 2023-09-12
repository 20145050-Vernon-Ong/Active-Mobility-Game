using UnityEngine;

public class pingpong : MonoBehaviour
{
    public float moveDistance = 1.0f;   // Total distance to move up and down.
    public float moveSpeed = 1.0f;      // Speed of the movement.
    private Vector3 initialPosition;    // Initial position of the object.
    private float yOffset;
    private void Awake()
    {
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        // Calculate the vertical movement using a sine wave.
        yOffset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        if (transform.rotation.z == 0)
        {
            transform.localPosition = initialPosition + new Vector3(yOffset, 0f, 0f);
        }
        else
        {
            transform.localPosition = initialPosition + new Vector3(0f, yOffset, 0f);
        }

    }
}
