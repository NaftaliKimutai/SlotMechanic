using UnityEngine;
using TMPro;
public class TotalCash : MonoBehaviour
{
    public TMP_Text Text;
    private void Update()
    {
        Text.text=PlayerPrefs.GetFloat("TotalCash").ToString("n2");
    }
}
