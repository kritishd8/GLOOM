using UnityEngine;
using System.Collections;

public class LanternFlicker : MonoBehaviour
{
    public float minFlickerDelay = 0.1f;
    public float maxFlickerDelay = 0.5f;
    public float flickerIntensity = 1.0f; // Initial intensity of the lantern
    public float minIntensity = 0.5f; // Minimum intensity during flicker
    public float maxIntensity = 1.0f; // Maximum intensity during flicker

    private Light lanternLight;
    private float baseIntensity;

    void Start()
    {
        lanternLight = GetComponentInChildren<Light>();
        baseIntensity = lanternLight.intensity;

        // Start the flickering coroutine
        StartCoroutine(LanternFlickerCoroutine());
    }

    IEnumerator LanternFlickerCoroutine()
    {
        while (true)
        {
            // Randomly wait before starting the flicker
            yield return new WaitForSeconds(Random.Range(minFlickerDelay, maxFlickerDelay));

            // Simulate flicker effect
            float flickerIntensity = Random.Range(minIntensity, maxIntensity);
            lanternLight.intensity = flickerIntensity;

            // Wait for a short duration
            yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));

            // Restore the base intensity
            lanternLight.intensity = baseIntensity;
        }
    }
}
