using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum JoystickDirection
{
    NONE = 0,
    LEFT = 1 << 0,
    RIGHT = 1 << 1,
    TOP = 1 << 2,
    BOTTOM = 1 << 3

}
public class PlayerChangeWeapon : MonoBehaviour
{
    [Header("- UI 관리")]
    [SerializeField] private PlayerWeaponUI weaponUI;

    private PlayerOwnedWeapons weapons;

    Vector2 joystickVec;
    JoystickDirection joystickDirection;


    int index;

    private void Awake()
    {
        weapons = GetComponent<PlayerOwnedWeapons>();
     
    }

    public void MoveJoystick(Vector2 vec)
    {
        joystickVec = vec;

        weaponUI.UpdateJoystickUI(joystickVec * 3);

        if (Mathf.Abs(joystickVec.x) == 1 || Mathf.Abs(joystickVec.y) == 1 || joystickVec == Vector2.zero)
        {
            joystickDirection = JoystickDirection.NONE;
        }

        // 우측
        if (joystickVec.x > 0 && 1 > joystickVec.x)
        {
            joystickDirection |= JoystickDirection.RIGHT;
        }
        // 좌측
        else if (joystickVec.x < 0 && -1 < joystickVec.x)
        {
            joystickDirection |= JoystickDirection.LEFT;
        }
        // 상단
        if (joystickVec.y > 0 && 1 > joystickVec.y)
        {
            joystickDirection |= JoystickDirection.TOP;
        }
        // 하단
        else if (joystickVec.y < 0 && -1 < joystickVec.y)
        {
            joystickDirection |= JoystickDirection.BOTTOM;
        }

       
        index = weapons.Index;
        if (joystickDirection.HasFlag(JoystickDirection.RIGHT) && joystickDirection.HasFlag(JoystickDirection.TOP))
            index = 0;
        if (joystickDirection.HasFlag(JoystickDirection.RIGHT) && joystickDirection.HasFlag(JoystickDirection.BOTTOM))
            index = 1;
        if (joystickDirection.HasFlag(JoystickDirection.LEFT) && joystickDirection.HasFlag(JoystickDirection.BOTTOM))
            index = 2;
        if (joystickDirection.HasFlag(JoystickDirection.LEFT) && joystickDirection.HasFlag(JoystickDirection.TOP))
            index = 3;
      
        weaponUI.UpdateExplainUI(index);
    }

    public void ChangeWeapon()
    {
      

        if(Mathf.Abs(joystickVec.x) == 1 || Mathf.Abs(joystickVec.y) == 1 || joystickVec == Vector2.zero)
        {
            joystickDirection = JoystickDirection.NONE;
            return;
        }
     
      
        if(weapons.Index == index)
        {
            joystickDirection = JoystickDirection.NONE;
            return;
        }


        if (index != 0 && PlayerSpecialBullet.Instance.SpecialBullet[index - 1] <= 0 && weapons.GetOwnedWeapons(index).GetMagazine() <= 0)
        {
            return;
        }


        weapons.Index = index;
        joystickDirection = JoystickDirection.NONE;
        weapons.SetCurrentWeapon();
        weaponUI.UpdateChangeToggleUI();

    }





}
