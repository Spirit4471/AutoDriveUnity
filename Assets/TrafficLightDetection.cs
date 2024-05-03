using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightDetection : MonoBehaviour
{
    public float detectionDistance = 100f;

    private Camera camera;

    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out hit, detectionDistance))
        {
            if (hit.transform.CompareTag("TrafficLight"))
            {
                Debug.Log("Detected a traffic light");
                // TODO: Display a warning
            }
            
        }
    }

}

