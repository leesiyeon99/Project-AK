using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class WHS_ItemManager : MonoBehaviour
{

    private static WHS_ItemManager instance;

    [System.Serializable]
    public class ItemInfo
    {
        [Header("아이템 프리팹")]
        public GameObject itemPrefab;
        [Header("아이템 드랍률")]
        public float dropRate;
    }    
    [SerializeField] List<ItemInfo> itemInfos;
    [Header("아이템 생성 높이")]
    [SerializeField] float itemHeight = 1f;
    [Header("아이템이 안 뜰 확률")]
    [Range(0, 100)]
    [SerializeField] float noneDropRate = 40;

    public static WHS_ItemManager Instance
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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 안 뜰 확률이 있는 랜덤 아이템 드랍
    private GameObject GetRandomItemWithProbability()
    {
        float randomValue = Random.Range(0, 100f);
        if(randomValue < noneDropRate) // noneDropRate보다 낮으면 아이템이 생성되지 않음
        {
            return null;
        }
        return GetRandomItem();
    }

    // 아이템을 무조건 드랍
    private GameObject GetRandomItem()
    {
        if(itemInfos.Count == 0)
        {
            Debug.Log("등록된 아이템이 없음");
            return null;
        }

        float totalRate = 0;
        foreach(var item in itemInfos)
        {
            totalRate += item.dropRate;
        }

        float randomValue = Random.Range(0, totalRate);
        float curRate = 0;

        foreach(var item in itemInfos)
        {
            curRate += item.dropRate;
            if(randomValue <= curRate)
            {
                return item.itemPrefab;
            }
        }

        return null;
    }

    // 랜덤아이템을 무조건 생성 - 엘리트몬스터, 특수지형
    public void SpawnItem(Vector3 pos)
    {       
        GameObject spawnedItem = GetRandomItem();
        if (spawnedItem != null)
        {
            Vector3 dropPos = pos + new Vector3(0, itemHeight, 0);
            Instantiate(spawnedItem, dropPos, Quaternion.identity);
        }
        else
        {
            Debug.Log("아이템 등록되지 않음");
        }
    }

    // 랜덤아이템을 확률로 생성 - 일반몬스터 등  
    public void SpawnItemWithProbability(Vector3 pos)
    {
        GameObject spawnedItem = GetRandomItemWithProbability();
        if (spawnedItem != null)
        {
            Vector3 dropPos = pos + new Vector3(0, itemHeight, 0);
            Instantiate(spawnedItem, dropPos, Quaternion.identity);            
        }
        else
        {
            Debug.Log("아이템 생성되지 않음");
        }
    }

}
