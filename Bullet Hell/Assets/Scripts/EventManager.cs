using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void BossDefeatAction();
    public static event BossDefeatAction OnBossDefeated;

    public static void BossDefeated()
    {
        OnBossDefeated();
    }
}
