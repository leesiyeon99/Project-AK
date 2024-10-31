using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Middle : MonoBehaviour
{
    public MonsterCount monsterCountLeft;
    public MonsterCount monsterCountRight;

    // Comment : 왼쪽 충돌체, 오른쪽 충돌체의 사이에서 코드 혼동을 막기 위한 스크립트
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EliteEnemy"))
        {
            if (other.GetComponent<HYJ_Enemy>() == null) return;

            if (other.GetComponent<HYJ_Enemy>().monsterShieldAtkPower >= 3)
            {
                monsterCountLeft.isMiddle = true;
                monsterCountRight.isMiddle = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EliteEnemy"))
        {
            if (other.GetComponent<HYJ_Enemy>() == null) return;

            if (other.GetComponent<HYJ_Enemy>().monsterShieldAtkPower >= 3)
            {
                monsterCountLeft.isMiddle = false;
                monsterCountRight.isMiddle = false;
            }
        }

    }
}
