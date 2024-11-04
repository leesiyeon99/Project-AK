using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerBullet : MonoBehaviour
{
    [Header("- 스파크 이펙트 프리팹")]
    [SerializeField] private GameObject sparkEffectPrefab;
    private Queue<GameObject> spark;
    [Header("- 스파크 이펙트 약점 프리팹")]
    [SerializeField] private GameObject sparkEffectWeakPrefab;
    private Queue<GameObject> sparkWeak;
    [Header("- 스플래시 이펙트 프리팹")]
    [SerializeField] private GameObject splashEffectPrefab;
    private Queue<GameObject> splash;
    private PlayerGunStatus playerGunStatus;
    [Header("- 스플래시 레이어 마스크")]
    [SerializeField] LayerMask mask;
    private void Awake()
    {
        playerGunStatus = GetComponent<PlayerGunStatus>();
        SetEffect();
    }
    private void SetEffect()
    {
        spark = new Queue<GameObject>();
        sparkWeak = new Queue<GameObject>();
        splash = new Queue<GameObject>();
        if (playerGunStatus.GunType.HasFlag(GunType.SPLASH))
        {
            float scale = playerGunStatus.SplashRadius;
            for (int i = 0; i < playerGunStatus.DefaultPierceCount; i++)
            {
                GameObject splashObj = Instantiate(splashEffectPrefab);
                splash.Enqueue(splashObj);
                splashObj.SetActive(false);
                splashObj.transform.localScale = new Vector3(scale, scale, scale);
            }
        }
        else if (playerGunStatus.GunType.HasFlag(GunType.PIERCE))
        {
            for (int i = 0; i < playerGunStatus.DefaultPierceCount; i++)
            {
                GameObject sparkObj = Instantiate(sparkEffectPrefab);
                spark.Enqueue(sparkObj);
                sparkObj.SetActive(false);

                GameObject sparkWeakObj = Instantiate(sparkEffectWeakPrefab);
                sparkWeak.Enqueue(sparkWeakObj);
                sparkWeakObj.SetActive(false);
            }
        }
        else
        {
            GameObject sparkObj = Instantiate(sparkEffectPrefab);
            spark.Enqueue(sparkObj);
            sparkObj.SetActive(false);

            GameObject sparkWeakObj = Instantiate(sparkEffectWeakPrefab);
            sparkWeak.Enqueue(sparkWeakObj);
            sparkWeakObj.SetActive(false);
        }
    }
    public void HitRay(RaycastHit hit)
    {
        bool weak = false;
        // 연동 테스트
        if (hit.collider.TryGetComponent(out HYJ_EnemyHitPoint enemy))
        {
            enemy.TakeDamage(playerGunStatus.BulletAttack);
            weak = enemy.weak;
        }
        if (hit.collider.TryGetComponent(out HYJ_BossHitPoint boss))
        {
            boss.TakeDamage(playerGunStatus.BulletAttack);
            weak = boss.weak;
        }
        if (hit.collider.TryGetComponent(out HYJ_BossHitPoint2 boos2))
        {
            boos2.TakeDamage(playerGunStatus.BulletAttack);

        }
        if (hit.collider.TryGetComponent(out HYJ_Boss2_Object_HitPoint obj))
        {
            obj.TakeDamage(playerGunStatus.BulletAttack);

        }
     
        if (playerGunStatus.GunType.HasFlag(GunType.SPLASH))
        {
            Splash(hit.point);
        }
        else
        {
            OnSparkEffect(hit.point, weak);
            if (hit.collider.TryGetComponent(out Fracture fractureObj))
            {
                fractureObj.CauseFracture();
            }
            if (hit.collider.TryGetComponent(out DeerScript deer))
            {
                deer.DieDeer();
            }
        }
    }
    // Comment : 관통
    public void HitRay(RaycastHit[] hit, Transform muzzlePoint)
    {
        int loop = hit.Length;
        int hitCount = playerGunStatus.DefaultPierceCount;
        for (int i = 0; i < loop; i++)
        {
            
            // 연동 테스트
            bool hitFlag = true;
            if (hit[i].collider.TryGetComponent(out HYJ_Boss2_Object_HitPoint obj))
            {
                hitFlag = obj.TakeDamage(playerGunStatus.BulletAttack);
                if (hitFlag == false)
                {
                    hitCount++;
                }
            }
            if (hit[i].collider.TryGetComponent(out HYJ_EnemyHitPoint enemy))
            {
                hitFlag = enemy.TakeDamage(playerGunStatus.BulletAttack);
                if (hitFlag == false)
                {
                    hitCount++;
                }
            }
            if (hit[i].collider.TryGetComponent(out HYJ_BossHitPoint boss))
            {
                hitFlag = boss.TakeDamage(playerGunStatus.BulletAttack);
                if (hitFlag == false)
                {
                    hitCount++;
                }
            }
            if (hit[i].collider.TryGetComponent(out HYJ_BossHitPoint2 boss2))
            {
                Debug.Log(playerGunStatus.BulletAttack);
                hitFlag = boss2.TakeDamage(playerGunStatus.BulletAttack);
                if (hitFlag == false)
                {
                    hitCount++;
                }
            }
            if (!playerGunStatus.GunType.HasFlag(GunType.SPLASH) && hitFlag)
            {
                OnSparkEffect(hit[i].point, true);
            }
            if (playerGunStatus.GunType.HasFlag(GunType.SPLASH))
            {
                Splash(hit[i].point);
            }
            else
            {
                if (hit[i].collider.TryGetComponent(out Fracture fractureObj))
                {
                    fractureObj.CauseFracture();
                }
                if (hit[i].collider.TryGetComponent(out DeerScript deer))
                {
                    deer.DieDeer();
                }
            }
            hitCount--;
            if (hitCount <= 0)
            {
                break;
            }
        }
    }
    private void OnSparkEffect(Vector3 vec, bool weak)
    {
        if (weak)
        {
            GameObject sparkObj = sparkWeak.Dequeue();
            sparkObj.SetActive(false);
            sparkObj.transform.position = vec;
            sparkObj.transform.LookAt(transform.position);
            sparkObj.SetActive(true);
            sparkWeak.Enqueue(sparkObj);
        }
        else
        {
            GameObject sparkObj = spark.Dequeue();
            sparkObj.SetActive(false);
            sparkObj.transform.position = vec;
            sparkObj.transform.LookAt(transform.position);
            sparkObj.SetActive(true);
            spark.Enqueue(sparkObj);
        }
       
    }
    private void Splash(Vector3 vec)
    {
        //TODO : 레이어 마스크 추가
        GameObject splashObj = splash.Dequeue();
        splashObj.SetActive(false);
        splashObj.transform.position = vec;
        splashObj.SetActive(true);
        splash.Enqueue(splashObj);
        Collider[] colliders = Physics.OverlapSphere(vec, playerGunStatus.SplashRadius, mask);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out DeerScript deer))
            {
                deer.DieDeer();
            }
            if (collider.TryGetComponent(out HYJ_Boss2_Object_HitPoint obj))
            {
                obj.TakeDamage(playerGunStatus.SplashDamage);
            }
            if (collider.TryGetComponent(out HYJ_EnemyHitPoint enemy))
            {
                enemy.TakeDamage(playerGunStatus.SplashDamage);
            }
            if (collider.TryGetComponent(out HYJ_BossHitPoint boss))
            {
                boss.TakeDamage(playerGunStatus.SplashDamage);
            }
            if (collider.TryGetComponent(out HYJ_BossHitPoint2 boss2))
            {
                boss2.TakeDamage(playerGunStatus.SplashDamage);
            }
            if (collider.TryGetComponent(out Fracture fractureObj))
            {
                fractureObj.CauseFracture();
            }
        }
    }
}