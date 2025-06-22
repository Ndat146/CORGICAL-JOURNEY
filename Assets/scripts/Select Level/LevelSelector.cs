using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public MapDataManager mapDataManager;  
    public Button[] levelButtons;  

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; 
    }

    void Start()
    {
        SetupLevelButtons();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SelectLevel")
        {
            SetupLevelButtons();
        }
    }

    void SetupLevelButtons()
    {
        if (LevelManager.Instance == null)
        {
            Debug.LogError("LevelManager chưa được khởi tạo.");
            return;
        }

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int index = i;  
            levelButtons[i].onClick.RemoveAllListeners();  
            levelButtons[i].onClick.AddListener(() => SelectLevel(index));  
        }
    }

    public void SelectLevel(int levelIndex)
    {
        LevelManager.Instance.selectedLevelIndex = levelIndex;

        SceneManager.LoadScene("Game");
    }
}
