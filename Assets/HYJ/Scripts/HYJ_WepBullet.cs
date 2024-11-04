using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class HYJ_WepBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * 10f *  Time.deltaTime);
    }
}
