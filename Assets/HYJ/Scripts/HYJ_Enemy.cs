using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Tilemaps.Tile;

public class HYJ_Enemy : MonoBehaviour
{
    [Header("�÷��̾�")]
    [SerializeField] GameObject player;

    [Header("���� ����")]
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

    [Header("�ִϸ��̼�")]
    [SerializeField] public float aniTime;
    [SerializeField] Animator monsterAnimator;

    [Header("���� �Ҵ� ����")]
    public bool isAttack;
    public bool nowAttack;
    public bool isDie;

    //public MonsterCountUI hyj_monsterCount;

    //------------------------���� ����---------------------------//
    [Header("���� ����")]
    [SerializeField] public bool hitFlag;
    public bool HitFlag { get { return hitFlag; } set { hitFlag = value; } }

    Coroutine hitFlagCoroutine;
    WaitForSeconds hitFlagWaitForSeconds = new WaitForSeconds(0.1f);

    public enum MonsterType
    {
        Nomal,
        Elite,
        Boar
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

    // Comment : ���� ���ݹ����� �����Ѵ�.
    public void MonsterSetAttackRange()
    {
        if (monsterAttackType == MonsterAttackType.shortAttackRange) // Comment : �ٰŸ� Ÿ���̶�� ���ݹ����� 3���� �����Ѵ�.
        {
            monsterAttackRange = 3;
        }
        else if (monsterAttackType == MonsterAttackType.longAttackRange) // Commnet : ���Ÿ� Ÿ���̶�� ���� ������ 7�� �����Ѵ�.
        {
            monsterAttackRange = 7;
        }
    }

    // Comment : Player �±��� ������Ʈ�� ã�� �ش� ������Ʈ�� Monster�� �̵��Ѵ�.
    public void MonsterMover()
    {
      
            if (player != null && monsterNowHp > 0)
        {
            if (monsterType == MonsterType.Boar)
            {
                monsterAnimator.SetBool("Run Forward", true);
                return;
            }

            playerDistance = Vector3.Distance(new Vector3(monster.transform.position.x, 0, monster.transform.position.z), new Vector3(player.transform.position.x, 0, player.transform.position.z)); // Comment : �÷��̾�� ������ �Ÿ�(x, z�ุ ���)

            if (playerDistance > monsterAttackRange) // Comment : �÷��̾���� �Ÿ��� ���ݹ��� ���� ��
            {
                Debug.Log("�̵� ��");
                monsterAnimator.SetBool("Run Forward", true);
                monster.transform.position = Vector3.MoveTowards(monster.transform.position, new Vector3(player.transform.position.x, 0, player.transform.position.z), monsterMoveSpeed / 50);
                monster.transform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z)); // Comment : ���� 
            }
            else if (playerDistance <= monsterAttackRange && isAttack == false && monsterNowHp > 0) //Comment : �÷��̾ ������ ���ݹ����� ������ ��
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

    // Comment : ��Ʈ���� ���͸� �̿��Ͽ� �Ѿ˰��� �浹 ���θ� Ȯ��, �浹 ��, ĳ������ ���ݷ� or ������ ���ݷ��� �Ϸ�Ǹ� ���� �ǰ� �Լ��� �����Ų��.
    public void MonsterTakeDamageCalculation(float damage)
    {
        monsterNowHp -= damage;
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

    // Comment : ���� ���� �ڷ�ƾ
    IEnumerator MonsterAttackCo()
    {
        monsterAnimator.SetTrigger("Attack");
        Debug.Log("���� ����");
        yield return new WaitForSeconds(aniTime);
        nowAttack = true;
        //nowAttack = false;
        yield return new WaitForSeconds(1f);
        isAttack = false;
    }

    // Comment : ���� ���
    public void MonsterDie()
    {
        if (monsterNowHp <= 0 && !isDie) // Comment : ������ Hp�� 0�� �Ǹ� ���� ������Ʈ�� �����Ѵ�.
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

            if (monsterType == MonsterType.Nomal)
            {
                WaveTimeline.Instance.DecreaseWaveCount();
                // ScoreUIManager.Instance.AddScore(100);
            }
            else if (monsterType == MonsterType.Elite)
            {
               // ScoreUIManager.Instance.AddScore(500);
            }


            Debug.Log("���� ���");
            isDie = true;
            monsterAnimator.SetTrigger("Die");
            transform.SetParent(null);
            //Destroy(gameObject.GetComponent<SphereCollider>());
            // Destroy(gameObject,2f);

            // ���� ��� �� �����, ������ ����
            WHS_TransparencyController.Instance.StartFadeOut(gameObject, 1);
            WHS_ItemManager.Instance.SpawnItemWithProbability(gameObject.transform.position);

        }
    }

    // Comment : �� ��� ������ ���� �±� ����
    public void MonsterTagSet(MonsterType monsterType)
    {
        if (monsterType == MonsterType.Nomal)
        {
            gameObject.tag = "Enemy";
        }
        else if (monsterType == MonsterType.Elite)
        {
            gameObject.tag = "EliteEnemy";
        }
    }

    /*
    // Comment : �ٸ� ������Ʈ�� �浹 ��
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            //MonsterTakeDamageCalculation();
            // TODO : �浹 ������ �ޱ�
            
            //other.transform.position -> �浹 ����
            // TODO : ���� �浹 ������ �Ӹ� / ���� ������� �Ǻ��ϱ�
            // TODO : �����̸� ���, �Ӹ��� ���������� ������ ǥ��
        }
    }

    // Comment : ����Ÿ�Կ� ���� ������ ü���� �����Ѵ�.
    public void MonsterSetHp()
    {
        if (monsterType == MonsterType.Boss) // Comment : ������ Ÿ���� Boss��� ������ BossHp�� Hp�� �����Ѵ�.
        {
            monsterHp = setBossHp;
        }
        else // Comment : Boss�� �ƴ� Nomal, Elite ���ʹ� Hp�� 100���� �����Ѵ�.
        {
            monsterHp = 100;
        }
    }
    */
}