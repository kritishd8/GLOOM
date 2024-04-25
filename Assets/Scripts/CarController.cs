using UnityEngine;

public class CarController : MonoBehaviour
{
    public Light[] headlights;
    public AudioClip lightsOffSound; // Audio clip for lights off

    // Call this function to turn off the headlights and play the lights off sound
    public void TurnOffHeadlights()
    {
        foreach (Light headlight in headlights)
        {
            headlight.enabled = false;
        }

        if (lightsOffSound != null)
        {
            AudioSource.PlayClipAtPoint(lightsOffSound, transform.position);
        }
    }
}
