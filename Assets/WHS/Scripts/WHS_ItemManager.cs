using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class WHS_ItemManager : MonoBehaviour
{

    [SerializeField] GameObject itemPrefab;

    [System.Serializable]    
    public class ItemInfo
    {
        [Header("총알 인덱스")]
        public int bulletIndex;
        [Header("총알 개수")]
        public int bulletAmount;
        [Header("아이템 드랍률")]
        public float dropRate;
    }

    [System.Serializable]
    public class DropInfo
    {
        [Header("몬스터 종류")]
        public string monsterType;
        [Header("아이템 테이블")]
        public List<ItemInfo> items;
    }

    [Header("몬스터 드랍률")]
    [SerializeField] List<DropInfo> dropInfo;

    [Header("오브젝트 드랍률")]
    [SerializeField] DropInfo objectDrops;

    private float itemHeight = 1f;

    private static WHS_ItemManager instance;

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
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 몬스터의 아이템 드랍
    public void SpawnItem(Vector3 pos, string monsterType)
    {
        // 몬스터의 종류 받아오기
        DropInfo itemDrops = dropInfo.Find(a => a.monsterType == monsterType);

        if (itemDrops != null)
        {
            ItemInfo selectedItem = GetRandomItem(itemDrops.items);
            if (selectedItem != null)
            {
                // 인덱스 -1일땐 아이템 생성하지 않음 (꽝)
                if (selectedItem.bulletIndex == -1)
                {
                    Debug.Log("아이템 획득하지 않음");
                }
                // 해당 인덱스의 아이템 획득
                else
                {
                    SpawnSelectedItem(pos, selectedItem);
                    Debug.Log($"{selectedItem.bulletIndex + 1}번 총알 {selectedItem.bulletAmount}개 생성");
                }
            }
        }

        else
        {
            Debug.Log($"타입 일치하지 않음 {monsterType}");
        }
    }

    // 오브젝트의 아이템 드랍
    public void SpawnItem(Vector3 pos)
    {
        ItemInfo selectedItem = GetRandomItem(objectDrops.items);

        if (selectedItem != null)
        {
            // -1번이면 다시 굴리기?
            if (selectedItem.bulletIndex == -1)
            {
                SpawnItem(pos);
            }
            else
            {
                SpawnSelectedItem(pos, selectedItem);
            }
        }
    }


    // 드랍 테이블에서 랜덤 아이템 획득
    private ItemInfo GetRandomItem(List<ItemInfo> items)
    {
        float totalRate = 0;
        foreach (ItemInfo item in items)
        {
            totalRate += item.dropRate;
        }

        float randomValue = Random.Range(0, totalRate);
        float curRate = 0;

        foreach (ItemInfo item in items)
        {
            curRate += item.dropRate;
            if (randomValue <= curRate)
            {
                return item;
            }
        }

        return null;
    }

    // 아이템 생성
    private void SpawnSelectedItem(Vector3 Pos, ItemInfo itemInfo)
    {
        Vector3 dropPos = Pos + new Vector3(0, itemHeight, 0);
        GameObject spawnedItem = Instantiate(itemPrefab, dropPos, Quaternion.identity);

        // WHS_Item의 bulletIndex, bulletAmount 설정
        WHS_Item item = spawnedItem.GetComponent<WHS_Item>();
        item.SetItemInfo(itemInfo.bulletIndex, itemInfo.bulletAmount);
    }
}
