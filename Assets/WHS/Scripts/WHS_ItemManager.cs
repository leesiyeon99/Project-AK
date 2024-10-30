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

    public GameObject GetRandomItem()
    {
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

    public void SpawnItem(Vector3 pos)
    {
        GameObject spawnedItem = GetRandomItem();
        Vector3 dropPos = pos + new Vector3(0, itemHeight, 0);
        Instantiate(spawnedItem, dropPos, Quaternion.identity);
    }
}
