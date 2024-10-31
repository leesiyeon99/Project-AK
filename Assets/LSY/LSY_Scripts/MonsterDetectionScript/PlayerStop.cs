using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStop : MonoBehaviour
{
    [SerializeField] CinemachineDollyCart cinemachineDollyCart;

    [Header("플레이어가 몬스터 감지하는 범위")]
    public float radius = 0f;

    [Header("감지되는 레이어 설정")]
    public LayerMask layer; // TODO : Enemy, EliteEnemy 레이어로 설정해야 함 EliteEnemy Layer 추가해야 함

    [Header("플레이어 충돌체 범위 안에 들어와 있는 적 목록")]
    public Collider[] colliders;

    int i = 0;
    void Update()
    {
        // Comment : 플레이어 범위 내의 Enemy레이어를 갖는 오브젝트를 찾아 함수 실행, 몬스터가 감지되지 않는다면 원래 상태로 초기화
        colliders = Physics.OverlapSphere(transform.position, radius, layer);

        PlayerMove();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Comment : 플레이어가 StopPoint 태그를 가진 오브젝트에 닿으면 속도가 0이 됨
        if (other.gameObject.CompareTag("StopPoint"))
        {
            cinemachineDollyCart.m_Speed = 0;
            Debug.Log("스피드 0");
        }
    }

    //Comment : 플레이어가 몬스터를 감지하는 범위
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    // Comment : 플레이어 주변 OverlapSphere 에 감지되는 Enemy, EliteEnemy레이어가 없다면 다시 출발하도록 함
    private void PlayerMove()
    {
        if (colliders.Length == 0)
        {
            cinemachineDollyCart.m_Speed = 2;
        }
    }

}
