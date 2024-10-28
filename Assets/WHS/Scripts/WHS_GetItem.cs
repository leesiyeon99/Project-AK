using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WHS_GetItem : MonoBehaviour
{
    [SerializeField] float hoverRange; // 떠있는 범위
    [SerializeField] float moveSpeed; // 움직이는 속도

    [SerializeField] int bulletAmount; // 획득할 총알 개수

    // 아이템은 오브젝트(몬스터)가 파괴된 자리에 생성
    // 특정 동작으로 아이템 획득 ( VR컨트롤러로 트리거? 몇초뒤 회수? )
    // -> 1초 뒤 회수

    private Vector3 startPos; // 아이템 생성 위치

    private void Start()
    {
        startPos = transform.position; // 아이템 위치를 저장
    }

    private void Update()
    {
        // hoverRange로 위아래로 움직이는 범위, moveSpeed로 이동 속도 조절
        float newY = startPos.y + Mathf.Sin(Time.time * moveSpeed) * hoverRange; // sin함수로 위아래로 이동하는 효과
        transform.position = new Vector3(transform.position.x, newY, transform.position.z); // y 위치를 이동시킴
    }

}
