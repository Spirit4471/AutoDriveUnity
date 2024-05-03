using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedDisplay : MonoBehaviour
{
    public GameObject car;
    private CarController carController;
    private TextMeshPro speedText;
    public TextMeshProUGUI trafficLightText;
  
    public Transform trafficLight; 
    public float warningDistance = 50f; 


    public GameObject[] aiCars; 
    



    private void Start()
    {
        speedText = GetComponent<TextMeshPro>();
        carController = car.GetComponent<CarController>();
    }

    private void Update()
    {
        if (carController != null)
        {
            speedText.text = "Speed: " + carController.KPH.ToString("F1") + " KPH";
        }
        float distanceToTrafficLight = Vector3.Distance(carController.transform.position, trafficLight.position);

        if (distanceToTrafficLight < warningDistance)
        {
            speedText.text += "\nRecommendation engine activated";
        }

        if (distanceToTrafficLight < warningDistance)
        {
            trafficLightText.text = "Warning: Traffic light ahead";
        }
        else
        {
            trafficLightText.text = "";
        }
        //detecting ai cars and displaying warning and recommendation speed that player should drive at
        for (int i = 0; i < aiCars.Length; i++)
        {
            float distanceToAiCar = Vector3.Distance(carController.transform.position, aiCars[i].transform.position);
            if (distanceToAiCar < warningDistance)
            {
                speedText.text += "\nWarning: Close to AI car " + aiCars[i].name;
            }

            if (distanceToAiCar < warningDistance)
            {
                //speedText.text += "\nRecommended speed: " + ( CarAI.Max* 0.8f).ToString("F1") + " KPH";
            }
            else
            {
                trafficLightText.text = "";
            }
        }
    }

    
}

