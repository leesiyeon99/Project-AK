using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class HYJ_Eneme : MonoBehaviour
{
    [SerializeField] GameObject monster;
    [SerializeField] MonsterType monsterType;
    [SerializeField] MonsterAttackType monsterAttackType;
    [SerializeField] float monsterAttackPower;
    [SerializeField] float monsterAttackRange;
    [SerializeField] float setBossHp;


    [SerializeField] float monsterHp;
    [SerializeField] float playerDistance;

    //---------------------------------------------------//

    public float playerAttackPower=20;

    public enum MonsterType
    {
        Nomal,
        Elite,
        Boss
    }

    public enum MonsterAttackType
    {
        shortAttackRange,
        longAttackRange
    }
    void Start()
    {
        MonsterSetHp();
        MonsterSetAttackRange();
    }

    void Update()
    {
        MonsterMover();
    }

    // Comment : 몬스터타입에 따라 몬스터의 체력을 조정한다.
    private void MonsterSetHp()
    {
        if (monsterType == MonsterType.Boss)
        {
            monsterHp = setBossHp;
        }
        else
        {
            monsterHp = 100;
        }
    }

    private void MonsterSetAttackRange()
    {
        if(monsterAttackType == MonsterAttackType.shortAttackRange)
        {
            monsterAttackRange = 3;
        }
        else{
            monsterAttackRange = 7;
        }
    }

    // Comment : Player 태그의 오브젝트를 찾고 해당 오브젝트로 Monster가 이동한다.
    private void MonsterMover()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // 태그를 레이어로 변경시키기!
        if (player != null)
        {
            playerDistance = Vector3.Distance(monster.transform.position, player.transform.position);
            if (playerDistance > monsterAttackRange)
            {
                Debug.Log(playerDistance);
            }
            else
            {
                MonsterAttack();
                //StartCoroutine(MonsterAttackCo());
            }
        }
    }

    //Comment : 온트리거 엔터를 이용하여 총알과의 충돌 여부를 확인, 충돌 시, 캐릭터의 공격력 or 무기의 공격력이 완료되면 몬스터 피격 함수를 진행시킨다.
    private void MonsterHit()
    {
        // TODO : 캐릭터or 무기의 공격력과 총알 구현이 완료되면 몬스터 피격 함수를 진행시킨다.
        // 현재는 임의로 playerAttackPower 변수를 활용하여 작성했다.
        if (monsterType == MonsterType.Nomal)
        {
            monsterHp -= playerAttackPower;
        }
        else if(monsterType == MonsterType.Elite)
        {
            if(playerAttackPower-15 > 0)
            {
                monsterHp -= playerAttackPower - 15;
            }
        }
    }

    private void MonsterAttack()// 코루틴으로?
    {
        Debug.Log("몬스터가 공격!");
    }

    IEnumerator MonsterAttackCo()
    {
        yield return new WaitForSeconds(10f);
        Debug.Log("몬스터가 공격 Co!");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet")){ // 태그를 레이어로 변경시키기!
            MonsterHit();
            //Destroy(other.gameObject);
        }
    }
}
