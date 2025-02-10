using UnityEngine;
using TMPro;
using System.Collections;

public class CashOutUi : MonoBehaviour
{
    //public TMP_Text Multiplier_Text;
    public TMP_Text Win_Text;
    public float TheWin;
    public AnimationCurve ScaleUpAnimation;
    public AnimationCurve ScaleDownAnimation;
    public Color FinalColor;

    public void ShowCashOut(string Multiplier,float Win)
    {
        TheWin = Win;
        
        gameObject.SetActive(true);
        StopAllCoroutines();

      StartCoroutine(UpdateAnim(Win));
    }
    public void FinalShow()
    {

    }
    public void Close()
    {
        gameObject.SetActive(false);

    }
    IEnumerator UpdateAnim(float target)
    {
        float TheScale = 0;
        float scaleoffset = 0;
        float temp=0;
        float amount = 0;
        bool isdone = false;
        Win_Text.color = Color.white;
        float TimeTaken = 0;
        while (!isdone)
        {
            TimeTaken += Time.deltaTime;
            amount = Mathf.Lerp(amount, target, 2 * Time.deltaTime);
            Win_Text.text = amount.ToString("n2");
            if (target - amount < 1&&TimeTaken>1)
            {
                isdone = true;
            }
            temp =(amount/target);
            scaleoffset = ScaleUpAnimation.Evaluate(temp);
            transform.localScale = Vector3.one * (TheScale + scaleoffset);
            yield return new WaitForSeconds(0.001f);
        }
        Win_Text.text =target.ToString("n2");
        yield return new WaitForSeconds(1);
        
        float thet = 0;
       isdone = false;
        while (!isdone)
        {
            Win_Text.color =Vector4.Lerp(Win_Text.color,FinalColor,2*Time.deltaTime);
            thet += Time.deltaTime*2;
            if (thet>1)
            {
                isdone = true;
            }
            temp = (thet);
            scaleoffset = ScaleDownAnimation.Evaluate(temp);
            transform.localScale = Vector3.one * (scaleoffset);
            yield return new WaitForSeconds(0.001f);
        }
        Close();

    }
}
