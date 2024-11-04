using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HYJ_Boss_Stage2_Object : MonoBehaviour
{
    [SerializeField] float recoveryTime;
    [SerializeField] float magicCircleHp;

    private void Start()
    {
        recoveryTime = 5f;
        magicCircleHp = 300f;
    }

    private void Update()
    {
        if(magicCircleHp <= 0)
        {

            StartCoroutine(MagicCircleRecovery());
        }
    }

    IEnumerator MagicCircleRecovery()
    {

        yield return new WaitForSeconds(recoveryTime);
    }
}
