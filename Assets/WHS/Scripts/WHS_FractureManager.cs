using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class WHS_FractureManager : MonoBehaviour
{
    // FractureManager에서 Fracture된 오브젝트의 파편을 제거, 아이템을 생성을 관리함
    // 부술 오브젝트에 Fracture 컴포넌트, BreakableObject 스크립트 추가

    // Fracture Options -> FragmentCount 에서 파괴후 갈라지는 파편의 개수 조절(10개 내외 권장)
    //                  -> Inside Metarial에서 갈라진 면의 메터리얼(적당히 비슷한 색상으로 설정)

    [SerializeField] float removeDelay = 1.5f; // delay초 뒤 파편 제거

    private static WHS_FractureManager instance; // 파괴할 Fracture 오브젝트들의 인스턴스
    private Dictionary<GameObject, Fracture> fractureObjects = new Dictionary<GameObject, Fracture>(); // 파괴할 오브젝트와 Fracture 컴포넌트를 저장
    
    public static WHS_FractureManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<WHS_FractureManager>(); // 인스턴스가 없으면 씬에서 FractureManager를 찾음
                if (instance == null)
                {
                    instance = new GameObject("WHS_FractureManager").AddComponent<WHS_FractureManager>(); // 없다면 FractureManager를 생성
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬에 있는 FractureManager를 인스턴스로 설정
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Fracture할 오브젝트를 딕셔너리에 등록
    public void GetFractureObject(GameObject obj)
    {
        Fracture fracture = obj.GetComponent<Fracture>(); // 오브젝트의 Fracture 컴포넌트 가져오기

        if (fracture != null && !fractureObjects.ContainsKey(obj)) // Fracture 컴포넌트가 있고, 아직 등록되지 않았으면
        {
            fractureObjects.Add(obj, fracture); // 딕셔너리에 파괴할 오브젝트 추가
            
            fracture.callbackOptions.onCompleted.AddListener(() => FractureOnCompleted(obj)); // Fracture가 완료되면 FractureOnCompleted 호출
        }
    }

    // Fracture가 완료(onCompleted) 되면 호출
    private void FractureOnCompleted(GameObject obj)
    {
        StartCoroutine(RemoveFragments(obj)); // Fracture이 완료되면 코루틴 시작
    }

    // Fracture 완료 후 아이템 생성 뒤 파편 제거
    private IEnumerator RemoveFragments(GameObject obj)
    {
        // 아이템 생성
        WHS_ItemManager.Instance.SpawnItem(obj.transform.position);

        // removeDelay초 뒤 파편 삭제
        yield return new WaitForSeconds(removeDelay);

        // 파편 제거        
        GameObject fragmentRoot = GameObject.Find($"{obj.name}Fragments"); // ~Fragments 이름을 가지는 파편 오브젝트 찾기
        if (fragmentRoot != null)
        {
            Destroy(fragmentRoot); // 파편 오브젝트 삭제
        }

        if (obj != null)
        {
            Destroy(obj); // 원본 오브젝트 삭제
        }

        fractureObjects.Remove(obj); // 딕셔너리에서 제거
    }
}