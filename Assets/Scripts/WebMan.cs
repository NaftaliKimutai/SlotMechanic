using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;
public class PlayerData
{
    public string game_id = "i94545454JKL45";
    public string session_id= "1234567";
    public string user_id= "999oo-59uut-jjr-ttt";
    public string client_id;
    public float stake_amount;
}
public class ReceivedData
{
    public string max_multiplier_text;
    public string max_multiplier_hash;
    public string game_id;
}
public class PlaceBetData
{
    public string bet_id;
    public string player_id;
    public float amount;
    public string game_id="12224";
    public string client_id="12345";
}
[System.Serializable]
public class PlaceBetResponse
{
    public string message;
    public string bet_id;
    public float new_wallet_balance;
    public string status;
}
public class BetWonData
{
    public string bet_id;
    public float amount_won;
    public string client_id;
}
public class BetWonResponse
{
    public string message;
    public string bet_id;
    public float amount_won;
    public float new_wallet_balance;
    public string status;
}
[System.Serializable]
public class PlayerInfo
{
    public string id;
    public string names;
    public string msisdn;
    public string account_number;
    public string email_address;
    public string is_blacklisted;
    public string wallet_balance;
    public string created_at;
    public string last_bet_date;
    public string last_win_date;
}
public class GameInfo
{
    public string client_id = "12345";
    public string player_id = "4";
    public string bet_id = "uuui-54545-gfg54-gfg-rtr-fc-trtrt-gE";
    public float stake_amount = 200;
    public string game_id = "2";
    public string action = "spin";
}
[System.Serializable]
public class GameInfoResponseData
{
    public float win_amount;
    public Reels reels;
}
[System.Serializable]
public class Reels
{
    public string reel1;
    public string reel2;
    public string reel3;
    public string reel_bonus;
}
[System.Serializable]
public class AddCashData
{
    public int customer_id=12;
    public string payment_method = "visa";
    public string transaction_id = "HGFBTNNRKgagagaT";
    public float amount = 20;
}
public class AddCashResponse
{

}
[System.Serializable]
public class MakeWithdrawalData
{
    public int customer_id = 12;
    public float amount = 20;
}

