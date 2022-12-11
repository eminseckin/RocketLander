using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    [SerializeField] Camera mCamera;
    [SerializeField] ParticleSystem starParticles;

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
        starParticles.Stop();
        if(!starParticles.isPlaying)
        {
            starParticles.Simulate(0.1f);
            starParticles.Play();
        }
      

        if (!audioSource.isPlaying)
        {
            AudioSource.PlayClipAtPoint(audioSource.clip, mCamera.transform.position);
        }
        
    }

}
