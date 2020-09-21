using UnityEngine;
using System.Collections;

public class PlayerCharacterGameOverTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Terrain"))
        {
                GameStageManager.Instance.IsGameOver = true;
        }
    }
}