using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Collections.Generic;

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
    public Sprite ThePromoIcon;
    public int approved = 1;
    public bool IsLoaded;
}
[System.Serializable]
public class GameList
{
    public Game_Data[] games;

}

public class Games_Catalog : MonoBehaviour
{
    public bool IsLoaded;
    public string ServerLink = "https://admin-api.ibibe.africa";
    public GameList gameList;
    void Start()
    {
        FetchGames();
    }
    [ContextMenu("FetchGames")]
    public void FetchGames()
    {
        ExtraMan.Instance.fakeLoading.Open(1);
        IsLoaded = false;
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
                //Debug.Log("Received: " + www.downloadHandler.text);
                gameList = JsonUtility.FromJson<GameList>("{\"games\":" + www.downloadHandler.text + "}");
                Array.Reverse(gameList.games);
                List<Game_Data> game_Datas = new List<Game_Data>();
                Game_Data temp = new Game_Data();
                temp.id = 0;
                temp.game_image_url = "";
                temp.promotional_image_url = "";
                temp.game_title = "";
                temp.approved = 0;
                game_Datas.Add(temp);
                for(int i = 0; i < gameList.games.Length; i++)
                {
                    game_Datas.Add(gameList.games[i]);
                }
                gameList.games = game_Datas.ToArray();
                DownloadPromoImages();
            }
            else
            {
                Debug.Log("Error: " + www.error);
            }
        } // The using block ensures www.Dispo
    }
    [ContextMenu("DownloadPromoImages")]
    public void DownloadPromoImages()
    {
        for(int i = 0; i < gameList.games.Length; i++)
        {
            if (gameList.games[i].approved == 1)
            {
                string TheUrl = gameList.games[i].promotional_image_url;
                if (TheUrl == "")
                {
                    TheUrl = gameList.games[i].game_image_url;
                }
                if (TheUrl != "")
                {
                    Sprite TheIcon = GetSavedIcon(i);
                    if (!TheIcon)
                    {
                        StartCoroutine(DownloadImage(TheUrl, i));
                    }
                    else
                    {
                        gameList.games[i].IsLoaded = true;
                        CheckIsFinishLoading();
                    }
                }
                else
                {
                    gameList.games[i].IsLoaded = true;
                    CheckIsFinishLoading();
                }
            }
            else
            {
                gameList.games[i].IsLoaded = true;
                CheckIsFinishLoading();
            }

        }
    }
    IEnumerator DownloadImage(string MediaUrl,int theId)
    {
        //Debug.Log(MediaUrl);
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            gameList.games[theId].IsLoaded = true;
            CheckIsFinishLoading();
          //  Debug.Log(request.error);
        }
        else
        {
            gameList.games[theId].IsLoaded = true;
            CheckIsFinishLoading();

            //            Debug.Log(request.downloadHandler.text);
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
           // Debug.Log(MediaUrl);
            if (texture)
            {
                byte[] imageBytes = texture.EncodeToPNG();
                DestroyImmediate(texture);
                string savePath = "/Icons";
                string FileName = "/Game_" + theId.ToString() + ".png";
                if (SystemInfo.deviceType == DeviceType.Handheld)
                {
                    savePath = "Icons";
                }
                else
                {
                    savePath = Application.persistentDataPath + "/Icons";
                }
                DirectoryInfo DataFolder = new DirectoryInfo(savePath);
                if (!DataFolder.Exists)
                {
                    Directory.CreateDirectory(savePath);
                }
                if (DataFolder.Exists)
                {
                    //Debug.Log("PathAvailable");

                }
                //Debug.Log(savePath);
                System.IO.File.WriteAllBytes(savePath + FileName, imageBytes);

            }

            // gameList.games[theId].ThePromoIcon= ((DownloadHandlerTexture)request.downloadHandler).texture;
            //YourRawImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }
    void CheckIsFinishLoading()
    {
        bool isfinished = true;
        for(int i = 0; i < gameList.games.Length; i++)
        {
            if (!gameList.games[i].IsLoaded)
            {
                isfinished = false;
                break;
            }
        }
        if (isfinished)
        {
            IsLoaded = true;
        }
    }
    public Sprite GetSavedIcon(int theId)
    {
        string savePath = "/Icons";
        string FileName = "/Game_" + theId.ToString() + ".png";
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            savePath = "Icons";
        }
        else
        {
            savePath = Application.persistentDataPath + "/Icons";
        }
        //DirectoryInfo DataFolder = new DirectoryInfo(savePath);
        if (!File.Exists(savePath+FileName))
        {
            //Debug.Log("NoFile_"+ savePath + FileName);
            return null;
        }
        byte[] byteArray = File.ReadAllBytes(savePath+FileName);

        Texture2D texture = new Texture2D(8, 8);
        texture.LoadImage(byteArray);
        Vector2 Resolution = new Vector2(texture.width, texture.height);
        Sprite s = Sprite.Create(texture, new Rect(0, 0, Resolution.x, Resolution.y), Vector2.zero, 0.001f);
        gameList.games[theId].ThePromoIcon = s;
        // RectTransform rt = Go.GetComponent(typeof(RectTransform)) as RectTransform;
        //rt.sizeDelta = Resolution / 5;

        return s;
    }
    

}
