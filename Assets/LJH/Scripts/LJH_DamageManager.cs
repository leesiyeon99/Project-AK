/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LJH_DamageManager : MonoBehaviour
{

    private float ljh__HP = 100;
    private Color ljh_curColor;
    private readonly Color ljh_initColor = Color.green;
    float ljh_curHp = 100;
    public Image ljh_hpBar;
    public Image ljh_bloodImage;
    private Coroutine ljh_bloodCoroutine;
    public Image ljh_shieldImage;
    private Coroutine ljh_shieldCoroutine;                          // 시연님꺼

    [SerializeField] float ljh_durability;
    [SerializeField] float ljh_shieldATK;
    [SerializeField] float ljh_HPATK;
    [SerializeField] bool ljh_isInvincibility;
    [SerializeField] float ljh_HP;

    [SerializeField] AudioSource ljh_damagedShield;
    [SerializeField] AudioSource ljh_damagedHP;


    [SerializeField] GameObject ljh_invincibility;
    private void Start()
    {
        ljh_curColor = ljh_initColor;
        ljh_hpBar.color = ljh_initColor;
    }

    // Update is called once per frame
    void Update()
    {
        ljh_durability = GetComponent<LJH_Shield>().durability;
        //shiledATK = GetComponent<몬스터스크립트>().쉴드공격력 - 용진님꺼
        //HPATK = GetComponent<몬스터스크립트>().체력공격력 - 용진님꺼
        ljh_isInvincibility = GetComponent<LJH_invincibility>().isInvincibility;
    }

   // private void OnTriggerEnter(Collider other)
   // {
   //     if (other.gameObject.CompareTag("Enemy"))
   //     {
   //         DisplayHpBar();
   //         // Todo : 이 부분은 방어가 없을 때 피격이 들어왔을 경우 실행되도록
   //         // Comment : 새로운 피격이 들어올 경우 진행하던 코루틴을 멈추고 재시작되도록
   //         if (bloodCoroutine != null)
   //         {
   //             StopCoroutine(bloodCoroutine);
   //         }
   //         bloodCoroutine = StartCoroutine(ShowBloodScreen());
   //         // Todo : 이 부분은 방어가 켜졌을 때 피격 받으면 실행되도록
   //         // Comment : 새로운 피격이 들어올 경우 진행하던 코루틴을 멈추고 재시작되도록
   //         if (shieldCoroutine != null)
   //         {
   //             StopCoroutine(shieldCoroutine);
   //         }
   //         shieldCoroutine = StartCoroutine(ShowShieldScreen());
   //     }
   // }
   //   ToDo: 방식 맞게 재조립해야함


    public void DamagedHp()
    {
        if (ljh_durability > 0)
        {
            Debug.Log("캐릭터 피해입음");

            ljh_HP -= ljh_shieldATK;

            ljh_damagedHP.Play();
            Debug.Log(ljh_HP);
        }
    }


    public void DamagedShield()
    {
        if (ljh_durability > 0)
        {
            Debug.Log("역장 피해입음");

            if (ljh_isInvincibility)
            {
                ljh_shieldATK = 0;
                ljh_durability -= ljh_shieldATK;
            }
            else if (!ljh_isInvincibility)
            {
                ljh_durability -= ljh_shieldATK;
                Instantiate(ljh_invincibility);
            }

            ljh_damagedShield.Play();
            Debug.Log(ljh_durability);
        }
    }

    private void DisplayHpBar()
    {
        float hpPercentage = ljh_curHp / ljh_HP;
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

    IEnumerator ShowBloodScreen()
    {
        ljh_bloodImage.color = new Color(1, 0, 0, UnityEngine.Random.Range(0.4f, 0.5f));
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
        ljh_shieldImage.color = new Color(0, 0, 1, UnityEngine.Random.Range(0.4f, 0.5f));
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
*/
