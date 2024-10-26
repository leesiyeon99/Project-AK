using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOwnedWeapons : MonoBehaviour
{
    [SerializeField] private int index;

    public int Index { get { return index; } set { index = value; } }

    [SerializeField] List<PlayerGun> ownedWeapons;
    [SerializeField] PlayerGun currentWeapon;
    [SerializeField] PlayerMagazine magazine;
    


    // Comment : 사용중인 무기 반환
    public PlayerGun GetCurrentWeapon()
    {
        return currentWeapon;
    }

    // Comment : 무기 교체
    public void SetCurrentWeapon()
    {
       
        currentWeapon.gameObject.SetActive(false);
        currentWeapon = ownedWeapons[index];
        currentWeapon.gameObject.SetActive(true);
    }

    // Comment : 보유중인 무기 수 반환
    public int GetOwnedWeaponsCount()
    {
        return ownedWeapons.Count-1;
    }

    // Comment : 재장전
    public void ReloadMagazine()
    {
        currentWeapon.Reload();
    }

    public void ReloadGripOnMagazine()
    {
        if (currentWeapon.MagazineRemainingCheck())
            return;
        magazine.gameObject.SetActive(true);
    }

    public void ReloadGripOffMagazine()
    {
        magazine.gameObject.SetActive(false);
    }
}
