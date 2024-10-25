using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOwnedWeapons : MonoBehaviour
{
    [SerializeField] private int index;

    public int Index { get { return index; } set { index = value; } }

    [SerializeField] List<PlayerGun> ownedWeapons;
    [SerializeField] PlayerGun currentWeapon;


    
    public PlayerGun GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public void SetCurrentWeapon()
    {
        currentWeapon.gameObject.SetActive(false);
        currentWeapon = ownedWeapons[index];
        currentWeapon.gameObject.SetActive(true);
    }

    public int GetOwnedWeaponsCount()
    {
        return ownedWeapons.Count-1;
    }
}
