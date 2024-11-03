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
using static LSY_SceneManager;

public class WHS_StageSelectScene : MonoBehaviour
{

    [SerializeField] TMP_Text stageText;
    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;
    [SerializeField] Button startButton;
    //[SerializeField] Transform compassNeedle;
    private float curAngle = 0f;

    // public int curStage = 1;
    // private int maxStage = 5;

    [SerializeField] ActionBasedController rightController;
    [SerializeField] InputActionProperty rightJoystickInput;
    [SerializeField] InputActionProperty triggerInput;

    private float stageInterval = 0.3f;
    private Coroutine stageChange;

    private static WHS_StageSelectScene instance;

    public static WHS_StageSelectScene Instance
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
            Destroy(this);
        }
    }


    private void Start()
    {
        UpdateSelectedStage();
        leftButton.onClick.AddListener(StageDown);
        rightButton.onClick.AddListener(StageUp);
        startButton.onClick.AddListener(LoadSelectedStage);
    }

    //private void Update()
    //{
    //    RotateNeedle();
    //}

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
        /*
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
        */
        if(Mathf.Abs(joystickVector.x) > 0)
        {
            if(stageChange == null)
            {
                // 0.3초마다 스테이지 변경
                stageChange = StartCoroutine(ChangeStageCoroutine(joystickVector.x > 0));
            }
        }
        else
        {
            if(stageChange != null)
            {
                StopCoroutine(stageChange);
                stageChange = null;
            }
        }
    }

    private IEnumerator ChangeStageCoroutine(bool isRight)
    {
        while (true)
        {
            if (isRight)
            {
                StageUp();
            }
            else
            {
                StageDown();
            }
            yield return new WaitForSeconds(stageInterval);
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
        if (WHS_StageIndex.curStage < WHS_StageIndex.maxStage)
        {
            WHS_StageIndex.curStage++;
            UpdateSelectedStage();
        }
    }

    public void StageDown()
    {
        if (WHS_StageIndex.curStage > 1)
        {
            WHS_StageIndex.curStage--;
            UpdateSelectedStage();
        }
    }

    private void UpdateSelectedStage()
    {
        stageText.text = $"{WHS_StageIndex.curStage}";
       // RotateNeedle();
    }

    public void LoadSelectedStage()
    {
        int sceneIndex = WHS_StageIndex.curStage;

        if (!Application.CanStreamedLevelBeLoaded(sceneIndex))
        {
            return;
        }
        else
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }

    //private void RotateNeedle()
    //{
    //    float rotateSpeed = 20f;
    //    curAngle += -rotateSpeed * Time.deltaTime;

    //    compassNeedle.localRotation = Quaternion.Euler(0, 0, curAngle);
    //}
}
