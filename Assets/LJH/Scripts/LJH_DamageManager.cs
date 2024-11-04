using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class LJH_DamageManager : MonoBehaviour
{
    [Header("버튼")]
    [SerializeField] InputActionReference cheatKeyButton;

    [Header("오브젝트")]
    [Header("무적 관리 오브젝트")]
    [SerializeField] GameObject invincibility;
    [Header("쉴드 오브젝트")]
    [SerializeField] GameObject shield;
    [Header("오디오 매니저")]
    [SerializeField] GameObject audioManager;


    [Header("스크립트")]
    [Header("UIManager 스크립트")]
    [SerializeField] LJH_UIManager uiManagerScript;
    

    
    [Header("현재 체력")]
    public float ljh_curHp;
    [Header("변수")]
    [Header("역장 내구도")]
    [SerializeField] public float durability;
    [Header("무적 활성화 여부")]
    [SerializeField] public bool isInvincibility;
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
    private Coroutine bloodCoroutine;
    [Header("역장 피격 코루틴")]
    private Coroutine shieldCoroutine;


    private void Start()
    {
        cheatKeyButton.action.performed += CheatKey;

        ljh_curHp = 10000;
        durability = shield.GetComponent<LJH_Shield>().durability;

    }
    void Update()
    {
        // Comment: 현재 체력 상황 띄워줌
        uiManagerScript.DisplayHpBar();

    }

    public void DamagedHP(float HPDamage)
    {
        ljh_curHp -= HPDamage;

    }

    public void DamagedShield(float shieldDamage)
    {
        if (durability > 0)
        {

            if (isInvincibility)
            {
                // Comment: 무적 상태일 때, 데미지를 0으로 변경
                float zeroDamage = 0;

                durability -= zeroDamage;
            }
            else if (!isInvincibility)
            {
                durability -= shieldDamage;
                uiManagerScript.UpdateShieldUI(durability);
                invincibility.SetActive(true);

                audioManager.GetComponent<AudioManager>().PlayTakeShield();
            }
            

        }

        if (durability <= 0)
        {
            shield.GetComponent<LJH_Shield>().BreakedShield();
        }
    }

    IEnumerator ShowBloodScreen()
    {
        if (ljh_bloodImage == null) yield break;

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
        if (LSY_SceneManager.Instance.curState == LSY_SceneManager.GameState.GameOver || LSY_SceneManager.Instance.curState == LSY_SceneManager.GameState.GameClear) return;

        if (shield.GetComponent<LJH_Shield>().isShield)
        {
            float damage = monsterScript.GetComponent<HYJ_Enemy>().monsterShieldAtkPower;
            DamagedShield(damage);
            if (shieldCoroutine != null)
            {
                StopCoroutine(shieldCoroutine);
            }
            shieldCoroutine = StartCoroutine(ShowShieldScreen());
        }

        else if (!shield.GetComponent<LJH_Shield>().isShield)
        {
            float damage = monsterScript.GetComponent<HYJ_Enemy>().monsterHpAtkPower;
            DamagedHP(damage);

            if (monsterScript.gameObject.CompareTag("Boss"))
            {
                audioManager.GetComponent<AudioManager>().PlayTakeHp();
            }
            
              

            if (bloodCoroutine != null)
            {
                StopCoroutine(bloodCoroutine);
            }
            bloodCoroutine = StartCoroutine(ShowBloodScreen());
        }
    }

    

    public void CheatKey(InputAction.CallbackContext obj)
    {
        ljh_curHp = 100000;
    }


}

