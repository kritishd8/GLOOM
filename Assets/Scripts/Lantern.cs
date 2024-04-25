using UnityEngine;
using System;
using System.Collections;
using TMPro;

public class Lantern : MonoBehaviour
{
    private bool isCarried = false;
    private Light lanternLight;
    private ParticleSystem fireParticleSystem;
    public AudioClip turnOnSound;
    public AudioClip turnOffSound;
    public AudioClip textVisibleSound; // New audio clip for text visibility
    private AudioSource audioSource;
    public TextMeshPro interactionText;
    public float interactionDistance = 2f;
    private Transform playerTransform;
    private bool wasTextVisible = false; // Track if the text was visible in the previous frame

    // Event triggered when the lantern is picked up
    public static event Action OnLanternPickup;

    void Start()
    {
        lanternLight = GetComponentInChildren<Light>();
        fireParticleSystem = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        interactionText.gameObject.SetActive(false);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        
        // Check if the interaction text has just become visible
        bool isTextVisible = distanceToPlayer <= interactionDistance && !isCarried;
        if (isTextVisible && !wasTextVisible)
        {
            // Play the audio clip for text visibility
            if (textVisibleSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(textVisibleSound);
            }
        }
        
        // Update the interaction text visibility state
        if (isTextVisible)
        {
            interactionText.gameObject.SetActive(true);
        }
        else
        {
            interactionText.gameObject.SetActive(false);
        }

        // Update the state of the text visibility for the next frame
        wasTextVisible = isTextVisible;
    }

    public void PickUp()
    {
        isCarried = true;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.SetParent(playerTransform);
        transform.localPosition = new Vector3(0.5f, 1f, 0.7f);
        transform.localRotation = Quaternion.identity;

        if (OnLanternPickup != null)
            OnLanternPickup();

        interactionText.gameObject.SetActive(false);
    }

    public void Drop()
    {
        isCarried = false;
        GetComponent<Rigidbody>().isKinematic = false;
        transform.SetParent(null);
    }

    public bool IsCarried()
    {
        return isCarried;
    }

    public void ToggleLight()
    {
        if (lanternLight.enabled)
        {
            StartCoroutine(ToggleLightCoroutine(lanternLight.intensity, 0));
            if (turnOffSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(turnOffSound);
            }
        }
        else
        {
            if (turnOnSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(turnOnSound);
            }
            StartCoroutine(ToggleLightCoroutine(0, 1));
        }

        var emission = fireParticleSystem.emission;
        emission.enabled = !lanternLight.enabled;
    }

    private IEnumerator ToggleLightCoroutine(float startIntensity, float targetIntensity)
    {
        float duration = 1f;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            lanternLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, t);

            yield return null;
        }

        lanternLight.intensity = targetIntensity;
        lanternLight.enabled = targetIntensity > 0;
    }
}