using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int iScoreToWin = 5;

    // Texts
    [SerializeField] private TMP_Text txtPaddle1Score;
    [SerializeField] private TMP_Text txtPaddle2Score;
    [SerializeField] private TMP_Text txtIAStateTextP1;
    [SerializeField] private TMP_Text txtIAStateTextP2;
    [SerializeField] private TMP_Text txtEndGame;

    [SerializeField] private GameObject goStartMessage;

    // GameObjects
    [SerializeField] private GameObject goPaddle1;
    [SerializeField] private GameObject goPaddle2;
    [SerializeField] private GameObject goBall;

    // Scripts
    private PaddleController pcPaddle1;
    private PaddleController pcPaddle2;
    private IAScript iaPaddle1;
    private IAScript iaPaddle2;


    private int paddle1Score;
    private int paddle2Score;
    private bool bIsPlaying = false;

    private bool bP1isIA;
    private bool bP2isIA;

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
        txtEndGame.enabled = false;

        // Show message to start game
        goStartMessage.GetComponent<TextScript>().startBlink();

        pcPaddle1 = goPaddle1.GetComponent<PaddleController>();
        pcPaddle2 = goPaddle1.GetComponent<PaddleController>();
        iaPaddle1 = goPaddle1.GetComponent<IAScript>();
        iaPaddle2 = goPaddle1.GetComponent<IAScript>();
    }

    private void Update()
    {
        if (!bIsPlaying && Input.GetButton("Submit"))
        {
            startGame();
        }

        if (Input.GetKeyDown("1"))
        {
            pcPaddle1.enabled = !pcPaddle1.enabled;
            iaPaddle1.enabled = !iaPaddle1.enabled;

            if (iaPaddle1.enabled)
            {
                txtIAStateTextP1.text = "PLAYER1 - IA: ENABLED";
                bP1isIA = true;
            }
            else 
            {
                txtIAStateTextP2.text = "PLAYER1 - IA: DISABLED";
                bP1isIA = false;
            }
            
        }

        if (Input.GetKeyDown("2"))
        {
            pcPaddle2.enabled = !pcPaddle2.enabled;
            iaPaddle2.enabled = !iaPaddle2.enabled;

            if (iaPaddle2.enabled)
            {
                txtIAStateTextP1.GetComponent<TMP_Text>().text = "PLAYER2 - IA: ENABLED";
                bP2isIA = true;
            }
            else
            {
                txtIAStateTextP2.text = "PLAYER2 - IA: DISABLED";
                bP2isIA = false;
            }
        }

        if (Input.GetKey("escape"))
        {
            Exit();
        }
    }

    private void startGame() {
        paddle1Score = 0;
        paddle2Score = 0;
        txtPaddle1Score.text = paddle1Score.ToString();
        txtPaddle2Score.text = paddle2Score.ToString();
        bIsPlaying = true;
        goStartMessage.GetComponent<TextScript>().stopBlink();
        txtEndGame.enabled = false;
        StartCoroutine(relaunch(0));
    }

    private void endGame()
    {
        bIsPlaying = false;
        txtEndGame.text = paddle1Score.Equals(iScoreToWin) ? "PLAYER1 WINS!" : "PLAYER2 WINS";
        txtEndGame.enabled = true;
    }

    public void playerScored(string player) {

        switch (player) {
            case "1":
                paddle1Score++;
                txtPaddle1Score.text = paddle1Score.ToString();
                if (bP2isIA) 
                { 
                
                }
                break;
            case "2":
                paddle2Score++;
                txtPaddle2Score.text = paddle2Score.ToString();
                if (bP1isIA) 
                { 
                
                }
                break;
        }

        if (paddle1Score.Equals(iScoreToWin) || paddle2Score.Equals(iScoreToWin)) {
            endGame();
        } else {
            StartCoroutine(relaunch(2));
        }
    }

    public IEnumerator relaunch(int seconds)
    {
        // Esperar 2 segundos
        yield return new WaitForSeconds(seconds);

        goBall.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        goBall.transform.position = new Vector2(0, 0);
        goBall.GetComponent<BallController>().Launch();
    }

    void Exit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    Application.Quit();
        #endif
    }
}


