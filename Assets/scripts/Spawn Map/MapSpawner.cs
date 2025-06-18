using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    public MapDataManager mapDataManager; 
    public GameObject GrassPrefab;
    public GameObject GrassWithTreePrefab;
    public GameObject water;

    void Start()
    {
        water.SetActive(true);
        int levelIndex = LevelManager.Instance?.selectedLevelIndex ?? 0;
        MapData currentMap = mapDataManager.levels[levelIndex];
        SpawnMapFromData(currentMap);
    }

    public void SpawnMapFromData(MapData mapData)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var block in mapData.blocks)
        {
            GameObject prefab = null;
            switch (block.blockType)
            {
                case BlockType.Grass:
                    prefab = GrassPrefab;
                    break;
                case BlockType.GrassWithTree:
                    prefab = GrassWithTreePrefab;
                    break;
            }

            if (prefab != null)
            {
                Instantiate(prefab, block.position, Quaternion.identity, this.transform);
            }
        }
    }
}
