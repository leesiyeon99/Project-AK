using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerWeaponUI : PlayerWeaponUIBase
{
    [SerializeField] PlayerOwnedWeapons weapons;

    // Comment : 남은 탄환 수 UI
    [SerializeField] TextMeshProUGUI magazineUI;
    // Comment : 발사 쿨타임 UI
    [SerializeField] TextMeshProUGUI firingCooltimeUI;

    // Comment : 무기 교체 UI
    [SerializeField] GameObject changeUI;
    // Comment : 무기 교체 UI 조이스틱
    [SerializeField] RectTransform changeJoystick;

    // Comment : 무기 교체 탄환 표시 UI
    [SerializeField] TextMeshProUGUI[] toggleMagazineUI;
    // Comment : 무기 교체 UI 활성화 색 구분
    [SerializeField] Image[] changeUIBackground;

    [Header("무기 교체 UI 배경 색상") ,ColorUsage(true)]
    [SerializeField] Color usedColor;
    [SerializeField] Color enableColor;
    [SerializeField] Color disableColor;



    // Comment : 무기 설명 UI
    [SerializeField] TextMeshProUGUI weaponNameUI;
    [SerializeField] TextMeshProUGUI weaponAbilityUI;
    [SerializeField] TextMeshProUGUI weaponAttackUI;
    [SerializeField] TextMeshProUGUI weaponMagazineUI;

    private StringBuilder stringBuilder = new StringBuilder();

    void Awake()
    {
        toggleMagazineUI = new TextMeshProUGUI[4];
        changeUIBackground = new Image[4];
        BindAll();
        InitUI();
    }

    private void InitUI()
    {
        StringBuilder initStringBuilder = new StringBuilder();
        changeUI = GetUI("ChangeUI");
        changeJoystick = GetUI<RectTransform>("ChangeJoystick");
        magazineUI = GetUI<TextMeshProUGUI>("RemainingBullets");
        firingCooltimeUI = GetUI<TextMeshProUGUI>("FiringCoolTime");
        for (int i = 0; i < toggleMagazineUI.Length; i++)
        {
            initStringBuilder.Clear();
            initStringBuilder.Append("Magazine");
            initStringBuilder.Append(i.ToString());
            toggleMagazineUI[i] = GetUI<TextMeshProUGUI>(initStringBuilder.ToString());
            initStringBuilder.Clear();
            initStringBuilder.Append("Weapon");
            initStringBuilder.Append(i.ToString());
            changeUIBackground[i] = GetUI<Image>(initStringBuilder.ToString());
        }
        weaponNameUI = GetUI<TextMeshProUGUI>("Name");
        weaponAbilityUI = GetUI<TextMeshProUGUI>("Ability");
        weaponAttackUI = GetUI<TextMeshProUGUI>("Attack");
        weaponMagazineUI = GetUI<TextMeshProUGUI>("MagazineText");
    }

    public void OnOffChangeUI(bool active)
    {
        
        UpdateChangeToggleUI();
        UpdateExplainUI(weapons.Index);
        changeUI.SetActive(active);
        magazineUI.gameObject.SetActive(!active);
    }

    public bool GetChangeUIActiveSelf()
    {
        return changeUI.activeSelf;
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

    public void UpdateFiringCooltimeUI(float cooltime)
    {

        stringBuilder.Clear();
        if (cooltime < 0)
        {
            stringBuilder.Append(0);
        }
        else
        {
            stringBuilder.Append(cooltime.ToString("0.0"));
        }
        firingCooltimeUI.text = stringBuilder.ToString();

    }

    public void UpdateChangeToggleUI()
    {
        
        for(int i = 0;i < toggleMagazineUI.Length;i++)
        {
            stringBuilder.Clear();
            PlayerGun weapon = weapons.GetOwnedWeapons(i); 
            stringBuilder.Append(weapon.GetMagazine());
            stringBuilder.Append("/");

            // Comment : 남은 탄환 수 표시

            if (i == 0)
            {
                stringBuilder.Append("∞");

            }
            else
            {
                stringBuilder.Append(PlayerSpecialBullet.Instance.SpecialBullet[i - 1]);

            }


            // Comment : UI 배경 색
            if (weapons.Index == i)
            {
                changeUIBackground[i].color = usedColor;
            }else if (i == 0)
            {
                changeUIBackground[i].color = enableColor;
            }
            else
            {
                if (PlayerSpecialBullet.Instance.SpecialBullet[i - 1] <= 0 && weapon.GetMagazine() <= 0)
                {
                    changeUIBackground[i].color = disableColor;
                }
                else
                {
                    changeUIBackground[i].color = enableColor;
                }
            }

            toggleMagazineUI[i].text = stringBuilder.ToString();
        }


    }

    public void UpdateExplainUI(int index)
    {
        PlayerGun weapon = weapons.GetOwnedWeapons(index);
        weaponNameUI.text = weapon.GetExplainStatus().name;
        weaponAbilityUI.text = weapon.GetExplainStatus().gunType.ToString();
        weaponAttackUI.text = weapon.GetExplainStatus().atk.ToString();
        weaponMagazineUI.text = weapon.GetExplainStatus().magazine.ToString();


      
    }
}
