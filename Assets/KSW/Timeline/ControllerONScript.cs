using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerONScript : MonoBehaviour
{
    [SerializeField] GameObject origin;


    public void OnOrigin()
    {
        origin.SetActive(true);
    }
}
