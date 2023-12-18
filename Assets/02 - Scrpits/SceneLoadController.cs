using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Enums;
using UnityEditor.SearchService;

public class SceneLoadController : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.sceneCount + 1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void LoadScene(SceneIdentificator sceneID)
    {
        SceneManager.LoadScene((int)sceneID);
    }
    public void LoadScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene((int)SceneIdentificator.MENU);
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene((int)SceneIdentificator.GAME);
    }
    public void QuitGameDelayed(float time)
    {
        StartCoroutine(DelayQuit(time));
    }

    private IEnumerator DelayQuit(float time)
    {
        yield return new WaitForSeconds(time);
        Application.Quit();
    }

}
