using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public int selectedLevelIndex = 0;  
    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Trùng lặp instance: " + this.GetType().Name);
            Destroy(gameObject);  
            return;
        }

        Instance = this;
        //DontDestroyOnLoad(gameObject);
        Debug.Log(this.GetType().Name + " instance đã được khởi tạo");

    }
}