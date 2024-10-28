using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerWeaponUI : PlayerWeaponUIBase
{
    [SerializeField] GameObject changeUI;
    [SerializeField] RectTransform changeJoystick;
    [SerializeField] TextMeshProUGUI magazineUI;
    [SerializeField] TextMeshProUGUI[] toggleMagazineUI;

    void Awake()
    {
        BindAll();
    }

    private void Start()
    {
        changeUI = GetUI("ChangeUI");
        changeJoystick = GetUI<RectTransform>("ChangeJoystick");
        magazineUI = GetUI<TextMeshProUGUI>("RemainingBullets");
        toggleMagazineUI[0] = GetUI<TextMeshProUGUI>("Magazine0");
        toggleMagazineUI[1] = GetUI<TextMeshProUGUI>("Magazine1");
        toggleMagazineUI[2] = GetUI<TextMeshProUGUI>("Magazine2");
        toggleMagazineUI[3] = GetUI<TextMeshProUGUI>("Magazine3");
    }
}
