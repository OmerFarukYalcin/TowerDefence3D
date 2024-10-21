using UnityEngine;

public class wayPoints : MonoBehaviour
{
    // Static array that holds references to waypoints, accessible globally
    public static Transform[] points;

    // This method is called when the script instance is being loaded
    private void Awake()
    {
        // Initialize the array to hold all child transforms (waypoints)
        points = new Transform[transform.childCount];

        // Loop through each child of this GameObject and assign it to the array
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
