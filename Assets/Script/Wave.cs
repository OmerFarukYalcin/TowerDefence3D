using UnityEngine;

[System.Serializable]
public class Wave
{
    // The enemy prefab to be spawned in the wave
    public GameObject Enemy;

    // The number of enemies to spawn in the wave
    public int count;

    // The rate at which enemies are spawned (e.g., how many enemies spawn per second)
    public int rate;
}
