using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public enum ColliderType { RightMonster, LeftMonster }
public class MonsterCount : MonoBehaviour
{

    [Header("몬스터 인디케이터 UI 이미지")]
    [SerializeField] Image monsterDetectionImageUI;

    [Header("몬스터 인디케이터 배경 이미지")]
    [SerializeField] Image monsterCountBackgroundImage;

    [Header("일반 몬스터 아이콘 이미지")]
    [SerializeField] Image normalEnemyIcon;

    [Header("강한 몬스터 아이콘 이미지")]
    [SerializeField] Image strongEnemyIcon;

    private Coroutine StrongAttackRoutine;
    private Coroutine MonsterDiedScoreMinusRoutine;

    public bool isMiddle = false;
    public ColliderType colType;
    public MonsterCountUI monsterCountUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EliteEnemy"))
        {
            if (other.GetComponent<LSY_Enemy>() == null) return;

            if (other.GetComponent<LSY_Enemy>().lsy_monsterShieldAtkPower >= 3)
            {
                HandleStrongEnemyEntry(other);
            }
            else
            {
                HandleEnemyEntry(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<LSY_Enemy>() == null) return;

        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EliteEnemy"))
        {

            if (other.GetComponent<LSY_Enemy>().lsy_monsterShieldAtkPower >= 3)
            {
                // 강한 몬스터 나감
                HandleStrongEnemyExit(other);
            }
            else
            {
                HandleEnemyExit(other);
            }
        }
    }


    private IEnumerator StrongMonsterAttack()
    {
        WaitForSeconds delay = new WaitForSeconds(0.2f);
        float elapsedTime = 0f;

        while (elapsedTime < 3f)
        {
            normalEnemyIcon.gameObject.SetActive(false);
            strongEnemyIcon.gameObject.SetActive(true);
            yield return delay;

            normalEnemyIcon.gameObject.SetActive(true);
            strongEnemyIcon.gameObject.SetActive(false);
            yield return delay;

            elapsedTime += 0.4f;
        }

        // 3초 뒤 강한 몬스터의 공격이 끝나면 모든 상태 초기화
        monsterDetectionImageUI.color = Color.white;
        monsterCountBackgroundImage.color = Color.white;
        normalEnemyIcon.gameObject.SetActive(true);
        strongEnemyIcon.gameObject.SetActive(false);
        yield break;
    }

    private void HandleEnemyEntry(Collider other)
    {
        monsterCountUI.counters[(int)colType]++;
        LSY_Enemy monster = other.GetComponent<LSY_Enemy>();
        monster.lsy_monsterCount = monsterCountUI;

        if (!monsterCountUI.Enemies.ContainsKey(monster))
        {
            monsterCountUI.Enemies.Add(monster, colType);
            monsterCountUI.isEnter[monster] = true;
        }
        else
        {
            monsterCountUI.isEnter[monster] = true; 
        }

        if (other.GetComponent<UnitToScreenBoundary>() != null)
            other.GetComponent<UnitToScreenBoundary>().isActiveUI = true;

        normalEnemyIcon.gameObject.SetActive(true);
        strongEnemyIcon.gameObject.SetActive(false);
    }

    private void HandleEnemyExit(Collider other)
    {
        monsterCountUI.counters[(int)colType]--;
        LSY_Enemy monster = other.GetComponent<LSY_Enemy>();
        monster.lsy_monsterCount = monsterCountUI;

        if (monsterCountUI.Enemies.ContainsKey(monster))
        {
            monsterCountUI.Enemies.Remove(monster); 
            monsterCountUI.isEnter[monster] = false; 
        }

        Debug.Log("일반 몬스터 화면 안으로 들어옴");
        normalEnemyIcon.gameObject.SetActive(true);
        strongEnemyIcon.gameObject.SetActive(false);
    }

    private void HandleStrongEnemyEntry(Collider other)
    {

        Debug.Log("강한 몬스터 화면 밖으로 나감");
        monsterCountUI.counters[(int)colType]++;
        LSY_Enemy monster = other.GetComponent<LSY_Enemy>();
        monster.lsy_monsterCount = monsterCountUI;
        if (!monsterCountUI.Enemies.ContainsKey(monster))
        {
            monsterCountUI.Enemies.Add(monster, colType);
            monsterCountUI.isEnter[monster] = true;
        }

        if (other.GetComponent<UnitToScreenBoundary>() != null)
            other.GetComponent<UnitToScreenBoundary>().isActiveUI = true;
        if (isMiddle == false)
        {
            monsterDetectionImageUI.color = Color.white;
            monsterCountBackgroundImage.color = Color.yellow;
        }
        else
        {
            monsterCountBackgroundImage.color = Color.yellow;
        }


        StrongAttackRoutine = StartCoroutine(StrongMonsterAttack());

    }

    private void HandleStrongEnemyExit(Collider other)
    {
        monsterCountUI.counters[(int)colType]--;
        LSY_Enemy monster = other.GetComponent<LSY_Enemy>();
        monster.lsy_monsterCount = monsterCountUI;

        if (monsterCountUI.Enemies.ContainsKey(monster))
        {
            monsterCountUI.Enemies.Remove(monster); 
            monsterCountUI.isEnter[monster] = false; 
        }

        if (isMiddle == false)
        {
            monsterDetectionImageUI.color = Color.yellow;
            monsterCountBackgroundImage.color = Color.white;
        }
        else
        {
            monsterCountBackgroundImage.color = Color.white;
        }

        normalEnemyIcon.gameObject.SetActive(true);
        strongEnemyIcon.gameObject.SetActive(false);

        if (StrongAttackRoutine != null)
        {
            StopCoroutine(StrongAttackRoutine);
            StrongAttackRoutine = null;
        }
    }

}
