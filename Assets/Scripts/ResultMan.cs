using UnityEngine;
[System.Serializable]
public class ResultData
{
    public SymbolControl symbolControl;
    public Symbol[] Results;
}
public class ResultMan : MonoBehaviour
{
    public ResultData[] TheResults;
    public void AssignResults()
    {
        for(int i = 0; i < TheResults.Length; i++)
        {
            for(int r = 0; r < TheResults[i].Results.Length; r++)
            {
                TheResults[i].symbolControl.StopTarget = TheResults[i].Results[r];

                    GameManager.Instance.playMan.AssignSymbol(TheResults[i].symbolControl.ActiveContainers[r],
                  TheResults[i].Results[r]);
            }
        }
    }
}
