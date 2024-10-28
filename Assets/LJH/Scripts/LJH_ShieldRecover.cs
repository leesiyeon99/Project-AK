using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LJH_ShieldRecover : MonoBehaviour
{
    [Header("오브젝트")]
    [SerializeField] GameObject shield;

    [Header("변수")]
    [SerializeField] float durability;
    [SerializeField] bool isBreaked;
    [SerializeField] bool isRecover;
    void Start()
    {
        //gameObject.SetActive(false); 활성화 상태 유지를 위해 삭제 예정
    }

    private void Update()
    {
       durability = shield.GetComponent<LJH_Shield>().durability;
       isBreaked = shield.GetComponent<LJH_Shield>().isBreaked;
       isRecover = shield.GetComponent <LJH_Shield>().isRecover;

        if (isRecover)
        {
            Debug.Log("쉴드리커버생성");
            //durability = shield.GetComponent<LJH_Shield>().durability;
            //isBreaked = shield.GetComponent<LJH_Shield>().isBreaked;

            // Comment: 역장이 파괴 상태일 때, 역장 파괴 쿨다운 코루틴 실행
            if (isBreaked)
            {
                Coroutine breaked = StartCoroutine(ShieldCoolDown());
                if (!isBreaked)
                {
                    Debug.Log("쉴드쿨다운코루틴종료");
                    StopCoroutine(breaked);
                }
            }

            // Comment: 역장이 파괴 상태가 아닐 때, 역장 수복 코루틴 실행
            else if (!isBreaked)
            {
                while (durability < 5)
                {
                    Coroutine recovery = StartCoroutine(RecoveryShield());
            
                    if (durability >= 5)
                    {
                        StopCoroutine(recovery);
                    }
                }
            }
        }
    }
    

    

    // Comment: 역장 파괴 쿨다운 코루틴
    IEnumerator ShieldCoolDown()
    {
        Debug.Log("역장 비활성화 카운트");
        // 2초간 역장 활성화 불가
        yield return new WaitForSecondsRealtime(2.0f);

        
        // Comment: 2초 뒤 RecoveryBreakedShield 코루틴 실행
        Coroutine recovery = StartCoroutine(RecoveryBreakedShield());
        
        if (durability >= 5)
        {
            StopCoroutine(recovery);
        }
    }

    // Comment: 파괴된 역장 수복 코루틴
    IEnumerator RecoveryBreakedShield()
    {
        Debug.Log("역장 복구 시작");
        // 역장 활성화 불가 + 3초 후 역장 수리 완료
        yield return new WaitForSecondsRealtime(3.0f);
        isBreaked = false;
        Debug.Log("역장 복구 완료");
        durability = 5;
        Debug.Log(shield.GetComponent<LJH_Shield>().durability);
        
    }

    // Comment: 역장 수리 코루틴
    // ToDo: 내구도 회복 시간 문의해야함
    IEnumerator RecoveryShield()
    {
        Debug.Log("실드 회복 시작(비파괴)");
        yield return new WaitForSecondsRealtime(1f);
        durability += 1;

    }
}
