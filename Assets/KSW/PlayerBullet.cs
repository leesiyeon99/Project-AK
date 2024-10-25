using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBullet : MonoBehaviour
{
    [SerializeField] protected Rigidbody rigidBody;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float returnDelay;
  

    protected PlayerGun playerGun;

    protected WaitForSeconds returnWaitForSeconds;
    protected Coroutine returnCoroutine;



    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        returnWaitForSeconds = new WaitForSeconds(returnDelay);
    }

    public void MoveBullet()
    {
        returnCoroutine = StartCoroutine(ReturnTime());
        rigidBody.velocity = transform.forward * bulletSpeed;
    
    }

    public void SetPlayerGun(PlayerGun _playerGun)
    {
        playerGun = _playerGun;
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

    protected virtual void HitBullet()
    {
        ReturnBullet();
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
}
