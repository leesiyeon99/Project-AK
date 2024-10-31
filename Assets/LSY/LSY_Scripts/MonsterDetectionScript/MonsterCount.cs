using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public enum ColliderType { RightMonster, LeftMonster }
public class MonsterCount : MonoBehaviour
{

    [Header("몬스터 인디케이터 아이콘 이미지")]
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

    [Header("충돌체에 감지되는 몬스터 숫자 관리 스크립트")]
    public MonsterCountUI monsterCountUI;

    private void OnTriggerEnter(Collider other)
    {
        // Comment : 충돌체에 적이 들어왔을 때 공격력에 따라 구분해서 함수 진행해줌
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EliteEnemy"))
        {
            if (other.GetComponent<HYJ_Enemy>() == null) return;

            if (other.GetComponent<HYJ_Enemy>().monsterShieldAtkPower >= 3)
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
        if (other.GetComponent<HYJ_Enemy>() == null) return;

        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EliteEnemy"))
        {

            if (other.GetComponent<HYJ_Enemy>().monsterShieldAtkPower >= 3)
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

    // 강한 적이 충돌체 안에 있을 때 코루틴 발생시켜서 깜박거리는 효과
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
        //monsterDetectionImageUI.color = Color.white;
        //monsterCountBackgroundImage.color = Color.white;
        //normalEnemyIcon.gameObject.SetActive(true);
        //strongEnemyIcon.gameObject.SetActive(false);
        yield break;
    }

    // Comment : 적이 충돌체 안에 들어왔을 때
    private void HandleEnemyEntry(Collider other)
    {
        // Comment : 해당 충돌체에 맞는 카운트ui의 숫자를 +1 해줌
        monsterCountUI.counters[(int)colType]++;
        HYJ_Enemy monster = other.GetComponent<HYJ_Enemy>();
        monster.hyj_monsterCount = monsterCountUI;

        if (!monsterCountUI.Enemies.ContainsKey(monster))
        {
            monsterCountUI.Enemies.Add(monster, colType);
            monsterCountUI.isEnter[monster] = true;
        }
        else
        {
            monsterCountUI.isEnter[monster] = true; 
        }

        // Comment : 충돌체에 들어온 collider의 스크립트에서 해당 몬스터 인디케이터 ui 아이콘을 켜줌
        if (other.GetComponent<UnitToScreenBoundary>() != null)
            other.GetComponent<UnitToScreenBoundary>().isActiveUI = true;

        normalEnemyIcon.gameObject.SetActive(true);
        strongEnemyIcon.gameObject.SetActive(false);
    }

    // Comment : 일반적이 충돌체에서 나갔을 때 숫자 카운트를 -1 해줌
    private void HandleEnemyExit(Collider other)
    {
        monsterCountUI.counters[(int)colType]--;
        HYJ_Enemy monster = other.GetComponent<HYJ_Enemy>();
        monster.hyj_monsterCount = monsterCountUI;

        if (monsterCountUI.Enemies.ContainsKey(monster))
        {
            monsterCountUI.Enemies.Remove(monster); 
            monsterCountUI.isEnter[monster] = false; 
        }

        Debug.Log("일반 몬스터 화면 안으로 들어옴");
        normalEnemyIcon.gameObject.SetActive(true);
        strongEnemyIcon.gameObject.SetActive(false);
    }

    // Comment : 강한 몬스터가 충돌체에 들어왔을 때 숫자 카운트를 +1 해주고 코루틴 시작
    private void HandleStrongEnemyEntry(Collider other)
    {

        Debug.Log("강한 몬스터 화면 밖으로 나감");
        monsterCountUI.counters[(int)colType]++;
        HYJ_Enemy monster = other.GetComponent<HYJ_Enemy>();
        monster.hyj_monsterCount = monsterCountUI;
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

    // Comment : 강한 몬스터가 충돌체에 들어왔을 때 숫자 카운트를 -1 해주고 진행중이던 코루틴을 멈춤
    private void HandleStrongEnemyExit(Collider other)
    {
        monsterCountUI.counters[(int)colType]--;
        HYJ_Enemy monster = other.GetComponent<HYJ_Enemy>();
        monster.hyj_monsterCount = monsterCountUI;

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
