using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;

    public float interactionDistance = 3f;
    public float doorOpenSpeed = 5f;

    public AudioClip openSound;
    public AudioClip closeSound;

    private bool isOpen = false;
    private Transform player;

    private Quaternion leftDoorStartRotation;
    private Quaternion rightDoorStartRotation;

    private AudioSource audioSource;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Ensure doors are initially closed
        CloseDoor();

        // Store initial door rotations
        leftDoorStartRotation = leftDoor.localRotation;
        rightDoorStartRotation = rightDoor.localRotation;

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && IsPlayerNearDoor())
        {
            ToggleDoor();
        }
    }

    bool IsPlayerNearDoor()
    {
        return Vector3.Distance(transform.position, player.position) <= interactionDistance;
    }

    void ToggleDoor()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            StartCoroutine(OpenDoorSmoothly());
            PlaySound(openSound);
        }
        else
        {
            StartCoroutine(CloseDoorSmoothly());
            PlaySound(closeSound);
        }
    }

    IEnumerator OpenDoorSmoothly()
    {
        float t = 0f;
        Quaternion targetRotation = Quaternion.Euler(0, 90, 0);
        while (t < 1f)
        {
            t += Time.deltaTime * doorOpenSpeed;
            leftDoor.localRotation = Quaternion.Lerp(leftDoorStartRotation, targetRotation, t);
            rightDoor.localRotation = Quaternion.Lerp(rightDoorStartRotation, Quaternion.Euler(0, -90, 0), t);
            yield return null;
        }
    }

    IEnumerator CloseDoorSmoothly()
    {
        float t = 0f;
        Quaternion targetRotation = Quaternion.identity;
        while (t < 1f)
        {
            t += Time.deltaTime * doorOpenSpeed;
            leftDoor.localRotation = Quaternion.Lerp(leftDoor.localRotation, leftDoorStartRotation, t);
            rightDoor.localRotation = Quaternion.Lerp(rightDoor.localRotation, rightDoorStartRotation, t);
            yield return null;
        }
    }

    void CloseDoor()
    {
        leftDoor.localRotation = leftDoorStartRotation;
        rightDoor.localRotation = rightDoorStartRotation;
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
