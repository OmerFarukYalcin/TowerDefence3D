using UnityEngine;
[System.Serializable]
public class TurretBuilding
{
    public GameObject prefab;
    public int cost;
    public GameObject upgradedPrefab;
    public int upgradeCost;

    public int GetSellAmount()
    {
        return cost / 2;
    }
}