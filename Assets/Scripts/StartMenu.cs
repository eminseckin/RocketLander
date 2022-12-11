using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    GameBrain gameBrain;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameObject.Find("GameBrain"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMenuPlayButton()
    {
        SceneManager.LoadScene("Level 1");
    }
}
