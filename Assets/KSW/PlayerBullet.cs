using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float bulletSpeed;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Fire();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {

        rigidBody.velocity = transform.forward * bulletSpeed;
    
    }
}
