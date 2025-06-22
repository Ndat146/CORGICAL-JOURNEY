using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public int selectedLevelIndex = 0;  // Chỉ số cấp độ hiện tại
    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);  // Nếu đã có một LevelManager, hủy bản sao này
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);  // Giữ LevelManager khi chuyển cảnh

        
}
}