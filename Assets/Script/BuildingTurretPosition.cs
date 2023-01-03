using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class BuildingTurretPosition
{
    public List<string> Objname;
    public List<float> buildPosition;
    float nodeOffset = 0.4f;
    int index;
    float missileOfset;
    public Vector3 GetOffsetPositionByName(string _name)
    {
        if (Objname.Contains(_name))
        {
            index = Objname.IndexOf(_name);
        }
        return new Vector3(0f, buildPosition[index] + nodeOffset, 0f);
    }
}