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

        // Lógica para control táctil en dispositivos móviles
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Si el jugador toca la mitad izquierda de la pantalla y isPlayer1 es true
            // o toca la mitad derecha de la pantalla y isPlayer1 es false
            if ((touch.position.x < Screen.width / 2 && isPlayer1) ||
                (touch.position.x > Screen.width / 2 && !isPlayer1))
            {
                // Si toca la parte superior de la pantalla, sube
                if (touch.position.y > Screen.height / 2)
                {
                    movement = 1;
                }
                // Si toca la parte inferior de la pantalla, baja
                else if (touch.position.y < Screen.height / 2)
                {
                    movement = -1;
                }
            }
        }
        // Lógica para control con teclado (como ya tenías)
        movement = (isPlayer1 ? Input.GetAxisRaw("Vertical") : Input.GetAxisRaw("Vertical2"));

        Vector2 paddlePosition = transform.position;
        paddlePosition.y = Mathf.Clamp(paddlePosition.y + movement * speed * Time.deltaTime, -yBound, yBound);
        transform.position = paddlePosition;
    }
}
