using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField] private float initialVelocity = 4f;
    [SerializeField] private float velocityMultiplier = 1.1f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Launch();
    }

    private void Launch() {
        float xVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
        float yVelocity = Random.Range(0, 2) == 0 ? 1 : -1; 
        rb2d.velocity = new Vector2(xVelocity, yVelocity) * initialVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle")) {
            rb2d.velocity *= velocityMultiplier;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal1"))
        {
            GameManager.Instance.paddle1Scored();
            GameManager.Instance.restart();
            Launch();
        } else {
            GameManager.Instance.paddle2Scored();
            GameManager.Instance.restart();
            Launch();
        }
    }

    void Update()
    {
        
    }
}
