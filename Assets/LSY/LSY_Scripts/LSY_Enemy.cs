using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LSY_Enemy : MonoBehaviour
{
    [Header("플레이어")]
    [SerializeField] GameObject lsy_player;

    [Header("몬스터 설정")]
    [SerializeField] GameObject lsy_monster;
    //[SerializeField] GameObject damageText;
    //[SerializeField] Transform damagePos;
    [SerializeField] public lsy_MonsterType lsy_monsterType;
    [SerializeField] public lsy_MonsterAttackType lsy_monsterAttackType;
    [SerializeField] public float lsy_monsterShieldAtkPower;
    [SerializeField] public float lsy_monsterHpAtkPower;
    [SerializeField] public float lsy_monsterAttackRange;
    [SerializeField] public float lsy_score;
    //[SerializeField] public float setBossHp;
    [SerializeField] public float lsy_monsterHp;
    [SerializeField] public float lsy_monsterMoveSpeed;
    [SerializeField] public float lsy_playerDistance;

    [Header("애니메이션")]
    //[SerializeField] public float aniTime;
    //[SerializeField] Animator monsterAnimator;

    [Header("몬스터 할당 점수")]
    public bool lsy_isAttack;
    public bool lsy_nowAttack;
    public bool lsy_isDie;


    public UnityEvent<Collider> lsy_OnEnemyDied; // 삭제해야함

    //------------------------임의 변수---------------------------//
    [Header("임의 변수")]
    public float lsy_playerAttackPower = 20;
    public bool lsy_isShield;

    [SerializeField] public bool lsy_hitFlag;
    public bool lsy_HitFlag { get { return lsy_hitFlag; } set { lsy_hitFlag = value; } }

    Coroutine lsy_hitFlagCoroutine;
    WaitForSeconds lsy_hitFlagWaitForSeconds = new WaitForSeconds(0.1f);

    //---------------lsy
    public MonsterCountUI lsy_monsterCount;

    public enum lsy_MonsterType
    {
        Nomal,
        Elite,
        Boss
    }

    public enum lsy_MonsterAttackType
    {
        shortAttackRange,
        longAttackRange
    }

    void Start()
    {
        lsy_player = GameObject.FindGameObjectWithTag("Player");
        lsy_isAttack = false;
        lsy_nowAttack = false;
        lsy_isDie = false;
        lsy_MonsterTagSet(lsy_monsterType);
        //MonsterSetHp();
        lsy_MonsterSetAttackRange();
        //StartCoroutine(MonsterDied());
    }

    //IEnumerator MonsterDied()
    //{
    //    yield return new WaitForSeconds(3f);
    //   //monsterHp = 0;
    //}

    void Update()
    {
        if (lsy_isDie == false)
        {
            lsy_MonsterDie();
        }
        //MonsterMover();
    }

    /*
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

    // Comment : 몬스터 공격범위를 조정한다.
    public void lsy_MonsterSetAttackRange()
    {
        if (lsy_monsterAttackType == lsy_MonsterAttackType.shortAttackRange) // Comment : 근거리 타입이라면 공격범위를 3으로 설정한다.
        {
            lsy_monsterAttackRange = 3;
        }
        else if (lsy_monsterAttackType == lsy_MonsterAttackType.longAttackRange) // Commnet : 원거리 타입이라면 공격 범위를 7로 설정한다.
        {
            lsy_monsterAttackRange = 7;
        }
    }

    // Comment : Player 태그의 오브젝트를 찾고 해당 오브젝트로 Monster가 이동한다.
    //public void MonsterMover()
    //{
    //    if (player != null && monsterHp > 0)
    //    {
    //        playerDistance = Vector3.Distance(monster.transform.position, player.transform.position); // Comment : 플레이어와 몬스터의 거리

    //        if (playerDistance > monsterAttackRange) // Comment : 플레이어와의 거리가 공격범위 밖일 때
    //        {
    //            Debug.Log("이동 중");
    //            monsterAnimator.SetBool("Run Forward", true);
    //            monster.transform.position = Vector3.MoveTowards(monster.transform.position, new Vector3(player.transform.position.x, 0, player.transform.position.z), monsterMoveSpeed / 50);
    //            monster.transform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z)); // Comment : 몬스터 
    //        }
    //        else if (playerDistance <= monsterAttackRange && isAttack == false && monsterHp > 0) //Comment : 플레이어가 몬스터의 공격범위로 들어왔을 때
    //        {
    //            monsterAnimator.SetBool("Run Forward", false);
    //            isAttack = true;
    //            StartCoroutine(MonsterAttackCo());
    //        }
    //        else
    //        {

    //        }
    //    }
    //}

    // Comment : 온트리거 엔터를 이용하여 총알과의 충돌 여부를 확인, 충돌 시, 캐릭터의 공격력 or 무기의 공격력이 완료되면 몬스터 피격 함수를 진행시킨다.
    public void lsy_MonsterTakeDamageCalculation(float damage)
    {
        // float -> 
        // Comment : 현재는 임의로 playerAttackPower 변수를 활용하여 작성했다.
        if (lsy_monsterType == lsy_MonsterType.Nomal)
        {
            lsy_monsterHp -= damage;
        }
        else if (lsy_monsterType == lsy_MonsterType.Elite)
        {
            if (lsy_playerAttackPower - 15 > 0)
            {
                lsy_monsterHp -= damage - 15;
            }
        }
    }

    public void lsy_StartHitFlagCoroutine()
    {
        if (lsy_hitFlagCoroutine != null)
        {
            StopCoroutine(lsy_hitFlagCoroutine);
        }
        lsy_hitFlagCoroutine = StartCoroutine(lsy_HitFlagCoroutine());
    }

    IEnumerator lsy_HitFlagCoroutine()
    {
        yield return lsy_hitFlagWaitForSeconds;
        lsy_hitFlag = false;
    }

    // Comment : 몬스터 공격 코루틴
    IEnumerator lsy_MonsterAttackCo()
    {
        //monsterAnimator.SetTrigger("Attack");
        Debug.Log("몬스터 공격");
        //yield return new WaitForSeconds(aniTime);
        lsy_nowAttack = true;
        lsy_nowAttack = false;
        yield return new WaitForSeconds(1f);
        lsy_isAttack = false;
    }

    // Comment : 몬스터 사망
    public void lsy_MonsterDie()
    {
        if (lsy_monsterHp <= 0) // Comment : 몬스터의 Hp가 0이 되면 몬스터 오브젝트를 삭제한다.
        {
            //----------------------------------lsy
            if (lsy_monsterCount != null)
            {
                if (lsy_monsterCount.Enemies.ContainsKey(this))
                {
                    if (lsy_monsterCount.isEnter[this] == true)
                    {
                        ColliderType col = lsy_monsterCount.Enemies[this];
                        lsy_monsterCount.counters[(int)col]--;
                    }
                    lsy_monsterCount.Enemies.Remove(this);
                }

                if (lsy_monsterCount.isEnter.ContainsKey(this))
                {
                    lsy_monsterCount.isEnter[this] = false;
                }

            }

            Debug.Log("몬스터 사망");
            //monsterAnimator.SetTrigger("Die");
            lsy_isDie = true;
            lsy_OnEnemyDied?.Invoke(GetComponent<Collider>());
            Destroy(gameObject.GetComponent<BoxCollider>());
            Destroy(gameObject.GetComponent<Rigidbody>());
            Destroy(gameObject, 2f);
        }
    }

    // Comment : 적 등급 설정에 따른 태그 변경
    public void lsy_MonsterTagSet(lsy_MonsterType monsterType)
    {
        if (monsterType == lsy_MonsterType.Nomal)
        {
            gameObject.tag = "Enemy";
        }
        else if (monsterType == lsy_MonsterType.Elite)
        {
            gameObject.tag = "EliteEnemy";
        }
        else if (monsterType == lsy_MonsterType.Boss)
        {
            gameObject.tag = "Boss";
        }
    }

    // TODO : 몬스터 등급에 따른 이펙트 만들기
    public void lsy_MonsterEffect()
    {

    }

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
    /*
    public void DamgeText(float damage)
    {
        GameObject text = Instantiate(damageText);
        text.transform.position = damagePos.position;
        text.GetComponent<HYJ_DamageText>().damage = damage;
    }
    */
}
