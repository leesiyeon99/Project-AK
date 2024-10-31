using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Middle : MonoBehaviour
{
    public MonsterCount monsterCountLeft;
    public MonsterCount monsterCountRight;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EliteEnemy"))
        {
            if (other.GetComponent<LSY_Enemy>() == null) return;

            if (other.GetComponent<LSY_Enemy>().lsy_monsterShieldAtkPower >= 3)
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
            if (other.GetComponent<LSY_Enemy>() == null) return;

            if (other.GetComponent<LSY_Enemy>().lsy_monsterShieldAtkPower >= 3)
            {
                monsterCountLeft.isMiddle = false;
                monsterCountRight.isMiddle = false;
            }
        }

    }
}
