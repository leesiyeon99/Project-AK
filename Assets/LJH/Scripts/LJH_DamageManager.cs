using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class LJH_DamageManager : MonoBehaviour
{
    [Header("��ư")]
    [SerializeField] InputActionReference cheatKeyButton;

    [Header("������Ʈ")]
    [Header("���� ���� ������Ʈ")]
    [SerializeField] GameObject invincibility;
    [Header("���� ������Ʈ")]
    [SerializeField] GameObject shield;
    [Header("����� �Ŵ���")]
    [SerializeField] GameObject audioManager;


    [Header("��ũ��Ʈ")]
    [Header("UIManager ��ũ��Ʈ")]
    [SerializeField] LJH_UIManager uiManagerScript;
    

    
    [Header("���� ü��")]
    public float ljh_curHp;
    [Header("����")]
    [Header("���� ������")]
    [SerializeField] public float durability;
    [Header("���� Ȱ��ȭ ����")]
    [SerializeField] public bool isInvincibility;
    [Header("������ ���� ���� ������")]
    [SerializeField] float takeShieldDamage;
    [Header("������ ���� ü�� ������")]
    [SerializeField] float takeHpDamage;

    [Header("�̹���")]
    [Header("ü�� �ǰ� �̹���")]
    public Image ljh_bloodImage;
    [Header("���� �ǰ� �̹���")]
    public Image ljh_shieldImage;

    [Header("�ڷ�ƾ")]
    [Header("ü�� �ǰ� �ڷ�ƾ")]
    private Coroutine bloodCoroutine;
    [Header("���� �ǰ� �ڷ�ƾ")]
    private Coroutine shieldCoroutine;


    private void Start()
    {
        cheatKeyButton.action.performed += CheatKey;

        ljh_curHp = 10000;
        durability = shield.GetComponent<LJH_Shield>().durability;

    }
    void Update()
    {
        // Comment: ���� ü�� ��Ȳ �����
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
                // Comment: ���� ������ ��, �������� 0���� ����
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
        // Commet : 1.5�� ���� ���� �̹����� ������������ ����
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
        // Commet : 1.5�� ���� ���� �̹����� ������������ ����
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

