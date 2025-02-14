using UnityEngine;
[System.Serializable]
public class animdata
{
    public float TargetTime = 0.1f;
    public float Speed = 2f;
    public Vector3 TargetScale = Vector3.one;
}
public class ScaleAnim : MonoBehaviour
{
    public bool IsLoop = true;
    float timestamp;
    public int Target;
    public animdata[] TargetData;
    Vector3 TheScale;
    float Speed;
    private void OnEnable()
    {
        ResetAnim();
    }
    public void ResetAnim()
    {
        Target = 0;
        TheScale = TargetData[Target].TargetScale;
        timestamp = Time.time + TargetData[Target].TargetTime;
        transform.localScale = TheScale;
    }
    void Update()
    {
        if (timestamp < Time.time)
        {
            if (Target == TargetData.Length)
            {
                if (IsLoop)
                {
                    ResetAnim();
                }

            }
            else
            {

                timestamp = Time.time + TargetData[Target].TargetTime;
                TheScale = TargetData[Target].TargetScale;
                Speed = TargetData[Target].Speed;
                Target += 1;

            }

        }
        transform.localScale = Vector3.Lerp(transform.localScale, TheScale, Speed * Time.deltaTime);
    }

}
