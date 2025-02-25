using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class HistoryMan : MonoBehaviour
{
    public TMP_Text GameNameText;
    public GameObject SelectionObj;
    public GameObject HistoryObj;
    public Transform SpawnTrans;
    public GameObject BtnPref;
    public List<GameObject> SpawnedBtns = new List<GameObject>();
    private void OnEnable()
    {
        OpenSelection();
    }
    public void OpenSelection()
    {
        SelectionObj.SetActive(true);
        HistoryObj.SetActive(false);
        SpawnGames();
    }
    public void OpenHistory(HistoryBtn which)
    {
        GameNameText.text = which.TheName;
        SelectionObj.SetActive(false);
        HistoryObj.SetActive(true);
    }
    void ClearBtns()
    {
        for(int i = 0; i < SpawnedBtns.Count; i++)
        {
            Destroy(SpawnedBtns[i]);
        }
        SpawnedBtns.Clear();
    }
    public void SpawnGames()
    {

        ClearBtns();
        for(int i = 0; i < ExtraMan.Instance.games_Catalog.gameList.games.Length; i++)
        {
            Game_Data _Game = ExtraMan.Instance.games_Catalog.gameList.games[i];
            GameObject go = Instantiate(BtnPref, SpawnTrans);
            SpawnedBtns.Add(go);

        }
    }

}
