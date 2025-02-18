using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class FixTextures : MonoBehaviour
{
    public Transform[] AllObjs;
    public string[] TextureNames;
    public string[] ThePath;
    [ContextMenu("GetAllObjs")]
   public void GetAllObjs()
    {
        AllObjs = GetComponentsInChildren<Transform>(true);
        TextureNames = new string[AllObjs.Length];
        ThePath = new string[AllObjs.Length];
        for(int i = 0; i < AllObjs.Length; i++)
        {
            if (AllObjs[i].GetComponent<Image>()&&AllObjs[i].GetComponent<Image>().sprite) {
                TextureNames[i] = AllObjs[i].GetComponent<Image>().sprite.name;
            }

        }
    }
    [ContextMenu("ApplyTextures")]
    void ApplyTexture()
    {
        for (int i = 0; i < AllObjs.Length; i++)
        {
            if (AllObjs[i].GetComponent<Image>())
            {
                TextureNames[i] = AllObjs[i].GetComponent<Image>().sprite.name;
            }

        }
    }
}
