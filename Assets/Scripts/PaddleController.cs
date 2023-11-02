using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private bool isPlayer1;
    private float yBound = 3.50f;

    void Update()
    {
        float movement = 0f;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if ((touch.position.x < Screen.width / 2 && isPlayer1) ||
                (touch.position.x > Screen.width / 2 && !isPlayer1))
            {
                if (touch.position.y > Screen.height / 2)
                {
                    movement = 1;
                }
                else if (touch.position.y < Screen.height / 2)
                {
                    movement = -1;
                }
            }
        }
        movement = (isPlayer1 ? Input.GetAxisRaw("Vertical") : Input.GetAxisRaw("Vertical2"));

        Vector2 paddlePosition = transform.position;
        paddlePosition.y = Mathf.Clamp(paddlePosition.y + movement * speed * Time.deltaTime, -yBound, yBound);
        transform.position = paddlePosition;
    }
}
