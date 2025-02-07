using UnityEngine;
using UnityEngine.UI;
public class TheSymbol : MonoBehaviour
{
    public Symbol _Symbol;
    public Image TheImg;
    public Animator Anim;
    private void Start()
    {
        ResetAnim();
    }
    public void ShowAnim()
    {
        if (TheImg)
        {
            TheImg.enabled = false;
        }
        if (Anim)
        {
            Anim.gameObject.SetActive(true);
        }
    }
    public void ResetAnim()
    {
        if (TheImg)
        {
            TheImg.enabled = true;
        }
        if (Anim)
        {
            Anim.gameObject.SetActive(false);
        }
    }
}
