using UnityEngine;

public class IAScript : MonoBehaviour
{
    [SerializeField] private bool isPlayer1;
    [SerializeField] private GameObject ball;      // Referencia a la pelota
    public float speed = 7f;                      // Velocidad de la pala
    public float maxYLimit = 3.5f;                // Límite superior/inferior que la pala no puede pasar
    public float inaccuracy = 0.5f;                // Grado de inexactitud en la percepción de la posición de la pelota

    private Transform transBall;
    private Rigidbody2D rb2DBall;

    private void Start()
    {
        transBall = ball.GetComponent<Transform>();
        rb2DBall = ball.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (checkIfCanMove())
        {
            float perceivedBallPositionY = transBall.position.y + Random.Range(-inaccuracy, inaccuracy);

            // Calcula la posición objetivo basada en la velocidad
            float targetY = Mathf.Clamp(perceivedBallPositionY, -maxYLimit, maxYLimit);
            float newY = Mathf.MoveTowards(transform.position.y, targetY, speed * Time.deltaTime);

            transform.position = new Vector2(transform.position.x, newY);
        }
    }

    private bool checkIfCanMove() 
    {
        if (((isPlayer1 && (transBall.position.x < 0 && transBall.position.x > -7.5f)  && rb2DBall.velocity.x < 0)
            || (!isPlayer1 && (transBall.position.x > 0 && transBall.position.x < 7.5f) && rb2DBall.velocity.x > 0))) 
        {
            return true;
        }

        return false;
    }

    public void aumSpeed() {
        speed += 0.2f;
    }

    public void resetSpeed() {
        speed = 7f;
    }
}

