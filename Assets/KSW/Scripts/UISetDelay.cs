using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISetDelay : MonoBehaviour
{
    [SerializeField] GameObject explain;
    [SerializeField] Camera uiCamera;
    void Start()
    {
        StartCoroutine(UIDelay());
    }

   IEnumerator UIDelay()
    {
        yield return new WaitForSeconds(0.5f);
        explain.SetActive(false);
        uiCamera.enabled = true;
    }
}
