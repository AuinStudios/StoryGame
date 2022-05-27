using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject SetSettingsOn;
    [SerializeField]
    private GameObject SetMainMenuOff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
  //  void Update()
  //  {
  //      
  //  }
    public void PlayGame()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
       
    }
    public void BackFromSettings()
    {
        SetMainMenuOff.SetActive(true);
        SetSettingsOn.SetActive(false);
    }
    public void Settings()
    {
        SetMainMenuOff.SetActive(false);
        SetSettingsOn.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
