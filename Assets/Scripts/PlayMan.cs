using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayMan : MonoBehaviour
{
    public SymbolControl[] spinningcontrols;
    public TheContainer[] AciveContainers;
    public bool ReceivedSymbols;
    public int Respins;
    public bool IsRespin;
    public float BetAmount;
    public float WinAmount;
    public TMP_Text WinText;
    private void Start()
    {
        ResetPlay();
    }

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
                    CalculateWinnings();
                }
            }
        }
    }
    public void ResetPlay()
    {
        ReceivedSymbols = false;
        WinAmount = 0;
        WinText.text =WinAmount.ToString("n2");
    }
    public void Play()
    {
        ResetPlay();

        for(int i = 0; i < spinningcontrols.Length; i++)
        {
            //spinningcontrols[i].Randomize();
            if (IsRespin && 1 == 3)
            {

            }
            else
            {
                spinningcontrols[i].Spin();
            }
        }
        if (GameManager.Instance.IsDemoMode)
        {
            GameManager.Instance.resultMan.RandomizeResults();
            ReceivedSymbols = true;
        }
    }
    public void UseRespin()
    {
        Respins -= 1;
        if (Respins <= 0)
        {
            Respins = 0;
            IsRespin = false;
        }
    }
    public void UpdateWin(float Amount)
    {
        WinAmount = Amount;
        WinText.text = Amount.ToString("n2");
    }

    void CalculateWinnings()
    {
        float thewin = BetAmount * 5;
        Symbol[] symbols = new Symbol[spinningcontrols.Length];
        for (int i = 0; i < spinningcontrols.Length; i++)
        {
            symbols[i] = spinningcontrols[i].StopTarget;
        }

        float ThePay = 0;

        if (symbols[0] == Symbol.Tripple_7 && symbols[1] == Symbol.Tripple_7 && symbols[2] == Symbol.Tripple_7)
        {
            ThePay = 1000;
        }
        else if (symbols[0] == Symbol.Double_7 && symbols[1] == Symbol.Double_7 && symbols[2] == Symbol.Double_7)
        {
            ThePay = 200;
        }
        else if (symbols[0] == Symbol.Single_7 && symbols[1] == Symbol.Single_7 && symbols[2] == Symbol.Single_7)
        {
            ThePay = 100;
        }
        else if (symbols[0] == Symbol.Double_Bar && symbols[1] == Symbol.Double_Bar && symbols[2] == Symbol.Double_Bar)
        {
            ThePay = 40;
        }
        else if (symbols[0] == Symbol.Single_Bar && symbols[1] == Symbol.Single_Bar && symbols[2] == Symbol.Single_Bar)
        {
            ThePay = 20;
        }
        else
        {
            bool all7s = true;
            for (int i = 0; i < symbols.Length - 1; i++)
            {
                if (symbols[i] == Symbol.Single_7 || symbols[i] == Symbol.Double_7 || symbols[i] == Symbol.Tripple_7)
                {
                }
                else
                {
                    all7s = false;
                    break;
                }
            }

            bool allbars = true;
            for (int i = 0; i < symbols.Length - 1; i++)
            {
                if (symbols[i] == Symbol.Single_Bar || symbols[i] == Symbol.Double_Bar)
                {
                }
                else
                {
                    allbars = false;
                    break;
                }
            }

            bool allSymbols = true;
            for (int i = 0; i < symbols.Length - 1; i++)
            {
                if (symbols[i] == Symbol.None)
                {
                    allSymbols = false;
                    break;
                }

            }

            if (all7s)
            {
                ThePay = 40;


            }
            else if (allbars)
            {
                ThePay = 10;

            }
            else if (allSymbols)
            {
                ThePay = 4;
            }
        }

        float Multiplier = 1;
        float ExtraPayout = 1;
        int _Respins = 0;
        Debug.Log(symbols[3]);
        if (symbols[3] == Symbol.Win_2x)
        {
            Multiplier = 2;
            if (ThePay == 0)
            {
            }
        }
        else if (symbols[3] == Symbol.Win_5x)
        {
            Multiplier = 5;
            if (ThePay == 0)
            {
            }
        }
        else if (symbols[3] == Symbol.Win_10x)
        {
            Multiplier = 10;
            if (ThePay == 0)
            {
            }
        }
        else if (symbols[3] == Symbol.Win_Extra_1)
        {
            ExtraPayout = 10;
            if (ThePay == 0)
            {
            }
        }
        else if (symbols[3] == Symbol.Win_Extra_2)
        {
            ExtraPayout = 100;
            if (ThePay == 0)
            {
            }
        }
        else if (symbols[3] == Symbol.Respin)
        {
            _Respins = Random.Range(1, 5);
            if (ThePay == 0)
            {
            }
        }


        if (_Respins > 0 )
        {
            if (!IsRespin)
            {
                IsRespin = true;
                Respins += _Respins;
            }


        }

        if (ThePay > 0)
        {
            ThePay += ExtraPayout;
        }

        thewin = ((BetAmount * ThePay) / 3) * Multiplier;
        if (GameManager.Instance.IsDemoMode)
        {
            UpdateWin(thewin);
        }

        if (WinAmount > 0)
        {
            GameManager.Instance.placeBet.CashOut();


        }
        
      
        Debug.Log("End");

    }





}
