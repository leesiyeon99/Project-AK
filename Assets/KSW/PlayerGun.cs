using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private Transform muzzle;

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float firingDelay;

    [SerializeField] private int bulletPoolSize;

    private Queue<PlayerBullet> playerBullets;

    private WaitForSeconds firingWaitForSeconds;
    private Coroutine firingCoroutine;

  
    private void Awake()
    {
        playerBullets = new Queue<PlayerBullet>();
        firingWaitForSeconds = new WaitForSeconds(firingDelay);
       
    }

    private void Start()
    {
        SetBullet();
    }

    public void OnFireCoroutine()
    {
        firingCoroutine = StartCoroutine(Firing());


    }
    public void OffFireCoroutine()
    {
        if (firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
        }
    }


    IEnumerator Firing()
    {
        while (true)
        {
            yield return firingWaitForSeconds;
            Fire();
           
        }
    }

    // 총알 오브젝트 풀링
    void SetBullet()
    {
        for (int i = 0; i < bulletPoolSize; i++)
        {
           GameObject bullet = Instantiate(bulletPrefab);
           PlayerBullet playerBullet = bullet.GetComponent<PlayerBullet>();
           playerBullets.Enqueue(playerBullet);
           playerBullet.SetPlayerGun(this);
           playerBullet.gameObject.SetActive(false);
       
        }
    }

    void Fire()
    {
   
        if (playerBullets.Count <= 0)
            return;

        PlayerBullet playerBullet = playerBullets.Dequeue();
        playerBullet.transform.position = muzzle.position;
        playerBullet.transform.rotation = muzzle.rotation;
        playerBullet.gameObject.SetActive(true);
        playerBullet.MoveBullet();
    }

    public void EnqueueBullet(PlayerBullet playerBullet)
    {
        playerBullets.Enqueue(playerBullet);
    }

}
