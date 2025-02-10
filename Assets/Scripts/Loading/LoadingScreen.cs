using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class LoadingScreen : MonoBehaviour
{
    public float LoadingTime = 5;
    float TheT;
    public Slider[] LoadingSlider;
    bool loaded;
    public TMP_Text[] LoadingText;
    bool permisionasked;
    public GameObject[] LoadingObj;
    public GameObject[] ContinueObj;
    public Toggle[] Thetoggle;
    public bool canskip;
    public bool IsSliderLoaded;
    public Animator FinalAnim;
    //public GameObject BlackScreen;
    //public MusicControl musicControl;
    private void Start()
    {
       // BlackScreen.SetActive(true);
        if (PlayerPrefs.GetInt("SkipContinue") == 1)
        {
            for(int i = 0; i < Thetoggle.Length; i++)
            {
                Thetoggle[i].isOn = true;
            }
          //  Thetoggle.isOn = true;
            canskip = true;
        }
        else
        {
            for (int i = 0; i < Thetoggle.Length; i++)
            {
                Thetoggle[i].isOn = false;
            }
           
            canskip=false;
        }
        ToggleObjArray(LoadingObj, true);
        ToggleObjArray(ContinueObj, false);
        // LoadingObj.SetActive(true);
        // ContinueObj.SetActive(false);
         //StartCoroutine(LoadScene());
        StartCoroutine(FakeLoad());
        //SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
    void Update()
    {
        TheT += Time.deltaTime;
       // LoadingSlider.fillAmount = TheT / LoadingTime;
        UpdateImageFillAmount(LoadingSlider, TheT / LoadingTime);
        if (TheT >= LoadingTime / 2&&!permisionasked)
        {
            permisionasked = true;
        }
        if (TheT >= LoadingTime)
        {
            if (!loaded)
            {
                IsSliderLoaded = true;
                if (canskip)
                {
                    loaded = true;
                }
                else
                {
                    ToggleObjArray(LoadingObj, false);
                    ToggleObjArray(ContinueObj, true);
                   // musicControl.TheMusic.SetActive(true);

                    //LoadingObj.SetActive(false);
                    //ContinueObj.SetActive(true);
                }
                //  loaded = true;
                // SceneManager.LoadScene(1);
            }
        }
        // LoadingText.text ="Loading...."+ Mathf.RoundToInt(LoadingSlider.fillAmount*100).ToString()+"%";
        UpdateTextArray(LoadingText, "Loading...." + Mathf.RoundToInt(LoadingSlider[0].value * 100).ToString() + "%");
    }
    public void ToggleSkip()
    {
        int which = 0;
        for (int i = 0; i < Thetoggle.Length; i++)
        {
            if (Thetoggle[i].gameObject.activeInHierarchy)
            {
                which = i;
               // break;
            }
        }
      //  Debug.Log(which);
        for (int i = 0; i < Thetoggle.Length; i++)
        {
            if (!Thetoggle[i].gameObject.activeInHierarchy)
            {
               // Debug.Log(i);
                Thetoggle[i].isOn = Thetoggle[which].isOn;
            }
        }
        if (Thetoggle[which].isOn)
        {
            PlayerPrefs.SetInt("SkipContinue", 1); 
        }
        else
        {
            PlayerPrefs.SetInt("SkipContinue", 0);
        }
    }
    public void Activate()
    {
        loaded=true;
    }
   
    IEnumerator FakeLoad()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1,LoadSceneMode.Additive);
        //Don't let the Scene activate until you allow it to
       asyncOperation.allowSceneActivation = false;


       // SceneManager.LoadScene(1, LoadSceneMode.Additive);
        //yield return new WaitForEndOfFrame();
       // BlackScreen.SetActive(false);

        while (!loaded)
        {
            //            Debug.Log(asyncOperation.isDone);
            /*if (asyncOperation.isDone)
            {
                BlackScreen.SetActive(false);
            }*/
            if (permisionasked)
            {
                asyncOperation.allowSceneActivation = true;
            }
            if (IsSliderLoaded)
            {
              //  asyncOperation.allowSceneActivation = true;
            }
            yield return new WaitForSeconds(0.1f);
        }

        FinalAnim.enabled = true;
        GameManager.Instance.FetchConfigData();
        yield return new WaitForSeconds(0.5f);
        SceneManager.UnloadSceneAsync(0);

    }
   
    IEnumerator LoadScene()
    {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
//        Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        bool isloaded = false;
        while (!asyncOperation.isDone&&!isloaded)
        {
            //Output the current progress
            //  m_Text.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f && loaded)
            {
                isloaded = true;
                //Change the Text to show the Scene is ready
                // m_Text.text = "Press the space bar to continue";
                //Wait to you press the space key to activate the Scene
                //if (Input.GetKeyDown(KeyCode.Space))
                //Activate the Scene
                //asyncOperation.allowSceneActivation = true;

            }

            yield return null;
           
        }
        FinalAnim.enabled = true;
        yield return new WaitForSeconds(1);
        asyncOperation.allowSceneActivation = true;
    }
    void ToggleObjArray(GameObject[] Which, bool IsActive)
    {
        for (int i = 0; i < Which.Length; i++)
        {
            Which[i].SetActive(IsActive);
        }
    }
    void UpdateTextArray(TMP_Text[] Which, string TheText)
    {
        for (int i = 0; i < Which.Length; i++)
        {
            Which[i].text = TheText;
        }
    }
    void UpdateImageFillAmount(Slider[] Which, float Amount)
    {
        for (int i = 0; i < Which.Length; i++)
        {
            Which[i].value = Amount;
        }
    }
}
