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
    [Header("- �ѱ� Ư��")]
    [SerializeField] private GunType gunType;
    public GunType GunType { get { return gunType; } }

    [Header("- źȯ �ӵ�")]
    [SerializeField] private float bulletSpeed;

    public float BulletSpeed { get { return bulletSpeed; } }

    [Header("- ���� Ƚ��")]
    [SerializeField] private int defaultPierceCount;

    public int DefaultPierceCount { get { return defaultPierceCount; } }


    [Header("- ���÷��� ����(����)")]
    [SerializeField, Range(0, 3)] private float splashRadius;

    public float SplashRadius { get { return splashRadius; } }

    [Header("- Ȯ��ź ��")]
    [SerializeField] private int spreadCount;

    public int SpreadCount { get { return spreadCount; } }

    [Header("- Ȯ��ź ����")]
    [SerializeField, Range(0, 90)] private float spreadAngleX;

    public float SpreadAngleX { get { return spreadAngleX; } }

    [SerializeField, Range(0, 90)] private float spreadAngleY;

    public float SpreadAngleY { get { return spreadAngleY; } }
}