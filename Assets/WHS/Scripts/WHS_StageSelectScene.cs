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
    private float velocity = 0f;

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

    // 컨트롤러 좌우 입력
    private void MoveJoystick(InputAction.CallbackContext context)
    {
        Vector2 joystickVector = context.ReadValue<Vector2>();
        Debug.Log(joystickVector);

        // 우측
        if (joystickVector.x > 0)
        {
            StageUp();
        }
        // 좌측
        else if (joystickVector.x < 0)
        {
            StageDown();
        }
    }

    // 트리거버튼
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
        Debug.Log($"{curStage} 스테이지 진입");
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
        float targetAngle = (curStage - 1) * -20f;
        curAngle = Mathf.SmoothDamp(curAngle, targetAngle, ref velocity, 0.2f);
        compassNeedle.localRotation = Quaternion.Euler(0, 0, curAngle);
    }
}
