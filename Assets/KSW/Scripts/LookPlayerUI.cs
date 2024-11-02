using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookPlayerUI : MonoBehaviour
{
    [SerializeField] Transform player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void Update()
    {
        transform.LookAt(player);
    }
}
