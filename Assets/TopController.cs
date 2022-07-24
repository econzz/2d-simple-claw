using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void On2DGameButtonPressed()
    {
        SceneManager.LoadScene("Game2DScene", LoadSceneMode.Single);
    }

    public void On3DGameButtonPressed()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
}
