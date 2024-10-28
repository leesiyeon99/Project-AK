using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LJH_ShieldRecover : MonoBehaviour
{
    [Header("오브젝트")]
    [SerializeField] GameObject shield;

    [Header("내구도 회복량(초당)")]
    [SerializeField] const float REPAIR = 1;

    [Header("변수")]
    [SerializeField] const float MAXDURABILITY = 5;
    [SerializeField] float durability;
    [SerializeField] bool isBreaked;
    [SerializeField] bool isRecover;
    [SerializeField] bool isShield;

    [SerializeField] bool coCheckA;             // Comment: 코루틴 다중 실행 방지용 bool 변수
    [SerializeField] bool coCheckB;
    [SerializeField] bool coCheckC;
    [SerializeField] bool coCheckD;

    int loopNum = 0;                            // ToDo: 무한루프 체킹용 삭제요망
    void Start()
    {
        coCheckA = false;                       // Comment: 코루틴 다중 실행 방지용 bool 변수 False로 스타트
        coCheckB = false;
        coCheckC = false;
        coCheckD = false;
    }

    private void Update()
    {
       durability = shield.GetComponent<LJH_Shield>().durability;
       isBreaked = shield.GetComponent<LJH_Shield>().isBreaked;
       isRecover = shield.GetComponent<LJH_Shield>().isRecover;
       isShield = shield.GetComponent<LJH_Shield>().isShield;

        if (!isShield)
        {


            
            
        }
    }

    // Comment: 역장 수리 코루틴
    // ToDo: 내구도 회복 시간 문의해야함
    IEnumerator RecoveryShield()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Debug.Log("실드 회복 시작");
        coCheckB = true;
        yield return new WaitForSecondsRealtime(1f);
        durability += REPAIR;

    }

    // Comment: 역장 파괴 쿨다운 코루틴
    // IEnumerator ShieldCoolDown()
    // {
    //     coCheckA = true;
    //     Debug.Log("역장 비활성화 카운트");
    //     // 2초간 역장 활성화 불가
    //     yield return new WaitForSecondsRealtime(2.0f);
    //
    //     
    //     // Comment: 2초 뒤 RecoveryBreakedShield 코루틴 실행
    //     Coroutine recoveryB = StartCoroutine(RecoveryBreakedShield());        //Comment: 기확 변경에 따라 필요 없어져 주석처리
    //     
    //     if (durability == MAXDURABILITY)
    //     {
    //         StopCoroutine(recoveryB);
    //     }
    // }

    // Comment: 파괴된 역장 수복 코루틴
    // IEnumerator RecoveryBreakedShield()
    // {
    //     Debug.Log("역장 복구 시작");
    //     // 역장 활성화 불가 + 3초 후 역장 수리 완료
    //     yield return new WaitForSecondsRealtime(3.0f);
    //     isBreaked = false;
    //     Debug.Log("역장 복구 완료");
    //     durability = 5;
    //     Debug.Log(shield.GetComponent<LJH_Shield>().durability);              //Comment: 기확 변경에 따라 필요 없어져 주석처리
    //     
    // }

}
