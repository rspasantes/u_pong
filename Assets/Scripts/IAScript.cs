using UnityEngine;

public class IAScript : MonoBehaviour
{
    [SerializeField] private bool isPlayer1;
    [SerializeField] private GameObject ball;
    [SerializeField] private float velocitySum = 0.6f;
    public float speed;           
    public float maxYLimit = 3.5f;          
    public float inaccuracy = 0.5f;
    

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
        speed += velocitySum;
        Debug.Log("Aumentamos velocidad " + speed);
    }

    public void resetSpeed() {
        speed = 7f;
    }
}

