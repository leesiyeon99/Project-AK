using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBullet : MonoBehaviour
{



    private Rigidbody rigidBody;

    [SerializeField] private int pierceCount;
     private PlayerGun playerGun;

    private WaitForSeconds returnWaitForSeconds;
    private Coroutine returnCoroutine;



    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
      
    }

    public void MoveBullet()
    {
        pierceCount = playerGun.CustomBullet.DefaultPierceCount;
        returnCoroutine = StartCoroutine(ReturnTime());
        rigidBody.velocity = transform.forward * playerGun.CustomBullet.BulletSpeed;
    
    }

    public void SetPlayerGun(PlayerGun _playerGun)
    {
        playerGun = _playerGun;
        returnWaitForSeconds = new WaitForSeconds(playerGun.BulletReturnDelay);
    }

    // Comment : 오브젝트 풀 회수
    public void ReturnBullet()
    {
        if (returnCoroutine != null)
        {
            StopCoroutine(returnCoroutine);
        }

        gameObject.SetActive(false);
        rigidBody.velocity = Vector3.zero;
        playerGun.EnqueueBullet(this);
    }

    private void HitBullet()
    {
        if (playerGun.CustomBullet.GunType.HasFlag(GunType.PIERCE))
        {
            pierceCount--;
            // TODO : 물체 관통 여부 확인 필요, 파괴 불가 오브젝트에 충돌시 관통 끝
        }
        else
        {
            pierceCount = 0;
        }

        if (playerGun.CustomBullet.GunType.HasFlag(GunType.SPLASH))
        {
            Splash();
        }
        if (pierceCount <= 0)
        {
            ReturnBullet();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // TODO : 데미지 구현

        HitBullet();
    }

    IEnumerator ReturnTime()
    {
        yield return returnWaitForSeconds;
        ReturnBullet();
    }


    private void Splash()
    {

        //TODO : 레이어 마스크 추가

        Collider[] colliders = Physics.OverlapSphere(transform.position, playerGun.CustomBullet.SplashRadius);

        foreach (Collider collider in colliders)
        {
            Debug.Log(collider.name);
            // TODO : 데미지 구현
        }

    }

    
 
    
}
