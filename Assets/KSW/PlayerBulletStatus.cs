using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletStatus : MonoBehaviour
{
    [SerializeField] private int attack;

    public int Attack { get { return attack; } }

    [SerializeField] private float bulletSpeed;

    public float BulletSpeed { get { return bulletSpeed; } }
}
