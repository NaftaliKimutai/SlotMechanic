using UnityEngine;

public class TheContainer : MonoBehaviour
{
    public GameObject ShowObj;
    public GameObject Spawned;
    private void Start()
    {
        CreateSymbol(PlayController.instance.GetSymbol());
    }
    void CreateSymbol(GameObject ThePref)
    {
        if (Spawned)
        {
            Destroy(Spawned);
        }
        if (ThePref)
        {
            ShowObj.SetActive(false);
        }
      Spawned= Instantiate(ThePref, transform);

    }
}
