using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishRamp : MonoBehaviour
{
    GameBrain gameBrain;
    GameObject landHereText;
    Light rampLight;
    MeshRenderer finishRamp;
    // Start is called before the first frame update
    void Start()
    {
        gameBrain = GameObject.Find("GameBrain").GetComponent<GameBrain>();
        landHereText = GameObject.Find("LandHereText");
        finishRamp = GameObject.Find("Finish Ramp").GetComponent<MeshRenderer>();
        rampLight = GameObject.Find("RampLight").GetComponent<Light>();


    }

    // Update is called once per frame
    void Update()
    {
        CheckStars();
    }

    void CheckStars()
    {
        if (gameBrain.starsCollected == gameBrain.starsToCollect)
        {
            landHereText.SetActive(false);
            finishRamp.enabled = true;
            rampLight.enabled = true;
        }
    }
}
