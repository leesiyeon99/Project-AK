using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WHS_BreakableObject : MonoBehaviour
{
    // 파괴할 오브젝트에 Fracture 컴포넌트와 함께 추가

    [SerializeField] GameObject itemPrefab;

    void Start()
    {
        // FractureManager의 딕셔너리에 등록해서, 파편화 후 제거되게 만듬
        FractureManager.Instance.GetFractureObject(gameObject);
    }

    // 파괴 후 비활성화 된 오브젝트의 자리에 itemPrefab 생성
    private void OnDisable()
    {
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }
    }

}