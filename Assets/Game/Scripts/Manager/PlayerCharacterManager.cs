using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacterManager : MonoBehaviour, IStageResettable
{
    [SerializeField] GameObject characterObject;
    [SerializeField] CharacterPresetList presetList;

    Vector3 characterInitialPosition;

    public GameObject CharacterObject
    {
        get => characterObject;
        set
        {
            PlayerCharacterController = value.GetComponent<PlayerCharacterController>();
            characterObject = value;
        }
    }

    public PlayerCharacterController PlayerCharacterController { get; private set; }

    #region Input Events Bypass
    public void OnLeftLegInput(InputAction.CallbackContext context)
    {
        PlayerCharacterController.OnLeftLegInput(context);
    }

    public void OnRightLegInput(InputAction.CallbackContext context)
    {
        PlayerCharacterController.OnRightLegInput(context);
    }

    public void OnMouseYDeltaInput(InputAction.CallbackContext context)
    {
        PlayerCharacterController.OnMouseYDeltaInput(context);
    }

    public void OnCharacterBalanceInput(InputAction.CallbackContext context)
    {
        PlayerCharacterController.OnCharacterBalanceInput(context);
    }
    #endregion


    private void Start()
    {
        characterInitialPosition = CharacterObject.transform.position;

        StageReset();
    }

    public void StageReset()
    {
        // 즉시 삭제
        Destroy(CharacterObject);

        StartCoroutine(CharacterLoadCoroutine());

        IEnumerator CharacterLoadCoroutine()
        {
            yield return null;
            // TODO: 저장된 캐릭터 세팅값에서 캐릭터를 불러오록 수정해야함
            CharacterObject = Instantiate(presetList.characterPreset[0].prefab);
            CharacterObject.transform.position = characterInitialPosition;
            GameStageManager.Instance.FocusCameraManager.TargetTransform = CharacterObject.transform;
        }
    }
}
