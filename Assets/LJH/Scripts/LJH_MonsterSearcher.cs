using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LJH_MonsterSearcher : MonoBehaviour
{
    [Header("스크립트")]
    [Header("몬스터 정보 스크립트")]
    [SerializeField] HYJ_Enemy monsterStats;
    [Header("데미지 계산 스크립트")]
    [SerializeField] LJH_DamageManager searchMonster;

    [SerializeField] bool isNowAttack;

    
    private void OnTriggerEnter(Collider other)
    {
        // Comment: 트리거엔터에 감지된 ohter가 몬스터일때,
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Comment: 몬스터 스탯 변수에 해당 몬스터 컴포넌트를 넣어줌
            monsterStats = other.GetComponentInParent<HYJ_Enemy>();
        }
        // Comment: 될지 안될지 몰라서 확인 빡시게 해야함, 안 될 확률 큼
    }

    private void OnTriggerStay(Collider other)
    {
        // Comment: 몬스터가 공격을 발동했을 때,
        if (other.gameObject.CompareTag("Enemy"))
        {
            isNowAttack = monsterStats.nowAttack;
            if (isNowAttack)
            {
                // Comment: TakeDamage에 몬스터 스탯 변수에 있는 스텟을 뽑아서 넣어줌
                searchMonster.TakeDamage(monsterStats);
                monsterStats.nowAttack = false;
            }
        }
    }
}
