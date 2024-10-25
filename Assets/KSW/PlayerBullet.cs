using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float bulletSpeed;

    private PlayerGun playerGun;

    [SerializeField] private float returnDelay;

    private WaitForSeconds returnWaitForSeconds;
    private Coroutine returnCoroutine;

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
  
    // 오브젝트 풀 회수
    private void ReturnBullet()
    {
        if (returnCoroutine != null)
        {
            StopCoroutine(returnCoroutine);
        }

        gameObject.SetActive(false);
        rigidBody.velocity = Vector3.zero;
        playerGun.EnqueueBullet(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        ReturnBullet();
    }

    IEnumerator ReturnTime()
    {
        yield return returnWaitForSeconds;
        ReturnBullet();
    }
}
