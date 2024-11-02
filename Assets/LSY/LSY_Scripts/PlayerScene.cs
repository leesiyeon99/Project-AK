using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScene : MonoBehaviour
{
    private void Awake()
    {
        if (LSY_SceneManager.Instance == null) return;
        if (LSY_SceneManager.Instance.lsy_isdie == true)
        {
            gameObject.transform.position = new Vector3(0, 1, 0);
        }
    }
}
