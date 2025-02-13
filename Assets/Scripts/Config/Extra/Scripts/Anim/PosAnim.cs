using UnityEngine;
[System.Serializable]
public class Posanimdata
{
    public float TargetTime = 0.1f;
    public float Speed = 2f;
    public Vector3 TargetPos = Vector3.one;
    public bool UseScale;
    public float ScaleSpeed = 2f;
    public Vector3 TargetScale = Vector3.one;
}
public class PosAnim : MonoBehaviour
{
    public bool IsLoop = true;
    float timestamp;
    public int Target;
    public Posanimdata[] TargetData;
    Vector3 ThePos;
    float Speed;
    public Transform TargetTrans;
    private void OnEnable()
    {
        ResetAnim();
    }
    public void ResetAnim()
    {
        Target = 0;
        ThePos = TargetData[Target].TargetPos;
        timestamp = Time.time + TargetData[Target].TargetTime;
        TargetTrans.localPosition = ThePos;
        if (TargetData[Target].UseScale)
        {
            TargetTrans.localScale=  TargetData[Target].TargetScale;
        }
    }
    void Update()
    {
        if (timestamp < Time.time)
        {
            if (Target == TargetData.Length - 1 && !IsLoop)
            {

            }
            else
            {
                Target += 1;
            }
            if (Target > TargetData.Length - 1 && IsLoop)
            {
                Target = 0;
                ResetAnim();
            }
            else
            {
                timestamp = Time.time + TargetData[Target].TargetTime;
                ThePos = TargetData[Target].TargetPos;
                Speed = TargetData[Target].Speed;
            }

          
        }
        TargetTrans.localPosition = Vector3.Lerp(TargetTrans.localPosition, ThePos, Speed * Time.deltaTime);
        if (TargetData[Target].UseScale)
        {
            TargetTrans.localScale = Vector3.Slerp(TargetTrans.localScale, TargetData[Target].TargetScale, TargetData[Target].ScaleSpeed*Time.deltaTime);
        }
    }

}
