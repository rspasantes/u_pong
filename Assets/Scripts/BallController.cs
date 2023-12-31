using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField] private float initialVelocity = 4f;
    [SerializeField] private float velocityMultiplier = 1.1f;

    private AudioSource audioSource;

    private bool isLaunched;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Launch() {
        gameObject.SetActive(true);
        float xVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
        float yVelocity = Random.Range(0, 2) == 0 ? 1 : -1; 
        rb2d.velocity = new Vector2(xVelocity, yVelocity) * initialVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.Play();
        if (collision.gameObject.CompareTag("Paddle")) {
            rb2d.velocity *= velocityMultiplier;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        gameObject.SetActive(false);

        if (collision.gameObject.CompareTag("Goal1"))
        {
            GameManager.Instance.playerScored("2");
        } else {
            GameManager.Instance.playerScored("1");
        }
    }

    void Update()
    {

    }
}
