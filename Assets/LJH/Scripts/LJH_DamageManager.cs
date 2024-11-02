using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LJH_DamageManager : MonoBehaviour
{
    [Header("������Ʈ")]
    [Header("���� ���� ������Ʈ")]
    [SerializeField] GameObject invincibility;
    [Header("���� ������Ʈ")]
    [SerializeField] GameObject shield;
    [Header("�������� ������Ʈ")]
    [SerializeField] GameObject bossMonster;

    [Header("��ũ��Ʈ")]
    [Header("HYJ_Enemy ��ũ��Ʈ")]
    [SerializeField] HYJ_Enemy hyj_EnemyScript;
    [Header("Shield ��ũ��Ʈ")]
    [SerializeField] LJH_Shield shieldScript;
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

    [Header("�����")]
    [Header("���� ���ؽ� ����")]
    [SerializeField] AudioSource damagedShieldSound;
    [Header("ü�� ���ؽ� ����")]
    [SerializeField] AudioSource damagedHPSound;

    private void Start()
    {
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

        //damagedHPSound.Play();

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
                switch(shieldDamage) //  ����Ÿ���� ������ �����ش޶�� ���� 
                {
                    //case : 
                }
                durability -= shieldDamage;
                uiManagerScript.UpdateShieldUI(durability);
                invincibility.SetActive(true);
            }
            //damagedShieldSound.Play();

        }

        if (durability <= 0)
        {
            shield.GetComponent<LJH_Shield>().BreakedShield();
        }
    }

    IEnumerator ShowBloodScreen()
    {
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

            if (bloodCoroutine != null)
            {
                StopCoroutine(bloodCoroutine);
            }
            bloodCoroutine = StartCoroutine(ShowBloodScreen());
        }
    }

}

