using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class MonsterCountUI : MonoBehaviour
{
    // Comment : 왼쪽 충돌체에 감지되는 몬스터와 오른쪽 충돌체에 감지되는 몬스터의 숫자를 관리
    public int[] counters = new int[2];

    public Dictionary<HYJ_Enemy, ColliderType> Enemies = new();

    public bool isEntered;

    public Dictionary<HYJ_Enemy, bool> isEnter = new Dictionary<HYJ_Enemy, bool>();

    [Header("숫자 카운트 UI")]
    public TextMeshProUGUI rightCount;
    public TextMeshProUGUI leftCount;


    private void Update()
    {
        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
        // Comment : 충돌체안의 적의 숫자를 계속 업데이트해서 UI에서 보여줌
        rightCount.text = counters[0].ToString();
        leftCount.text = counters[1].ToString();
    }
}
