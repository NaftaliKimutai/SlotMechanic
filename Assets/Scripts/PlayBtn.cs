using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayBtn : MonoBehaviour
{
    public GameObject NotPlayingObj;
    public GameObject PlayingObj;
    public TMP_Text BetText;
    void Update()
    {
        if (!GameManager.Instance.IsGameStarted)
        {
            BetText.text =GameManager.Instance.playMan.BetAmount.ToString();
            NotPlayingObj.SetActive(true);
            PlayingObj.SetActive(false);
            GetComponent<Button>().interactable = true;
        }
        else
        {
            NotPlayingObj.SetActive(false);
            PlayingObj.SetActive(true);
            GetComponent<Button>().interactable = false;
        }
    }
    public void AddBetBalance(float Amount)
    {
        float betamount = GameManager.Instance.playMan.BetAmount;
        betamount += Amount;
        if (betamount < 0)
        {
            betamount = 1;
        }
        GameManager.Instance.playMan.BetAmount = betamount;
    }
  
}
