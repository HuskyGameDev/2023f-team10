using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private static float initialMovementSpeed;
    [SerializeField] private static float msUpgradeAmount;

    [SerializeField] private static float initialHealth;
    [SerializeField] private static float healthUpgradeAmount;

    private static float movementSpeed;
    private static float health;

    public static float MovementSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed += (value - movementSpeed) * msUpgradeAmount; }
    }

    public static float Health
    {
        get { return health; }
        set { health += (value - health) * healthUpgradeAmount; }
    }

    [SerializeField] private static List<GameObject> commonUpgrades;
    [SerializeField] private static List<GameObject> rareUpgrades;

    [SerializeField] private static float upgradeChance;
    [SerializeField] private static float rareChance;

    private static System.Random random = new System.Random();

    private static bool Roll(float chance)
    {
        return (Random.value <= chance);
    }

    public static GameObject GetUpgrade(bool guaranteed, bool rare)
    {
        if (guaranteed || Roll(upgradeChance))
        {
            if (rare || Roll(rareChance))
            {
                if (rareUpgrades.Count > 0)
                {
                    int index = random.Next(rareUpgrades.Count);
                    return rareUpgrades[index];
                }
            }
            else
            {
                if (commonUpgrades.Count > 0)
                {
                    int index = random.Next(commonUpgrades.Count);
                    return commonUpgrades[index];
                }
            }
        }
        return null;
    }

    public static void ResetUpgrades()
    {
        movementSpeed = initialMovementSpeed;
        health = initialHealth;
    }
}
