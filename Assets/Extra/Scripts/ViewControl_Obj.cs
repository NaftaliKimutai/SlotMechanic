using UnityEngine;

public class ViewControl_Obj : MonoBehaviour
{
    public Transform TheObj;
    public Transform[] TheObjs;
    public Transform LandScape;
    public Transform Potrait;
    public void SetPotrait()
    {
        LandScape.gameObject.SetActive(false);
        Potrait.gameObject.SetActive(true);
        if (TheObj)
        {
            TheObj.transform.SetParent(Potrait);
            TheObj.transform.SetAsFirstSibling();
            TheObj.transform.localPosition = Vector3.zero;
            TheObj.transform.localScale = Vector3.one;
        }
        SetArray(Potrait);

    }
    public void SetLandScape()
    {
        LandScape.gameObject.SetActive(true);
        Potrait.gameObject.SetActive(false);
        if (TheObj)
        {
            TheObj.transform.SetParent(LandScape);
            TheObj.transform.SetAsFirstSibling();
            TheObj.transform.localPosition = Vector3.zero;
            TheObj.transform.localScale = Vector3.one;
        }
        SetArray(LandScape);
    }
    void SetArray(Transform TheParent)
    {
        for(int i = 0; i < TheObjs.Length; i++)
        {
            TheObjs[i].transform.SetParent(TheParent);
            TheObjs[i].transform.SetAsFirstSibling();
            TheObjs[i].transform.localPosition = Vector3.zero;
            TheObjs[i].transform.localScale = Vector3.one;
        }
    }
    private void OnEnable()
    {
        SetUp();
    }
    void SetUp()
    {
        Refresh();

    }
    public void Refresh()
    {
        if (FindFirstObjectByType<ViewMan>().IsLandScape)
        {
            SetLandScape();
        }
        else
        {
            SetPotrait();
        }
    }
}
