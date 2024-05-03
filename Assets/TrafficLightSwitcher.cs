using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TrafficLightSwitcher : MonoBehaviour
{
    public GameObject redLight;
    public GameObject yellowLight;
    public GameObject greenLight;

    public float redDuration = 5.0f;
    public float yellowDuration = 2.0f;
    public float greenDuration = 5.0f;

    private Renderer redRenderer;
    private Renderer yellowRenderer;
    private Renderer greenRenderer;

    private IEnumerator Start()
    {
        redRenderer = redLight.GetComponent<Renderer>();
        yellowRenderer = yellowLight.GetComponent<Renderer>();
        greenRenderer = greenLight.GetComponent<Renderer>();

        while (true)
        {
            // Red light
            SetEmissionIntensity(redRenderer, 1.0f);
            SetEmissionIntensity(yellowRenderer, 0.0f);
            SetEmissionIntensity(greenRenderer, 0.0f);
            yield return new WaitForSeconds(redDuration);

            // Yellow light
            SetEmissionIntensity(redRenderer, 0.0f);
            SetEmissionIntensity(yellowRenderer, 1.0f);
            SetEmissionIntensity(greenRenderer, 0.0f);
            yield return new WaitForSeconds(yellowDuration);

            // Green light
            SetEmissionIntensity(redRenderer, 0.0f);
            SetEmissionIntensity(yellowRenderer, 0.0f);
            SetEmissionIntensity(greenRenderer, 1.0f);
            yield return new WaitForSeconds(greenDuration);
        }
    }

   private void SetEmissionIntensity(Renderer renderer, float intensity)
{
    Material material = renderer.material;
    Color mainColor = material.GetColor("_Color");
    material.SetColor("_Illum", mainColor * intensity);
}

}


