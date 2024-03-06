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

    void Start()
    {
        atk = initialATK; 
    }

    public void IncreaseAtk()
    {
        atk *= increaseAmount;
    }

    public void Reset()
    {
        atk = initialATK;
    }
}
