using UnityEngine;

public class PlayMan : MonoBehaviour
{
    public SymbolControl[] spinningcontrols;
    public TheContainer[] AciveContainers;
    bool ReceivedSymbols;
    public GameObject[] SymbolsPref;
  

    void Update()
    {
        if (GameManager.Instance.IsGameStarted)
        {
            if (!GameManager.Instance.IsGameOver)
            {
                bool isspin = false;
                for (int i = 0; i < spinningcontrols.Length; i++)
                {
                    if (spinningcontrols[i].spintime < 0 && ReceivedSymbols)
                    {
                        if (i == 0)
                        {
                            if (!spinningcontrols[i].CanStop)
                            {
                                /* if (i + 1 < symbolControls.Length - 1)
                                 {
                                     symbolControls[i + 1].spintime = 0.3f;
                                 }
                                 for (int r = 1; r < symbolControls.Length; r++)
                                 {
                                     symbolControls[r].spintime = 0.3f;
                                 }*/
                                for (int r = i; r < spinningcontrols.Length; r++)
                                {
                                    if (spinningcontrols[i].IsTurbo)
                                    {
                                        spinningcontrols[r].spintime = 0;

                                    }
                                    else
                                    {
                                        spinningcontrols[r].spintime = 0.3f;

                                    }
                                }
                                GameManager.Instance.resultMan.AssignResults();
                                spinningcontrols[i].ActivateCanStop();
                            }
                            //spinningcontrols[i].CanStop = true;


                        }
                        else
                        {
                            if (!spinningcontrols[i - 1].IsSpinning)
                            {
                                if (!spinningcontrols[i].CanStop)
                                {
                                    /*if (i + 1 < symbolControls.Length - 1)
                                    {
                                        if (GameManager.Instance.bottomHolder.IsTurbo)
                                        {
                                            symbolControls[i + 1].spintime = 0;

                                        }
                                        else
                                        {
                                            symbolControls[i + 1].spintime = 1f;

                                        }
                                    }*/
                                    for (int r = i; r < spinningcontrols.Length; r++)
                                    {
                                        if (spinningcontrols[i].IsTurbo)
                                        {
                                            spinningcontrols[r].spintime = 0;

                                        }
                                        else
                                        {
                                            spinningcontrols[r].spintime = 0.3f;

                                        }
                                    }
                                    int rand = Random.Range(0, 3);
                                    if (rand != 2)
                                    {
                                        spinningcontrols[i].IsFakeStop = true;
                                    }
                                    spinningcontrols[i].ActivateCanStop();
                                }
                                
                               // spinningcontrols[i].CanStop = true;

                            }
                        }
                    }
                }
            }
        }
    }
    public void Play()
    {
        ReceivedSymbols = true;
        for(int i = 0; i < spinningcontrols.Length; i++)
        {
            //spinningcontrols[i].Randomize();
            spinningcontrols[i].Spin();
        }
    }

    public GameObject GetSymbol()
    {
        return SymbolsPref[Random.Range(0, SymbolsPref.Length)];
    }
    public void AssignSymbol(TheContainer Which,Symbol thesymbol)
    {
       for(int i = 0; i < SymbolsPref.Length; i++)
        {
            if(thesymbol == SymbolsPref[i].GetComponent<TheSymbol>()._Symbol)
            {
                Which.CreateSymbol(SymbolsPref[i]);
            }
        }
       

    }
}
