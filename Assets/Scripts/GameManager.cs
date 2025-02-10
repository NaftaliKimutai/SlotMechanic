using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool IsDemoMode;
    public static GameManager Instance;
    public bool IsGameStarted;
    public bool IsGameOver;
    public PlayMan playMan;
    public ResultMan resultMan;
    public WebMan webMan;
    public PromtMan promtMan;
    public bool IsGameQuit;
    public InternetIndicator internetIndicator;
    public CashMan cashMan;
    public CashOutUi cashOutUi;
    public PlaceBet placeBet;
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
        SetUp();
        OpenMenu();
    }
    void SetUp()
    {
        webMan.ManualStart();
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
        if (playMan.Respins > 0)
        {
            placeBet.Pressed();

        }
    }
}
