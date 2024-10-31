using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LJH_TestSC : MonoBehaviour
{
    [Header("오브젝트")]
    [Header("무적 관리 오브젝트")]
    [SerializeField] GameObject ljh_invincibility;
    [Header("쉴드 오브젝트")]
    [SerializeField] GameObject shield;
    [Header("몬스터 오브젝트")]
    [SerializeField] GameObject monster;

    [Header("스크립트")]
    [Header("MonsterTest 스크립트")]
    [SerializeField] LJH_monsterTest enemyScript;
    [Header("HYK_Enemy 스크립트")]
    [SerializeField] HYJ_Enemy hyj_EnemyScript;
    [Header("Shield 스크립트")]
    [SerializeField] LJH_Shield shieldScript;
    [Header("UIManager 스크립트")]
    [SerializeField] LJH_UIManager uiManagerScript;

    [Header("변수")]
    [Header("현재 체력")]
    float ljh_curHp = 100;
    [Header("역장 내구도")]
    [SerializeField] float ljh_durability;
    [Header("역장 활성화 여부")]
    [SerializeField] bool ljh_isInvincibility;
    [Header("데미지 계산용 역장 데미지")]
    [SerializeField] float takeShieldDamage;
    [Header("데미지 계산용 체력 데미지")]
    [SerializeField] float takeHpDamage;

    [Header("이미지")]
    [Header("체력 피격 이미지")]
    public Image ljh_bloodImage;
    [Header("역장 피격 이미지")]
    public Image ljh_shieldImage;

    [Header("코루틴")]
    [Header("체력 피격 코루틴")]
    private Coroutine ljh_bloodCoroutine;
    [Header("역장 피격 코루틴")]
    private Coroutine ljh_shieldCoroutine;

    [Header("오디오")]
    [Header("역장 피해시 사운드")]
    [SerializeField] AudioSource ljh_damagedShield;
    [Header("체력 피해시 사운드")]
    [SerializeField] AudioSource ljh_damagedHP;

    void Update()
    {
        ljh_durability = shield.GetComponent<LJH_Shield>().durability;
        ljh_isInvincibility = shield.GetComponent<LJH_Shield>().isInvincibility;

        // Comment: 몬스터에서 공격력을 받아와 TakeDamage에서 실행
        TakeDamage(hyj_EnemyScript);
        // Comment: 현재 체력 상황 띄워줌
        uiManagerScript.DisplayHpBar();

    }

    public void DamagedHP(float HPDamage)
    {
        Debug.Log("체력 피해입음");
        ljh_curHp -= HPDamage;

        //damagedHP.Play();

    }

    public void DamagedShield(float shieldDamage)
    {
        if (ljh_durability > 0)
        {

            if (ljh_isInvincibility)
            {
                // Comment: 무적 상태일 때, 데미지를 0으로 변경
                float zeroDamage = 0;

                Debug.Log("역장 무적 상태");
                ljh_durability -= zeroDamage;
            }
            else if (!ljh_isInvincibility)
            {
                Debug.Log("역장 피해입음");
                ljh_durability -= shieldDamage;
                uiManagerScript.UpdateShieldUI(ljh_durability);
                ljh_invincibility.SetActive(true);
            }

            //damagedShield.Play();
            Debug.Log(ljh_durability);
        }
    }

    IEnumerator ShowBloodScreen()
    {
        ljh_bloodImage.color = new Color(1, 0, 0, UnityEngine.Random.Range(0.9f, 1f));
        float duration = 1.5f;
        float elapsedTime = 0f;
        Color initialColor = ljh_bloodImage.color;
        // Commet : 1.5초 동안 점차 이미지가 투명해지도록 설정
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(initialColor.a, 0, elapsedTime / duration);
            ljh_bloodImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null;
        }
        ljh_bloodImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0);
    }

    IEnumerator ShowShieldScreen()
    {
        ljh_shieldImage.color = new Color(0, 0, 1, UnityEngine.Random.Range(0.9f, 1f));
        float duration = 1.5f;
        float elapsedTime = 0f;
        Color initialColor = ljh_shieldImage.color;
        // Commet : 1.5초 동안 점차 이미지가 투명해지도록 설정
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(initialColor.a, 0, elapsedTime / duration);
            ljh_shieldImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null;
        }
        ljh_shieldImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0);
    }

    public void TakeDamage(HYJ_Enemy monsterScript)
    {
        if (shield.GetComponent<LJH_Shield>().isShield)
        {
            float damage = monsterScript.GetComponent<HYJ_Enemy>().monsterShieldAtkPower;
            DamagedShield(damage);

            if (ljh_shieldCoroutine != null)
            {
                StopCoroutine(ljh_shieldCoroutine);
            }
            ljh_shieldCoroutine = StartCoroutine(ShowShieldScreen());
        }

        else if (!shield.GetComponent<LJH_Shield>().isShield)
        {
            float damage = monsterScript.GetComponent<HYJ_Enemy>().monsterHpAtkPower;
            DamagedHP(damage);

            if (ljh_bloodCoroutine != null)
            {
                StopCoroutine(ljh_bloodCoroutine);
            }
            ljh_bloodCoroutine = StartCoroutine(ShowBloodScreen());
        }
    }

}

