using UnityEngine;
using System.Collections.Generic;
public enum Symbol
{
    None,
    Single_Bar,
    Double_Bar,
    Single_7,
    Double_7,
    Tripple_7,


    Respin,
    Win_2x,
    Win_5x,
    Win_10x,
    Win_Extra_1,
    Win_Extra_2,
    Container
}
public class SymbolControl : MonoBehaviour
{
    public GameObject[] SymbolsPref;
    public TheContainer[] ActiveContainers;
    public bool IsTest;
    bool emptyavailable;
    public Symbol TestSymbol;
    public bool IsFakeStop;
    public bool IsTurbo;
    public List<Symbol> ActiveSymbols = new List<Symbol>();
    public TheSymbol ActiveSymbol;
    public int CenterTarget;
    public Symbol StopTarget;
    public float Speed = 200;
    public float Rate = 0.5f;
    float timestamp;
    int Active = 0;
    public TheSymbol[] Symbols;
    public int[] Targets;
    public Vector3[] TargetsPos;
    public bool IsSpinning;
    public bool CanStop = false;
    public bool FullStop;
    public float spintime;
    bool wasspinning;
    float offset;
    float offsetT;
    public AnimationCurve StoppingCurve;
    public AnimationCurve FakeStoppingCurve;
    float shaketimestamp;
    float shakerange;
    float fakestimesoundtimestamp;
    private void Start()
    {
        StopTarget = Symbols[CenterTarget]._Symbol;
        Targets = new int[Symbols.Length];
        TargetsPos = new Vector3[Symbols.Length];
        for(int i = 0; i < Symbols.Length; i++)
        {
            if (Symbols[i]._Symbol == Symbol.None)
            {
                emptyavailable = true;
            }
            Targets[i] = i;
            TargetsPos[i] = Symbols[i].transform.localPosition;
            if (!ActiveSymbols.Contains(Symbols[i]._Symbol))
            {
                ActiveSymbols.Add(Symbols[i]._Symbol);
            }

        }
        int step = Random.Range(4, 20);
        while (step > 0)
        {
            bool notnone = false;
            for (int i = 0; i < Targets.Length; i++)
            {
                int which = Targets[i];
                which += 1;
                if (which > Targets.Length - 1)
                {
                    which = 0;
                }
                Targets[i] = which;
                Symbols[i].transform.localPosition = TargetsPos[which];
                if (Targets[i] == CenterTarget)
                {
                    if (Symbols[i]._Symbol != Symbol.None)
                    {
                        notnone = true;
                    }
                }
            }
            if (notnone)
            {
                step -= 1;

            }
        }
        
    }
    public GameObject GetSymbol()
    {
        return SymbolsPref[Random.Range(0, SymbolsPref.Length)];
    }
    public void AssignSymbol(TheContainer Which, Symbol thesymbol)
    {
        for (int i = 0; i < SymbolsPref.Length; i++)
        {
            if (thesymbol == SymbolsPref[i].GetComponent<TheSymbol>()._Symbol)
            {
                Which.CreateSymbol(SymbolsPref[i]);
            }
        }


    }
    public Symbol GetRandomSymbol()
    {
        bool emptyavailable = false;
        for (int i = 0; i < SymbolsPref.Length; i++)
        {
            if (SymbolsPref[i].GetComponent<TheSymbol>()._Symbol == Symbol.None)
            {
                emptyavailable = true;
                break;
            }
        }
        for (int i = 0; i < ActiveSymbols.Count; i++)
        {
            if (ActiveSymbols[i] == Symbol.None)
            {
                emptyavailable = true;
                break;
            }
        }
        if (emptyavailable)
        {
            int rand = Random.Range(0, 10);
            if (rand != 5)
            {
                return SymbolsPref[Random.Range(0, SymbolsPref.Length)].GetComponent<TheSymbol>()._Symbol;

            }
            else
            {
                return Symbol.None;
            }
        }
        else
        {
            return SymbolsPref[Random.Range(0, SymbolsPref.Length)].GetComponent<TheSymbol>()._Symbol;
        }
    }
    [ContextMenu("Randomize")]
    public void Randomize()
    {
        if (IsTest)
        {
            StopTarget = TestSymbol;
           // TestSymbol = Symbol.None;
        }
        else {
            IsFakeStop = false;
            if (emptyavailable)
            {
                int rand = Random.Range(0, 10);
                if (rand != 5)
                {
                    StopTarget = ActiveSymbols[Random.Range(0, ActiveSymbols.Count)];

                }
                else
                {
                    StopTarget = Symbol.None;
                }
            }
            else
            {
                StopTarget = ActiveSymbols[Random.Range(0, ActiveSymbols.Count)];
            }

        }

    }
   
