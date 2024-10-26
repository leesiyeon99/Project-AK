using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerGun : MonoBehaviour
{
    private PlayerGunStatus playerGunStatus;

    private PlayerBulletCustom customBullet;
    public PlayerBulletCustom CustomBullet { get { return customBullet; } }

    private LineRenderer aimLineRenderer;

    private Animator animator;

    [SerializeField] private Transform muzzle;

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private GameObject fireEffect;

    [SerializeField] private float firingCoolDown;


    // Comment : 오브젝트 풀 관련 변수
    [SerializeField] private int bulletPoolSize;

    [SerializeField] private float bulletReturnDelay;

    public float BulletReturnDelay { get { return bulletReturnDelay; } }

    [SerializeField] private GameObject aim;

    private Queue<PlayerBullet> playerBullets;

   
    private Coroutine firingCoroutine;

    [SerializeField] private LayerMask mask;

    private void Update()
    {
        MoveAim();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        customBullet = GetComponent<PlayerBulletCustom>();
        playerGunStatus = GetComponent<PlayerGunStatus>();
        aimLineRenderer = GetComponent<LineRenderer>();
        aim = GameObject.Find("Aim");

        playerBullets = new Queue<PlayerBullet>();
       
    }

    private void Start()
    {
        SetBullet();
    }

    public void OnFireCoroutine()
    {
        CoroutineCheck();
        firingCoroutine = StartCoroutine(Firing());
    }
    public void OffFireCoroutine()
    {
        CoroutineCheck();

        firingCoroutine = StartCoroutine(BackgroundFiringCooldown());
    }

    private void CoroutineCheck()
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
            firingCoolDown -= Time.deltaTime;

            if (firingCoolDown <= 0)
            {
                Fire();
                firingCoolDown = playerGunStatus.FiringDelay;
            }
            yield return null;
            
           
        }
    }

    // Comment : 발사 중이 아닐때 발사 쿨다운 감소 
    IEnumerator BackgroundFiringCooldown()
    {
        while (firingCoolDown > 0)
        {
            firingCoolDown -= Time.deltaTime;
            yield return null;


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

    public void Fire()
    {
        if (playerGunStatus.Magazine <= 0)
            return;
        if (playerBullets.Count <= 0)
            return;


        // Comment : 비주얼적 부분
        fireEffect.SetActive(false);
        animator.SetTrigger("Shot");
        fireEffect.SetActive(true);

        PlayerBullet playerBullet = playerBullets.Dequeue();
        playerBullet.transform.position = muzzle.position;
        playerBullet.transform.rotation = muzzle.rotation;
        playerBullet.gameObject.SetActive(true);
        playerBullet.MoveBullet();
        playerGunStatus.Magazine--;
    }

    public void Reload()
    {
        if(MagazineRemainingCheck())
        return;

        playerGunStatus.Magazine = playerGunStatus.MaxMagazine;
        
    }

    // Comment : 총알 최대 수 보유중인지 체크
    public bool MagazineRemainingCheck()
    {
        if (playerGunStatus.MaxMagazine <= playerGunStatus.Magazine)
            return true;

        return false;
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
            aimLineRenderer.enabled = true;
            aimLineRenderer.SetPosition(0, muzzle.position);
            aimLineRenderer.SetPosition(1, hit.point);
            aim.transform.position = hit.point;

        }
        else
        {
            aimLineRenderer.enabled = false;
            aim.transform.position = Vector3.zero;
        }
      
    }

    private void OnDisable()
    {
        fireEffect.SetActive(false);
        StopAllCoroutines();
    }

}
