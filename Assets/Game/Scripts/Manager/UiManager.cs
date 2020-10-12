using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; }

    private static int count = 0;
    private int index;
    [SerializeField] Slider slider1;
    [SerializeField] Slider slider2;
    [SerializeField] Slider slider3;
    //  [SerializeField] Text MouseInverseName;

    [SerializeField] public int stageClearCheck=0;
    [SerializeField] public int savedStageCleckCheck = 0;

    [SerializeField] private bool tutorialClear = false;
    [SerializeField] private bool stage1_1Clear = false;
    [SerializeField] private bool stage1_2Clear = false;
    [SerializeField] private bool stage1_3Clear = false;
    [SerializeField] private bool stage1_4Clear = false;
    [SerializeField] private bool stage1_5Clear = false;
    [SerializeField] private bool stage1_6Clear = false;
   
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
        // DontDestroyOnLoad(this.gameObject);

        //  savedStageCleckCheck = PlayerPrefs.GetInt("StageCheck", 0);

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

    }

    void Start()
    {
        Debug.Log("사운드 로드");
        SoundLoad();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (slider1 == null)
        {
            slider1 = GameObject.Find("Canvas").transform.Find("Option Menu").transform.Find("Sound1").transform.Find("Slider1").GetComponent<Slider>();
        }
        else if (slider2 == null)
        {
            slider2 = GameObject.Find("Canvas").transform.Find("Option Menu").transform.Find("Sound2").transform.Find("Slider2").GetComponent<Slider>();
        }
        else if (slider3 == null)
        {
            slider3 = GameObject.Find("Canvas").transform.Find("Option Menu").transform.Find("Sound3").transform.Find("Slider3").GetComponent<Slider>();
        }

        if(stageClearCheck> savedStageCleckCheck)
        {
            //PlayerPrefs.SetInt("StageCheck", stageClearCheck);
            //Debug.Log("스테이지 세이브");
        }
    }
}
