using UnityEngine;
using System.Collections;

public class SceneDarkener : MonoBehaviour
{
    public Light directionalLight;
    public GameObject[] streetLampPrefabs;
    public float delayBeforeDarkening = 3f;
    public float darkeningSpeed = 0.1f;
    public float maxIntensityReduction = 0.5f; // Maximum reduction in directional light intensity
    public float maxFogDensity = 3f; // Maximum fog density
    public AudioClip darkeningSoundClip; // Audio clip to be played when the scene darkens

    public bool isDarkening = false;
    private bool hasPlayedDarkeningSound = false; // Track whether the darkening sound has been played
    private float originalFogDensity; // Store the original fog density
    private AudioSource audioSource; // Reference to AudioSource component to play the sound

    void Start()
    {
        // Store the original fog density
        originalFogDensity = RenderSettings.fogDensity;

        // Subscribe to the lantern pickup event
        Lantern.OnLanternPickup += StartDarkeningProcess;

        // Get or add an AudioSource component to this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Assign the audio clip to the AudioSource
        if (darkeningSoundClip != null)
            audioSource.clip = darkeningSoundClip;
    }

    void StartDarkeningProcess()
    {
        // Start the darkening process after a delay if it hasn't started already
        if (!isDarkening)
            StartCoroutine(DarkenSceneAfterDelay());
    }

    IEnumerator DarkenSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeDarkening);

        // Play the darkening sound if it hasn't been played before
        if (!hasPlayedDarkeningSound && audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
            hasPlayedDarkeningSound = true;
        }

        // Begin darkening the scene
        isDarkening = true;

        // Turn off street lamps
        foreach (GameObject prefab in streetLampPrefabs)
        {
            TurnOffStreetLampLights(prefab);
        }

        // Increase fog density
        RenderSettings.fogDensity = maxFogDensity;
    }

    void TurnOffStreetLampLights(GameObject prefab)
    {
        // Find all point lights and spot lights in the street lamp prefab and turn them off
        Light[] lights = prefab.GetComponentsInChildren<Light>();
        foreach (Light light in lights)
        {
            light.enabled = false;
        }
    }

    void Update()
    {
        // Gradually decrease the intensity of the directional light while darkening
        if (isDarkening && directionalLight.intensity > 0)
        {
            // Calculate the amount of intensity reduction based on the darkening speed and maximum reduction
            float intensityReduction = darkeningSpeed * Time.deltaTime;
            float newIntensity = directionalLight.intensity - intensityReduction;

            // Clamp the intensity to prevent it from going below 0 or beyond the maximum reduction
            directionalLight.intensity = Mathf.Clamp(newIntensity, 0f, directionalLight.intensity + maxIntensityReduction);
        }
    }

    // Unsubscribe from the lantern pickup event when the script is disabled
    void OnDisable()
    {
        Lantern.OnLanternPickup -= StartDarkeningProcess;

        // Reset fog density to its original value
        RenderSettings.fogDensity = originalFogDensity;
    }
}
