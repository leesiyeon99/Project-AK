using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WHS_Item : MonoBehaviour
{
    // 획득할 아이템 오브젝트에 추가해서 사용

    [SerializeField] float hoverRange = 0.3f; // 아이템이 위아래로 움직이는 범위
    [SerializeField] float moveSpeed = 5; // 움직이는 속도
    public float rotationSpeed = 120; // 회전 속도

    private Vector3 startPos; // 아이템 생성 위치
    private Transform playerTransform; // 플레이어 위치

    [Header("생성 후 n초 뒤 획득")]
    [SerializeField] float moveDelay = 1f; // 생성 후 플레이어에게 delay초 뒤 이동 (1초뒤 아이템이 날아와 획득)
    [Header("아이템 날아오는 속도")]
    [SerializeField] float moveToPlayerSpeed = 10f; // 아이템이 다가오는 속도

    [SerializeField] float itemGetRange = 1f; // 아이템 습득 범위

    private bool isMovingtoPlayer = false; // 플레이어에게 이동중인지

    [Header("각 총알 개수")]
    [SerializeField] int[] bulletAmounts = new int[3]; // 각 인덱스의 얻을 총알 개수
    private int bulletIndex; // 특수 총알 인덱스

    private void Start()
    {
        startPos = transform.position; // 아이템 위치를 저장
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // 플레이어 위치

        bulletIndex = Random.Range(0, 3); // 0~2 랜덤 인덱스 아이템

        StartCoroutine(MoveToPlayer()); // 플레이어에게 1초 뒤 이동

    }

    private void Update()
    {
        if (!isMovingtoPlayer) // 습득중이지 않을때 아이템 생성 움직임
        {
            // hoverRange로 위아래로 움직이는 범위, moveSpeed로 이동 속도 조절
            float newY = startPos.y + Mathf.Sin(Time.time * moveSpeed) * hoverRange; // 위아래로 이동하는 효과
            transform.position = new Vector3(transform.position.x, newY, transform.position.z); // y 위치를 이동시킴
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
        else
        {
            // 아이템이 플레이어로 이동
            transform.position = Vector3.Lerp(transform.position, playerTransform.position, moveToPlayerSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, playerTransform.position) < itemGetRange)
            {
                GetItem(); // 아이템이 범위에 들어오면 아이템 획득
            }
        }
    }

    // 아이템 생성 후 1초뒤 획득
    private IEnumerator MoveToPlayer()
    {
        yield return new WaitForSeconds(moveDelay); // delay초 뒤 이동
        isMovingtoPlayer = true;
    }

    // 아이템 습득
    private void GetItem()
    {
        // PlayerSpecialBullet의 인스턴스
        if(PlayerSpecialBullet.Instance != null)
        {
            int amountBullet = bulletAmounts[bulletIndex];
            PlayerSpecialBullet.Instance.SpecialBullet[bulletIndex] += amountBullet; // index번 총알 Amout만큼 획득

            Debug.Log($"{bulletIndex+1}번 탄환 {amountBullet}개 획득");
        }
        else
        {
            Debug.Log("PlayerSpecialBullet 인스턴스 찾을 수 없음");
        }
        Destroy(gameObject);
    }

}
