using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public MapDataManager mapDataManager; 
    public Button[] levelButtons; 

    void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int index = i;  
            levelButtons[i].onClick.AddListener(() => SelectLevel(index));
        }
    }

    void SelectLevel(int levelIndex)
    {
        LevelManager.Instance.selectedLevelIndex = levelIndex;
        SceneManager.LoadScene("Game"); 
    }
}
