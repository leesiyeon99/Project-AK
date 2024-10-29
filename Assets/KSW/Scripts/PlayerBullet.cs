using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBullet : MonoBehaviour
{
    [Header("- 스파크 이펙트 프리팹")]
    [SerializeField] private GameObject sparkEffectPrefab;
    private List<GameObject> spark;

    [Header("- 스플래시 이펙트 프리팹")]
    [SerializeField] private GameObject splashEffectPrefab;
    private GameObject splash;

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
        spark = new List<GameObject>();
        if (playerGunStatus.GunType.HasFlag(GunType.PIERCE))
        {
            for (int i = 0; i < playerGunStatus.DefaultPierceCount; i++)
            {
                spark.Add(Instantiate(sparkEffectPrefab));
                spark[i].SetActive(false);
            }
 
        }
        else
        {
            spark.Add(Instantiate(sparkEffectPrefab));
            spark[0].SetActive(false);
        }
        if (playerGunStatus.GunType.HasFlag(GunType.SPLASH))
        {
            splash = Instantiate(splashEffectPrefab);
            splash.SetActive(false);
            float scale = playerGunStatus.SplashRadius;
            splash.transform.localScale = new Vector3(scale, scale, scale);
        }


    }

    public void HitRay(RaycastHit hit)
    {
        OnSparkEffect(hit.point, 0);


        // 연동 테스트
        /*
        if (hit.collider.TryGetComponent(out HYJ_Enemy enemy))
        {
            enemy.MonsterTakeDamageCalculation();
        }
        */

        if (hit.collider.TryGetComponent(out Fracture fractureObj))
        {
            fractureObj.CauseFracture();
        }

        if (playerGunStatus.GunType.HasFlag(GunType.SPLASH))
        {
            Splash(hit.point);

        }

    }


    // Comment : 관통
    public void HitRay(RaycastHit[] hit, Transform muzzlePoint)
    {

        int loop = playerGunStatus.DefaultPierceCount;
        if (hit.Length < playerGunStatus.DefaultPierceCount)
        {
            loop = hit.Length;
        }

  

        for (int i = 0; i < loop; i++)
        {
            OnSparkEffect(hit[i].point, i);


            // 연동 테스트
          /*  
           *  if (hit[i].collider.TryGetComponent(out HYJ_Enemy enemy))
            {
                enemy.MonsterTakeDamageCalculation();
            }
           */

            if (hit[i].collider.TryGetComponent(out Fracture fractureObj))
            {
                fractureObj.CauseFracture();
            }

            if (playerGunStatus.GunType.HasFlag(GunType.SPLASH))
            {
                Splash(hit[i].point);

            }
        }
    }

  

    private void OnSparkEffect(Vector3 vec, int cnt)
    {

        spark[cnt].SetActive(false);

        spark[cnt].transform.position = vec;
        spark[cnt].transform.LookAt(transform.position);
        spark[cnt].SetActive(true);

    }


    private void Splash(Vector3 vec)
    {

        //TODO : 레이어 마스크 추가

        splash.SetActive(false);
        splash.transform.position = vec;
        splash.SetActive(true);


        Collider[] colliders = Physics.OverlapSphere(vec, playerGunStatus.SplashRadius, mask);

        foreach (Collider collider in colliders)
        {

            if (collider.TryGetComponent(out Fracture fractureObj))
            {
                fractureObj.CauseFracture();
            }
            //Debug.Log(collider.name);
            // TODO : 데미지 구현
        }

    }




}
