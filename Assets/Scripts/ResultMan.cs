using UnityEngine;
[System.Serializable]
public class ResultData
{
    public SymbolControl symbolControl;
    public Symbol[] Results;
}
public class ResultMan : MonoBehaviour
{
    public bool IsTest;
    public ResultData[] TheResults;
    public void AssignResults()
    {
        for(int i = 0; i < TheResults.Length; i++)
        {
            for(int r = 0; r < TheResults[i].Results.Length; r++)
            {
                TheResults[i].symbolControl.StopTarget = TheResults[i].Results[r];

                    TheResults[i].symbolControl.AssignSymbol(TheResults[i].symbolControl.ActiveContainers[r],
                  TheResults[i].Results[r]);
            }
        }
    }
    public void RandomizeResults()
    {
        if (IsTest)
            return;
        for (int i = 0; i < TheResults.Length; i++)
        {
            for (int r = 0; r < TheResults[i].Results.Length; r++)
            {
                TheResults[i].Results[r] =TheResults[i].symbolControl.GetRandomSymbol();
            }
        }
    }
}
