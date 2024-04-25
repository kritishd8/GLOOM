using UnityEngine;

public class TableCollision : MonoBehaviour
{
    [Tooltip("Drag and drop the door object from the scene here")]
    public GameObject doorObject;

    [Tooltip("Drag and drop the left door object from the scene here")]
    public GameObject leftDoorObject;

    [Tooltip("Drag and drop the right door object from the scene here")]
    public GameObject rightDoorObject;

    [Tooltip("Drag and drop the Text Mesh Pro object from the scene here")]
    public GameObject textMeshProObject;

    [Tooltip("Drag and drop the lantern object from the scene here")]
    public GameObject lanternObject;

    [Tooltip("Drag and drop the player object from the scene here")]
    public GameObject playerObject;

    public AudioClip doorCloseSound;
    public AudioClip lanternDisappearSound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
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

                    if (leftDoorObject != null && rightDoorObject != null)
                    {
                        leftDoorObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                        rightDoorObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    }

                    if (textMeshProObject != null)
                    {
                        Destroy(textMeshProObject);
                    }

                    if (doorCloseSound != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(doorCloseSound);
                    }
                }
            }

            if (lanternObject != null)
            {
                if (playerObject != null)
                {
                    PlayerController playerController = playerObject.GetComponent<PlayerController>();
                    if (playerController != null)
                    {
                        Destroy(playerController);
                    }
                }

                Destroy(lanternObject);

                if (lanternDisappearSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(lanternDisappearSound);
                }
            }
        }
    }
}
