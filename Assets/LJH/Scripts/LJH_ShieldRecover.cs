using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LJH_ShieldRecover : MonoBehaviour
{
    [Header("오브젝트")]
    [Header("쉴드 오브젝트")]
    [SerializeField] GameObject shield;
    [Header("데미지 매니저")]
    [SerializeField] GameObject damageManager;

    [Header("스크립트")]
    [Header("UIManager 스크립트")]
    [SerializeField] LJH_UIManager uiManager;

    [Header("변수")]
    [Header("내구도 회복량(초당)")]
    [SerializeField] const float REPAIR = 1;
    [Header("역장 최대 내구도")]
    [SerializeField] const float MAXDURABILITY = 5;
    [Header("역장 내구도")]
    [SerializeField] public float durability;
    [Header("역장 파괴/비파괴 여부")]
    [SerializeField] public bool isBreaked;
    [Header("역장 수리중 여부")]
    [SerializeField] public bool isRecover;
    [Header("역장 활성화 여부")]
    [SerializeField] public bool isShield;

    [Header("코루틴")]
    private Coroutine recovery;

    void Start()
    {
        gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        durability = damageManager.GetComponent<LJH_DamageManager>().durability;
        isBreaked = shield.GetComponent<LJH_Shield>().isBreaked;
        isRecover = shield.GetComponent<LJH_Shield>().isRecover;
        isShield = shield.GetComponent<LJH_Shield>().isShield;

        if (durability != MAXDURABILITY)
        {
            recovery = StartCoroutine(RecoveryShield());
        }

        if (durability < 0)
        {
            durability = 0;
        }
    }

    

    // Comment: 역장 수리 코루틴
    // ToDo: 내구도 회복 시간 문의해야함
    IEnumerator RecoveryShield()
    {
        yield return new WaitForSecondsRealtime(1f);

        while (true)
        {
        yield return new WaitForSecondsRealtime(0.5f);
            durability += REPAIR;
            uiManager.UpdateShieldUI(durability);
            if (durability == MAXDURABILITY)
            {
                isRecover = false;
                isBreaked = false;

                shield.GetComponent<LJH_Shield>().durability = durability;
                damageManager.GetComponent<LJH_DamageManager>().durability = durability;
                shield.GetComponent<LJH_Shield>().isRecover = isRecover;
                shield.GetComponent<LJH_Shield>().isBreaked = isBreaked;
                StopCoroutine(recovery);
                break;
            }
        }
    }
}
