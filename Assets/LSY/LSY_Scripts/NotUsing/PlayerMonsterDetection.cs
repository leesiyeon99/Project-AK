using SETUtil.SceneUI;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class PlayerMonsterDetection : MonoBehaviour
{
    public float radius = 0f;
    public LayerMask layer;
    public Collider[] colliders;


    public List<GameObject> gameObjects = new List<GameObject>();
    int i = 0;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("UIActivePoint"))
        {
            if (gameObjects[i] == null) return;
            gameObjects[i].GetComponent<UnitToScreenBoundary>().isActiveUI = true;
            i++;
        }
    }

    void Update()
    {
        // Comment : 플레이어 범위 내의 Enemy레이어를 갖는 오브젝트를 찾아 함수 실행, 몬스터가 감지되지 않는다면 원래 상태로 초기화
        colliders = Physics.OverlapSphere(transform.position, radius, layer);
    }

}
