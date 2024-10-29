using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponUI : PlayerWeaponUIBase
{
    [SerializeField] PlayerOwnedWeapons weapon;

    [SerializeField] GameObject changeUI;
    [SerializeField] RectTransform changeJoystick;
    [SerializeField] TextMeshProUGUI magazineUI;
    [SerializeField] TextMeshProUGUI[] toggleMagazineUI;
    private StringBuilder stringBuilder = new StringBuilder();

    void Awake()
    {
        BindAll();
        InitUI();
    }

    private void InitUI()
    {
        StringBuilder initStringBuilder = new StringBuilder();
        changeUI = GetUI("ChangeUI");
        changeJoystick = GetUI<RectTransform>("ChangeJoystick");
        magazineUI = GetUI<TextMeshProUGUI>("RemainingBullets");
        for (int i = 0; i < toggleMagazineUI.Length; i++)
        {
            initStringBuilder.Clear();
            initStringBuilder.Append("Magazine");
            initStringBuilder.Append(i.ToString());
            toggleMagazineUI[i] = GetUI<TextMeshProUGUI>(initStringBuilder.ToString());

        }
    }

    public void OnOffChangeUI(bool active)
    {
        UpdateChangeToggleUI();
        changeUI.SetActive(active);
        magazineUI.gameObject.SetActive(!active);
    }

    public void UpdateJoystickUI(Vector2 vec)
    {
        changeJoystick.anchoredPosition = vec;
    }

    public void UpdateMagazineUI(int magazine, int maxMagazine)
    {
      
        stringBuilder.Clear();
        stringBuilder.Append(magazine.ToString());
        stringBuilder.Append("/");
        stringBuilder.Append(maxMagazine.ToString());
        magazineUI.text = stringBuilder.ToString();

    }

    public void UpdateChangeToggleUI()
    {
        
        for(int i = 0;i < toggleMagazineUI.Length;i++)
        {
            stringBuilder.Clear();
            
            stringBuilder.Append(weapon.GetOwnedWeapons(i).GetMagazine());
            stringBuilder.Append("/");
            if (i == 0)
            {
                stringBuilder.Append("¡Ä");
            }
            else
            {
                stringBuilder.Append(weapon.GetOwnedWeapons(i).GetMaxMagazine());
            }
            toggleMagazineUI[i].text = stringBuilder.ToString();
        }

        
       

    }
}
