using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private bool isPlayer1;
    private float yBound = 3.50f;

    void Update()
    {
        float movement = (isPlayer1 ? Input.GetAxisRaw("Vertical") : Input.GetAxisRaw("Vertical2"));

        Vector2 paddlePosition = transform.position;
        paddlePosition.y = Mathf.Clamp(paddlePosition.y + movement * speed * Time.deltaTime, -yBound, yBound);
        transform.position = paddlePosition;
    }
}
