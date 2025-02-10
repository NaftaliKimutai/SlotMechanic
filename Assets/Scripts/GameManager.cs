using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool IsDemo;
    public static GameManager Instance;
    public bool IsGameStarted;
    public bool IsGameOver;
    public PlayMan playMan;
    public ResultMan resultMan;
    private void Awake()
    {
        if (Instance)
        {
            Destroy(Instance);
        }
        Instance = this;
    }
    private void Start()
    {
        OpenMenu();
    }
    public void StartGame()
    {
        IsGameStarted = true;
        playMan.Play();
    }
    public void GameOver()
    {
        if (IsGameOver)
            return;
        IsGameOver = true;


        Invoke(nameof(OpenMenu), 0.1f);
    }
    public void OpenMenu()
    {
        IsGameOver = false;
        IsGameStarted = false;
    }
}
