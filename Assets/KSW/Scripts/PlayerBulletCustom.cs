using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Flags]
public enum GunType
{
    NORMAL = 1 << 0,
    REPEATER = 1 << 1,
    PIERCE = 1 << 2,
    SPLASH = 1 << 3,
   
}
public class PlayerBulletCustom : MonoBehaviour
{
    [Header("- 총기 특성")]
    [SerializeField] private GunType gunType;
    public GunType GunType { get { return gunType; } }

    [Header("- 공격력")]
    [SerializeField] private float bulletAttack;

    public float BulletAttack { get { return bulletAttack; } }

    [Header("- 관통 횟수")]
    [SerializeField] private int defaultPierceCount;

    public int DefaultPierceCount { get { return defaultPierceCount; } }


    [Header("- 스플래시 범위(원형)")]
    [SerializeField, Range(0, 3)] private float splashRadius;

    public float SplashRadius { get { return splashRadius; } }

}
