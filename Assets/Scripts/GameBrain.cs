using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBrain : MonoBehaviour
{
    [SerializeField] int lives;

    public int starsCollected;
    int totalStartsCollected;
    public int starsToCollect = 5;
    public bool isGamePaused = false;
    public int level = 0;
    public bool gameCompleted = false;

    static GameBrain instance = null;
    GameCanvas canvas;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        
    }

    void Update()
    {
       PauseGame(); 
    }   

    void PauseGame()
    {
        if (SceneManager.GetActiveScene().name != "Entry") 
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !isGamePaused)
            {
                Time.timeScale = 0f;
                isGamePaused = true;                

            }
            
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape) && isGamePaused)
                {
                    Time.timeScale = 1f;
                    isGamePaused = false;                    
                }
               
            }
        }
        
    }

    public int GetLives()
    {
        return lives;
    }

    public int GetStarsCollected()
    {
        return totalStartsCollected;
    }

    public void IncreaseStarsCollected(int stars)
    {
        starsCollected += stars;
        totalStartsCollected += stars;
    }

    public void RestartLevelStarsCount()
    {
        totalStartsCollected -= starsCollected;
        starsCollected= 0;
    }

    public void FinishLevelStarsCount()
    {
        starsCollected = 0;
    }
   

    public void DecreaseLivesCount()
    {
        lives -= 1;
    }

    public IEnumerator CheckGameStatus(float time)
    {
        if (lives <= 0 )
        {
            yield return new WaitForSeconds(time);
            GameOver();
        } 
        else
        {

        } 
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void GameOver()
    {
        if (SceneManager.GetActiveScene().name == "Level 10" && gameCompleted )
        {
            canvas = GameObject.Find("Canvas").GetComponent<GameCanvas>();
            canvas.gameOverMenu.SetActive(true);
            canvas.gameOverText.text = "Congratulations";
            canvas.finalScoreText.text = "You beat the game";
            
        } else
        {
            canvas = GameObject.Find("Canvas").GetComponent<GameCanvas>();
            canvas.gameOverMenu.SetActive(true);
            canvas.finalScoreText.text = $"Max level reached: {SceneManager.GetActiveScene().name}";
        }
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    

}
