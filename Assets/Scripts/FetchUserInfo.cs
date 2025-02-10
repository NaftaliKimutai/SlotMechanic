using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;
public class FetchUserInfo : MonoBehaviour
{
    public ConfigMan configMan;
    string theuserid;
    public Toggle DemoToggle;
    [Header("Test")]
    public string Test_PlayerId = 5.ToString();
    public string Test_GameId = 20.ToString();
    public string Test_ClientId = 100.ToString();


    [ContextMenu("Test")]
   public void Test()
    {
        SetUserId(Test_PlayerId);
        SetGameId(int.Parse(Test_GameId));
        SetClientId(int.Parse(Test_ClientId));
    }
   
    public void SetUserId(string userId)
    {
        theuserid = userId;
       // Debug.Log("Received userId: " + userId);
      //  ConfigMan.ConfigInstance.PassCustomerId(userId);
        configMan.PassCustomerId(userId);
       configMan.PlayerIdText.text = userId;

        // Use the userId in your game logic
    }
    public void SetMode(int mode)
    {
        Debug.Log("TheMode: " +mode);
        if (mode == 0)
        {
            configMan.IsDemo = true;
        }
        else
        {
            configMan.IsDemo = false;
        }
        if (DemoToggle)
        {
            DemoToggle.isOn = configMan.IsDemo;
        }
    }
    public void SetGameId(int id)
    {

        //Debug.Log("TheGameId: " + id.ToString());
        configMan.PassGameId(id.ToString());
       configMan.GameIdText.text =  id.ToString();

    }
    public void SetClientId(int id)
    {
        //Debug.Log("TheGameId: " + id.ToString());
        configMan.PassClientId(id.ToString());
       configMan.ClientIdText.text = id.ToString();

    }
    public void SetLanguage(string id)
    {
        Debug.Log("TheLanguage: " + id.ToString());
        if (id == "en")
        {
            LanguageMan.instance._SetLanguage(Extra_TheLanguage.English);
        }
        else if (id == "zh") {
            LanguageMan.instance._SetLanguage(Extra_TheLanguage.Chinese);
        }
        else if (id == "es")
        {
            LanguageMan.instance._SetLanguage(Extra_TheLanguage.Spanish);
        }
        else if (id == "ja")
        {
            LanguageMan.instance._SetLanguage(Extra_TheLanguage.Japan);
        }
        else if (id == "sw")
        {
            LanguageMan.instance._SetLanguage(Extra_TheLanguage.Swahili);
        }
        else if (id == "da")
        {
            LanguageMan.instance._SetLanguage(Extra_TheLanguage.Danish);
        }
    }
}
