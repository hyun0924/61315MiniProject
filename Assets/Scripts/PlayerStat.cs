using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private float initialATK;
    [SerializeField] private float increaseAmount;
    public static float atk;

    public static PlayerStat Instance => instance;
    private static PlayerStat instance;

    public PlayerStat()
    {
        instance = this;
    }

    void Awake()
    {
        atk = initialATK; 
    }

    public void IncreaseAtk()
    {
        atk += increaseAmount;

        increaseAmount = 1 + (int) (atk / 25);
    }

    public void Reset()
    {
        atk = initialATK;
    }
}
