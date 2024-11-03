using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HYJ_Spider_Shoot : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] HYJ_Enemy enemy;
    [SerializeField] GameObject wepBullet;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponent<HYJ_Enemy>();
    }

    
    void Update()
    {
        if (enemy.nowAttack)
        {
            ShootWep();
        }
    }

    void ShootWep()
    {
        Debug.Log("∞≈πÃ¡Ÿ");
        enemy.nowAttack = false;
        Instantiate(wepBullet,new Vector3(enemy.transform.position.x, enemy.transform.position.y+0.4f, enemy.transform.position.z),Quaternion.LookRotation(player.transform.position));
        //Destroy(wepBullet,3f);
    }
}
