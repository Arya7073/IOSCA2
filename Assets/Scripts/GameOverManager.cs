using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [Header("Game Manager")]
    public GameManager gameManager;

    [Header("Score Limit")]
    public int scoreLimit = 5;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI winnerText;

    private bool gameIsOver = false;



    void Update()
    {
        if (!gameIsOver && (gameManager.Player1Score >= scoreLimit || gameManager.Player2Score >= scoreLimit))
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        gameIsOver = true;

        // Determine the winner
        string winner = gameManager.Player1Score >= scoreLimit ? "Player 1" : "Player 2";
        winnerText.text = $"{winner} Wins!";

        // Show the game over panel
        gameOverPanel.SetActive(true);

        // Stop the ball and paddles
        gameManager.ball.GetComponent<Ball>().rb.velocity = Vector2.zero;
        gameManager.ball.SetActive(false);
        gameManager.player1Paddle.SetActive(false);
        gameManager.player2Paddle.SetActive(false);
    }

    public void RestartGame()
    {
        // Hide the Game Over panel
        gameOverPanel.SetActive(false);

        // Reset scores
        gameManager.Player1Score = 0;
        gameManager.Player2Score = 0;
        gameManager.Player1Text.GetComponent<TextMeshProUGUI>().text = "0";
        gameManager.Player2Text.GetComponent<TextMeshProUGUI>().text = "0";

        // Reset positions and reactivate paddles and ball
        gameManager.ball.SetActive(true);
        gameManager.ball.GetComponent<Ball>().Reset();
        gameManager.player1Paddle.SetActive(true);
        gameManager.player2Paddle.SetActive(true);

        gameIsOver = false;
    }


}
