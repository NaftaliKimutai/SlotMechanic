using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class Game_Data
{
    public int id = 35;
    public string game_title = "Scratch Fortune Gem";
    public string game_image;
    public string game_image_url = "https://admin-api.ibibe.africa/gd";
    public string promotional_image_url;
    public string game_description;
    public string game_type = "Normal";
}
[System.Serializable]
public class GameList
{
    public Game_Data[] games;

}

public class Games_Catalog : MonoBehaviour
{
    public string ServerLink = "https://admin-api.ibibe.africa";
    public GameList gameList;
    void Start()
    {
        
    }
    [ContextMenu("FetchGames")]
    public void FetchGames()
    {
        StartCoroutine(_FetchGames(ServerLink + "/api/v1/games/"));
    }
    IEnumerator _FetchGames(string url)
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
                gameList = JsonUtility.FromJson<GameList>("{\"games\":" + www.downloadHandler.text + "}");
                Array.Reverse(gameList.games);
            }
            else
            {
                Debug.Log("Error: " + www.error);
            }
        } // The using block ensures www.Dispo
    }
   
}
