using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerGun : MonoBehaviour
{
    // Comment : ������
    [Header("- źȯ ������")]
    [SerializeField] private GameObject bulletPrefab;

    // Comment : �߻� ����Ʈ
    [Header("- �߻� ����Ʈ")]
    [SerializeField] private GameObject fireEffect;

    // Comment : ������Ʈ
    private PlayerGunStatus playerGunStatus;

    private PlayerBulletCustom customBullet;
    public PlayerBulletCustom CustomBullet { get { return customBullet; } }

    private LineRenderer aimLineRenderer;

    private Animator animator;


    // Comment : �ѱ� ��ġ
    [Header("- �ѱ� ��ġ")]
    [SerializeField] private Transform muzzle;

    // Comment : UI
    [Header("- UI")]
    [SerializeField] private TextMeshProUGUI magazineUI;
    [SerializeField] private TextMeshProUGUI toggleMagazineUI;
    [SerializeField] private LayerMask aimMask;
    private GameObject aim;



    // Comment : ������Ʈ Ǯ ���� ����
    [Header("- ������Ʈ Ǯ")]
    [SerializeField] private int bulletPoolSize;
    [SerializeField] private float bulletReturnDelay;
    public float BulletReturnDelay { get { return bulletReturnDelay; } }
    private Queue<PlayerBullet> playerBullets;
    

    // Comment : �⺻ ���� Ȯ��
    [Header("- �⺻ ���� Ȯ��")]
    [SerializeField] bool isDefaultWeapon;



    // Comment : �߻� ��ٿ�
    private float firingCoolDown;

    private Coroutine firingCoroutine;

    StringBuilder stringBuilder;

    private void Update()
    {
        MoveAim();
    }


    // Commnet : �ʱ�ȭ
    public void InitGun()
    {


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

    #region �߻�
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

    public void Fire()
    {
        if (playerGunStatus.Magazine <= 0)
            return;
        if (playerBullets.Count <= 0)
            return;


        // Comment : ���־��� �κ�
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

    // Comment : �߻� ���� �ƴҶ� �߻� ��ٿ� ���� 
    IEnumerator BackgroundFiringCooldown()
    {
        while (firingCoolDown > 0)
        {
            firingCoolDown -= Time.deltaTime;
            yield return null;


        }
    }

    #endregion

    #region ������Ʈ Ǯ
    // Comment : �Ѿ� ������Ʈ Ǯ��
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

    // Comment : ȸ�� �� �Ѿ� pool�� ����
    public void EnqueueBullet(PlayerBullet playerBullet)
    {
        playerBullets.Enqueue(playerBullet);
    }
    #endregion

  

    // Comment : ����ź
    public void ActiveBullet()
    {
        PlayerBullet playerBullet = playerBullets.Dequeue();
        playerBullet.transform.position = muzzle.position;
        playerBullet.transform.rotation = muzzle.rotation;
        playerBullet.gameObject.SetActive(true);
        playerBullet.MoveBullet();
    }

    // Comment : Ȯ��ź 
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

    #region ������
    public void Reload()
    {
        if(MagazineRemainingCheck())
        return;

        playerGunStatus.Magazine = playerGunStatus.MaxMagazine;
        
    }

    // Comment : �Ѿ� �ִ� �� ���������� üũ
    public bool MagazineRemainingCheck()
    {
        if (playerGunStatus.MaxMagazine <= playerGunStatus.Magazine)
            return true;

        return false;
    }
    #endregion

    #region UI
    // Comment : �Ѿ� ui ������Ʈ
    public void UpdateMagazineUI(int magazine)
    {

        magazineUI.text = magazine.ToString();

    }
    public void UpdateMagazineToggleUI(int magazine)
    {
        stringBuilder.Clear();
        stringBuilder.Append(magazine.ToString());
        stringBuilder.Append("/");

        
        if (isDefaultWeapon)
        {
            stringBuilder.Append("��");


        }
        else
        {
            stringBuilder.Append(playerGunStatus.MaxMagazine);
        
        }
      
        toggleMagazineUI.text = stringBuilder.ToString();
      
    }
    public void UpdateMagazineToggleUI()
    {

        UpdateMagazineToggleUI(playerGunStatus.Magazine);
    }
 
  

    // Comment : ������ �̵�
    // TODO : �Ͻ������� Bullet�� UI ���̾� �ο�, ���� ���̾� ���� �� ����ũ ���̾� ���� �ʿ� 
    // ����ũ ���̾ �ٸ����� �����ؼ� �ϳ��� ����ϴ°͵� �ʿ�

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


    // Comment : UI �̺�Ʈ �߰�, ����
    private void OnEnable()
    {
        playerGunStatus.OnMagazineChanged += UpdateMagazineUI;
        playerGunStatus.OnMagazineChanged += UpdateMagazineToggleUI;
        UpdateMagazineUI(playerGunStatus.Magazine);
    }

    private void OnDisable()
    {
        playerGunStatus.OnMagazineChanged -= UpdateMagazineUI;
        playerGunStatus.OnMagazineChanged -= UpdateMagazineToggleUI;
        fireEffect.SetActive(false);
        StopAllCoroutines();
    }

}