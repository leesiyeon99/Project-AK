using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusCheck : MonoBehaviour
{
    [SerializeField] float radius;

    [ColorUsage(true)]
    [SerializeField] Color color;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos() { Gizmos.color = color; Gizmos.DrawSphere(transform.position, radius);  }

}
