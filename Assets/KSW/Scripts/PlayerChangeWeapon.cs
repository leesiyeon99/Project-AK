using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeWeapon : MonoBehaviour
{
    private PlayerOwnedWeapons weapons;
   
    private void Awake()
    {
        weapons = GetComponent<PlayerOwnedWeapons>();
     
    }


    public void ChangeWeapon(bool left)
    {
        if (left)
        {
            if (weapons.Index == 0)
            {
                weapons.Index = weapons.GetOwnedWeaponsCount();

            }
            else
            {
                weapons.Index--;
            }
        }
        else
        {
            if (weapons.Index == weapons.GetOwnedWeaponsCount())
            {
                weapons.Index = 0;

            }
            else
            {
                weapons.Index++;
            }
        }
   
        weapons.SetCurrentWeapon();
    

    }


}
