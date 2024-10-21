using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Camera panning (scrolling) speed
    [SerializeField] float panSpeed = 30f;

    // Border thickness used to detect when the mouse is near the edge of the screen for panning
    [SerializeField] float panBorderThickness = 10f;

    // Speed at which the camera zooms in and out when using the mouse scroll wheel
    [SerializeField] float scrollSpeed = 5f;

    // Minimum and maximum Y values to limit how far the camera can zoom in and out
    [SerializeField] float minY = 10f;
    [SerializeField] float maxY = 100f;

    void Update()
    {
        // Check if the game has ended, if so disable the camera movement
        if (GameManager.gameEnded)
        {
            this.enabled = false;
            return;
        }

        // Get the input from the horizontal (A, D, Left Arrow, Right Arrow) and vertical (W, S, Up Arrow, Down Arrow) axes
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // Check if the player is pressing the forward key or if the mouse is near the top of the screen, then move forward
        if (y > 0 || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }

        // Check if the player is pressing the backward key or if the mouse is near the bottom of the screen, then move backward
        if (y < 0 || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        // Check if the player is pressing the right key or if the mouse is near the right edge of the screen, then move right
        if (x > 0 || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        // Check if the player is pressing the left key or if the mouse is near the left edge of the screen, then move left
        if (x < 0 || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        // Get the input from the mouse scroll wheel to zoom in/out
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // Adjust the camera's Y position based on the scroll input (zooming effect)
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;

        // Clamp the Y position to prevent zooming too far in or out
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        // Update the camera's position
        transform.position = pos;
    }
}
