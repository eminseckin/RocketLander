using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2.2f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] List<GameObject> rocketBodyObjects;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    Rigidbody rb;
    TextMeshProUGUI infoText;
    GameBrain gameBrain;
    Fuel fuel;

    void Start()
    {
       rb = GetComponent<Rigidbody>();
       gameBrain= GameObject.Find("GameBrain").GetComponent<GameBrain>();
       infoText = GameObject.Find("InfoText").GetComponent<TextMeshProUGUI>();       
       fuel = GameObject.Find("Rocket").GetComponent<Fuel>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled= !collisionDisabled;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisabled) { return;}       

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                GetComponent<Movement>().canRotate = false;
                break;
            case "Finish":
                GetComponent<Movement>().canRotate = false;
                if (Mathf.Abs(gameObject.transform.rotation.z) <= 0.08)
                {
                    StartSuccessSequence();        
                }
                break;                   
            default:
                StartCrashSequence();                
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Barrier")
        {
            infoText.text = "You are leaving mission area... \n" +
                            "Returning to base !";    
            Invoke("ReloadLevel", 3f);
        }

        if (other.gameObject.tag == "Star")
        {
            gameBrain.IncreaseStarsCollected(1);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Fuel Tank")
        {            
            fuel.ResetFuel();
            Destroy(other.gameObject);
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        GameObject.Find("Main Camera").GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
        GetComponent<Movement>().enabled = false;
        GetComponents<AudioSource>()[0].Stop();
        GetComponents<AudioSource>()[1].Stop();
        GetComponent<AudioSource>().PlayOneShot(crash);
        crashParticles.Play();    
        rocketBodyObjects[0].transform.Translate(RandomNumber(), RandomNumber(), RandomNumber());      
        rocketBodyObjects[1].transform.Translate(RandomNumber(), RandomNumber(), RandomNumber());       
        rocketBodyObjects[2].transform.Translate(RandomNumber(), RandomNumber(), RandomNumber());      
        rocketBodyObjects[3].transform.Translate(RandomNumber(), RandomNumber(), RandomNumber());       
        rocketBodyObjects[4].transform.Translate(RandomNumber(), RandomNumber(), RandomNumber());
        gameBrain.DecreaseLivesCount();
        StartCoroutine(gameBrain.CheckGameStatus(2f));
        Invoke("ReloadLevel", levelLoadDelay);
    }

    float RandomNumber()
    {
        return Random.Range(-0.2f, 0.2f);
    }

    void StartSuccessSequence()
    {
        isTransitioning= true;
        GetComponent<Movement>().enabled = false;
        rb.constraints = RigidbodyConstraints.FreezePosition;
        GetComponents<AudioSource>()[0].Stop();
        GetComponents<AudioSource>()[1].Stop();
        GetComponent<AudioSource>().PlayOneShot(success);
        successParticles.Play();
        gameBrain.FinishLevelStarsCount();
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;        
        SceneManager.LoadScene(currentSceneIndex);
        gameBrain.RestartLevelStarsCount();
    }    

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex= 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
