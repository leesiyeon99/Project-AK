using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerPool : MonoBehaviour
{
    [SerializeField] GameObject deerPrefabs;
    [SerializeField] float poolSize;
   
    [SerializeField] float spawnTime;
    Queue<DeerScript> deerQueue;

    [SerializeField] Transform[] spawnPoint;

    Coroutine spawnCoroutine;
    WaitForSeconds spawnWaitForSeconds;

    DeerScript recentDeer;

    private void Awake()
    {
        deerQueue = new Queue<DeerScript>();
        spawnWaitForSeconds = new WaitForSeconds(spawnTime);
        SetDeer();
    }
    private void Start()
    {
        SpawnDeer();
    }
    void SetDeer()
    {
        for (int i = 0; i < poolSize; i++)
        {
            DeerScript deer = Instantiate(deerPrefabs).GetComponent<DeerScript>();
            deer.gameObject.SetActive(false);

            deerQueue.Enqueue(deer);

        }
    }
 

    public void SpawnDeer()
    {
        spawnCoroutine = StartCoroutine(SpawnCorutine());
       
    }

    public void ReturnDeer(DeerScript deer)
    {
        deerQueue.Enqueue(deer);
        deer.DieEvent -= ReturnDeer;
       
    }

    IEnumerator SpawnCorutine()
    {
        int i = 0;

        while (true)
        {
            if (deerQueue.Count > 0)
            {
               
                DeerScript deer = deerQueue.Dequeue();
                deer.DieEvent += ReturnDeer;

                deer.transform.position = spawnPoint[i].position;
              
                deer.transform.rotation = spawnPoint[i].rotation;

                recentDeer = deer;
               
                i++;
                if (i >= spawnPoint.Length)
                {
                    i = 0;
                }

                deer.gameObject.SetActive(true);
                deer.StartMoveCheckTime();
            }
            yield return spawnWaitForSeconds;

        }
    }

    public DeerScript GetRidingPoint()
    {
   
        return recentDeer;
    }
}
