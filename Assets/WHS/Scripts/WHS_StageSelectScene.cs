using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class WHS_StageSelectScene : MonoBehaviour
{
    [SerializeField] TMP_Text stageText;
    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;
    [SerializeField] Button startButton;
    [SerializeField] Transform compassNeedle;
    private float curAngle = 0f;

    private int curStage = 1;
    private int maxStage = 5;

    [SerializeField] ActionBasedController rightController;
    [SerializeField] InputActionProperty rightJoystickInput;
    [SerializeField] InputActionProperty triggerInput;

    private void Start()
    {
        UpdateSelectedStage();
        leftButton.onClick.AddListener(StageDown);
        rightButton.onClick.AddListener(StageUp);
        startButton.onClick.AddListener(LoadSelectedStage);
    }

    private void Update()
    {
        RotateNeedle();
    }

    private void OnEnable()
    {
        rightJoystickInput.action.performed += MoveJoystick;
        rightJoystickInput.action.canceled += MoveJoystick;
        triggerInput.action.performed += LoadSceneTrigger;
    }

    private void OnDisable()
    {
        rightJoystickInput.action.performed -= MoveJoystick;
        rightJoystickInput.action.canceled -= MoveJoystick;
        triggerInput.action.performed -= LoadSceneTrigger;
    }

    // ��Ʈ�ѷ� �¿� �Է�
    private void MoveJoystick(InputAction.CallbackContext context)
    {
        Vector2 joystickVector = context.ReadValue<Vector2>();
        Debug.Log(joystickVector);

        // ����
        if (joystickVector.x > 0)
        {
            StageUp();
        }
        // ����
        else if (joystickVector.x < 0)
        {
            StageDown();
        }
    }

    // Ʈ���Ź�ư
    public void LoadSceneTrigger(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            LoadSelectedStage();
        }
    }

    public void StageUp()
    {
        if (curStage < maxStage)
        {
            curStage++;
            UpdateSelectedStage();
        }
    }

    public void StageDown()
    {
        if (curStage > 1)
        {
            curStage--;
            UpdateSelectedStage();
        }
    }

    private void UpdateSelectedStage()
    {
        stageText.text = $"{curStage}";
        RotateNeedle();
    }

    public void LoadSelectedStage()
    {
        Debug.Log($"{curStage} �������� ����");
        string sceneName = $"KSJ{curStage}Stage";

        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            return;
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    private void RotateNeedle()
    {
        float rotateSpeed = 20f;
        curAngle += -rotateSpeed * Time.deltaTime;

        compassNeedle.localRotation = Quaternion.Euler(0, 0, curAngle);
    }
}
