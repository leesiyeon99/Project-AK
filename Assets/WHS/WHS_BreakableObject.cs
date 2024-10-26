using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WHS_BreakableObject : MonoBehaviour
{
    // 파괴 가능한 지형 - 다른 지형들과 구분돼 테두리가 두껍고 반짝이게 해 확인 가능

    // 파괴하면 굴러떨어져 사라지거나 즉시 사라짐

    // 발사된 무기(총알 등)에 닿으면 오브젝트 부수기

    private Fracture fracture;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                fracture = hit.transform.GetComponent<Fracture>();
                if (fracture != null)
                {
                    Debug.Log("부수기");
                    fracture.CauseFracture(); // 오브젝트 부수기
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet")) // 총알 태그와 충돌하면
        {
            Debug.Log("총알과 충돌");
            if (fracture != null) // fracture이 있으면
            {
                fracture.CauseFracture(); // 오브젝트 부수기
            }
            Destroy(collision.gameObject);
        }
    }
}