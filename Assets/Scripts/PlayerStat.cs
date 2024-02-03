using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    // Start is called before the first frame update
    public static int atk;
    void Start()
    {
        atk = 10;    
    }
    public static void atkIncrease(int inc)
    {
        atk += inc;
    }

   
}
