using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class HYJ_Boss_Stage1 : MonoBehaviour
{
    [Header("데미지매니저")]
    [SerializeField] LJH_DamageManager damagerManager;

    [Header("플레이어")]
    [SerializeField] GameObject player;
    [Header("보스 설정")]
    [SerializeField] Animator animator;
    [SerializeField] GameObject monster;
    [SerializeField] public float nowHp;
    [SerializeField] public float SetHp;
    //[SerializeField] float
    [SerializeField] public float monsterShieldAtkPower;
    [SerializeField] public float monsterHpAtkPower;
    [SerializeField] public float monsterMoveSpeed;
    [Header("임의 변수")]
    [SerializeField] public bool hitFlag;
    public bool HitFlag { get { return hitFlag; } set { hitFlag = value; } }
    public bool isAttack;
    public bool nowAttack;
    public bool isDie;
    Coroutine hitFlagCoroutine;
    WaitForSeconds hitFlagWaitForSeconds = new WaitForSeconds(0.05f);
    private bool firstBattleEnd = false;
    private bool pFirst = false;
    private bool pSecond = false;
    private bool p10 = false;
    private bool p40 = false;
    private bool p70 = false;
    [SerializeField] float xNow = 0;
    [SerializeField] float xMoveDirection = 0.1f;
    private bool isSiuu = false;
    bool isPattern = false;

    bool isBossDie;

    public void OnEnable()
    {
        isBossDie = false;
    }
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.tag = "Boss";
        SetHp = 3500f;
        monsterMoveSpeed = 1.5f;
        nowHp = SetHp;
    }
    private void Update()
    {
        if (!isBossDie)
        {
            MonsterDie();
        }
        BossMove();
        if (!firstBattleEnd && !pFirst && !pSecond)
        {
            BattleStartCO = StartCoroutine(BossBattleStart());
        }
        else if (firstBattleEnd && pFirst && pSecond)
        {
            StopCoroutine(BattleStartCO);
            if (BossAiCo == null)
            {
                BossAiCo = StartCoroutine(BossAI());
            }
        }
    }
    Coroutine BattleStartCO;
    Coroutine BossAiCo;
    // Comment : 헤드스핀 패턴
    IEnumerator PatternHeadSpin()
    {
        Debug.Log("헤드스핀");
        monsterHpAtkPower = 4000f;
        monsterShieldAtkPower = 5f;
        animator.SetBool("HeadSpin", true);
        nowAttack = true;
        damagerManager.BossTakeDamage(this.gameObject);

        yield return new WaitForSeconds(2);
    }
    // Comment : 브레이크댄스 패턴
    IEnumerator PatternBreakDance()
    {
        Debug.Log("브레이크댄스");
        monsterHpAtkPower = 1000;
        monsterShieldAtkPower = 3;
        animator.SetBool("BreakDance", true);
        //isSiuu =true;
        nowAttack = true;
        damagerManager.BossTakeDamage(this.gameObject);
        yield return new WaitForSeconds(5.5f);
        //isSiuu=false;
    }
    // Comment : 세레모니 패턴
    IEnumerator PatternSiiuuuu()
    {
        isSiuu = true;
        Debug.Log("세레머니");
        monsterHpAtkPower = 3000f;
        monsterShieldAtkPower = 1f;
        animator.SetBool("Siu", true);
        nowAttack = true;
        damagerManager.BossTakeDamage(this.gameObject);
        Vector3 beforeBossPos = monster.transform.position; //원위치 용
        while (Vector3.Distance(player.transform.position, monster.transform.position) > 1f)
        {
            monster.transform.position = Vector3.MoveTowards(monster.transform.position, player.transform.position, Time.deltaTime * 20f);
            yield return null;
        }
        animator.SetBool("Siu", false);
        monster.transform.position = beforeBossPos;
        isSiuu = false;
    }
    // Comment : 보스 죽음 패턴
    private void MonsterDie()
    {
        if (nowHp <= 0)
        {
            isBossDie = true;
            //사망 애니메이션
            //monsterAnimator.SetTrigger("Die");
            Debug.Log("사망");
            Destroy(gameObject, 2f);
            LSY_SceneManager.Instance.GameClear();
        }
    }
    // Comment : 보스 조우 패턴
    IEnumerator BossBattleStart()
    {
        if (!pFirst)
        {
            // Comment : 첫 패턴으로 브레이크 댄스를 한다.
            Debug.Log("보스 학습용 첫 패턴으로 브레이크 댄스를 한다.");
            PatternBreakDance();
            pFirst = true;
            yield return new WaitForSeconds(4f);
        }
        if (pFirst && !pSecond)
        {
            // Comment : 브레이크 댄스를 한 후, 세레모니 패턴을 한다.
            Debug.Log("보스 학습용 두번째 패턴으로 세레모니 패턴을 한다.");
            PatternSiiuuuu();
            pSecond = true;
            yield return new WaitForSeconds(4f);
            firstBattleEnd = true;
        }
    }
    // Comment : 보스의 패턴 AI
    IEnumerator BossAI()
    {
        while (true)
        {
            if (nowHp < 2450f && !p70)
            {
                // Comment : 보스 HP가 처음으로 70퍼 아래가 되어 헤드스핀을 사용한다.
                p70 = true;
                PatternHeadSpin();
                Debug.Log("보스 HP가 처음으로 70퍼 아래가 되어 헤드스핀을 사용한다.");
                yield return new WaitForSeconds(4f);
            }
            else if (nowHp < 1400f && !p40)
            {
                // Comment : 보스 HP가 처음으로 40퍼 아래가 되어 헤드스핀을 사용한다."
                p40 = true;
                PatternHeadSpin();
                Debug.Log("보스 HP가 처음으로 40퍼 아래가 되어 헤드스핀을 사용한다.");
                yield return new WaitForSeconds(4f);
            }
            else if (0 < nowHp && nowHp < 350f && !p10)
            {
                // Comment : 보스 HP가 처음으로 10퍼 아래가 되어 헤드스핀을 사용한다.
                p10 = true;
                PatternHeadSpin();
                Debug.Log("보스 HP가 처음으로 10퍼 아래가 되어 헤드스핀을 사용한다.");
                yield return new WaitForSeconds(4f);
            }
            switch (Random.Range(0, 2))
            {
                case 0:
                    yield return PatternSiiuuuu();
                    break;
                case 1:
                    yield return PatternBreakDance();
                    break;
            }
            yield return new WaitForSeconds(4);
        }
    }
    public void MonsterTakeDamageCalculation(float damage)
    {
        nowHp -= damage;
    }
    public void StartHitFlagCoroutine()
    {
        if (hitFlagCoroutine != null)
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
    // Comment : 보스 이동
    void BossMove()
    {
        if (!isSiuu)
        {
            float xMax = 8f;
            float xMin = -8f;
            xNow += xMoveDirection;
            monster.transform.position = new Vector3(xNow, monster.transform.position.y, monster.transform.position.z);
            if (xNow >= xMax)
            {
                Debug.Log("방향 전환");
                xMoveDirection = -Time.deltaTime * 3f;
            }
            else if (xNow <= xMin)
            {
                xMoveDirection = Time.deltaTime * 3f;
            }
        }
    }
    void SiuuMove()
    {
        Vector3 beforeBossPos = monster.transform.position; //원위치 용
        transform.Translate(transform.forward * 3f * Time.deltaTime);
    }
}