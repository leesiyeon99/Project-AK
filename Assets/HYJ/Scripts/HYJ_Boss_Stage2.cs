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
    WaitForSeconds hitFlagWaitForSeconds = new WaitForSeconds(0.1f);
    public float fireBallCoolTime = 10;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.tag = "Boss";
        SetHp = 4000f;
        nowHp = SetHp;


    }

    void Update()
    {
        if(nowHp < 50)
        {
            fireBallCoolTime = 4f;
        }
    }

    IEnumerator MagicCircleDestroy()
    {

        yield return new WaitForSeconds(6f);
    }

    IEnumerator FireBall()
    {
        monsterShieldAtkPower = 1f;
        monsterHpAtkPower = 3000f;
        //FireBall 오브젝트 제작
        Instantiate(fireBallPrefab, new Vector3(monster.transform.position.x, monster.transform.position.y + 2f, monster.transform.position.z + 0.5f), Quaternion.LookRotation(player.transform.position));
        Instantiate(fireBallPrefab, new Vector3(monster.transform.position.x + 2f, monster.transform.position.y + 2f, monster.transform.position.z + 0.5f), Quaternion.LookRotation(player.transform.position));
        Instantiate(fireBallPrefab, new Vector3(monster.transform.position.x - 2f, monster.transform.position.y + 2f, monster.transform.position.z + 0.5f), Quaternion.LookRotation(player.transform.position)); 
        yield return new WaitForSeconds(fireBallCoolTime);
    }

    IEnumerator SilentBall()
    {
        // 투척 거미 스크립트를 활용하여 SilentBall 제작
        Instantiate(silentBallPrefab, new Vector3(monster.transform.position.x, monster.transform.position.y + 2f, monster.transform.position.z + 0.5f), Quaternion.LookRotation(player.transform.position));
        yield return new WaitForSeconds(10f);
    }

    IEnumerator StonePa()
    {
        monsterShieldAtkPower = 1f;
        monsterHpAtkPower = 1000f;
        // 스톤파 오브젝트 제작
        Instantiate(stonePaPrefab);
        yield return new WaitForSeconds(10f);
    }

    public void Defenseless()
    {

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

    
}
