using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private float initialATK;
    [SerializeField] private float increaseAmount;
    public static float atk;
    private static int level;

    public static PlayerStat Instance => instance;
    private static PlayerStat instance;

    public PlayerStat()
    {
        instance = this;
    }

    void Awake()
    {
        Reset();
    }

    public void IncreaseAtk()
    {
        atk += increaseAmount;
        level++;
        increaseAmount = 1 + level / 25;
    }

    public void Reset()
    {
        atk = initialATK;
        level = 0;
    }
}
