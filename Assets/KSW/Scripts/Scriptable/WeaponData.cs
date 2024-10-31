using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = int.MaxValue)]
public class WeaponData : ScriptableObject
{
    [Header("- 이름")]
    [SerializeField] public string weaponName;
    [Header("- 총기 특성")]
    [SerializeField] public GunType gunType;
    [Header("- 무기 티어")]
    [SerializeField] public Tier tier;
    [Header("- 공격력")]
    [SerializeField] public float bulletAttack;
    [Header("- 기본 발사 간격")]
    [SerializeField] public float defaultFiringDelay;

    [Header("- 가속 도달 시간")]
    [SerializeField] public float accelerationTime;
    [Header("- 최대 장탄량")]
    [SerializeField] public int maxMagazine;

    [Header("- 재장전 속도")]
    [SerializeField] public float reloadSpeed;
    [Header("- 사정거리")]
    [SerializeField] public float range;
 
}
