using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BossData : ScriptableObject
{
    [SerializeField] private string bossName;
    [SerializeField] private Sprite[] stages;
    [SerializeField] private string[] scripts;

    public string Name => bossName;
    public Sprite[] Stages => stages;
    public string[] Scripts => scripts;
    public int ScriptsCnt => scripts.Length;
}
