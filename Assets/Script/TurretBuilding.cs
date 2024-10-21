using UnityEngine;

[System.Serializable]
public class TurretBuilding
{
    // Prefab of the basic turret to build
    public GameObject prefab;

    // Cost to build the turret
    public int cost;

    // Prefab of the upgraded version of the turret
    public GameObject upgradedPrefab;

    // Cost to upgrade the turret to the upgraded version
    public int upgradeCost;

    // Method to calculate the sell amount of the turret
    public int GetSellAmount()
    {
        // Returns half of the turret's cost when sold
        return cost / 2;
    }
}
