using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LJH_DamageManager : MonoBehaviour
{

    float ljh_curHp = 100;
    public RawImage ljh_bloodImage;
    private Coroutine ljh_bloodCoroutine;
    public Image ljh_shieldImage;
    private Coroutine ljh_shieldCoroutine;                          // 시연님꺼

    [SerializeField] float ljh_durability;
    [SerializeField] bool ljh_isInvincibility;


    [SerializeField] AudioSource ljh_damagedShield;
    [SerializeField] AudioSource ljh_damagedHP;

    [Header("오브젝트")]
    [SerializeField] GameObject ljh_invincibility;
    [SerializeField] GameObject shield;
    [SerializeField] GameObject monster;

    [Header("스크립트")]
    [SerializeField] HYJ_Enemy enemyScript;
    [SerializeField] LJH_Shield shieldScript;
    [SerializeField] LJH_UIManager uiManagerScript;
    

    // Update is called once per frame
    void Update()
    {
        ljh_durability = shield.GetComponent<LJH_Shield>().durability;
        ljh_isInvincibility = GetComponent<LJH_invincibility>().isInvincibility;

        //if(enemyScript.nowAttack)
        //{
            if (shield.GetComponent<LJH_Shield>().isShield)
            {
                float damage = TakeDamage(enemyScript);
                DamagedShield(damage);

                if (ljh_shieldCoroutine != null)
                {
                    StopCoroutine(ljh_shieldCoroutine);
                }
                ljh_shieldCoroutine = StartCoroutine(ShowShieldScreen());
            }
            else if (!shield.GetComponent<LJH_Shield>().isShield)
            {
                float damage = TakeDamage(enemyScript);
                DamagedHP(damage);

                if (ljh_bloodCoroutine != null)
                {
                    StopCoroutine(ljh_bloodCoroutine);
                }
                ljh_bloodCoroutine = StartCoroutine(ShowBloodScreen());
            }

            uiManagerScript.DisplayHpBar();
        //}

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Todo : 이 부분은 방어가 없을 때 피격이 들어왔을 경우 실행되도록
            // Comment : 새로운 피격이 들어올 경우 진행하던 코루틴을 멈추고 재시작되도록
            if (ljh_bloodCoroutine != null)
            {
                StopCoroutine(ljh_bloodCoroutine);
            }
            ljh_bloodCoroutine = StartCoroutine(ShowBloodScreen());
            // Todo : 이 부분은 방어가 켜졌을 때 피격 받으면 실행되도록
            // Comment : 새로운 피격이 들어올 경우 진행하던 코루틴을 멈추고 재시작되도록
            if (ljh_shieldCoroutine != null)
            {
                StopCoroutine(ljh_shieldCoroutine);
            }
            ljh_shieldCoroutine = StartCoroutine(ShowShieldScreen());
        }
    }


    //ToDo: 방식 맞게 재조립해야함


    public void DamagedHP(float HPDamage)
    {
        
        Debug.Log("체력 피해입음");
        ljh_curHp -= HPDamage;
        
        //damaged.Play();
        Debug.Log(ljh_durability);
        
    }


    public void DamagedShield(float shieldDamage)// 인수 지워야함
    {
        if (ljh_durability > 0)
        {
            // ToDo : 피격시 사운드 구현해야함

            if (ljh_isInvincibility)
            {
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

            //damaged.Play();
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

    public float TakeDamage(HYJ_Enemy monsterScript)
    {
        if(shield.GetComponent<LJH_Shield>().isShield)
        {
            float damage;
            //Todo: 머지 이후 적용(퍼블릭 이슈)
            //return damage = monsterScript.GetComponent<HYJ_Enemy>().monsterShieldAtkPower;
            return damage = 1;
        }
    
        else if(!shield.GetComponent<LJH_Shield>().isShield)
        {
            float damage;
            //Todo: 머지 이후 적용(퍼블릭 이슈)
            //return damage = monsterScript.GetComponent<HYJ_Enemy>().monsterHpAtkPower;
            return damage = 1000;
        }
        return 0;
    }
    
}