    [ContextMenu("Spin")]
    public void Spin()
    {
        FullStop = false;
        offsetT = 0;
        offset = 0;
        wasspinning = true;
        CanStop = false;
        IsSpinning = true;
        if (IsTurbo)
        {
            spintime = 0.5f;

        }
        else
        {
            spintime = 1.5f;


        }
        if (ActiveSymbol)
        {
            ActiveSymbol.ResetAnim();
        }
    }
    [ContextMenu("StopSpin")]
    public void StopSpin()
    {
        IsSpinning = false;
        for (int i = 0; i < Targets.Length; i++)
        {
            Symbols[i].transform.localPosition = TargetsPos[Targets[i]];

        }

       
      
    }
    public void AddShake(float Amount=0.1f,float Range=4)
    {
        shaketimestamp = Time.time + Amount;
        shakerange = Range;
    }
    Vector3 Shake( )
    {
        if (shaketimestamp > Time.time)
        {
            float thex = Random.Range(-shakerange, shakerange);
            return new Vector3(0, thex, 0);
        }
        return Vector3.zero;
    }
    private void Update()
    {
        if (!IsSpinning)
        {
            if (wasspinning&&!IsTurbo)
            {
                for (int i = 0; i < Targets.Length; i++)
                {
                    Symbols[i].transform.localPosition = TargetsPos[Targets[i]] + new Vector3(0, -offset*50, 0)+Shake();

                }
                if (offsetT < 1)
                {
                    if (IsFakeStop)
                    {
                        offsetT += Time.deltaTime;

                        offset = FakeStoppingCurve.Evaluate(offsetT);
                        if (offsetT < 0.5f && offsetT > 0.2f)
                        {
                            AddShake();
                            if (fakestimesoundtimestamp < Time.time)
                            {
                                fakestimesoundtimestamp = Time.time + 5f;
                            }
                        }
                    }
                    else
                    {
                        offsetT += Time.deltaTime * 2;

                        offset = StoppingCurve.Evaluate(offsetT);
                    }
                  
                }
                else
                {
                    offset = 0;
                    if (!FullStop)
                    {
                    }
                    FullStop = true;
                }

            }
            else
            {
                FullStop = true;
            }
            return;
        }
        int Current = 0;

        if (timestamp < Time.time)
        {
            timestamp = Time.time + Rate;
           for(int i = 0; i < Targets.Length; i++)
            {
                int which = Targets[i];
                which += 1;
                if (which > Targets.Length - 1)
                {
                    which = 0;
                    Symbols[i].transform.localPosition = TargetsPos[which];
                }
                Targets[i] = which;
                if (Targets[i] == CenterTarget)
                {
                    ActiveSymbol = Symbols[i];
                    Current = i;
                }
               // Symbols[i].transform.position = TargetsPos[which];
            }
        }
        for(int i = 0; i < Symbols.Length; i++)
        {
            Symbols[i].transform.localPosition = Vector3.MoveTowards(
                Symbols[i].transform.localPosition, TargetsPos[Targets[i]],Speed * Time.deltaTime);
        }
        spintime -= Time.deltaTime;
        if (spintime < 0&&CanStop)
        {
            if (CenterTarget == Current||ActiveSymbol._Symbol==Symbol.None&&StopTarget==Symbol.None)
            {
                StopSpin();
            }

        }


    }
    public void ActivateCanStop()
    {
      
        CanStop = true;
    }
}
