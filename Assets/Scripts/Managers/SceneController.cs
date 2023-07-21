using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void GoToSurvival()
    {
        SceneManager.LoadScene("SurvivalMode");
    }

    public void GoToDefense()
    {
        SceneManager.LoadScene("DefenseMode");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
