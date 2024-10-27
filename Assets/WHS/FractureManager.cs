using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FractureManager : MonoBehaviour
{
    [SerializeField] float removeDelay = 2f;
    private static FractureManager instance; // 파괴할 Fracture 오브젝트들의 인스턴스
    private Dictionary<GameObject, Fracture> fractureObjects = new Dictionary<GameObject, Fracture>(); // 파괴할 오브젝트들을 저장, 게임오브젝트를 키, fracture을 값으로

    public static FractureManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<FractureManager>(); // 씬에서 FractureManager를 찾음
                if (instance == null)
                {
                    instance = new GameObject("FractureManager").AddComponent<FractureManager>(); // 없다면 FractureManager를 생성
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
        Fracture fracture = obj.GetComponent<Fracture>(); // Fracture 컴포넌트를 가진 오브젝트
        if (fracture != null && !fractureObjects.ContainsKey(obj))
        {
            fractureObjects.Add(obj, fracture); // 딕셔너리에 오브젝트 추가
            fracture.callbackOptions.onCompleted.AddListener(() => FractureOnCompleted(obj));
        }
    }

    // Fracture가 완료(onCompleted) 되면 호출
    private void FractureOnCompleted(GameObject obj)
    {
        StartCoroutine(RemoveFragments(obj));
    }

    // Fracture로 생성된 파편들을 제거함
    private IEnumerator RemoveFragments(GameObject obj)
    {
        yield return new WaitForSeconds(removeDelay); // removeDelay초 뒤 오브젝트 삭제

        GameObject fragmentRoot = GameObject.Find($"{obj.name}Fragments"); // 오브젝트이름+Fragments 이름을 가지는 파편 오브젝트 찾기
        if (fragmentRoot != null)
        {
            Destroy(fragmentRoot); // 파편 오브젝트 삭제
        }

        Destroy(obj); // 원본 오브젝트 삭제
        fractureObjects.Remove(obj); // 딕셔너리에서 제거
    }
}