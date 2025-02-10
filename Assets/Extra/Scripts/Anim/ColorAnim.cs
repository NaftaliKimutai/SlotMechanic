using UnityEngine;
using UnityEngine.UI;
using TMPro;
[System.Serializable]
public class Color_animdata
{
    public float TargetTime = 0.1f;
    public float Speed = 2f;
    public Color TargetColor=Color.white;
}
public class ColorAnim : MonoBehaviour
{
    float timestamp;
    public int Target;
    public Color_animdata[] TargetData;
    public Image TheImg;
    public TMP_Text TheText;
    Color TheColor;
    float Speed;
    private void OnEnable()
    {
        Target = 0;
        timestamp = 0;
        TheColor = TargetData[Target].TargetColor;
        Speed = TargetData[Target].Speed;
        if (TheImg)
        {
            TheImg.color =  TheColor;
        }
        if (TheText)
        {
            TheText.color = TheColor;
        }
    }
    void Update()
    {
        if (timestamp < Time.time)
        {
            timestamp = Time.time+TargetData[Target].TargetTime;
            TheColor = TargetData[Target].TargetColor;

            Speed = TargetData[Target].Speed;
            Target += 1;
            if (Target > TargetData.Length - 1)
            {
                Target = 0;
            }
        }
        if (TheImg)
        {
            TheImg.color = Vector4.Lerp(TheImg.color, TheColor, Speed * Time.deltaTime);
        }
        if (TheText)
        {
            TheText.color = Vector4.Lerp(TheText.color, TheColor, Speed * Time.deltaTime);
        }
    }
}
