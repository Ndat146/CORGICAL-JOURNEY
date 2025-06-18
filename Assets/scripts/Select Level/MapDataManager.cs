using UnityEngine;

public class MapDataManager : MonoBehaviour
{
    public MapData[] levels;  
    private int currentLevel = 0;  

    public MapData GetCurrentLevelData()
    {
        return levels[currentLevel];
    }

    public void SetLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < levels.Length)
        {
            currentLevel = levelIndex;
        }
    }
}
