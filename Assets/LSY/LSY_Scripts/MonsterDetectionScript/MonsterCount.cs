using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterCount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
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
        }

    }
}
