
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public ColorToPrefab[] colorMappings;
     
    // Start is called before the first frame update
    public void Init()
    {
        Debug.Log(" LevelGeneratorinited");
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    private void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);
        //Debug.Log(pixelColor);
        if (pixelColor.a == 0)
        {
            return;
        }
        foreach (ColorToPrefab colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                Vector2 placement = new Vector2(x, y);
                Instantiate(colorMapping.prefab, placement, Quaternion.identity, transform);
               // Debug.Log(colorMapping.prefab.name + colorMapping.prefab.transform.position);              
            }
        }
    }

}
