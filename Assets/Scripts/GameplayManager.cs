using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    private static GameplayManager _instance;

    public static GameplayManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<GameplayManager>();
            }

            return _instance;
        }
    }

    public static bool MenuSelectedLevel;
    public static string SelectedLevelFromMenu;
    [SerializeField] private string currTestingLevel;
    private string _currLevelId;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: cache light wave and particles
        // ObjectPool.Instance.CacheObject();
        if (MenuSelectedLevel)
        {
            _currLevelId = SelectedLevelFromMenu;
        }
        else
        {
            _currLevelId = currTestingLevel;
        }
        Debug.Log("Curr level: " + _currLevelId);
        SetUpGame();
    }

    public void SetUpGame()
    {
        Debug.Log("Setting up game");
        AudioManager.Instance.Play("AudioTest");
    }
    
    // Update is called once per frame
    void Update()
    {
        // update
    }
    
    public void QuitLevel()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ResetLevel()
    {
        Debug.Log("Reset level");
        SetUpGame();
    }
}
