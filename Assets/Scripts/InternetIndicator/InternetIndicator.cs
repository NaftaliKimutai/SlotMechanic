using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.Collections;
using UnityEngine.Networking;

public class InternetIndicator : MonoBehaviour
{
    public int TheLevel;
    public float InternetSpeed;
    [System.Serializable]
    public class InternetLevels
    {
        public GameObject[] Levels;
    }
    public InternetLevels[] Levels;
    private void Start()
    {
        Refresh(0);
    }
    [ContextMenu("Check")]

    public void CheckInternet()
    {
        //Debug.Log("CheckInternet");
        StartCoroutine(CheckConnection());
    }
    IEnumerator CheckConnection()
    {
        float starttime=Time.time;
        float strenght = 1;
        bool isconnection = false;
        using (UnityWebRequest www = UnityWebRequest.Get(GameManager.Instance.webMan.GetInternetUrl()))
        {
            www.useHttpContinue = false;
            yield return www.SendWebRequest();
            //Debug.Log(www.result);

            if (www.result == UnityWebRequest.Result.Success)
            {
                isconnection = true;
                strenght = Time.time - starttime;
                //Debug.Log(strenght);

            }
            else
            {
             //   Debug.Log("FailedInternetCheck");
            }
        }
        //        Debug.Log(strenght);
        InternetSpeed = strenght;

        if (strenght <= 0)
        {
            Refresh(0);
        }
        else if (strenght <= 0.05f)
        {
            Refresh(1);
        }
        else if (strenght <= 0.1f)
        {
            Refresh(2);
        }
        else if (strenght <= 0.5f)
        {
            Refresh(3);
        }
        else
        {
            Refresh(4);
        }
    }
    
    void Refresh(int Level)
    {
        TheLevel = Level;
        for (int i = 0; i <Levels.Length; i++)
        {
            if (i == Level)
            {

                SetActive(Levels[i], true);

            }
            else
            {
                SetActive(Levels[i], false);

            }

        }
    }
    void SetActive(InternetLevels which, bool istrue)
    {
        
        for (int i = 0; i <which.Levels.Length; i++)
        {
            if (istrue)
            {
                which.Levels[i].SetActive(true);
            }
            else
            {
                which.Levels[i].SetActive(false);
            }


        }
    }
 
  
}