using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LJH_ShieldRecover : MonoBehaviour
{
    [Header("오브젝트")]
    [SerializeField] GameObject shield;

    [Header("변수")]
    float durability;
    bool isBreaked;
    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        // Comment: 역장 파괴 코루틴 실행
        Coroutine braked = StartCoroutine(ShieldCoolDown());
    }

    private void OnEnable()
    {
        durability = shield.GetComponent<LJH_Shield>().durability;
        isBreaked = shield.GetComponent<LJH_Shield>().isBreaked;
    }

    IEnumerator ShieldCoolDown()
    {
        Debug.Log("역장 비활성화 카운트");
        // 2초간 역장 활성화 불가
        yield return new WaitForSeconds(2.0f);


        // 2초 뒤 RecoveryShield 코루틴 실행
        Coroutine recovery = StartCoroutine(RecoveryBreakedShield());

        //ToDo : 코루틴 종료해야함
    }

    IEnumerator RecoveryBreakedShield()
    {
        Debug.Log("역장 복구 시작");
        // 역장 활성화 불가 + 3초 후 역장 수리 완료
        yield return new WaitForSeconds(3.0f);
        isBreaked = false;
        Debug.Log("역장 복구 완료");
        durability = 5;
        Debug.Log(shield.GetComponent<LJH_Shield>().durability);
        
        //ToDo : 코루틴 종료해야함
        
    }
}
