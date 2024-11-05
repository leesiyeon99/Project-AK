using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HYJ_Boss_Stage2_Object : MonoBehaviour
{
    [SerializeField] HYJ_Boss_Stage2 boss;
    [SerializeField] float recoveryTime;
    [SerializeField] public float magicCircleNowHp;
    [SerializeField] public float magicCircleSetHp;
    //[SerializeField] GameObject magicCirclePrefab;

    [SerializeField] public bool hitFlag;
    public bool HitFlag { get { return hitFlag; } set { hitFlag = value; } }
    Coroutine hitFlagCoroutine;
    WaitForSeconds hitFlagWaitForSeconds = new WaitForSeconds(0.05f);

    float offsetY;

    private void Awake()
    {
        offsetY = transform.position.y;
        boss = GetComponentInParent<HYJ_Boss_Stage2>();
    }

    private void OnEnable()
    {
        recoveryTime = 20f;
        magicCircleSetHp = 300f;
        magicCircleNowHp = magicCircleSetHp;
    }

    private void OnDisable()
    {
       // MagicCircleRecovery();

    }

    private void Update()
    {
        if(magicCircleNowHp <= 0)
        {
            boss.BreakObject();
            transform.position = new Vector3(transform.position.x, -50f, transform.position.z);

            if (MagicCircleRecoveryCo == null)
            MagicCircleRecoveryCo = StartCoroutine(MagicCircleRecovery());

        }
    }

    Coroutine MagicCircleRecoveryCo;
    

    IEnumerator MagicCircleRecovery()
    {
        magicCircleNowHp = magicCircleSetHp;
        yield return new WaitForSeconds(recoveryTime);
     
        transform.position = new Vector3(transform.position.x, offsetY, transform.position.z);
       
        MagicCircleRecoveryCo = null;
    }

    public void MonsterTakeDamageCalculation(float damage)
    {
        magicCircleNowHp -= damage;
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
