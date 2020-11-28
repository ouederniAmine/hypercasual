using UnityEngine.SceneManagement;
using UnityEngine;

public class menu : MonoBehaviour
{
     void Start()
    {
     
       
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void SetTouch()
    {
        PlayerPrefs.SetString("mode", "touch");
        
    }
    public void SetInfinity()
    {
        PlayerPrefs.SetString("mode", "infinity");

    }
    
    
}