public class WebMan : MonoBehaviour
{
    public string ServerLink = "https://admin-api.ibibe.africa";
    public PlayerInfo playerInfo;
    public bool IsFetched;
    public string MaxMultiplier;
    public string Player_Id = "4";
    public string Game_Id = "1234";
    public string Client_id = "12345";
    public bool IsTest;
    public TMP_Text[] TransactionsText;
    public void ManualStart()
    {
        for (int i = 0; i < TransactionsText.Length; i++)
        {
            TransactionsText[i].text ="";
        }
        if (GameManager.Instance.IsDemoMode)
        {
            playerInfo.names = "Demo";
            playerInfo.wallet_balance = 2000.ToString();
        }
        FetchPlayerInfo();
    }
    public void ShowTransaction(string thetrans)
    {
        thetrans = "Transaction 15614 - 040024 -" + thetrans;
        for (int i = 0; i < TransactionsText.Length; i++)
        {
            TransactionsText[i].text = thetrans;
        }
    }
    
   
    [ContextMenu("ListCustomers")]
    public void ListCustomers()
    {
        StartCoroutine(_FetchCustomers(ServerLink + "/api/v1/customer/details"));
    }
    IEnumerator _FetchCustomers(string url)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.useHttpContinue = false;
           www.SetRequestHeader("Cache-Control", "no-cache, no-store, must-revalidate");
          www.SetRequestHeader("Pragma", "no-cache");
           www.SetRequestHeader("Expires", "0");
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Received: " + www.downloadHandler.text);
            }
            else
            {
                Debug.Log("Error: " + www.error);
                GameManager.Instance.promtMan.DisplayPrompt(PromptType.ConnectionError);
            }
        } // The using block ensures www.Dispo
    }
    public void MakeWithdrawal(float _Amount)
    {
        MakeWithdrawalData Data = new MakeWithdrawalData();
        Data.customer_id = int.Parse(Player_Id);
        Data.amount = _Amount;
        string jsonString = JsonUtility.ToJson(Data);
        string TheUrl = ServerLink + "/api/withdraw/money";
        StartCoroutine(_MakeWithdrawal(ServerLink + "/api/v1/withdraw/money", jsonString));
    }
    IEnumerator _MakeWithdrawal(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Cache-Control", "no-cache, no-store, must-revalidate");
        request.SetRequestHeader("Pragma", "no-cache");
        request.SetRequestHeader("Expires", "0");
        yield return request.SendWebRequest();
        //Debug.Log("Status Code: " + request.responseCode);
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Received: " + request.downloadHandler.text);

            FetchPlayerInfo();

        }
        else
        {
            Debug.Log("Error: " + request.error);
            GameManager.Instance.promtMan.DisplayPrompt(PromptType.ConnectionError);
        }
    }
    public void AddCashAmount(float _Amount)
    {
        AddCashData Data = new AddCashData();
        Data.transaction_id= UnityEngine.Random.Range(100, 10000000).ToString()+"_"+ UnityEngine.Random.Range(100, 10000000).ToString();
        Data.customer_id =int.Parse(Player_Id);
        Data.amount = _Amount;
        string jsonString = JsonUtility.ToJson(Data);
        StartCoroutine(_AddCashAmount(ServerLink + "/api/v1/add_game_payment", jsonString));
    }
    IEnumerator _AddCashAmount(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Cache-Control", "no-cache, no-store, must-revalidate");
        request.SetRequestHeader("Pragma", "no-cache");
        request.SetRequestHeader("Expires", "0");
        yield return request.SendWebRequest();
        //Debug.Log("Status Code: " + request.responseCode);
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Received: " + request.downloadHandler.text);

            FetchPlayerInfo();

        }
        else
        {
            Debug.Log("Error: " + request.error);
            GameManager.Instance.promtMan.DisplayPrompt(PromptType.ConnectionError);
        }
        GameManager.Instance.internetIndicator.CheckInternet();
    }

    
  
    public string GetInternetUrl()
    {
        return ServerLink + "/api/v1/check/connection";
    }
    public void FetchPlayerInfo()
    {
        if (GameManager.Instance.IsDemoMode)
        {
            GameManager.Instance.cashMan.UpdateCashAmout(float.Parse(playerInfo.wallet_balance));
        }
        else
        {
            StartCoroutine(_FetchPlayerInfo(ServerLink + "/api/v1/customer/details?customer_id=" + Player_Id));
        }
        GameManager.Instance.internetIndicator.CheckInternet();
    }
    IEnumerator _FetchPlayerInfo(string url)
    {
        using (UnityWebRequest www = UnityWebRequestHelper.GetWithTimestamp(url))
        {
            www.useHttpContinue = false;
            www.SetRequestHeader("Cache-Control", "no-cache, no-store, must-revalidate");
            www.SetRequestHeader("Pragma", "no-cache");
            www.SetRequestHeader("Expires", "0");
            yield return www.SendWebRequest();
           
            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Received: " + www.downloadHandler.text);
                playerInfo = JsonUtility.FromJson<PlayerInfo>(www.downloadHandler.text);
                GameManager.Instance.cashMan.UpdateCashAmout(float.Parse(playerInfo.wallet_balance));
            }
            else
            {
                Debug.Log("Error: " + www.error);
                GameManager.Instance.promtMan.DisplayPrompt(PromptType.ConnectionError);
            }
        } // The using block ensures www.Dispo
    }

   
   


    public void FetchSlots()
    {

        PlaceBet placeBet = FindObjectOfType<PlaceBet>();

        GameInfo Data = new GameInfo();
        //Data.game_id = "crazy-seven";
        Data.game_id = Game_Id;
        Data.client_id = Client_id;
        Data.stake_amount = GameManager.Instance.playMan.BetAmount;
        // string theid = UnityEngine.Random.Range(100, 10000000).ToString();
        Data.bet_id = placeBet.BetData.bet_id;
       

        string jsonString = JsonUtility.ToJson(Data);
        StartCoroutine(_FetchSlots("https://speed.ibibe.africa/game/crazy-seven/", jsonString));
    }
    IEnumerator _FetchSlots(string url, string bodyJsonString)
    {
        Debug.Log(bodyJsonString);
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Cache-Control", "no-cache, no-store, must-revalidate");
        request.SetRequestHeader("Pragma", "no-cache");
        request.SetRequestHeader("Expires", "0");
        yield return request.SendWebRequest();
        //Debug.Log("Status Code: " + request.responseCode);
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Received: " + request.downloadHandler.text);


            //JSONNode root = JsonUtility.Parse(yourJsonText);

            string _Data = JsonHelper.GetJsonObject(request.downloadHandler.text, "data");

            GameInfoResponseData responsedata = new GameInfoResponseData();
            responsedata = JsonUtility.FromJson<GameInfoResponseData>(_Data);

            GameManager.Instance.playMan.UpdateWin(responsedata.win_amount);
            Debug.Log(responsedata.reels.reel1);
            GameManager.Instance.resultMan.TheResults[0].Results[0] = GetSymbol(responsedata.reels.reel1);
            GameManager.Instance.resultMan.TheResults[1].Results[0] = GetSymbol(responsedata.reels.reel2);
            GameManager.Instance.resultMan.TheResults[2].Results[0] = GetSymbol(responsedata.reels.reel3);
            GameManager.Instance.resultMan.TheResults[3].Results[0] = GetSymbol(responsedata.reels.reel_bonus);


            GameManager.Instance.playMan.ReceivedSymbols = true;

            // Debug.Log(responsedata.amount);
            // Debug.Log(responsedata.isMine);
            //GameManager.Instance.cashMan.UpdateCashAmout(responsedata.new_wallet_balance);


        }
        else
        {
            Debug.Log("Error: " + request.error);
            GameManager.Instance.promtMan.DisplayPrompt(PromptType.ConnectionError);
        }
    }
    Symbol GetSymbol(string Which)
    {
        
        if(Which== "777")
        {
            return Symbol.Tripple_7;
        }else if(Which== "77")
        {
            return Symbol.Double_7;
        }
        else if (Which == "7")
        {
            return Symbol.Single_7;
        }
        else if (Which == "double_bar")
        {
            return Symbol.Double_Bar;
        }
        else if (Which == "single_bar")
        {
            return Symbol.Single_Bar;
        }
        else if (Which == "empty")
        {
            return Symbol.None;
        }
        else if (Which == "respin")
        {
            return Symbol.Respin;
        }
        else if (Which == "10x")
        {
            return Symbol.Win_10x;
        }
        else if (Which == "5x")
        {
            return Symbol.Win_5x;
        }
        else if (Which == "2x")
        {
            return Symbol.Win_2x;
        }
        else if (Which == "coin2")
        {
            return Symbol.Win_Extra_2;
        }
        else if (Which == "coin1")
        {
            return Symbol.Win_Extra_1;
        }
        return Symbol.None;
    }
    public void PlaceBet(float Amount,PlaceBet Which)
    {
        GameManager.Instance.playMan.ReceivedSymbols = false;
        PlaceBetData Data = new PlaceBetData();
        string theid = UnityEngine.Random.Range(100, 10000000).ToString();
        Data.game_id = Game_Id;
        ShowTransaction(theid);
        Data.bet_id = theid;
        Data.player_id = playerInfo.id;
        Data.amount = Amount;
        if (GameManager.Instance.IsDemoMode)
        {

            PlaceBetResponse responsedata = new PlaceBetResponse();
            responsedata.new_wallet_balance = float.Parse(playerInfo.wallet_balance) - Amount;
            GameManager.Instance.cashMan.UpdateCashAmout(responsedata.new_wallet_balance);
            Which.BetData = responsedata;
            GameManager.Instance.playMan.ReceivedSymbols = true;
            playerInfo.wallet_balance = responsedata.new_wallet_balance.ToString();


        }
        else
        {
            string jsonString = JsonUtility.ToJson(Data);
            StartCoroutine(_PlaceBet(ServerLink + "/api/v1/bet/place_bet", jsonString, Which));
        }
        GameManager.Instance.internetIndicator.CheckInternet();
        ExtraMan.Instance.missionsMan.AddMissionPoints(Amount);
        ExtraMan.Instance.giftsMan.AddGiftPoints(Amount);

    }
    IEnumerator _PlaceBet(string url, string bodyJsonString, PlaceBet Which)
    {
        Debug.Log(bodyJsonString);
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Cache-Control", "no-cache, no-store, must-revalidate");
        request.SetRequestHeader("Pragma", "no-cache");
        request.SetRequestHeader("Expires", "0");
        yield return request.SendWebRequest();
        //Debug.Log("Status Code: " + request.responseCode);
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Received: " + request.downloadHandler.text);

            PlaceBetResponse responsedata = new PlaceBetResponse();
            responsedata = JsonUtility.FromJson<PlaceBetResponse>(request.downloadHandler.text);
            GameManager.Instance.cashMan.UpdateCashAmout(responsedata.new_wallet_balance);
            Which.BetData = responsedata;
            FetchSlots();


        }
        else
        {
            Debug.Log("Error: " + request.error);
            GameManager.Instance.promtMan.DisplayPrompt(PromptType.ConnectionError);
        }
    }
    
    public void BetWon(PlaceBet Which,float WinAmount)
    {
        BetWonData Data = new BetWonData();
        Data.bet_id = Which.BetData.bet_id;
        Data.amount_won = WinAmount;
        Data.client_id = "12345";
        if (GameManager.Instance.IsDemoMode)
        {
            BetWonResponse responsedata = new BetWonResponse();
            responsedata.new_wallet_balance =float.Parse(playerInfo.wallet_balance)+WinAmount;
            responsedata.amount_won = WinAmount;
            GameManager.Instance.cashMan.UpdateCashAmout(responsedata.new_wallet_balance);
            GameManager.Instance.cashOutUi.ShowCashOut("x", responsedata.amount_won);
            playerInfo.wallet_balance = responsedata.new_wallet_balance.ToString();
        }
        else {
            string jsonString = JsonUtility.ToJson(Data);
            StartCoroutine(_BetWon(ServerLink + "/api/v1/update_bet", jsonString, Which));
        }


    }
    IEnumerator _BetWon(string url, string bodyJsonString, PlaceBet Which)
    {
       
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Cache-Control", "no-cache, no-store, must-revalidate");
        request.SetRequestHeader("Pragma", "no-cache");
        request.SetRequestHeader("Expires", "0");
        yield return request.SendWebRequest();
        //Debug.Log("Status Code: " + request.responseCode);
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Received: " + request.downloadHandler.text);

            BetWonResponse responsedata = new BetWonResponse();
            responsedata = JsonUtility.FromJson<BetWonResponse>(request.downloadHandler.text);
            GameManager.Instance.cashMan.UpdateCashAmout(responsedata.new_wallet_balance);
            GameManager.Instance.cashOutUi.ShowCashOut( "x", responsedata.amount_won);
           

        }
        else
        {
            Debug.Log(bodyJsonString);
            Debug.Log("Error: " + request.error);
            GameManager.Instance.promtMan.DisplayPrompt(PromptType.ConnectionError);
        }

    }
}
