using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WHS_TitleScene : MonoBehaviour
{
    private enum JoystickDirection
    {
        NONE = 0,
        LEFT = 1,
        RIGHT = 2
    }

    [SerializeField] TMP_Text stageText;
    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;

    private int curStage = 1;
    private int maxStage = 5;

    [SerializeField] private InputActionReference rightJoystickInput;
    [SerializeField] private InputActionReference triggerInput;

    private void Start()
    {
        UpdateSelectedStage();
        leftButton.onClick.AddListener(StageDown);
        rightButton.onClick.AddListener(StageUp);
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

    private JoystickDirection joystickDirection;

    private void MoveJoystick(InputAction.CallbackContext context)
    {
        Vector2 joystickVector = context.ReadValue<Vector2>();

        if(Mathf.Abs(joystickVector.x) == 1 || joystickVector == Vector2.zero)
        {
            joystickDirection = JoystickDirection.NONE;
        }

        // ¿ìÃø
        if(joystickVector.x > 0 && joystickVector.x < 1)
        {
            joystickDirection |= JoystickDirection.RIGHT;
            joystickDirection &= ~JoystickDirection.LEFT;
            StageUp();
        }
        // ÁÂÃø
        else if (joystickVector.x < 0 && joystickVector.x > -1)
        {
            joystickDirection |= JoystickDirection.LEFT;
            joystickDirection &= ~JoystickDirection.RIGHT;
            StageDown();
        }
    }

    private void LoadSceneTrigger(InputAction.CallbackContext context)
    {
        LoadSelectedStage();
    }

    private void StageUp()
    {
        if(curStage < maxStage)
        {
            curStage++;
            UpdateSelectedStage();
        }
    }

    private void StageDown()
    {
        if(curStage > 1)
        {
            curStage--;
            UpdateSelectedStage();
        }
    }

    private void UpdateSelectedStage()
    {
        stageText.text = $"{curStage}";
    }

    private void LoadSelectedStage()
    {
        string sceneName = $"KSJ{curStage}Stage";
        SceneManager.LoadScene(sceneName);
    }
    /*
    private void OnRightJoystick(InputAction.CallbackContext obj)
    {
        MoveJoystick(obj.ReadValue<Vector2>());
    }
    
    private void OffRightJoystick(InputAction.CallbackContext obj)
    {
        MoveJoystick(Vector2.zero);
    }*/
}
