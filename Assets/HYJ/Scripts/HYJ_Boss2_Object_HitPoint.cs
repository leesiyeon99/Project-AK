using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HYJ_Boss2_Object_HitPoint : MonoBehaviour
{
    [SerializeField] HYJ_Boss_Stage2_Object magicCircle;
    [SerializeField] bool weak;

    [Header("데미지 텍스트 설정")]
    [SerializeField] public GameObject canvas;
    [SerializeField] public Text damageText;

    private void Awake()
    {
        magicCircle = GetComponentInParent<HYJ_Boss_Stage2_Object>();
    }

    public bool TakeDamage(float damage)
    {
        Debug.Log("피격");
        if (magicCircle.HitFlag == false)
        {
            if (weak)
            {
                Debug.Log("약점");
                magicCircle.MonsterTakeDamageCalculation(damage);
            }
            else
            {
                Debug.Log("일반");
                magicCircle.MonsterTakeDamageCalculation(damage);
            }

            DamageText(weak, damage);
            magicCircle.HitFlag = true;
            magicCircle.StartHitFlagCoroutine();

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetHitFlag()
    {
        return magicCircle.HitFlag;
    }


    public void DamageText(bool isWeak, float damage)
    {
        // 몬스터 위에 데미지 폰트 생성
        // 데미지 set damge로 설정
        // 데미지 color 설정 (약점이면 빨강/아니면 하얀색)
        Debug.Log(isWeak);
        Debug.Log(damage);
        StartCoroutine(OnDamageText(isWeak, damage));
        damageText.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2, 0));
    }

    public IEnumerator OnDamageText(bool isWeak, float damage)
    {
        if (isWeak)
        {
            damage = damage * 2f;
            damageText.fontSize = 70;
            //damageText 굵게
            damageText.text = "<b>" + damage.ToString() + "</b>";
        }
        else if (!isWeak)
        {
            damageText.fontSize = 60;
            //damageText 굵지 않게
            damageText.text = damage.ToString();
        }
        canvas.SetActive(true);
        float colorHpF = (magicCircle.magicCircleNowHp / magicCircle.magicCircleSetHp) * 255;
        byte colorHpB = (byte)colorHpF;

        damageText.color = new Color32(255, colorHpB, colorHpB, 255);

        for (int i = damageText.fontSize; i >= 30; i--)
        {
            damageText.fontSize = i;
            yield return new WaitForFixedUpdate(); // 다음 FixedUpdte까지 기다림
        }

        yield return new WaitForSeconds(0.2f);
        damageText.text = "";
    }
}
