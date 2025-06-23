using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    public MapDataManager mapDataManager; 
    public GameObject GrassPrefab;
    public GameObject GrassWithTreePrefab;
    public GameObject water;
    public GameObject PlayerPrefab;
    public GameObject StickPrefab;  
    public GameObject GrassWithPalmPrefab;
    public GameObject HomeBtn;
    void Start()
    {
        water.SetActive(true);  
        HomeBtn.SetActive(true);
        int levelIndex = LevelManager.Instance?.selectedLevelIndex ?? 0;
        //int levelIndex = 0;
        //LevelSelectorInGame levelSelector = Object.FindFirstObjectByType<LevelSelectorInGame>();  // Tìm LevelSelectorInGame
        //if (levelSelector != null)
        //{
        //    levelIndex = levelSelector.selectedLevelIndex;  // Lấy level được chọn
        //}
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
                case BlockType.GrassWithPalm:
                    prefab = GrassWithPalmPrefab;
                    break;
            }

            if (prefab != null)
            {
                Instantiate(prefab, block.position, Quaternion.identity, this.transform);
            }
        }
        if (mapData.playerInfo.position != Vector3.zero && PlayerPrefab != null)
        {
            Instantiate(PlayerPrefab, mapData.playerInfo.position, mapData.playerInfo.rotation, this.transform);
        }
        foreach (var item in mapData.items)
        {
            if (item.itemPrefab != null)
            {
                Instantiate(item.itemPrefab, item.position, item.rotation, this.transform);
            }
        }
    }
}
