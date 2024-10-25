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

    [SerializeField] private GameObject aim;

    private Queue<PlayerBullet> playerBullets;

    private WaitForSeconds firingWaitForSeconds;
    private Coroutine firingCoroutine;

    [SerializeField] private LayerMask mask;

    private void Update()
    {
        MoveAim();
    }

    private void Awake()
    {
        aim = GameObject.Find("Aim");
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
            Fire();
            yield return firingWaitForSeconds;
            
           
        }
    }

    // Comment : 총알 오브젝트 풀링
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

    // Comment : 회수 된 총알 pool에 저장
    public void EnqueueBullet(PlayerBullet playerBullet)
    {
        playerBullets.Enqueue(playerBullet);
    }

    // Comment : 조준점 이동
    // TODO : 일시적으로 Bullet에 UI 레이어 부여, 추후 레이어 합의 후 마스크 레이어 관리 필요 
    // 마스크 레이어를 다른곳에 정의해서 하나만 사용하는것도 필요

    public void MoveAim()
    {

        RaycastHit hit;

        if (Physics.Raycast(muzzle.position, muzzle.forward, out hit, 100f, mask))
        {
           
            aim.transform.position = hit.point;

        }
      
    }

}
