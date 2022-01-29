using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
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
        CacheObject();
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

    [HideInInspector] public GameObject waveLightRayGameObject;
    [HideInInspector] public GameObject particleLightRayGameObject;
    [HideInInspector] public GameObject particleGameObject;
    public Material generalPathLineMaterial;
    public Material receiverPathLineMaterial;
    [SerializeField] private Transform winGlowEffect;
    [SerializeField] private TextMeshProUGUI winText;
    private bool _hasWin;

    private void CacheObject()
    {
        waveLightRayGameObject = Resources.Load<GameObject>("GameComponents/WaveLightRay");
        particleLightRayGameObject = Resources.Load<GameObject>("GameComponents/ParticleLightRay");
        particleGameObject = Resources.Load<GameObject>("GameComponents/Particle");
        ObjectPool.Instance.CacheObject(waveLightRayGameObject, 25);
        ObjectPool.Instance.CacheObject(particleLightRayGameObject, 10);
        ObjectPool.Instance.CacheObject(particleGameObject, 40);
    }

    public void SetUpGame()
    {
        Debug.Log("Setting up game");
        _currGameLevel = ObjectPool.Instance.CreateObject(
            Resources.Load<GameObject>("GameLevels/" + _currLevelId)).GetComponent<GameLevel>();
        _currGameLevel.Init();
        _hasWin = false;
        // AudioManager.Instance.Play("AudioTest");
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (_currGameLevel)
        {
            foreach (var waveParticleConverter in _currGameLevel.waveParticleConverters)
            {
                waveParticleConverter.PreUpdateConverter();
            }
            foreach (var lightReceiver in _currGameLevel.lightReceivers)
            {
                lightReceiver.UpdateReceiver();
            }
            foreach (var lightSource in _currGameLevel.lightSources)
            {
                lightSource.UpdateLightSource();
            }
            foreach (var waveParticleConverter in _currGameLevel.waveParticleConverters)
            {
                waveParticleConverter.UpdateConverter();
            }

            if (!_hasWin && _currGameLevel.WinConditionFulfilled())
            {
                Debug.Log("Win level");
                _hasWin = true;
                WinGame();
            }
        }
    }

    private void WinGame()
    {
        DOVirtual.DelayedCall(3, () =>
        {
            SceneManager.LoadScene("MainScene");
        });

        winGlowEffect.DOScale(10, 1.5f);
        DOVirtual.DelayedCall(1f, () =>
        {
            winText.DOFade(1, 1f);
        });
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
