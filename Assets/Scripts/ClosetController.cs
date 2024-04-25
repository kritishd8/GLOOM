using UnityEngine;
using System.Collections;

public class ClosetController : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;

    public float interactionDistance = 3f;
    public float doorOpenSpeed = 5f;

    public AudioClip openSound;
    public AudioClip closeSound;

    private bool isOpen = false;
    private Transform player;

    private Quaternion leftDoorClosedRotation;
    private Quaternion rightDoorClosedRotation;

    private AudioSource audioSource;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Ensure closet is initially closed
        CloseCloset();

        // Store initial closet rotations
        leftDoorClosedRotation = leftDoor.localRotation;
        rightDoorClosedRotation = rightDoor.localRotation;

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && IsPlayerNearCloset())
        {
            ToggleCloset();
        }
    }

    bool IsPlayerNearCloset()
    {
        return Vector3.Distance(transform.position, player.position) <= interactionDistance;
    }

    void ToggleCloset()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            StartCoroutine(OpenClosetSmoothly());
            PlaySound(openSound);
        }
        else
        {
            StartCoroutine(CloseClosetSmoothly());
            PlaySound(closeSound);
        }
    }

    IEnumerator OpenClosetSmoothly()
    {
        float t = 0f;
        Quaternion targetRotation = Quaternion.Euler(0, -90, 0);
        while (t < 1f)
        {
            t += Time.deltaTime * doorOpenSpeed;
            leftDoor.localRotation = Quaternion.Lerp(leftDoorClosedRotation, targetRotation, t);
            rightDoor.localRotation = Quaternion.Lerp(rightDoorClosedRotation, Quaternion.Euler(0, 90, 0), t);
            yield return null;
        }
    }

    IEnumerator CloseClosetSmoothly()
    {
        float t = 0f;
        Quaternion targetRotation = Quaternion.identity;
        while (t < 1f)
        {
            t += Time.deltaTime * doorOpenSpeed;
            leftDoor.localRotation = Quaternion.Lerp(leftDoor.localRotation, leftDoorClosedRotation, t);
            rightDoor.localRotation = Quaternion.Lerp(rightDoor.localRotation, rightDoorClosedRotation, t);
            yield return null;
        }
    }

    void CloseCloset()
    {
        leftDoor.localRotation = leftDoorClosedRotation;
        rightDoor.localRotation = rightDoorClosedRotation;
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
