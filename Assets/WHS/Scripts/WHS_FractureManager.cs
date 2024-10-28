using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WHS_FractureManager : MonoBehaviour
{
    [SerializeField] float removeDelay = 1.5f;
    private static WHS_FractureManager instance; // 파괴할 Fracture 오브젝트들의 인스턴스
    private Dictionary<GameObject, Fracture> fractureObjects = new Dictionary<GameObject, Fracture>(); // 파괴할 오브젝트와 Fracture 컴포넌트를 저장
    private Dictionary<GameObject, GameObject> itemPrefabs = new Dictionary<GameObject, GameObject>(); // 파괴할 오브젝트와 아이템 프리팹을 저장

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
    public void GetFractureObject(GameObject obj, GameObject itemPrefab)
    {
        Fracture fracture = obj.GetComponent<Fracture>(); // 오브젝트의 Fracture 컴포넌트 가져오기

        if (fracture != null && !fractureObjects.ContainsKey(obj)) // Fracture 컴포넌트가 있고, 아직 등록되지 않았으면
        {
            fractureObjects.Add(obj, fracture); // 딕셔너리에 파괴할 오브젝트 추가
            itemPrefabs.Add(obj, itemPrefab); // 딕셔너리에 아이템프리팹 추가

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
        GameObject itemPrefab = itemPrefabs[obj]; // 아이템프리팹 받아오기
        Vector3 dropPos = obj.transform.position + new Vector3(0, 1.5f, 0);
        Instantiate(itemPrefab, dropPos, Quaternion.identity); // 오브젝트가 파괴된 자리에 1.5높이에 아이템 생성

        // removeDelay초 뒤 파편 삭제
        yield return new WaitForSeconds(removeDelay);

        // 파편 제거
        GameObject fragmentRoot = GameObject.Find($"{obj.name}Fragments"); // 오브젝트의 이름+Fragments 이름을 가지는 파편 오브젝트 찾기

        Destroy(fragmentRoot); // 파편 오브젝트 삭제
        Destroy(obj); // 원본 오브젝트 삭제

        fractureObjects.Remove(obj); // 딕셔너리에서 제거
        itemPrefabs.Remove(obj);
    }
}