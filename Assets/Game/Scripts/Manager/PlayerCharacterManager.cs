using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacterManager : MonoBehaviour, IStageResettable
{
    [SerializeField] GameObject characterObject;
    [SerializeField] PrefabList characterPrefabList;
    [SerializeField] GameObject basketPrefab;

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
        CharacterObject = CharacterObject;
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
            Debug.Log("  캐릭터 초기화");

            // TODO: 저장된 캐릭터 프리셋에서 캐릭터를 불러오록 수정해야함
            var characterPrefab = characterPrefabList[0];
            Debug.Log($"캐릭터 프리팹 : {characterPrefab}");
            CharacterObject = Instantiate(characterPrefab);
            CharacterObject.transform.position = characterInitialPosition;
            GameStageManager.Instance.FocusCameraManager.TargetTransform = CharacterObject.transform;

            // 바구니 설정
            var basketInitObject = GameObject.FindGameObjectWithTag("BasketObject");
            var basketInitTransform = basketInitObject.transform;
            var basketParentTransform = basketInitTransform.parent;

            Debug.Log($"바구니 프리팹 : {basketPrefab}");
            var basketObject = Instantiate(basketPrefab);

            basketObject.transform.parent = basketParentTransform;
            basketObject.transform.localPosition = basketInitTransform.localPosition;
            basketObject.transform.localRotation = basketInitTransform.localRotation;

            Destroy(basketInitObject);
        }
    }
}
