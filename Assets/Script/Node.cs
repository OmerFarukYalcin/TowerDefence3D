using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

[Serializable]
public class BuildingPosition
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
    //0.118253
    //0.1182537
    //0.1182537
    //0.1182534
    //-50.89
    //-0.1482105
    //.2517895
}

public class Node : MonoBehaviour
{
    public Color hovorColor;
    public GameObject turret;
    private Renderer rend;
    private Color startColor;
    BuildManager buildManager;
    public BuildingPosition buildPos;

    private void Start()
    {
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (turret != null)
        {
            Debug.Log("Cant build there!!!");
            return;
        }

        buildManager.BuildTurretOn(this);
    }

    public Vector3 GetBuildPosition(string _name)
    {
        return transform.position + buildPos.GetOffsetPositionByName(_name);
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;
        rend.material.color = hovorColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
