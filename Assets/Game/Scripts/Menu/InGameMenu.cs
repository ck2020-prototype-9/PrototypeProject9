using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject menuSet;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      

    }

    public void OnClickExit()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OnClickContinue()
    {
        menuSet.SetActive(false);
    }
}
