using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] List<GameObject> livesImage;
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public GameObject gameOverMenu;
    [SerializeField] public TextMeshProUGUI finalScoreText;
    [SerializeField] public TextMeshProUGUI gameOverText;


    GameBrain gameBrain;
    TextMeshProUGUI scoreText;
    TextMeshProUGUI levelText;   


    void Start()
    {
        gameBrain = GameObject.Find("GameBrain").GetComponent<GameBrain>();
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        levelText = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        ShowPauseMenu();
        ShowLivesIcons(gameBrain.GetLives());
        scoreText.text = $"{gameBrain.starsCollected} of {gameBrain.starsToCollect}";
        levelText.text = SceneManager.GetActiveScene().name;
    }

    void ShowPauseMenu()
    {
        if (gameBrain.isGamePaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void MenuResumeButton()
    {
        gameBrain.isGamePaused = false; 
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MenuControlsButton()
    {
        SceneManager.LoadScene("Controls");
    }

    public void MenuMainMenuButton()
    {
        gameBrain.isGamePaused = false;       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Entry");
    }

    public void MenuQuitButton()
    {
        Application.Quit();
    }

    void ShowLivesIcons(int livesCount)
    {

        for (int i = 0; i < livesCount; i++)
        {
            if (livesImage[i] != null)
            {
                livesImage[i].SetActive(true);
            }           
        }


    }
}
