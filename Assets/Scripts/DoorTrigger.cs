using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [Tooltip("Drag and drop the door object from the scene here")]
    public GameObject doorObject;

    [Tooltip("Drag and drop the left door object from the scene here")]
    public GameObject leftDoorObject;

    [Tooltip("Drag and drop the Text Mesh Pro object from the scene here")]
    public GameObject textMeshProObject;

    [Tooltip("Drag and drop the player object from the scene here")]
    public GameObject playerObject;

    [Tooltip("Drag and drop the ghost object from the scene here")]
    public GameObject ghostObject;

    public AudioClip doorCloseSound;
    public AudioClip additionalSound;
    public AudioClip secondAdditionalSound;
    public AudioClip ghostAppearSound;
    public AudioClip additionalAudioClip; // New audio clip to play after the fade out

    private AudioSource audioSource;
    private bool ghostShown = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Hide the ghost at the start
        if (ghostObject != null)
        {
            ghostObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (doorObject != null)
            {
                DoorController doorScript = doorObject.GetComponent<DoorController>();
                if (doorScript != null)
                {
                    Destroy(doorScript);
                    ObjectInteraction objectInteraction = doorObject.GetComponent<ObjectInteraction>();
                    if (objectInteraction != null)
                    {
                        Destroy(objectInteraction);
                    }

                    if (leftDoorObject != null)
                    {
                        leftDoorObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    }

                    if (textMeshProObject != null)
                    {
                        Destroy(textMeshProObject);
                    }

                    if (doorCloseSound != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(doorCloseSound);
                    }

                    if (additionalSound != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(additionalSound);
                    }

                    if (secondAdditionalSound != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(secondAdditionalSound);
                    }

                    // Show the ghost and play ghost appear sound
                    if (ghostObject != null && ghostAppearSound != null && audioSource != null && !ghostShown)
                    {
                        ghostObject.SetActive(true);
                        audioSource.PlayOneShot(ghostAppearSound);
                        ghostShown = true;

                        // Call FadeOut from ScreenFader
                        ScreenFader screenFader = FindObjectOfType<ScreenFader>();
                        if (screenFader != null)
                        {
                            screenFader.FadeOut();
                            screenFader.additionalAudioClip = additionalAudioClip; // Assign additional audio clip
                        }
                    }
                }
            }
        }
    }
}
