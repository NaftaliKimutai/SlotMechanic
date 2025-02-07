using UnityEngine;

public class TheContainer : MonoBehaviour
{
    public Symbol ActiveSymbol;
    public GameObject ShowObj;
    public GameObject Spawned;
    private void Start()
    {
        CreateSymbol(GameManager.Instance.playMan.GetSymbol());
    }
    public void CreateSymbol(GameObject ThePref)
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
