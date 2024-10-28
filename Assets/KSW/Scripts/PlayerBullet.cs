using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBullet : MonoBehaviour
{



    private Rigidbody rigidBody;

    [Header("- ����ũ ����Ʈ ������")]
    [SerializeField] private GameObject sparkEffectPrefab;
    private GameObject spark;
    private int pierceCount;
    private PlayerGun playerGun;

    private WaitForSeconds returnWaitForSeconds;
    private Coroutine returnCoroutine;



    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        spark = Instantiate(sparkEffectPrefab);
        spark.SetActive(false);
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

    // Comment : ������Ʈ Ǯ ȸ��
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
            // TODO : ��ü ���� ���� Ȯ�� �ʿ�, �ı� �Ұ� ������Ʈ�� �浹�� ���� ��
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
        // TODO : ������ ����
   
        OnEffect(other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position));

        //HitBullet();
    }

    private void OnEffect(Vector3 vec)
    {
        
        spark.SetActive(false);

        spark.transform.position = vec;
        spark.transform.LookAt(playerGun.transform.position);
        spark.SetActive(true);
    }

    IEnumerator ReturnTime()
    {
        yield return returnWaitForSeconds;
        ReturnBullet();
    }


    private void Splash()
    {

        //TODO : ���̾� ����ũ �߰�

        Collider[] colliders = Physics.OverlapSphere(transform.position, playerGun.CustomBullet.SplashRadius);

        foreach (Collider collider in colliders)
        {
            Debug.Log(collider.name);
            // TODO : ������ ����
        }

    }

    
 
    
}