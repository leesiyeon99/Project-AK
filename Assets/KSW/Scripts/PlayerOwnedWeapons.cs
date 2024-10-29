using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOwnedWeapons : MonoBehaviour
{
    [SerializeField] private int index;

    public int Index { get { return index; } set { index = value; } }

    [Header("- 보유중인 무기")]
    [SerializeField] List<PlayerGun> ownedWeapons;
    [Header("- 사용중인 무기")]
    [SerializeField] PlayerGun currentWeapon;
    [Header("- 탄창 오브젝트")]
    [SerializeField] PlayerMagazine magazine;

    private void Awake()
    {
        SetWeapons();
    }

    // Comment : 무기 초기화 함수 호출
    public void SetWeapons()
    {
        foreach (PlayerGun weapon in ownedWeapons)
        {

            weapon.InitGun();
        }
      
    }

    // Comment : 사용중인 무기 반환
    public PlayerGun GetCurrentWeapon()
    {
        return currentWeapon;
    }

  

    // Comment : 보유중인 무기 반환
    public PlayerGun GetOwnedWeapons(int _index)
    {
        return ownedWeapons[_index];
    }


    // Comment : 보유중인 무기 수 반환
    public int GetOwnedWeaponsCount()
    {
        return ownedWeapons.Count-1;
    }

   // Comment : 무기 교체
    public void SetCurrentWeapon()
    {
       
        currentWeapon.gameObject.SetActive(false);
        currentWeapon = ownedWeapons[index];
        currentWeapon.gameObject.SetActive(true);
    }
    // Comment : 재장전
    public void ReloadMagazine()
    {
        currentWeapon.Reload(index - 1);
    }

    public void ReloadGripOnMagazine()
    {
        if (currentWeapon.MagazineRemainingCheck())
            return;
        if (index != 0 && PlayerSpecialBullet.Instance.SpecialBullet[index-1] <= 0)
        {
            return;
        }

        magazine.gameObject.SetActive(true);
    }

    public void ReloadGripOffMagazine()
    {
        magazine.gameObject.SetActive(false);
    }

   

}
