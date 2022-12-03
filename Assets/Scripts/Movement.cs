using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    // PARAMETERS - for tuning, typically set in the editor
    // CACHE - e.g. references for readability or speed
    // STATE - private instance (member) variables    

    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip sideEngines;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftEngineParticles;
    [SerializeField] ParticleSystem rightEngineParticles;
    [SerializeField] Material launchPadMaterial;


    Rigidbody rb;
    AudioSource audioSourceMainEngine;
    AudioSource audioSourceSideEngines;
    Fuel fuel;


    bool isAlive;
    public bool canRotate = true;



    void Start()
    {
        launchPadMaterial.color = new Color(0, 82/255f, 255/255f);
        rb = GetComponent<Rigidbody>();
        fuel = GetComponent<Fuel>();
        audioSourceMainEngine = GetComponents<AudioSource>()[0];
        audioSourceSideEngines = GetComponents<AudioSource>()[1];        
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }    

    void StartThrusting()
    {
        if (fuel.FuelLeft() > 0)
        {
            fuel.MainEngineFuelConsumption();
            canRotate = true;
            rb.AddRelativeForce(mainThrust * Time.deltaTime * Vector3.up);
            if (!audioSourceMainEngine.isPlaying)
            {
                audioSourceMainEngine.PlayOneShot(mainEngine);
            }
            if (!mainEngineParticles.isPlaying)
            {
                mainEngineParticles.Play();
            }
            if (gameObject.transform.position.y > 10)
            {
                GameObject.Find("Start Ramp").tag = "Untagged";
                GameObject.Find("Start Ramp").transform.GetChild(0).gameObject.GetComponent<Light>().color = Color.red; 
                launchPadMaterial.color = new Color(217/255f, 227/255f, 238/255f);
            }
        } 
        else
        {
            StopThrusting();
        }
        
    }

    void StopThrusting()
    {
        audioSourceMainEngine.Stop();
        mainEngineParticles.Stop();       
    }

    void ProcessRotation()
    {       
        if (Input.GetKey(KeyCode.A) && canRotate && fuel.FuelLeft() > 0)
        {
            fuel.SideEnginesFuelConsumption();
            ApplyRotation(Vector3.forward);
            
            if (!rightEngineParticles.isPlaying)
            {
                rightEngineParticles.Play();              
                
            }
            if (!audioSourceSideEngines.isPlaying)
            {
                audioSourceSideEngines.PlayOneShot(sideEngines);

            }

        }
        else if (Input.GetKey(KeyCode.D) && canRotate && fuel.FuelLeft() > 0)
        {
            fuel.SideEnginesFuelConsumption();
            rightEngineParticles.Stop();
            ApplyRotation(Vector3.back);
          
            if (!leftEngineParticles.isPlaying)
            {
                leftEngineParticles.Play();
            }
            if (!audioSourceSideEngines.isPlaying)
            {
                audioSourceSideEngines.PlayOneShot(sideEngines);

            }
        } 
        else
        {
            leftEngineParticles.Stop();
            rightEngineParticles.Stop();
            audioSourceSideEngines.Stop();
        }
        
       
    }

    void ApplyRotation(Vector3 rotationWay)
    {
        rb.freezeRotation = true;
        transform.Rotate(rotationThrust * Time.deltaTime * rotationWay);
        rb.freezeRotation = false;
    }
}
