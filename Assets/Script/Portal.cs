using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private string[] sceneName;
    private int sceneCount;
    private int loadIndex;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.tag = "Portal";
    }

    public void changeScene()
    {
        sceneCount = sceneName.Length;
        loadIndex = Random.Range(0, sceneCount);
        SceneManager.LoadScene(sceneName[loadIndex]);
    }
}
