using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }
}
