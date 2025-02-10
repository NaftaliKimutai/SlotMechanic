using UnityEngine;

public class CashMan : MonoBehaviour
{
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            AddCash(100);
        }
    }
    public void AddCash(float Amount)
    {
        PlayerPrefs.SetFloat("TotalCash", PlayerPrefs.GetFloat("TotalCash") + Amount);
    }
    public void RemoveCash(float Amount)
    {
        PlayerPrefs.SetFloat("TotalCash", PlayerPrefs.GetFloat("TotalCash") - Amount);
    }
    public void UpdateCashAmout(float Amount)
    {
        PlayerPrefs.SetFloat("TotalCash", Amount);
    }
}
