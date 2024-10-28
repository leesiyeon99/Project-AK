using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject bulletPrefab; // 총알 프리팹
    public Transform shootPos;

    // WASD로 이동해서 클릭으로 총알 발사, 오브젝트 파괴 및 아이템 테스트

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(x, y, 0f) * 5 * Time.deltaTime;
        transform.position += movement; 
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, shootPos.position, shootPos.rotation); // 전방에 총알 생성
    }
}
