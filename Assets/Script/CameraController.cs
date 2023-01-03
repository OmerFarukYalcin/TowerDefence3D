using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float panSpeed = 30f;
    [SerializeField] float panBorderThickness = 10f;
    [SerializeField] float scroolSpeed = 5f;
    [SerializeField] float minY = 10f;
    [SerializeField] float maxY = 100f;
    void Update()
    {
        if (GameManager.gameEnded)
        {
            this.enabled = false;
            return;
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (y > 0 || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (y < 0 || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (x > 0 || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (x < 0 || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        float scrool = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scrool * 1000 * scroolSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;

    }
}
