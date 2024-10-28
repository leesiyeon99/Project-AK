using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[Flags]
public enum GunType
{
    NORMAL = 1 << 0,
    REPEATER = 1 << 1,
    PIERCE = 1 << 2,
    SPLASH = 1 << 3,

}
public class PlayerGunStatus : MonoBehaviour
{
    [Header("- 총기 특성")]
    [SerializeField] private GunType gunType;
    [Header("- 무기 티어")]
    [SerializeField] private int tier;
    [Header("- 공격력")]
    [SerializeField] private float bulletAttack;
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
    [Header("- 사정거리")]
    [SerializeField] private float range;
    [Header("- 관통 횟수")]
    [SerializeField] private int defaultPierceCount;
    [Header("- 스플래시 범위(원형)")]
    [SerializeField, Range(0, 3)] private float splashRadius;
    [Header("- 스플래시 공격력")]
    [SerializeField] private float splashDamage;
    [Header("- 가속률")]
    [SerializeField] private float accelerationRate;


    public GunType GunType { get { return gunType; } }
    public float BulletAttack { get { return bulletAttack; } }

    public float DefaultFiringDelay { get { return defaultFiringDelay; } }
    public float FiringDelay { get { return firingDelay; } set { firingDelay = value; } }
    public float AccelerationTime { get { return accelerationTime; }  }
    public int MaxMagazine { get { return maxMagazine; } }
    public int Magazine { get { return magazine; } set { magazine = value; OnMagazineChanged?.Invoke(magazine); } }

    public UnityAction<int> OnMagazineChanged;

    public float ReloadSpeed { get { return reloadSpeed; } }

    public int Tier { get { return tier; } }

    public float Range { get { return range; } }

    public int DefaultPierceCount { get { return defaultPierceCount; } }
    public float SplashRadius { get { return splashRadius; } }
    public float SplashDamage { get { return splashDamage; } }
    public float AccelerationRate { get { return accelerationRate; } }


    void Awake()
    {
        switch (tier)
        {
            case 1:
                defaultPierceCount = 2;
                splashRadius = 0.3f;
                accelerationRate = 0.3f;
                splashDamage = bulletAttack * 0.3f;
                break;
            case 2:
                defaultPierceCount = 3;
                splashRadius = 0.5f;
                accelerationRate = 0.5f;
                splashDamage = bulletAttack * 0.5f;
                break;
            case 3:
                defaultPierceCount = 4;
                splashRadius = 1f;
                accelerationRate = 0.7f;
                splashDamage = bulletAttack;
                break;
        }


    }
}
