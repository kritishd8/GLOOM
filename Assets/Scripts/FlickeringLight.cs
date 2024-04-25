using UnityEngine;
using System.Collections;
public class ScaryFlickeringLight : MonoBehaviour
{
    public float minFlickerDelay = 0.5f;
    public float maxFlickerDelay = 3.0f;
    public float flickerDuration = 0.5f;
    public float maxIntensity = 2.0f;

    private Light[] pointLights;
    private Light[] spotLights;

    void Start()
    {
        pointLights = transform.GetComponentsInChildren<Light>();
        spotLights = transform.GetComponentsInChildren<Light>();

        // Start the flickering coroutine
        StartCoroutine(ScaryFlicker());
    }

    IEnumerator ScaryFlicker()
    {
        while (true)
        {
            // Wait for a random delay before starting the flicker
            yield return new WaitForSeconds(Random.Range(minFlickerDelay, maxFlickerDelay));

            // Randomly choose flicker behavior
            float flickerStartTime = Time.time;
            bool flickerSlow = Random.value < 0.5f; // 50% chance for slow flicker

            while (Time.time < flickerStartTime + flickerDuration)
            {
                // Calculate intensity based on flicker behavior
                float flickerIntensity = flickerSlow ? SlowFlickerIntensity() : InstantFlickerIntensity();

                // Apply intensity to lights
                for (int i = 0; i < pointLights.Length; i++)
                {
                    pointLights[i].intensity = flickerIntensity;
                    spotLights[i].intensity = flickerIntensity;
                }

                // Wait for a short time
                yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
            }
        }
    }

    // Calculate intensity for slow flicker
    float SlowFlickerIntensity()
    {
        float baseIntensity = Random.Range(0.1f, 0.8f) * maxIntensity;
        return baseIntensity * Mathf.PerlinNoise(Time.time * Random.Range(0.5f, 1.0f), 0);
    }

    // Calculate intensity for instant flicker
    float InstantFlickerIntensity()
    {
        return Random.value < 0.5f ? maxIntensity : 0f; // 50% chance for instant flicker on/off
    }
}
