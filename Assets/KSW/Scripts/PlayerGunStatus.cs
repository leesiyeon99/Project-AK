using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunStatus : MonoBehaviour
{
    [SerializeField] private float firingDelay;
    [SerializeField] private int maxMagazine;
    [SerializeField] private int magazine;

    public float FiringDelay { get { return firingDelay; }  }
    public int MaxMagazine { get { return maxMagazine; } }
    public int Magazine { get { return magazine; } set { magazine = value; } }
}
