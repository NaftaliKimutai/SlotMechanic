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
        SetGameId(Test_GameId);
        SetClientId(Test_ClientId);
    }
   
    public void SetUserId(string userId)
    {
        theuserid = userId;
       // Debug.Log("Received userId: " + userId);
      //  ConfigMan.ConfigInstance.PassCustomerId(userId);
        configMan.PassCustomerId(userId);
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
    public void SetGameId(string id)
    {
        //Debug.Log("TheGameId: " + id.ToString());
        configMan.PassGameId(id.ToString());
    }
    public void SetClientId(string id)
    {
        //Debug.Log("TheGameId: " + id.ToString());
        configMan.PassClientId(id.ToString());
    }
}
