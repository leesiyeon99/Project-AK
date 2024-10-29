using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WHS_Bullet : MonoBehaviour
{
    public float speed = 20f;

    private Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.AddForce(transform.forward * speed, ForceMode.Impulse);

        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}