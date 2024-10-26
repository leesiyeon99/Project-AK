using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Flags]
public enum GunType
{
    NORMAL = 1 << 0,
    PIERCE = 1 << 1,
    SPLASH = 1 << 2
}
public class PlayerBulletCustom : MonoBehaviour
{
    [SerializeField] private GunType gunType;
    public GunType GunType { get { return gunType; } }

   
    [SerializeField] private float bulletSpeed;

    public float BulletSpeed { get { return bulletSpeed; } }


    [SerializeField] private int defaultPierceCount;

    public int DefaultPierceCount { get { return defaultPierceCount; } }



    [SerializeField] private float splashRadius;

    public float SplashRadius { get { return splashRadius; } }
}
