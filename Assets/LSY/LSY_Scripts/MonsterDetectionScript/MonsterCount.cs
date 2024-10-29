using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterCount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] Image monsterDetectionImageUI;
    [SerializeField] Image monsterCountBackgroundImage;
    [SerializeField] Image normalEnemyIcon;
    [SerializeField] Image strongEnemyIcon;
    int score = 0;

    private void Start()
    {
        textMeshPro.text = score.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            score++;
            textMeshPro.text = score.ToString();
            if (other.GetComponent<UnitToScreenBoundary>() != null)
            other.GetComponent<UnitToScreenBoundary>().isActiveUI = true;
            normalEnemyIcon.gameObject.SetActive(true);
            strongEnemyIcon.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("EliteEnemy"))
        {
            score++;
            textMeshPro.text = score.ToString();
            if (other.GetComponent<UnitToScreenBoundary>() != null)
                other.GetComponent<UnitToScreenBoundary>().isActiveUI = true;

            monsterDetectionImageUI.color = Color.white;
            monsterCountBackgroundImage.color = Color.yellow;
            normalEnemyIcon.gameObject.SetActive(false);
            strongEnemyIcon.gameObject.SetActive(true);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            score--;
            textMeshPro.text = score.ToString();
            // 몬스터가 죽기전까지는 계속 화면상에 들어오면 UI 이미지 활성화? 
            //other.GetComponent<UnitToScreenBoundary>().isActiveUI = false;
            normalEnemyIcon.gameObject.SetActive(true);
            strongEnemyIcon.gameObject.SetActive(false);
        }
        
        if (other.gameObject.CompareTag("EliteEnemy"))
        {
            score--;
            textMeshPro.text = score.ToString();

            monsterDetectionImageUI.color = Color.yellow;
            monsterCountBackgroundImage.color= Color.white;
            normalEnemyIcon.gameObject.SetActive(true);
            strongEnemyIcon.gameObject.SetActive(false);
        }

    }
}
