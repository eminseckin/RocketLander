using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] List<GameObject> rocketBodyObjects;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    Rigidbody rb;
    GameObject outOfBoundsText;

    void Start()
    {
       rb = GetComponent<Rigidbody>();
       outOfBoundsText = GameObject.Find("Out Of Bounds Text");
        outOfBoundsText.SetActive(false);
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
            case "Barrier":
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
            outOfBoundsText.SetActive(true);
            Invoke("ReloadLevel", 3f);
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
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(success);
        successParticles.Play();        
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;        
        SceneManager.LoadScene(currentSceneIndex);
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
