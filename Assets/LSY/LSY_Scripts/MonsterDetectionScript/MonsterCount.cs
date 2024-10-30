using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterCount : MonoBehaviour
{
    [Header("몬스터 숫자 카운트 UI")]
    [SerializeField] TextMeshProUGUI textMeshPro;

    [Header("몬스터 인디케이터 UI 이미지")]
    [SerializeField] Image monsterDetectionImageUI;

    [Header("몬스터 인디케이터 배경 이미지")]
    [SerializeField] Image monsterCountBackgroundImage;

    [Header("일반 몬스터 아이콘 이미지")]
    [SerializeField] Image normalEnemyIcon;

    [Header("강한 몬스터 아이콘 이미지")]
    [SerializeField] Image strongEnemyIcon;

   // public HYJ_Enemy lsy_Enemy;
    private float test_monsterAttackPower = 5;

    int score = 0;
    private Coroutine StrongAttackRoutine;

    private void Start()
    {
        textMeshPro.text = score.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // if (other.GetComponent<HYJ_Enemy>.isdied = true)
            // test_monsterAttackPower 부분을 용진님 몬스터 공격력을 받아와서 if 작성
            if (test_monsterAttackPower >= 3) //other.GetComponent<HYJ_Enemy>.공격력 >= 3 
            {
                //강한 적
            }
            else
            {
                //일반 적
            }
            HandleEnemyEntry(other);
        }

        if (other.gameObject.CompareTag("EliteEnemy"))
        {
            HandleStrongEnemyEntry(other);
        }

        // 몬스터가 죽었을 때는 exit한 것과 같은 코드 구현하도록 이벤트 발생
        var enemy = other.GetComponent<HYJ_Enemy>();
        if (enemy != null)
        {
            //enemy.OnEnemyDied.AddListener(HandleEnemyExitOnDeath);
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    score++;
    //    UpdateScoreText();
    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            HandleEnemyExit();
        }

        if (other.gameObject.CompareTag("EliteEnemy"))
        {
            HandleStrongEnemyExit();
        }
    }

    private void HandleEnemyEntry(Collider other)
    {
        score++;
        Debug.Log("일반 몬스터 들어옴");
        UpdateScoreText();

        if (other.GetComponent<UnitToScreenBoundary>() != null)
            other.GetComponent<UnitToScreenBoundary>().isActiveUI = true;

        normalEnemyIcon.gameObject.SetActive(true);
        strongEnemyIcon.gameObject.SetActive(false);
    }

    private void HandleStrongEnemyEntry(Collider other)
    {
        Debug.Log("강한 몬스터 들어옴");
        score++;
            UpdateScoreText();

            if (other.GetComponent<UnitToScreenBoundary>() != null)
                other.GetComponent<UnitToScreenBoundary>().isActiveUI = true;



            monsterDetectionImageUI.color = Color.white;
            monsterCountBackgroundImage.color = Color.yellow;

            StrongAttackRoutine = StartCoroutine(StrongMonsterAttack());
    }

    private void HandleEnemyExit()
    {
        Debug.Log("일반 몬스터 나감");
        score--;
        UpdateScoreText();
        normalEnemyIcon.gameObject.SetActive(true);
        strongEnemyIcon.gameObject.SetActive(false);
    }

    private void HandleStrongEnemyExit()
    {
        Debug.Log("간한 몬스터 나감");
        score--;
        UpdateScoreText();

        monsterDetectionImageUI.color = Color.yellow;
        monsterCountBackgroundImage.color = Color.white;

        normalEnemyIcon.gameObject.SetActive(true);
        strongEnemyIcon.gameObject.SetActive(false);

        if (StrongAttackRoutine != null)
        {
            StopCoroutine(StrongAttackRoutine);
            StrongAttackRoutine = null;
        }
    }

    private void HandleEnemyExitOnDeath(Collider collider)
    {
        if (collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("EliteEnemy"))
        {
            HandleEnemyExit(); 
        }
    }

    private void UpdateScoreText()
    {
        textMeshPro.text = score.ToString();
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

}
