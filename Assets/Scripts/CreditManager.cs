using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadScene", 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void LoadScene()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
