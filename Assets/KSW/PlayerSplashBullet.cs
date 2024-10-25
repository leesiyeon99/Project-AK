using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSplashBullet : PlayerBullet
{
    [SerializeField] float splashRadius;

    protected override void HitBullet()
    {

        //TODO : 레이어 마스크 추가

        Collider[] colliders = Physics.OverlapSphere(transform.position, splashRadius);

        foreach (Collider collider in colliders)
        {
           // TODO : 데미지 구현
        }

        ReturnBullet();

        
    }

    // Comment : 범위 확인
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.transform.position, splashRadius);
    }

}
