using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTexture : MonoBehaviour
{
    
    public Material material;

    //public GameObject mazeGameObject;
    
    Texture2D texture;

    private System.Random rnd;
    
    bool toSet = false; 
    bool toGen = true;
    
    // Start is called before the first frame update
    void Start()
    {
        texture = new Texture2D(128, 128);
        texture.filterMode = FilterMode.Point;
        rnd = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        if (toGen)
        {
            for (int i = 0; i < texture.width; i++)
            {
                for (int j = 0; j < texture.height; j++)
                {
                    if (rnd.Next() % 2 == 0)
                    {
                        texture.SetPixel(i, j, Color.black);
                    }
                    else
                    {
                        texture.SetPixel(i, j, Color.red);
                    }
                }
            }
            texture.Apply();
            toSet = true;
            toGen = false;
        }

        if (toSet)
        {
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
            toSet = false;
        }
    }
}
