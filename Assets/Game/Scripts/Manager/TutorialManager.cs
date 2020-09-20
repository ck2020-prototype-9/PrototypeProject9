using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.InputSystem;
//using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Controls;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private Text text;
    public int count=1;

    [SerializeField] GameObject tutorialExit;
    [SerializeField] public bool tutorialCheck;
    [SerializeField] public float checkTime=1;
    [SerializeField] public float currentCheckTime;

    [SerializeField] private GameObject leftBox;
    [SerializeField] private GameObject rightBox;
    [SerializeField] private GameObject reset;
    [SerializeField] private GameObject checkBox;
    [SerializeField] private GameObject timer;
    private void Awake()
    {
        currentCheckTime = checkTime;
        
    }
    private void Update()
    {
        check();
    }
 

   public void check()
    {
        switch (count)
        {
            case 1:
                text.text = "W,A,S,D를 꾹 눌러보십시오.\n캐릭터 의 상체가 기울어집니다.";
                if(count == 1)
                {
                   // checkBox.SetActive(false);
                    leftBox.SetActive(false);
                    if (!tutorialCheck)
                    {
                        if (Keyboard.current[Key.A].isPressed || Keyboard.current[Key.S].isPressed || Keyboard.current[Key.W].isPressed || Keyboard.current[Key.D].isPressed)
                        {
                            currentCheckTime -= Time.deltaTime;
                            if (currentCheckTime < 0)
                            {
                                Debug.Log("성공!");
                                tutorialCheck = true;
                            //    checkBox.SetActive(true);
                                //  StageRe();
                            }
                        }
                    }
                    else if (count == 1 && tutorialCheck)
                    {
                        rightBox.SetActive(true);
                        //checkBox.SetActive(true);
                    }
                }
                
                break;

            case 2:
                text.text = "R키를 눌러보십시오.\n캐릭터가 리셋됩니다.";
                if(count == 2)
                {
                //    checkBox.SetActive(false);
                    if (!tutorialCheck)
                    {
                        leftBox.SetActive(true);
                        rightBox.SetActive(false);
                        if (Keyboard.current[Key.R].wasPressedThisFrame)
                        {
                            Debug.Log("성공!");
                            tutorialCheck = true;
                       //     checkBox.SetActive(true);
                        }
                    }
                    else if (count == 2 && tutorialCheck)
                    {
                        rightBox.SetActive(true);
                    //    checkBox.SetActive(true);      
                    }
                }
               
                break;

            case 3:
                text.text = "좌클릭을 한 채로 위, 아래로 드래그 해보십시오.\n왼쪽 다리가 움직입니다\n넘어진다면 R키로 다시 시도해보십시오.";
                if(count == 3)
                {
                 //   checkBox.SetActive(false);
                    if (!tutorialCheck)
                    {
                        leftBox.SetActive(true);
                        rightBox.SetActive(false);
                        if (Mouse.current.leftButton.isPressed)
                        {
                            currentCheckTime -= Time.deltaTime;
                            if (currentCheckTime < 0)
                            {
                                Debug.Log("성공!");
                                tutorialCheck = true;
                         //       checkBox.SetActive(true);

                            }
                        }
                    }
                    else if (tutorialCheck)
                    {
                        rightBox.SetActive(true);
                    //    checkBox.SetActive(true);
                    }
                }
                
                break;

            case 4:
                text.text = "우클릭을 한 채로 위, 아래로 드래그 해보십시오.\n 오른쪽 다리가 움직입니다.\n넘어진다면 R키 로 다시 시도해보십시오.";
                if (count == 4)
                {
                 //   checkBox.SetActive(false);
                    if (!tutorialCheck)
                    {
                        leftBox.SetActive(true);
                        rightBox.SetActive(false);
                        if (Mouse.current.rightButton.isPressed)
                        {
                            currentCheckTime -= Time.deltaTime;
                            if (currentCheckTime < 0)
                            {
                                Debug.Log("성공!");
                                tutorialCheck = true;
                            //    checkBox.SetActive(true);

                            }
                        }
                    }
                    else if (tutorialCheck)
                    {
                        rightBox.SetActive(true);
                     //   checkBox.SetActive(true);
                    }
                }
                break;

            case 5:
                // checkBox.SetActive(false);
                rightBox.SetActive(false);
                text.text = "당신은 조작키를 마스터했습니다.\n이제 앞으로 전진할 일만 남았군요!\n도착지점까지 가봅시다.";
                if (Mouse.current.rightButton.isPressed || Mouse.current.leftButton.isPressed)
                {
                    tutorialExit.SetActive(false);
                    gameObject.SetActive(false);
                    timer.SetActive(true);
                }
                    break;

            case 6:
              
                break;
        }
    }
    public void StageRe()
    {
        reset.GetComponent<GameStageManager>().StageReset();
    }
}
