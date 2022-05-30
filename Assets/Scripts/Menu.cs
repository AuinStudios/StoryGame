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
    [SerializeField]
    private Transform Cam;
    [SerializeField]
    private Transform OpenDoors;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(gotowardsdoors());
    }

    // Update is called once per frame
   // void Update()
   // {
   //    
   // }

    private IEnumerator gotowardsdoors()
    {
        float i = 0;
        //Vector3 tg = v
        Quaternion rotationOfDoor1 = Quaternion.Euler(0, 100, 0);
       Quaternion rotationOfDoor2 = Quaternion.Euler(0, 50, 0);
        while (i <= 0.4f)
        {
           i +=  0.1f * Time.deltaTime;
            Cam.transform.position = Vector3.Lerp(Cam.transform.position, transform.position, i / 10 );
            if(i >= 0.2f)
            {
                OpenDoors.GetChild(0).localRotation = Quaternion.Lerp(OpenDoors.GetChild(0).localRotation, rotationOfDoor1, i / 10);
                OpenDoors.GetChild(1).localRotation = Quaternion.Lerp(OpenDoors.GetChild(1).localRotation, rotationOfDoor2, i / 10);
            }
           
            yield return new WaitForFixedUpdate();
        }
        SetMainMenuOff.SetActive(true);
    }
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
