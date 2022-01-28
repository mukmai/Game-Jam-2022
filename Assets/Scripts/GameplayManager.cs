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
    private GameLevel _currGameLevel;

    private Transform _currGrabbingObject;
    private bool _isGrabRotation;
        
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
        _currGrabbingObject = null;
        SetUpGame();
    }

    public void SetUpGame()
    {
        Debug.Log("Setting up game");
        _currGameLevel = ObjectPool.Instance.CreateObject(
            Resources.Load<GameObject>("GameLevels/" + _currLevelId)).GetComponent<GameLevel>();
        // AudioManager.Instance.Play("AudioTest");
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_currGameLevel)
        {
            
        }
    }
    
    public void QuitLevel()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ResetLevel()
    {
        Debug.Log("Reset level");
        SceneManager.LoadScene("GameScene");
    }

    public bool TryGrabTool(Transform transform, bool isRotation)
    {
        if (!_currGrabbingObject)
        {
            _currGrabbingObject = transform;
            _isGrabRotation = isRotation;
            return true;
        }

        return false;
    }

    public bool TryReleaseTool(Transform transform, bool isRotation)
    {
        if (_currGrabbingObject == transform && _isGrabRotation == isRotation)
        {
            _currGrabbingObject = null;
            return true;
        }

        return false;
    }
}
