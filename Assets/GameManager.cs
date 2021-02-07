using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Player
    public Transform player;
    private Vector3 playerPos;
    private Vector3 playerRot;

    // Texts
    public TextMeshProUGUI currScoreText;
    public TextMeshProUGUI bestScoreText;

    // Track scores
    private float currScore = 0.0f;
    private float bestScore = 0.0f;

    // Track game state
    private bool isGameActive = false;

    // Start is called before the first frame update
    void Start()
    {
        // Start game
        isGameActive = true;

        // Remember player's starting position
        playerPos = new Vector3(player.position.x, player.position.y, player.position.z);
        playerRot = new Vector3(player.eulerAngles.x, player.eulerAngles.y, player.eulerAngles.z);

        // Set up scores
        currScoreText.text = "Timer: " + currScore;
        bestScoreText.text = "Best: " + bestScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive)
        {
            currScore += Time.deltaTime;
        }

        currScoreText.text = "Timer: " + currScore;
        bestScoreText.text = "Best: " + bestScore;
    }

    // Detect Game Over
    void GameOver()
    {
        // Set scores
        if (bestScore == 0 || currScore < bestScore)
        {
            bestScore = currScore;
        }
        currScore = 0.0f;

        // Move player back to start
        player.position = playerPos;
        player.eulerAngles = playerRot;
    }
}
