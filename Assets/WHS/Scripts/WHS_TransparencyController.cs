using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WHS_TransparencyController : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] float transparency = 1f;
    private Renderer render;

    private void Start()
    {
        render = GetComponent<Renderer>();
    }

    private void Update()
    {
        Color color = render.material.color;

        color.a = transparency;

        render.material.color = color;
    }
}
