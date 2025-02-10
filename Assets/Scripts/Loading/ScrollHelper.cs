using UnityEngine;
using UnityEngine.UI;
public class ScrollHelper : MonoBehaviour
{
    public int Which;
    public Transform[] Targets;
    public GameObject[] RightArrow;
    public GameObject[] LeftArrow;
    Vector2 startpos;
    bool ispress;
    public Color DisabledColor;
    public Color ActiveColor;
    public Image[] KnobImages;
    float ScrollTimestamp;
    float autoscrolltimestamp;
    private void Start()
    {
       // GetComponent<ScrollRectSnap>().SnapTo(Targets[Which].GetComponent<RectTransform>(), 0);
        autoscrolltimestamp = Time.time + 5f;
        Refresh();
    }
    private void Update()
    {
        if (autoscrolltimestamp < Time.time)
        {
            Next();
            autoscrolltimestamp = Time.time + 5f;
        }
        if (Input.GetMouseButton(0)&&ScrollTimestamp<Time.time)
        {
            autoscrolltimestamp = Time.time + 5f;
            if (!ispress)
            {
                ispress = true;
                startpos = Input.mousePosition;
            }
            float Diff = Input.mousePosition.x - startpos.x;
           // Debug.Log(Diff);
            if (Diff < -100)
            {
                Next();
                ScrollTimestamp = Time.time + 0.3f;
                ispress = false;
            }
            if (Diff > 100)
            {
                Prev();
                ScrollTimestamp = Time.time + 0.3f;
                ispress = false;
            }

        }
        else
        {
            ispress = false;
        }
    }
    [ContextMenu("Next")]
    public void Next()
    {
        Which += 1;
        if (Which > Targets.Length - 1)
        {
            Which = Targets.Length - 1;
        }
        Refresh();

    }
    [ContextMenu("Prev")]
    public void Prev()
    {
        Which -= 1;
        if (Which < 0)
        {
            Which = 0;
        }
        Refresh();

    }
    [ContextMenu("Refresh")]
    public void Refresh()
    {
        GetComponent<ScrollRectSnap>().SnapTo(Targets[Which].GetComponent<RectTransform>(),0);
        if (Which == Targets.Length - 1)
        {
            ToggleObjArray(RightArrow,false);
          //  RightArrow.SetActive(false);
        }
        else
        {
            ToggleObjArray(RightArrow, true);
           // RightArrow.SetActive(true);
        }
       if (Which == 0)
        {
            ToggleObjArray(LeftArrow, false);
           // LeftArrow.SetActive(false);
        }
        else
        {
            ToggleObjArray(LeftArrow, true);
           // LeftArrow.SetActive(true);
        }
        RefreshKnob();
    }
    void RefreshKnob()
    {
        for (int i = 0; i < KnobImages.Length; i++)
        {
            if (i == Which)
            {
                KnobImages[i].color = ActiveColor;
            }
            else
            {
                KnobImages[i].color = DisabledColor;
            }
        }
    }
    public void ToggleObjArray(GameObject[] Which, bool IsActive)
    {
        for (int i = 0; i < Which.Length; i++)
        {
            Which[i].SetActive(IsActive);
        }
    }
}
