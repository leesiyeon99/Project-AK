using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterIndicator : MonoBehaviour
{
    [SerializeField] Image monsterCountBackgroundImage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EliteEnemy"))
        {
            if (other.GetComponent<HYJ_Enemy>() == null) return;
            if (other.GetComponent<HYJ_Enemy>().monsterShieldAtkPower >= 3)
            {
                // 강한 몬스터 나감
                if (other.GetComponent<UnitToScreenBoundary>() != null)
                    other.GetComponent<UnitToScreenBoundary>().isActiveUI = true;
                monsterCountBackgroundImage.color = Color.yellow;

            }
            else
            {
                if (other.GetComponent<UnitToScreenBoundary>() != null)
                    other.GetComponent<UnitToScreenBoundary>().isActiveUI = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EliteEnemy"))
        {
            if (other.GetComponent<HYJ_Enemy>() == null) return;
            if (other.GetComponent<HYJ_Enemy>().monsterShieldAtkPower >= 3)
            {
                // 강한 몬스터 나감
                if (other.GetComponent<UnitToScreenBoundary>() != null)
                    other.GetComponent<UnitToScreenBoundary>().isActiveUI = true;
                monsterCountBackgroundImage.color = Color.yellow;

            }
            else
            {
                if (other.GetComponent<UnitToScreenBoundary>() != null)
                    other.GetComponent<UnitToScreenBoundary>().isActiveUI = true;
            }
        }
    }
}
