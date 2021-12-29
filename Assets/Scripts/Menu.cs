using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject menu;
    public void Level_1()
    {
        SceneManager.LoadScene("Level_1");
    }
    public void Level_2()
    {
        SceneManager.LoadScene("Level_2");
    }
    public void Level_3()
    {
        SceneManager.LoadScene("Level_3");
    }
}