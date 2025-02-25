using UnityEngine;
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

public class Games_Catalog : MonoBehaviour
{

    public Game_Data[] TheGames;
    void Start()
    {
        
    }
    [ContextMenu("FetchGames")]
    void FetchGames()
    {

    }
    void FetchGame(int GameId)
    {

    }
}
