using UnityEngine;

public class TapToMove : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                targetPosition = Camera.main.ScreenToWorldPoint(touch.position);
                targetPosition.z = transform.position.z; // Keep the same Z position as the object

                isMoving = true;
            }
        }

        if (isMoving)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
    }
}
