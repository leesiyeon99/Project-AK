using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerGunStatus : MonoBehaviour
{
    [Header("- 발사 간격")]
    [SerializeField] private float firingDelay;
    [Header("- 최대 장탄량")]
    [SerializeField] private int maxMagazine;
    [Header("- 현재 장탄량")]
    [SerializeField] private int magazine;

    public float FiringDelay { get { return firingDelay; }  }
    public int MaxMagazine { get { return maxMagazine; } }
    public int Magazine { get { return magazine; } set { magazine = value; OnMagazineChanged?.Invoke(magazine); } }

    public UnityAction<int> OnMagazineChanged;
}
