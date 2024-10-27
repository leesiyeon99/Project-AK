using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Flags]
public enum GunType
{
    NORMAL = 1 << 0,
    PIERCE = 1 << 1,
    SPLASH = 1 << 2,
    SPREAD = 1 << 3
}
public class PlayerBulletCustom : MonoBehaviour
{
    [Header("- 총기 특성")]
    [SerializeField] private GunType gunType;
    public GunType GunType { get { return gunType; } }

    [Header("- 탄환 속도")]
    [SerializeField] private float bulletSpeed;

    public float BulletSpeed { get { return bulletSpeed; } }

    [Header("- 관통 횟수")]
    [SerializeField] private int defaultPierceCount;

    public int DefaultPierceCount { get { return defaultPierceCount; } }


    [Header("- 스플래시 범위(원형)")]
    [SerializeField, Range(0, 3)] private float splashRadius;

    public float SplashRadius { get { return splashRadius; } }

    [Header("- 확산탄 수")]
    [SerializeField] private int spreadCount;

    public int SpreadCount { get { return spreadCount; } }

    [Header("- 확산탄 각도")]
    [SerializeField, Range(0, 90)] private float spreadAngleX;

    public float SpreadAngleX { get { return spreadAngleX; } }

    [SerializeField, Range(0, 90)] private float spreadAngleY;

    public float SpreadAngleY { get { return spreadAngleY; } }
}
