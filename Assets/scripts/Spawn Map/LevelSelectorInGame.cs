using UnityEngine;

public class LevelSelectorInGame : MonoBehaviour
{
    public MapDataManager mapDataManager;
    public int selectedLevelIndex = 0; 

    private void Start()
    {
        
        LoadAndSpawnLevel();
    }

    private void LoadAndSpawnLevel()
    {
        if (mapDataManager == null || mapDataManager.levels.Length <= selectedLevelIndex)
        {
            Debug.LogError("Level không hợp lệ!");
            return;
        }

        MapData selectedMap = mapDataManager.levels[selectedLevelIndex];
        MapSpawner mapSpawner = Object.FindFirstObjectByType<MapSpawner>();  
        if (mapSpawner != null)
        {
            mapSpawner.SpawnMapFromData(selectedMap);  
            Debug.Log("Đã spawn level: " + selectedLevelIndex);
        }
        else
        {
            Debug.LogError("Không tìm thấy MapSpawner trong scene!");
        }
    }
}
