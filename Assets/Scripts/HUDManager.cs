using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text livesText;
    [SerializeField] GameObject gameOverPanel;

    [SerializeField] AudioClip gameOverSFX;

    int playerScore;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: 0";
        
        gameOverPanel.SetActive(false);

        playerScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int score) 
    {
        playerScore += score;
        scoreText.text = $"Score: {playerScore}";
    }

    public void UpdateLives(int newLives)
    {
        livesText.text = $"Lives: {newLives}";
    }

    public void DisplayGameOver(Vector3 playerPosition)
    {
        gameOverPanel.SetActive(true);
        AudioSource.PlayClipAtPoint(gameOverSFX, playerPosition);
    }

    public void PlayAgainButton()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
