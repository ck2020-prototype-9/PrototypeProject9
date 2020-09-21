using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private static int count = 0;
    private int index;
    [SerializeField] Slider slider1;
    [SerializeField] Slider slider2;
    [SerializeField] Slider slider3;
  //  [SerializeField] Text MouseInverseName;

    public void SoundSave()
    {
        Debug.Log("저장됌");
        PlayerPrefs.SetFloat("SoundA", slider1.value);
        PlayerPrefs.SetFloat("SoundB", slider2.value);
        PlayerPrefs.SetFloat("SoundC", slider3.value);
        PlayerPrefs.Save();
    }

    public void SoundLoad()
    {
        //if (!PlayerPrefs.HasKey("SoundA")|| !PlayerPrefs.HasKey("SoundB")|| !PlayerPrefs.HasKey("Sound"))
        //    return;
        slider1.value = PlayerPrefs.GetFloat("SoundA");
        slider2.value = PlayerPrefs.GetFloat("SoundB");
        slider3.value = PlayerPrefs.GetFloat("SoundC");


    //    MouseInverseName.text = PlayerPrefs.GetString("MouseInverseName");

    }

     void Awake()
    {
        //slider1 = GameObject.Find("Canvas").transform.Find("Slider1").GetComponent<Slider>();
        index = count;
        count++;  
            DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        SoundLoad();

      
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
