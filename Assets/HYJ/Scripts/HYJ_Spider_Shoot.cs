using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HYJ_Spider_Shoot : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] HYJ_Enemy enemy;
    [SerializeField] GameObject wepBullet;

    Coroutine shootRoutine;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponent<HYJ_Enemy>();
    }

    private void OnEnable()
    {
        shootRoutine = StartCoroutine(ShootRoutine());
    }


    private void OnDestroy()
    {
        StopCoroutine(shootRoutine);
    }


    IEnumerator ShootRoutine()
    {
        float ran = UnityEngine.Random.Range(0.1f, 3f);
        WaitForSeconds delay = new WaitForSeconds(ran);

        while (true)
        {
            yield return delay;
            Instantiate(wepBullet, new Vector3(enemy.transform.position.x, enemy.transform.position.y + 0.4f, enemy.transform.position.z), Quaternion.LookRotation(player.transform.position));
            yield return new WaitForSeconds(5f);
        }
    }


}
