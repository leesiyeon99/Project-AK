using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HYJ_Boss_Stage1 : MonoBehaviour
{
    [Header("보스 설정")]
    [SerializeField] GameObject monster;
    [SerializeField] float nowHp;
    [SerializeField] float SetHp;
    //[SerializeField] float 
    [SerializeField] public float monsterShieldAtkPower;
    [SerializeField] public float monsterHpAtkPower;
    [SerializeField] public float monsterMoveSpeed;

    [Header("임의 변수")]
    [SerializeField] public bool hitFlag;
    public bool HitFlag { get { return hitFlag; } set { hitFlag = value; } }

    Coroutine hitFlagCoroutine;
    WaitForSeconds hitFlagWaitForSeconds = new WaitForSeconds(0.1f);
    private void Start()
    {
        gameObject.tag = "Boss";
        SetHp = 3500f;
        monsterMoveSpeed = 1.5f;
        nowHp = SetHp;
    }

    private void Update()
    {
        
    }

    private void PatternHeadSpin()
    {
        monsterHpAtkPower = 4000f;
    }
    
    private void PatternBreakDance()
    {

    }

    private void PatternSiiuuuu()
    {

    }
}
