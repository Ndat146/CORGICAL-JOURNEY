using UnityEngine;

public class MapDataManager : MonoBehaviour
{
    public static MapDataManager Instance { get; private set; }  

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

    //private void Awake()
    //{
    //    if (Instance != null && Instance != this)
    //    {
    //        Debug.LogWarning("Trùng lặp instance: " + this.GetType().Name);
    //        Destroy(gameObject); 
    //        return;
    //    }

    //    Instance = this;  
    //    DontDestroyOnLoad(gameObject);
    //    Debug.Log(this.GetType().Name + " instance đã được khởi tạo");
    //}
}
