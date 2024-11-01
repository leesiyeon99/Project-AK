using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Tilemaps.Tile;

public class HYJ_Enemy : MonoBehaviour
{
    [Header("플레이어")]
    [SerializeField] GameObject player;

    [Header("몬스터 설정")]
    [SerializeField] GameObject monster;
    [SerializeField] public MonsterType monsterType;
    [SerializeField] public MonsterAttackType monsterAttackType;
    [SerializeField] public float monsterShieldAtkPower;
    [SerializeField] public float monsterHpAtkPower;
    [SerializeField] public float monsterAttackRange;
    [SerializeField] public float monsterNowHp;
    [SerializeField] public float monsterSetHp;
    [SerializeField] public float monsterMoveSpeed;
    [SerializeField] public float playerDistance;

    [Header("애니메이션")]
    [SerializeField] public float aniTime;
    [SerializeField] Animator monsterAnimator;

    [Header("몬스터 할당 점수")]
    public bool isAttack;
    public bool nowAttack;
    public bool isDie;

    public MonsterCountUI hyj_monsterCount;

    public UnityEvent<Collider> OnEnemyDied;

    //------------------------임의 변수---------------------------//
    [Header("임의 변수")]
    [SerializeField] public bool hitFlag;
    public bool HitFlag { get { return hitFlag; } set { hitFlag = value; } }

    Coroutine hitFlagCoroutine;
    WaitForSeconds hitFlagWaitForSeconds = new WaitForSeconds(0.1f);
    
    public enum MonsterType
    {
        Nomal,
        Elite
    }

    public enum MonsterAttackType
    {
        shortAttackRange,
        longAttackRange
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        isAttack = false;
        nowAttack = false;
        isDie = false;
        monsterNowHp = monsterSetHp;
        MonsterTagSet(monsterType);
        //MonsterSetHp();
        MonsterSetAttackRange();
    }

    void Update()
    {
        MonsterDie();
        MonsterMover();
    }

    // Comment : 몬스터 공격범위를 조정한다.
    public void MonsterSetAttackRange()
    {
        if(monsterAttackType == MonsterAttackType.shortAttackRange) // Comment : 근거리 타입이라면 공격범위를 3으로 설정한다.
        {
            monsterAttackRange = 3;
        }
        else if(monsterAttackType == MonsterAttackType.longAttackRange) // Commnet : 원거리 타입이라면 공격 범위를 7로 설정한다.
        { 
            monsterAttackRange = 7;
        }
    }

    // Comment : Player 태그의 오브젝트를 찾고 해당 오브젝트로 Monster가 이동한다.
    public void MonsterMover()
    {
        if (player != null && monsterNowHp > 0)
        {
            playerDistance = Vector3.Distance(new Vector3(monster.transform.position.x, 0, monster.transform.position.z), new Vector3(player.transform.position.x, 0, player.transform.position.z)); // Comment : 플레이어와 몬스터의 거리(x, z축만 계산)
            
            if (playerDistance > monsterAttackRange) // Comment : 플레이어와의 거리가 공격범위 밖일 때
            {
                Debug.Log("이동 중");
                monsterAnimator.SetBool("Run Forward",true);
                monster.transform.position = Vector3.MoveTowards(monster.transform.position, new Vector3(player.transform.position.x,0,player.transform.position.z), monsterMoveSpeed/50);
                monster.transform.LookAt(new Vector3(player.transform.position.x,0,player.transform.position.z)); // Comment : 몬스터 
            }
            else if(playerDistance <= monsterAttackRange && isAttack==false && monsterNowHp > 0) //Comment : 플레이어가 몬스터의 공격범위로 들어왔을 때
            {
                monsterAnimator.SetBool("Run Forward", false);
                isAttack = true;
                StartCoroutine(MonsterAttackCo());
            }
            else
            {

            }
        }
    }

