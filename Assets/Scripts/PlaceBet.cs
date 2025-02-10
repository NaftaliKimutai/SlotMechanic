using UnityEngine;
using UnityEngine.UI;
public enum PlaceState
{
    Place,
    playing
}
public class PlaceBet : MonoBehaviour
{
    public float CantPlaceTimestamp;
    public PlaceState placeState;
    public Button[] Btns;
    public PlaceBetResponse BetData;
    public void Refesh(PlaceState state)
    {
        placeState = state;
        if (placeState == PlaceState.Place)
        {
            for(int i = 0; i < Btns.Length; i++)
            {
                Btns[i].interactable = true;
            }
        }
        else {
        }
    }
    public void AddCantPlaceTimestamp(float Amount = 1)
    {
        CantPlaceTimestamp = Time.time + Amount;
    }
    public void Pressed()
    {
        if (CantPlaceTimestamp > Time.time)
        {
            return;
        }
        if (placeState == PlaceState.Place)
        {
            if (!GameManager.Instance.IsGameStarted)
            {
                float Balance = PlayerPrefs.GetFloat("TotalCash");
                //Balance = 30;
                if (GameManager.Instance.playMan.Respins > 0)
                {
                    GameManager.Instance.webMan.PlaceBet(0, this);
                    GameManager.Instance.StartGame();
                    GameManager.Instance.playMan.UseRespin();

                    for (int i = 0; i < Btns.Length; i++)
                    {
                        Btns[i].interactable = false;
                    }
                    Invoke(nameof(delayactivate), 1);

                }else  if (Balance >= GameManager.Instance.playMan.BetAmount)
                {
                    GameManager.Instance.webMan.PlaceBet(GameManager.Instance.playMan.BetAmount, this);
                    GameManager.Instance.StartGame();
                    for (int i = 0; i < Btns.Length; i++)
                    {
                        Btns[i].interactable = false;
                    }
                    Invoke(nameof(delayactivate), 1);
                }
                else
                {
                    Debug.Log("InsufficientBalance");
                    GameManager.Instance.promtMan.DisplayPrompt(PromptType.InsufficientFunds);
                }

            }
        }
    }
    void delayactivate()
    {
        for (int i = 0; i < Btns.Length; i++)
        {
            Btns[i].interactable = true;
        }
    }
    public void CashOut()
    {
        if (GameManager.Instance.playMan.WinAmount > 0)
        {
            GameManager.Instance.webMan.BetWon(this, GameManager.Instance.playMan.WinAmount);
        }

    }
}
