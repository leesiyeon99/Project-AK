using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LJH_DamageManager : MonoBehaviour
{

    private float lsy_hp = 100;
    private Color curColor;
    private readonly Color initColor = Color.green;

    [SerializeField] float curHp;
    public Image hpBar;

    [SerializeField] float durability;
    [SerializeField] float shieldATK;
    [SerializeField] float HPATK;
    [SerializeField] bool isInvincibility;
    [SerializeField] float HP;

    [SerializeField] AudioSource damagedShield;
    [SerializeField] AudioSource damagedHP;


    [SerializeField] GameObject invincibility;
    private void Start()
    {
        curColor = initColor;
        hpBar.color = initColor;
    }

    // Update is called once per frame
    void Update()
    {
        durability = GetComponent<LJH_Shield>().durability;
        //shiledATK = GetComponent<몬스터스크립트>().쉴드공격력 - 용진님꺼
        //HPATK = GetComponent<몬스터스크립트>().체력공격력 - 용진님꺼
        isInvincibility = GetComponent<LJH_invincibility>().isInvincibility;
    }


    public void DamagedHp()
    {
        if (durability > 0)
        {
            Debug.Log("캐릭터 피해입음");

            HP -= shieldATK;

            damagedHP.Play();
            Debug.Log(HP);
        }
    }


    public void DamagedShield()
    {
        if (durability > 0)
        {
            Debug.Log("역장 피해입음");

            if (isInvincibility)
            {
                shieldATK = 0;
                durability -= shieldATK;
            }
            else if (!isInvincibility)
            {
                durability -= shieldATK;
                Instantiate(invincibility);
            }

            damagedShield.Play();
            Debug.Log(durability);
        }
    }

    private void DisplayHpBar()
    {
        float hpPercentage = curHp / lsy_hp;
        if (hpPercentage > 0.5f)
        {
            curColor = Color.green;
        }
        else if (hpPercentage > 0.3f)
        {
            curColor = Color.yellow;
        }
        else
        {
            curColor = Color.red;
        }
        hpBar.color = curColor;
        hpBar.fillAmount = hpPercentage;
    }

   // public int TakeDamage(GameObject monster)
   // {
   //     if(역장 활성화)
   //     {
   //         return 공격력 = 인수값 몬스터의 (쉴드용)공격력;
   //     }
   //
   //     else if(역장 비활성화)
   //     {
   //         return 공격력 = 인수값 몬스터의 (체력용)공격력;
   //     }
   //     return 0;
   // }
}
