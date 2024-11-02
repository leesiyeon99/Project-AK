using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalkFairy : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    public void SetTextUI(string str)
    {
        text.text = str;
    }
}
