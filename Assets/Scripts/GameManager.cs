using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text paddle1ScoreText;
    [SerializeField] private TMP_Text paddle2ScoreText;

    [SerializeField] private Transform paddle1Transform;
    [SerializeField] private Transform paddle2Transform;
    [SerializeField] private GameObject ball;

    [SerializeField] private GameObject startMessageObject;

    private int paddle1Score;
    private int paddle2Score;
    private bool bIsPlaying;

    private static GameManager instance;

    public static GameManager Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }

    private void Start()
    {
        bIsPlaying = false;
        startMessageObject.GetComponent<TextScript>().startBlink();
    }

    private void Update()
    {
        if (!bIsPlaying && Input.GetButton("Submit"))
        {
            bIsPlaying = true;
            ball.GetComponent<BallScript>().Launch();
            startMessageObject.GetComponent<TextScript>().stopBlink();
        }
    }

    public void playerScored(string player) {

        switch (player) {
            case "1":
                paddle1Score++;
                paddle1ScoreText.text = paddle1Score.ToString();
                break;
            case "2":
                paddle2Score++;
                paddle2ScoreText.text = paddle2Score.ToString();
                break;
        }
        
        restart();
    }

    public void restart() {
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        ball.transform.position = new Vector2(0,0);
        startMessageObject.GetComponent<TextScript>().startBlink();
        bIsPlaying = false;
    }
}
