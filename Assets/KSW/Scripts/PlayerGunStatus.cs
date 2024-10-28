using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerGunStatus : MonoBehaviour
{
    [Header("- 기본 발사 간격")]
    [SerializeField] private float defaultFiringDelay;
    [Header("- 현재 발사 간격")]
    [SerializeField] private float firingDelay;
    [Header("- 가속 도달 시간")]
    [SerializeField] private float accelerationTime;
    [Header("- 최대 장탄량")]
    [SerializeField] private int maxMagazine;
    [Header("- 현재 장탄량")]
    [SerializeField] private int magazine;
    [Header("- 재장전 속도")]
    [SerializeField] private float reloadSpeed;
    [Header("- 무기 티어")]
    [SerializeField] private int tier;

    public float DefaultFiringDelay { get { return defaultFiringDelay; } }
    public float FiringDelay { get { return firingDelay; } set { firingDelay = value; } }
    public float AccelerationTime { get { return accelerationTime; }  }
    public int MaxMagazine { get { return maxMagazine; } }
    public int Magazine { get { return magazine; } set { magazine = value; OnMagazineChanged?.Invoke(magazine); } }

    public UnityAction<int> OnMagazineChanged;

    public float ReloadSpeed { get { return reloadSpeed; } }

    public int Tier { get { return tier; } }


}
