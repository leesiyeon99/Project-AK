using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HYJ_StonePa : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector3.forward * 10f * Time.deltaTime);
    }
}
