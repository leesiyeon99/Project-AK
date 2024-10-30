using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LJH_HPBar : MonoBehaviour
{
    [Header("Ã¼·Â UI")]
    private float ljh_MaxHP = 100;
    private Color ljh_curColor;
    private readonly Color ljh_initColor = Color.green;
    [SerializeField] float ljh_curHp = 100;
    public Image ljh_hpBar;
    public Image ljh_bloodImage;
    private Coroutine ljh_bloodCoroutine;
    public Image ljh_shieldImage;
    private Coroutine ljh_shieldCoroutine;                          // ½Ã¿¬´Ô²¨

    private void Update()
    {
        DisplayHpBar();
    }


    private void DisplayHpBar()
    {
        float hpPercentage = ljh_curHp / ljh_MaxHP;
        if (hpPercentage > 0.5f)
        {
            ljh_curColor = Color.green;
        }
        else if (hpPercentage > 0.3f)
        {
            ljh_curColor = Color.yellow;
        }
        else
        {
            ljh_curColor = Color.red;
        }
        ljh_hpBar.color = ljh_curColor;
        ljh_hpBar.fillAmount = hpPercentage;
    }

}
