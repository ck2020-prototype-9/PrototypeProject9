using UnityEngine;
using System.Collections;

public class PlayerCharacterGameOverTrigger : MonoBehaviour
{
    [SerializeField] public GameObject tutorial;

    public void Awake()
    {
        tutorial = GameObject.Find("Tutorialmanager").transform.Find("Tutorial").gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Terrain"))
        {
            // 게임오버 처리
           if( tutorial.activeSelf)
            {

            }
           else
            {
                GameStageManager.Instance.IsGameOver = true;
            }
        }
    }
}