using UnityEngine;
using TMPro;

public class ObjectInteraction : MonoBehaviour
{
    public float interactionDistance = 3f; // Distance from the object to trigger interaction
    public AudioClip interactionAudioClip; // Audio clip to play when interaction text is shown
    public TextMeshPro interactionTextMeshPro; // Reference to the TextMeshPro component for the interaction prompt

    private Transform playerTransform; // Reference to the player's transform

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Find the player's transform
        interactionTextMeshPro.gameObject.SetActive(false); // Hide interaction text initially
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= interactionDistance)
        {
            if (!interactionTextMeshPro.gameObject.activeSelf)
            {
                // Interaction text is shown for the first time
                interactionTextMeshPro.gameObject.SetActive(true);
                PlayInteractionAudio();
            }
        }
        else
        {
            interactionTextMeshPro.gameObject.SetActive(false);
        }
    }

    void PlayInteractionAudio()
    {
        if (interactionAudioClip != null)
        {
            AudioSource.PlayClipAtPoint(interactionAudioClip, transform.position);
        }
    }
}
