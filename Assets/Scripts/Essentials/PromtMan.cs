using UnityEngine;
using TMPro;
public enum PromptType
{
    InsufficientFunds,
    IdleForTooLong,
    ConnectionError,
    DemoWarning
}

public class PromtMan : MonoBehaviour
{
    public Promt[] Prompts;
    public GameObject Holder;
    int test;
    float idletimestamp;
    public GameObject QuitScreenObj;


    /*private void Update()
    {
        if (Input.anyKeyDown)
        {
            idletimestamp = 0;
        }
        idletimestamp += Time.deltaTime;
        if (idletimestamp > 60)
        {
            DisplayPrompt(PromptType.IdleForTooLong);
        }
    }*/

    public void DisplayPrompt(PromptType Which)
    {
        for(int i = 0; i < Prompts.Length; i++)
        {
            if (Which == Prompts[i].TheType)
            {
                Prompts[i].gameObject.SetActive(true);
            }
            else
            {
                Prompts[i].gameObject.SetActive(false);
            }
        }
        Holder.SetActive(true);
    }
    public void DemoWarning(string TheT)
    {
        DisplayPrompt(PromptType.DemoWarning);
       Holder.GetComponentInChildren<TMP_Text>().text = TheT;
    }
    public void Close()
    {
        Holder.SetActive(false);
    }
    [ContextMenu("TestPrompt")]
    public void TestPrompt()
    {
        DisplayPrompt((PromptType)test);
        test += 1;
        if (test > Prompts.Length - 1)
        {
            test = 0;
        }
    }
    public void QuitGame()
    {
        GameManager.Instance.IsGameQuit = true;
        QuitScreenObj.SetActive(true);
        Close();
        Application.Quit();

    }
}
