using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class MonsterCountUI : MonoBehaviour
{
    public int[] counters = new int[2];

    public Dictionary<LSY_Enemy, ColliderType> Enemies = new();

    public bool isEntered;

    public Dictionary<LSY_Enemy, bool> isEnter = new Dictionary<LSY_Enemy, bool>();

    public TextMeshProUGUI rightCount;
    public TextMeshProUGUI leftCount;


    private void Update()
    {
        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
        rightCount.text = counters[0].ToString();
        leftCount.text = counters[1].ToString();
    }
}
