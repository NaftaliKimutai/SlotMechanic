using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 Dir = new Vector3(0, 0, 1);
    public float RotSpeed = 20;
    void Update()
    {
        transform.Rotate(Dir * RotSpeed * Time.deltaTime);
    }
}
