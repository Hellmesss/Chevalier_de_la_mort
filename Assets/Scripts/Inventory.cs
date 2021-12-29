using UnityEngine;
using TMPro;
public class Inventory : MonoBehaviour
{
    public int monstersCount;
    public TextMeshProUGUI monstersCountText;
    public static Inventory instance;
    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    public void AddMonsters(int count)
    {
        monstersCount = monstersCount + count;
        monstersCountText.text = monstersCount.ToString();
    }
}