    // Comment : 온트리거 엔터를 이용하여 총알과의 충돌 여부를 확인, 충돌 시, 캐릭터의 공격력 or 무기의 공격력이 완료되면 몬스터 피격 함수를 진행시킨다.
    public void MonsterTakeDamageCalculation(float damage)
    {
        monsterNowHp -= damage;
    }

    public void StartHitFlagCoroutine()
    {
        if(hitFlagCoroutine != null)
        {
            StopCoroutine(hitFlagCoroutine);
        }
        hitFlagCoroutine = StartCoroutine(HitFlagCoroutine());
    }

    IEnumerator HitFlagCoroutine()
    {
        yield return hitFlagWaitForSeconds;
        hitFlag = false;
    }

    // Comment : 몬스터 공격 코루틴
    IEnumerator MonsterAttackCo()
    {
        monsterAnimator.SetTrigger("Attack");
        Debug.Log("몬스터 공격");
        yield return new WaitForSeconds(aniTime);
        nowAttack = true;
        //nowAttack = false;
        yield return new WaitForSeconds(1f);
        isAttack = false;
    }

    // Comment : 몬스터 사망
    public void MonsterDie()
    {
        if(monsterNowHp <= 0 && !isDie) // Comment : 몬스터의 Hp가 0이 되면 몬스터 오브젝트를 삭제한다.
        {
            
          // if (hyj_monsterCount != null)
          // {
          //     if (hyj_monsterCount.Enemies.ContainsKey(this))
          //     {
          //         if (hyj_monsterCount.isEnter[this] == true)
          //         {
          //             ColliderType col = hyj_monsterCount.Enemies[this];
          //             hyj_monsterCount.counters[(int)col]--;
          //         }
          //         hyj_monsterCount.Enemies.Remove(this);
          //     }
          //     if (hyj_monsterCount.isEnter.ContainsKey(this))
          //     {
          //         hyj_monsterCount.isEnter[this] = false;
          //         //this.gameObject.GetComponent<UnitToScreenBoundary>().image.color = Color.white;
          //     }
          // }
          // 
          /*
           if (monsterType == MonsterType.Nomal)
           {
               WaveTimeline.Instance.DecreaseWaveCount();
              // ScoreUIManager.Instance.AddScore(100);
           }
           else if (monsterType == MonsterType.Elite)
           {
               ScoreUIManager.Instance.AddScore(500);
           }
            */

            Debug.Log("몬스터 사망");
            isDie = true;
            monsterAnimator.SetTrigger("Die");
            OnEnemyDied?.Invoke(GetComponent<Collider>());
            //Destroy(gameObject.GetComponent<SphereCollider>());
            // Destroy(gameObject,2f);
            WHS_TransparencyController.Instance.StartFadeOut(gameObject, 1);
            WHS_ItemManager.Instance.SpawnItemWithProbability(gameObject.transform.position);

        }
    }

    // Comment : 적 등급 설정에 따른 태그 변경
    public void MonsterTagSet(MonsterType monsterType)
    {
        if(monsterType == MonsterType.Nomal)
        {
            gameObject.tag = "Enemy";
        }
        else if (monsterType == MonsterType.Elite)
        {
            gameObject.tag = "EliteEnemy";
        }
    }

    /*
    // Comment : 다른 오브젝트와 충돌 시
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            //MonsterTakeDamageCalculation();
            // TODO : 충돌 지점을 받기
            
            //other.transform.position -> 충돌 지점
            // TODO : 받은 충돌 지점이 머리 / 몸통 어디인지 판별하기
            // TODO : 몸통이면 흰색, 머리면 빨간색으로 데미지 표기
        }
    }

    // Comment : 몬스터타입에 따라 몬스터의 체력을 조정한다.
    public void MonsterSetHp()
    {
        if (monsterType == MonsterType.Boss) // Comment : 몬스터의 타입이 Boss라면 설정한 BossHp로 Hp가 설정한다.
        {
            monsterHp = setBossHp;
        }
        else // Comment : Boss가 아닌 Nomal, Elite 몬스터는 Hp가 100으로 설정한다.
        {
            monsterHp = 100;
        }
    }
    */
}