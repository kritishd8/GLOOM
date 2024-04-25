using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(AudioSource))]
public class ScreenFader : MonoBehaviour
{
    public float fadeInSpeed = 1.0f;
    public float fadeOutSpeed = 0.5f;
    public AudioClip audioClip;
    public AudioClip additionalAudioClip; // New audio clip to play after the fade out
    public CarController carController; // Reference to the CarController script
    public Text endText; // Reference to the "End" text

    private Image fadeImage;
    private AudioSource audioSource;

    private void Start()
    {
        fadeImage = GetComponent<Image>();
        fadeImage.color = Color.black;
        fadeImage.CrossFadeAlpha(0, fadeInSpeed, false);

        audioSource = GetComponent<AudioSource>();
        if (audioSource != null && audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        Invoke("TurnOffCarLights", fadeInSpeed); // Invoke the method after the fade in is complete
    }

    private void TurnOffCarLights()
    {
        if (carController != null)
        {
            carController.TurnOffHeadlights();
        }
    }

    // Call this function to fade the screen to darkness at the end of the game
    public void FadeOut()
    {
        if (fadeImage != null)
        {
            fadeImage.CrossFadeAlpha(1, fadeOutSpeed, false);
            Invoke("PlayAdditionalAudioClip", fadeOutSpeed);
            Invoke("ShowEndText", fadeOutSpeed); // Show the "End" text after the fade out
        }
    }

    private void PlayAdditionalAudioClip()
    {
        if (audioSource != null && additionalAudioClip != null)
        {
            audioSource.clip = additionalAudioClip;
            audioSource.Play();
        }
    }

    private void ShowEndText()
    {
        if (endText != null)
        {
            endText.gameObject.SetActive(true);
        }
    }
}
