using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HYJ_WepBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    void Start()
    {
        Destroy(gameObject,5f);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(player.transform.position.x, player.transform.position.y - 0.3f, player.transform.position.z), 0.1f);
    }

    private void OnDestroy()
    {
        PlayerOwnedWeapons.Instance.StartSlow();
    }

}
