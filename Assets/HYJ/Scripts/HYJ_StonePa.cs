using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HYJ_StonePa : MonoBehaviour
{
    [SerializeField] public float nowHp;
    [SerializeField] public float setHp;
    [SerializeField] public bool hitFlag;
    public bool HitFlag { get { return hitFlag; } set { hitFlag = value; } }
    Coroutine hitFlagCoroutine;
    WaitForSeconds hitFlagWaitForSeconds = new WaitForSeconds(0.1f);
    void Start()
    {
        setHp = 100;
        nowHp = setHp;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * 10f * Time.deltaTime);
        if (nowHp <= 0)
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
}
