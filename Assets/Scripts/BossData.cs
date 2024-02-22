using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BossData : ScriptableObject
{
    [SerializeField] private string bossName;
    [SerializeField] private Sprite[] stages;

    public string Name => bossName;
    public Sprite[] Stages => stages;
}
