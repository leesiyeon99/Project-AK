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
        dialogueText.text = "���ΰ�: ���õ� ������ �Ϸ�. �ϴ��� ������ ����� ��â������";
        text.text = "������ ��Ʈ�ѷ� A��ư�� �����ּ���.";
        StartCoroutine(TextRoutine());

        dialogues = new string[]
        {
            "�� ��, ���� �޸� ���� �׸��ڰ� �ٴ��� ���� ��������. �� �Ҹ��� �Բ� ���� ��������.",
            "���ΰ�: ???",
            "�ϴ��� �÷��� �������� �׸����� ������ ������ �ʾҴ�.",
            "���ΰ�: ���� ����������?..",
            "���ΰ�: ��ħ��...? ��ǰ�ΰ�? �ĸ鿡 ��ư ������ �ֳ�",
            "���ΰ�: �����̴� �ǰ�?",
            "����� �������̷� ��ħ���� �����߰�, ��ħ���� �������̷� �۵��ߴ�.",
            "��� ��, ����� �ֺ��� ������ ���� �������� �ֿ��մ�.",
            "���ΰ�: ����!",
            "�׸��� ��ħ���� �����ߴ�",
            "���ΰ�: �ƴ�!",
            "��ħ���� �ν����鼭 �������� �°� ���������� ��������.",
            "�׸���, ���鿡 ���ܳ� �������� ����� �������.",
            "���ΰ�: ���ƾ�",
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
        // Commet : 1�� ���� ���� �̹����� ������������ ����
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
                Debug.Log("��ħ�� ����");
            }
            if (currentIndex == dialogues.Length)
            {
                playertransform.position = new Vector3(0,0, 0);
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
