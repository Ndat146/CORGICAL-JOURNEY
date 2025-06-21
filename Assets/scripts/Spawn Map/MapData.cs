using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "Map/MapData")]
public class MapData : ScriptableObject
{
    [System.Serializable]
    public struct BlockInfo
    {
        public BlockType blockType; 
        public Vector3 position;    
    }
    [System.Serializable]
    public struct PlayerInfo
    {
        public Vector3 position;  
        public Quaternion rotation;  
    }
    public BlockInfo[] blocks;
    public PlayerInfo playerInfo;
}
