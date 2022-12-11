using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank : MonoBehaviour
{
    [SerializeField] Camera mCamera;
    //[SerializeField] ParticleSystem fuelParticles;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //fuelParticles.Stop();
        //if (!fuelParticles.isPlaying)
        //{
        //    fuelParticles.Simulate(0.1f);
        //    fuelParticles.Play();
        //}


        if (!audioSource.isPlaying)
        {
            AudioSource.PlayClipAtPoint(audioSource.clip, mCamera.transform.position);
        }

    }
}
