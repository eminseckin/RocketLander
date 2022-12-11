
using UnityEngine;
using UnityEngine.UI;


public class Fuel : MonoBehaviour
{  
    [SerializeField] float fuel;
    [SerializeField] float mainEngineFuelSpent;
    [SerializeField] float sideEnginesFuelSpent;

    Slider fuelSlider;

    private void Start()
    {
        fuelSlider = GameObject.Find("FuelSlider").GetComponent<Slider>();
    }

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

    public void ResetFuel()
    {
        fuel = 100;
    }
}
