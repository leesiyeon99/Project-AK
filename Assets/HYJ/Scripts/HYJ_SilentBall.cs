using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HYJ_SilentBall : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector3.forward * 10f * Time.deltaTime);
    }
}
