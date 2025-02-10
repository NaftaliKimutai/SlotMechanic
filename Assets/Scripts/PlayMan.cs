using UnityEngine;

public class PlayMan : MonoBehaviour
{
    public SymbolControl[] spinningcontrols;
    public TheContainer[] AciveContainers;
    public bool ReceivedSymbols;

    void Update()
    {
        if (GameManager.Instance.IsGameStarted)
        {
            if (!GameManager.Instance.IsGameOver)
            {
                bool isspin = false;
                for (int i = 0; i < spinningcontrols.Length; i++)
                {
                    if (!spinningcontrols[i].FullStop||spinningcontrols[i].IsSpinning)
                    {
                        isspin = true;
                    }
                    if (spinningcontrols[i].spintime < 0 && ReceivedSymbols)
                    {
                        if (i == 0)
                        {
                            if (!spinningcontrols[i].CanStop)
                            {
                              
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


                        }
                        else
                        {
                            if (!spinningcontrols[i - 1].IsSpinning)
                            {
                                if (!spinningcontrols[i].CanStop)
                                {
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
                            }
                        }
                    }
                }

                if (!isspin)
                {
                    GameManager.Instance.GameOver();
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

    
   
   
}
