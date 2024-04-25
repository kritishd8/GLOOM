using UnityEngine;
using System.Collections;

public class LightningController : MonoBehaviour
{
    public Material skyboxMaterial;
    public float maxIntensity = 2f; // Adjust this value to control the intensity of the lightning
    public float lightningDuration = 0.1f; // Duration of the lightning effect

    private float originalExposure;

    void Start()
    {
        // Store the original exposure value of the skybox
        originalExposure = skyboxMaterial.GetFloat("_Exposure");

        // Call DoLightning directly to start the lightning effect immediately
        StartCoroutine(DoLightning());
    }

    IEnumerator DoLightning()
    {
        // Set a high intensity for the skybox during lightning
        skyboxMaterial.SetFloat("_Exposure", maxIntensity);

        // Wait for the lightning duration
        yield return new WaitForSeconds(lightningDuration);

        // Restore the original intensity of the skybox
        skyboxMaterial.SetFloat("_Exposure", originalExposure);

        // Trigger the lightning effect again for continuous lightning
        StartCoroutine(DoLightning());
    }
}
