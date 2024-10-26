using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject bulletPrefab; // 총알 프리팹
    public Transform shootPos;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 클릭 시
        {
            Shoot();
        }

        float move = Input.GetAxis("Horizontal"); // A, D 키 입력 감지
        Vector3 movement = new Vector3(move, 0f, 0f) * 5 * Time.deltaTime;
        transform.position += movement; // 오브젝트 이동
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, shootPos.position, shootPos.rotation); // 전방에 총알 생성
    }
}
