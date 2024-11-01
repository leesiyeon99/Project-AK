using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class WHS_TransparencyController : MonoBehaviour
{
    // 몬스터 죽으면서 1초동안 투명해지고 사라지게
    // 몬스터가 죽을 때 Destroy 대신 StartFadeOut() 호출해 사용
    
    private static WHS_TransparencyController instance;

    public static WHS_TransparencyController Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void StartFadeOut(GameObject obj, float duration)
    {
        StartCoroutine(FadeOut(obj, duration));
    }

    private IEnumerator FadeOut(GameObject obj, float duration)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();

        foreach (Renderer render in renderers)
        {
            foreach (Material material in render.materials)
            {
                SetMaterialToTransparent(material); // 죽을 때 RenderingMode를 Transparent로 변경
            }
        }

        float elapsedtime = 0f;

        while(elapsedtime < duration) // 1초동안
        {
            elapsedtime += Time.deltaTime;
            float alpha = 1f - (elapsedtime / duration); // 알파값 점차 감소

            foreach(Renderer render in renderers)
            {
                foreach(Material material in render.materials)
                {
                    Color color = material.color;
                    color.a = alpha;
                    material.color = color;
                }
            }

            yield return null;
        }

        Destroy(obj);
    }

    public void StartFadeIn(GameObject obj, float duration)
    {
        StartCoroutine(FadeIn(obj, duration));
    }

    private IEnumerator FadeIn(GameObject obj, float duration)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();

        foreach(Renderer render in renderers)
        {
            foreach(Material material in render.materials)
            {
                SetMaterialToTransparent(material);
                Color color = material.color;
                color.a = 0f;
                material.color = color;
            }
        }

        float elapsedTime = 0f;
        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = elapsedTime / duration;

            foreach(Renderer render in renderers)
            {
                foreach(Material material in render.materials)
                {
                    Color color = material.color;
                    color.a = alpha;
                    material.color = color;
                }
            }

            yield return null;
        }

        foreach (Renderer render in renderers)
        {
            foreach(Material material in render.materials)
            {
                SetMaterialToOpaque(material);
            }
        }
    }

    private void SetMaterialToTransparent(Material material)
    {
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;
    }

    private void SetMaterialToOpaque(Material material)
    {
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = -1;
    }
}
