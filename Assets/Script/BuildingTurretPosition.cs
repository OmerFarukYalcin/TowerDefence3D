using UnityEngine;
using System.Collections.Generic;

// This class represents the positions where turrets (or other objects) can be placed on a building.
[System.Serializable] // Allows this class to be serialized and shown in the Unity Inspector.
public class BuildingTurretPosition
{
    // List of object names, each representing a potential turret or item
    [SerializeField] private List<string> Objname;

    // List of positions (float values) corresponding to the objects in Objname
    [SerializeField] private List<float> buildPosition;

    // Offset applied to the position of the turret or object
    private float nodeOffset = 0.4f;

    // Stores the index of the object being searched for
    private int index;

    // Method to get the adjusted position for a given object by its name
    public Vector3 GetOffsetPositionByName(string _name)
    {
        // Check if the list contains the object name
        if (Objname.Contains(_name))
        {
            // Get the index of the object name
            index = Objname.IndexOf(_name);
        }

        // Return the position with the offset applied to the Y axis
        return new Vector3(0f, buildPosition[index] + nodeOffset, 0f);
    }
}
