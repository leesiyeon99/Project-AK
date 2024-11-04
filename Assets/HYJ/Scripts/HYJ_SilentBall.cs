using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HYJ_SilentBall : MonoBehaviour
{
    [SerializeField] LJH_UIManager player;
    [SerializeField] public float nowHp;
    [SerializeField] public float setHp;
    [SerializeField] public bool hitFlag;
    public bool HitFlag { get { return hitFlag; } set { hitFlag = value; } }
    Coroutine hitFlagCoroutine;
    WaitForSeconds hitFlagWaitForSeconds = new WaitForSeconds(0.1f);
    private void Awake()
    {
        player = GetComponent<LJH_UIManager>();
    }

    void Start()
    {
        setHp = 600;
        nowHp = setHp;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * 2f * Time.deltaTime);
        if(nowHp <= 0)
        {
            Destroy(gameObject);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            // 만약 안되면 위에 Damage매니저로 변경
            player.ljh_curHp = 0;
        }
    }
}
