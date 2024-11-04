using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HYJ_Boss_Stage2 : MonoBehaviour
{
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
    [SerializeField] public bool canAttack;

    [Header("기믹 오브젝트")]
    [SerializeField] GameObject fireBallPrefab;
    [SerializeField] GameObject silentBallPrefab;
    [SerializeField] GameObject stonePaPrefab;

    [Header("임의 변수")]
    [SerializeField] public bool hitFlag;
    public bool HitFlag { get { return hitFlag; } set { hitFlag = value; } }
    public bool isAttack;
    public bool nowAttack;
    public bool isDie;
    Coroutine hitFlagCoroutine;
    WaitForSeconds hitFlagWaitForSeconds = new WaitForSeconds(0.05f);
    public float fireBallCoolTime = 10;

    

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.tag = "Boss";
        SetHp = 4000f;
        nowHp = SetHp;

        bossRoutine = StartCoroutine(BossPatternRoutine());
    }

    private void OnDisable()
    {
        StopCoroutine(bossRoutine);
    }

    Coroutine bossRoutine;

    void Update()
    {
        if(nowHp < 50)
        {
            fireBallCoolTime = 4f;
        }
    }

    IEnumerator BossPatternRoutine()
    {
        while (true)
        {
            if(canAttack)
            {
                Defenseless();
            }

            switch (Random.Range(0, 3))
            {
                case 0:
                    yield return FireBall();
                    break;
                case 1:
                    yield return SilentBall();
                    break;
                case 2:
                    yield return StonePa();
                    break;
            }
        }
    }

    IEnumerator MagicCircleDestroy()
    {

        yield return new WaitForSeconds(6f);
    }

    IEnumerator FireBall()
    {
        Debug.Log("파이어 볼");
        monsterShieldAtkPower = 1f;
        monsterHpAtkPower = 3000f;
        //FireBall 오브젝트 제작
        animator.SetTrigger("FireBall");
        Instantiate(fireBallPrefab, new Vector3(monster.transform.position.x, monster.transform.position.y + 2f, monster.transform.position.z + 0.5f), Quaternion.LookRotation(player.transform.position));
        Instantiate(fireBallPrefab, new Vector3(monster.transform.position.x + 2f, monster.transform.position.y + 2f, monster.transform.position.z + 0.5f), Quaternion.LookRotation(player.transform.position));
        Instantiate(fireBallPrefab, new Vector3(monster.transform.position.x - 2f, monster.transform.position.y + 2f, monster.transform.position.z + 0.5f), Quaternion.LookRotation(player.transform.position)); 
        yield return new WaitForSeconds(fireBallCoolTime);
    }

    IEnumerator SilentBall()
    {
        Debug.Log("사일런트 볼");
        // 투척 거미 스크립트를 활용하여 SilentBall 제작
        animator.SetTrigger("SilentBall");
        Instantiate(silentBallPrefab, new Vector3(monster.transform.position.x, monster.transform.position.y + 2f, monster.transform.position.z + 0.5f), Quaternion.LookRotation(player.transform.position));
        yield return new WaitForSeconds(10f);
    }

    IEnumerator StonePa()
    {
        Debug.Log("스톤파");
        monsterShieldAtkPower = 1f;
        monsterHpAtkPower = 1000f;
        // 스톤파 오브젝트 제작 10개
        animator.SetTrigger("StonePa");
        for(int i = 0; i < 10; i++)
        {
            float x = Random.Range(-3,3);
            float y = Random.Range(0.5f,3.5f);
            float z = Random.Range(1,3.5f);
            
            Instantiate(stonePaPrefab, new Vector3(x, y, z),Quaternion.LookRotation(player.transform.position));
        }
        yield return new WaitForSeconds(10f);
    }

    // Comment : 마법진을 파괴하여 보스가 무력화
    IEnumerator Defenseless()
    {
        yield return new WaitForSeconds(5f);
        canAttack = false;
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

    public void KnightCreate()
    {

    }
}
