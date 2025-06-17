using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    public MapData mapData;  
    public GameObject soilPrefab;      
    public GameObject soilWithTreePrefab; 

    void Start()
    {
        SpawnMap();
    }

    void SpawnMap()
    {
        
        foreach (var block in mapData.blocks)
        {
            GameObject blockPrefab = null;

            // Chọn prefab phù hợp dựa trên loại block
            switch (block.blockType)
            {
                case BlockType.Grass:
                    blockPrefab = soilPrefab;
                    break;
                case BlockType.GrassWithTree:
                    blockPrefab = soilWithTreePrefab;
                    break;
            }

            
            if (blockPrefab != null)
            {
                Instantiate(blockPrefab, block.position, Quaternion.identity);
            }
        }
    }
}
