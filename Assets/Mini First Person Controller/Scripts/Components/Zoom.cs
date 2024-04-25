using UnityEngine;

[ExecuteInEditMode]
public class Zoom : MonoBehaviour
{
    Camera camera;
    public float defaultFOV = 60;
    public float maxZoomFOV = 15;
    public float zoomSpeed = 5f;
    private bool isZooming = false;

    void Awake()
    {
        // Get the camera on this gameObject and the defaultZoom.
        camera = GetComponent<Camera>();
        if (camera)
        {
            defaultFOV = camera.fieldOfView;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right mouse button pressed
        {
            isZooming = true;
        }
        else if (Input.GetMouseButtonUp(1)) // Right mouse button released
        {
            isZooming = false;
        }

        if (isZooming)
        {
            // Zoom in
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, maxZoomFOV, Time.deltaTime * zoomSpeed);
        }
        else
        {
            // Zoom out
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, defaultFOV, Time.deltaTime * zoomSpeed);
        }
    }
}
