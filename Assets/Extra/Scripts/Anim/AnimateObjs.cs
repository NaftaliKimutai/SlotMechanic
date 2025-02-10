using UnityEngine;

public class AnimateObjs : MonoBehaviour
{
    public float Rate = 0.1f;
    public GameObject[] Objs;
    float Timestamp;
    int Active;
    void Update()
    {
        if (Timestamp < Time.time)
        {
            for (int i = 0; i < Objs.Length; i++)
            {
                if (i == Active)
                {
                    Objs[i].SetActive(true);
                }
                else
                {
                    Objs[i].SetActive(false);
                }
            }
            Timestamp = Time.time+Rate;
            Active += 1;
            if (Active > Objs.Length - 1)
            {
                Active = 0;
            }
        }
    
    }
}
