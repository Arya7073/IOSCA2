using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Ball")]
    public GameObject ball;

    [Header("Player 1")]
    public GameObject player1Paddle;
    public GameObject player1Goal;

    [Header("Player 2")]
    public GameObject player2Paddle;
    public GameObject player2Goal;

    [Header("Score UI")]
    public GameObject Player1Text;
    public GameObject Player2Text;

    [Header("Scoreboard")]
    public ScoreboardManager scoreboardManager; // Reference to the ScoreboardManager

    public int Player1Score;
    public int Player2Score;

    public int scoreLimit = 5; // Set a score limit to end the game

    public void Player1Scored()
    {
        Player1Score++;
        Player1Text.GetComponent<TextMeshProUGUI>().text = Player1Score.ToString();
        ResetPosition();

        // Check if Player 1 wins
        if (Player1Score >= scoreLimit)
        {
            EndGame();
        }
    }

    public void Player2Scored()
    {
        Player2Score++;
        Player2Text.GetComponent<TextMeshProUGUI>().text = Player2Score.ToString();
        ResetPosition();

        // Check if Player 2 wins
        if (Player2Score >= scoreLimit)
        {
            EndGame();
        }
    }

    private void ResetPosition()
    {
        ball.GetComponent<Ball>().Reset();
        player1Paddle.GetComponent<Paddle>().Reset();
        player2Paddle.GetComponent<Paddle>().Reset();
    }

    private void EndGame()
    {
        Debug.Log($"Game Over! Player 1: {Player1Score}, Player 2: {Player2Score}");

        // Add both players' scores to the scoreboard
        if (scoreboardManager != null)
        {
            scoreboardManager.AddScore("Player 1", Player1Score);
            scoreboardManager.AddScore("Player 2", Player2Score);
            scoreboardManager.ShowScoreboard(); // Show the scoreboard
        }

        // Additional Game Over logic (e.g., disable paddles, ball, etc.)
        ball.SetActive(false);
        player1Paddle.SetActive(false);
        player2Paddle.SetActive(false);
    }
}
