
using UnityEngine;
using UnityEngine.UI;


public class Fuel : MonoBehaviour
{    
    [SerializeField] Slider fuelSlider;
    [SerializeField] float fuel;
    [SerializeField] float mainEngineFuelSpent;
    [SerializeField] float sideEnginesFuelSpent;


    // Update is called once per frame
    void Update()
    {
        fuelSlider.value = fuel;
    }

    public void MainEngineFuelConsumption()
    {       
        
        fuel -= mainEngineFuelSpent * Time.deltaTime;        
    }

    public void SideEnginesFuelConsumption()
    {

        fuel -= sideEnginesFuelSpent * Time.deltaTime;
    }

    public float FuelLeft()
    {
        return fuel;
    }
}
