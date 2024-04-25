using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Lantern lantern;
    private GameObject lanternObject;

    public float maxPickupDistance = 2f;

    void Start()
    {
        lanternObject = GameObject.FindGameObjectWithTag("Lantern");
        lantern = lanternObject.GetComponent<Lantern>();
    }

    void Update()
    {
        float distanceToLantern = Vector3.Distance(transform.position, lanternObject.transform.position);

        if (distanceToLantern <= maxPickupDistance && Input.GetKeyDown(KeyCode.E))
        {
            if (!lantern.IsCarried())
            {
                lantern.PickUp();
            }
            else
            {
                lantern.Drop();
            }
        }

        // Check if the player is carrying the lantern and presses the F key to toggle the light
        if (lantern.IsCarried() && Input.GetKeyDown(KeyCode.F))
        {
            lantern.ToggleLight();
        }
    }
}
