using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    // Comment : 프리팹
    [Header("- 탄환 프리팹")]
    [SerializeField] private GameObject bulletPrefab;

    // Comment : 발사 이펙트
    [Header("- 발사 이펙트")]
    [SerializeField] private GameObject fireEffect;

    // Comment : 컴포넌트
    private PlayerGunStatus playerGunStatus;

    private PlayerBulletCustom customBullet;
    public PlayerBulletCustom CustomBullet { get { return customBullet; } }

    private LineRenderer aimLineRenderer;

    private Animator animator;


    // Comment : 총구 위치
    [Header("- 총구 위치")]
    [SerializeField] private Transform muzzle;

    // Comment : UI
    [Header("- UI")]
    [SerializeField] private TextMeshProUGUI magazineUI;
    [SerializeField] private TextMeshProUGUI toggleMagazineUI;
    [SerializeField] private LayerMask aimMask;
    private GameObject aim;



    // Comment : 오브젝트 풀 관련 변수
    [Header("- 오브젝트 풀")]
    [SerializeField] private int bulletPoolSize;
    [SerializeField] private float bulletReturnDelay;
    public float BulletReturnDelay { get { return bulletReturnDelay; } }
    private Queue<PlayerBullet> playerBullets;


    // Comment : 기본 무기 확인
    [Header("- 기본 무기 확인")]
    [SerializeField] bool isDefaultWeapon;



    // Comment : 발사 쿨다운
    [SerializeField] private float firingCoolDown;

    private Coroutine firingCoroutine;
    private Coroutine firingAccelerationCoroutine;
    private WaitForSeconds firingAccelerationWaitForSeconds;

    StringBuilder stringBuilder;

    private void Update()
    {
        MoveAim();
    }


    // Commnet : 초기화
    public void InitGun()
    {
        firingAccelerationWaitForSeconds = new WaitForSeconds(0.1f);

        stringBuilder = new StringBuilder();

        animator = GetComponent<Animator>();
        customBullet = GetComponent<PlayerBulletCustom>();
        playerGunStatus = GetComponent<PlayerGunStatus>();
        aimLineRenderer = GetComponent<LineRenderer>();
        aim = GameObject.Find("AimTarget");

        playerBullets = new Queue<PlayerBullet>();
    }

    private void Start()
    {
        SetBullet();
    }

    #region 발사
    public void OnFireCoroutine()
    {
        CoroutineCheck();
        if (customBullet.GunType.HasFlag(GunType.REPEATER))
        {
           
            firingCoroutine = StartCoroutine(Firing());
            firingAccelerationCoroutine = StartCoroutine(FiringAcceleration());
        }
        else
        {
            FiringOnce();
        }
    }
    public void OffFireCoroutine()
    {
      
        if (customBullet.GunType.HasFlag(GunType.REPEATER))
        {
            CoroutineCheck();
            playerGunStatus.FiringDelay = playerGunStatus.DefaultFiringDelay;
            firingCoroutine = StartCoroutine(BackgroundFiringCooldown());
        }
    }

    private void CoroutineCheck()
    {
        if (firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);

        }
        if (firingAccelerationCoroutine != null)
        {
            StopCoroutine(firingAccelerationCoroutine);

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

        if (customBullet.GunType.HasFlag(GunType.SPREAD))
        {
            for (int i = 0; i < customBullet.SpreadCount; i++)
            {
                ActiveBulletSpread();
            }
        }
        else
        {
            ActiveBullet();
        }


        playerGunStatus.Magazine--;
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
    void FiringOnce()
    {

        if (firingCoolDown <= 0)
        {
            Fire();
            firingCoolDown = playerGunStatus.FiringDelay;
        }
        firingCoroutine = StartCoroutine(BackgroundFiringCooldown());
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
 
    // Commnet : 연사 특성 가속 
    IEnumerator FiringAcceleration()
    {
        // 가속값
        float accle = (0.1f + playerGunStatus.Tier * 0.2f)/ (playerGunStatus.AccelerationTime*10); 

        // 0.1초 x * 10회 반복
        for (int i = 1; i <= playerGunStatus.AccelerationTime*10; i++)
        {
            // 0.1초
            yield return firingAccelerationWaitForSeconds;

            playerGunStatus.FiringDelay = playerGunStatus.DefaultFiringDelay / (1 + (accle*i));

        }

    }
    #endregion

    #region 오브젝트 풀
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

    // Comment : 회수 된 총알 pool에 저장
    public void EnqueueBullet(PlayerBullet playerBullet)
    {
        playerBullets.Enqueue(playerBullet);
    }
    #endregion



    // Comment : 직선탄
    public void ActiveBullet()
    {
        PlayerBullet playerBullet = playerBullets.Dequeue();
        playerBullet.transform.position = muzzle.position;
        playerBullet.transform.rotation = muzzle.rotation;
        playerBullet.gameObject.SetActive(true);
        playerBullet.MoveBullet();
    }

    // Comment : 확산탄 
    public void ActiveBulletSpread()
    {
        PlayerBullet playerBullet = playerBullets.Dequeue();
        playerBullet.transform.position = muzzle.position;
        Quaternion quaternion = muzzle.rotation;
        float angleX = customBullet.SpreadAngleX;
        float angleY = customBullet.SpreadAngleY;
        playerBullet.transform.rotation = Quaternion.Euler(quaternion.eulerAngles.x + Random.Range(-angleX, angleX), quaternion.eulerAngles.y + Random.Range(-angleY, angleY), quaternion.eulerAngles.z);
        playerBullet.gameObject.SetActive(true);
        playerBullet.MoveBullet();
    }

    #region 재장전
    public void Reload()
    {
        if (MagazineRemainingCheck())
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
    #endregion

    #region UI
    // Comment : 총알 ui 업데이트
    public void UpdateMagazineUI(int magazine)
    {

        magazineUI.text = magazine.ToString();

    }
    public void UpdateChangeToggleUI(int magazine)
    {
        stringBuilder.Clear();
        stringBuilder.Append(magazine.ToString());
        stringBuilder.Append("/");


        if (isDefaultWeapon)
        {
            stringBuilder.Append("∞");


        }
        else
        {
            stringBuilder.Append(playerGunStatus.MaxMagazine);

        }

        toggleMagazineUI.text = stringBuilder.ToString();

    }
    public void UpdateChangeToggleUI()
    {

        UpdateChangeToggleUI(playerGunStatus.Magazine);
    }



    // Comment : 조준점 이동
    // TODO : 일시적으로 Bullet에 UI 레이어 부여, 추후 레이어 합의 후 마스크 레이어 관리 필요 
    // 마스크 레이어를 다른곳에 정의해서 하나만 사용하는것도 필요

    public void MoveAim()
    {

        RaycastHit hit;

        if (Physics.Raycast(muzzle.position, muzzle.forward, out hit, 100f, aimMask))
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
    #endregion


    // Comment : UI 이벤트 추가, 제거
    private void OnEnable()
    {
        playerGunStatus.OnMagazineChanged += UpdateMagazineUI;
        playerGunStatus.OnMagazineChanged += UpdateChangeToggleUI;
        UpdateMagazineUI(playerGunStatus.Magazine);
    }

    private void OnDisable()
    {
        playerGunStatus.OnMagazineChanged -= UpdateMagazineUI;
        playerGunStatus.OnMagazineChanged -= UpdateChangeToggleUI;
        fireEffect.SetActive(false);
        StopAllCoroutines();
    }

}
