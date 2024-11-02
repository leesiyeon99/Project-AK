using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

public class StoryOpening : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI text;
    private string[] dialogues;
    private int currentIndex = 0;
    public InputActionReference nextTextButton;

    public Transform playertransform;

    public Image compassImage;

    void Start()
    {
        dialogueText.text = "주인공: 오늘도 더러운 하루. 하늘은 누렇고 기분은 진창같구나";
        text.text = "오른쪽 컨트롤러 A버튼을 눌러주세요.";
        StartCoroutine(TextRoutine());

        dialogues = new string[]
        {
            "그 때, 날개 달린 작은 그림자가 바닥을 스쳐 지나간다. 툭 소리와 함께 뭔가 떨어진다.",
            "주인공: ???",
            "하늘을 올려다 보았지만 그림자의 주인은 보이지 않았다.",
            "주인공: 뭐가 떨어진거지?..",
            "주인공: 나침반...? 골동품인가? 후면에 버튼 같은게 있네",
            "주인공: 움직이는 건가?",
            "당신은 마구잡이로 나침반을 조작했고, 나침반은 마구잡이로 작동했다.",
            "어느 새, 당신의 주변을 무수히 많은 차원문이 애워쌌다.",
            "주인공: 뭐야!",
            "그리고 나침반은 폭발했다",
            "주인공: 아니!",
            "나침반이 부숴지면서 조각들이 온갖 차원문으로 빨려들어갔다.",
            "그리고, 정면에 생겨난 차원문이 당신을 끌어당겼다.",
            "주인공: 으아아",
            ""
        };

        nextTextButton.action.performed += ShowNextDialogue;
        nextTextButton.action.Enable(); 
    }

    IEnumerator TextRoutine()
    {
        yield return new WaitForSeconds(1);
        float duration = 1.5f;
        float elapsedTime = 0f;
        Color initialColor = text.color;
        // Commet : 1초 동안 점차 이미지가 투명해지도록 설정
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(initialColor.a, 0, elapsedTime / duration);
            text.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null;
        }
        text.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0);
    }


    void ShowNextDialogue(InputAction.CallbackContext obj)
    {
        if (currentIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[currentIndex];
            currentIndex++;
            if (currentIndex == 5)
            {
                compassImage.gameObject.SetActive(true);
            }
            if (currentIndex == 10)
            {
                compassImage.gameObject.SetActive(false);
                Debug.Log("나침반 폭발");
            }
            if (currentIndex == dialogues.Length)
            {
                playertransform.position = new Vector3(0,1, 0);
                nextTextButton.action.performed -= ShowNextDialogue;
                dialogueText.text = "";
            }
        }
        else
        {
            return;
        }
    }
}
