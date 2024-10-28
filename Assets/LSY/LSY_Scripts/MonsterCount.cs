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
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            score--;
            textMeshPro.text = score.ToString();
        }

    }
}
