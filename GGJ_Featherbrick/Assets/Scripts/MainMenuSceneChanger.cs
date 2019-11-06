using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuSceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void Exit()
    {
        Application.Quit();
    }

    void GoToPlayerSelect()
    {
        SceneManager.LoadScene("Assets/Scenes/Menu Player Select.unity", LoadSceneMode.Single);
    }
}
