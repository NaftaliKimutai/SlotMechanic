using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
public class FixTextures : MonoBehaviour
{
    public Transform[] AllObjs;
    public string[] TextureNames;
    public string[] ThePath;
    public string[]SpritePath;
    public string[] TheGuid;
    [ContextMenu("GetAllObjs")]
   public void GetAllObjs()
    {
        AllObjs = GetComponentsInChildren<Transform>(true);
        TextureNames = new string[AllObjs.Length];
        ThePath = new string[AllObjs.Length];
        SpritePath = new string[AllObjs.Length];
        for(int i = 0; i < AllObjs.Length; i++)
        {
            if (AllObjs[i].GetComponent<Image>()&&AllObjs[i].GetComponent<Image>().sprite) {
                TextureNames[i] = AllObjs[i].GetComponent<Image>().sprite.name;
                ThePath[i]=AssetDatabase.GetAssetPath(AllObjs[i].GetComponent<Image>().sprite);
                string[] tockens = ThePath[i].Split("/");
                SpritePath[i] = "";
                for(int r = 0; r < tockens.Length; r++)
                {
                    if (r == tockens.Length-1 )
                    {
                        SpritePath[i] = SpritePath[i] + "/" + TextureNames[i];

                    }
                    else
                    {
                        if (r == 0)
                        {
                            SpritePath[i] = tockens[r];

                        }
                        else
                        {
                            SpritePath[i] = SpritePath[i] + "/" + tockens[r];

                        }

                    }
                }
               // ThePath[i] = ThePath[i] + "/" + TextureNames[i];
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
                //string ThePath;
                /*for (int r = 0; r < tockens.Length; r++)
                {
                    if (r == tockens.Length - 1)
                    {
                        ThePath[i] = ThePath[i] + "/" + TextureNames[i];

                    }
                    else
                    {
                        ThePath[i] = ThePath[i] + "/" + tockens[r];

                    }
                }*/
                Sprite thesprite = (Sprite)AssetDatabase.LoadAssetAtPath(SpritePath[i], typeof(Sprite));
                Texture2D t = (Texture2D)AssetDatabase.LoadAssetAtPath(ThePath[i], typeof(Texture2D));
                if (thesprite)
                {
                    Debug.Log(thesprite.name);
                    //AllObjs[i].GetComponent<Image>().sprite = ConvertToSpriteExtensiton.ConvertToSprite(t);
                    AllObjs[i].GetComponent<Image>().sprite = thesprite;
                }
                //AssetDatabase.fi
                //AssetDatabase.FindAssets(TextureNames[i]);
                //TextureNames[i] = AllObjs[i].GetComponent<Image>().sprite.name;
            }

        }
    }
    
}
public static class ConvertToSpriteExtensiton
{
    public static Sprite ConvertToSprite(this Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }
}
