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
    [SerializeField] private Transform ballTransform;

    private int paddle1Score;
    private int paddle2Score;

    private static GameManager instance;

    public static GameManager Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }

    public void paddle1Scored() {

        paddle1Score++;
        paddle1ScoreText.text = paddle1Score.ToString();
        restart();
    }

    public void paddle2Scored()
    {

        paddle1Score++;
        paddle1ScoreText.text = paddle1Score.ToString();
        restart();
    }

    public void restart() {
        paddle1Transform.position = new Vector2(paddle1Transform.position.x, 0);
        paddle2Transform.position = new Vector2(paddle2Transform.position.x, 0);
        ballTransform.position = new Vector2(0,0);
    }

}
