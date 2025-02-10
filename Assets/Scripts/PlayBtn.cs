using UnityEngine;
using UnityEngine.UI;
public class PlayBtn : MonoBehaviour
{
    public GameObject NotPlayingObj;
    public GameObject PlayingObj;
    void Update()
    {
        if (!GameManager.Instance.IsGameStarted)
        {
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
  
}
