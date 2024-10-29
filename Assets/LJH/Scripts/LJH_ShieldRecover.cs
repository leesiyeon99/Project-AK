using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LJH_ShieldRecover : MonoBehaviour
{
    [Header("오브젝트")]
    [SerializeField] GameObject shield;

    [SerializeField] LJHTest test;

    [Header("내구도 회복량(초당)")]
    [SerializeField] const float REPAIR = 1;

    [Header("변수")]
    [SerializeField] const float MAXDURABILITY = 5;
    [SerializeField] public float durability;
    [SerializeField] public bool isBreaked;
    [SerializeField] public bool isRecover;
    [SerializeField] public bool isShield;

    void Awake()
    {
        gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        durability = shield.GetComponent<LJH_Shield>().durability;
        isBreaked = shield.GetComponent<LJH_Shield>().isBreaked;
        isRecover = shield.GetComponent<LJH_Shield>().isRecover;
        isShield = shield.GetComponent<LJH_Shield>().isShield;

        if (durability != MAXDURABILITY)
        {
            Coroutine recovery = StartCoroutine(RecoveryShield());

            if (!isRecover)
            {
                StopCoroutine(recovery);
            }
        }
    }

    

    // Comment: 역장 수리 코루틴
    // ToDo: 내구도 회복 시간 문의해야함
    IEnumerator RecoveryShield()
    {
        
        Debug.Log("실드 회복 시작");
        yield return new WaitForSecondsRealtime(1f);

        while (true)
        {
        yield return new WaitForSecondsRealtime(0.5f);
            Debug.Log("내구도 1 회복");
            durability += REPAIR;
            test.UpdateShieldUI(durability); //이거임
            if (durability == MAXDURABILITY)
            {
                Debug.Log("회복멈춤");
                isRecover = false;
                isBreaked = false;

                shield.GetComponent<LJH_Shield>().isRecover = isRecover;
                shield.GetComponent<LJH_Shield>().isBreaked = isBreaked;
                break;
            }
        }
    }

    


    
}
