using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidingEnemy : MonoBehaviour
{
    [SerializeField] DeerPool pool;


    private void Start()
    {
        Ride();
    }

    public void Ride()
    {
        transform.SetParent(pool.GetRidingPoint().transform);
        pool.GetRidingPoint().enemy = this;
        transform.localPosition = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Respawn")
        Ride();
    }

    public void Die()
    {
        Debug.Log("NOOO");
        Destroy(gameObject);
    }

}
