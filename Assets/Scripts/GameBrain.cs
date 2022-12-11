using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBrain : MonoBehaviour
{
    [SerializeField] int lives;

    int starsCollected;
    int totalStartsCollected;
    public bool isGamePaused = false;



    static GameBrain instance = null;


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
        starsCollected+= stars;
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
        if (lives <= 0)
        {
            yield return new WaitForSeconds(time);
            SceneManager.LoadScene("GameOver");
        } 
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    

}
