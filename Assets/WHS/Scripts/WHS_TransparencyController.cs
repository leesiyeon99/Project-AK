using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class WHS_TransparencyController : MonoBehaviour
{
    // 몬스터 죽으면서 1초동안 투명해지고 사라지게
    // 몬스터가 죽을 때 Destroy 대신 StartFadeOut() 사용
    // 서서히 드러나는 몬스터가 등장할때 StartFadeIn() 사용
    
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

    // 투명해지면서 사라짐
    public void StartFadeOut(GameObject obj, float duration)
    {
        StartCoroutine(FadeOut(obj, duration));
    }

    private IEnumerator FadeOut(GameObject obj, float duration)
    {
        yield return new WaitForSeconds(0.5f); // 0.5초 (죽는 애니메이션 시간 정도) 뒤부터 사라지게

        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>(); // 자식 오브젝트들의 렌더 

        // RenderingMode를 Transparent로 변경
        foreach (Renderer render in renderers)
        {
            foreach (Material material in render.materials)
            {
                SetMaterialToTransparent(material);
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
                    material.color = color; // 변경된 알파값을 매터리얼에 적용
                }
            }

            yield return null;
        }

        Destroy(obj);
    }

    // 투명한 오브젝트가 서서히 드러남
    public void StartFadeIn(GameObject obj, float duration)
    {
        StartCoroutine(FadeIn(obj, duration));
    }

    private IEnumerator FadeIn(GameObject obj, float duration)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>(); // 자식 오브젝트들의 렌더 

        // 매터리얼의 렌더 모드를 Transparent로 변경
        foreach (Renderer render in renderers)
        {
            foreach(Material material in render.materials)
            {
                SetMaterialToTransparent(material);
            }
        }

        float elapsedTime = 0f;
        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = elapsedTime / duration; // 알파값 증가시킴

            foreach(Renderer render in renderers)
            {
                foreach(Material material in render.materials)
                {
                    Color color = material.color;
                    color.a = alpha;
                    material.color = color; // 변경된 알파값을 매터리얼에 적용
                }
            }

            yield return null;
        }

        // 매터리얼의 렌더 모드를 다시 Opaque로 변경
        foreach (Renderer render in renderers)
        {
            foreach(Material material in render.materials)
            {
                SetMaterialToOpaque(material);
            }
        }
    }

    // 매터리얼의 Rendering Mode를 Transparent로 변경
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

    // 매터리얼의 Rendering Mode를 Opaque로 변경
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
