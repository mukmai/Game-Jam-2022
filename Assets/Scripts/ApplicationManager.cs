using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ApplicationManager : MonoBehaviour
{
    private static ApplicationManager _instance;

    public static ApplicationManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<ApplicationManager>();
            }

            return _instance;
        }
    }

    [SerializeField] private Transform frontPanel;
    [SerializeField] private Transform levelSelectPanel;
    [SerializeField] private Transform chapterList;
    [SerializeField] private TextMeshProUGUI currChapterNameText;
    [SerializeField] private Button nextChapterButton;
    [SerializeField] private Button prevChapterButton;
    private int _currChapterIndex;

    [SerializeField] private GameObject levelButtonGameObject;
    [SerializeField] private Transform chapter1LevelContainer;
    [SerializeField] private List<string> chapter1LevelList;
    [SerializeField] private Transform chapter2LevelContainer;
    [SerializeField] private List<string> chapter2LevelList;
    [SerializeField] private Transform chapter3LevelContainer;
    [SerializeField] private List<string> chapter3LevelList;

    void Start()
    {
        frontPanel.localScale = Vector3.one;
        levelSelectPanel.localScale = Vector3.zero;
        _currChapterIndex = 0;
        SetToCurrChapter();
        SetUpLevelButtons();
    }

    public void GoToSelectLevelPanel()
    {
        frontPanel.DOScale(0, 0.3f);
        levelSelectPanel.DOScale(1, 0.3f);
    }

    public void GoToMainMenuPanel()
    {
        frontPanel.DOScale(1, 0.3f);
        levelSelectPanel.DOScale(0, 0.3f);
    }

    public void GoToNextChapter()
    {
        if (_currChapterIndex < chapterList.childCount - 1)
        {
            _currChapterIndex++;
            SetToCurrChapter();
        }
    }

    public void GoToPrevChapter()
    {
        if (_currChapterIndex > 0)
        {
            _currChapterIndex--;
            SetToCurrChapter();
        }
    }

    private void SetToCurrChapter()
    {
        for (int i = 0; i < chapterList.childCount; i++)
        {
            chapterList.GetChild(i).gameObject.SetActive(i == _currChapterIndex);
        }

        currChapterNameText.text = chapterList.GetChild(_currChapterIndex).name;
        nextChapterButton.gameObject.SetActive(_currChapterIndex < chapterList.childCount - 1);
        prevChapterButton.gameObject.SetActive(_currChapterIndex > 0);
    }

    public void EnterGameplay(string levelName)
    {
        GameplayManager.SelectedLevelFromMenu = levelName;
        GameplayManager.MenuSelectedLevel = true;
        SceneManager.LoadScene("GameScene");
    }

    private void SetUpLevelButtons()
    {
        for (int j = 0; j < chapter1LevelList.Count; j++)
        {
            // Debug.Log(currChapter.name + " " + j);
            GameObject currButtonGameObject = Instantiate(levelButtonGameObject, chapter1LevelContainer);
            
            LevelSelectButton currButton = currButtonGameObject.GetComponent<LevelSelectButton>();
            currButton.Init(chapter1LevelList[j]);
        }
        
        for (int j = 0; j < chapter2LevelList.Count; j++)
        {
            // Debug.Log(currChapter.name + " " + j);
            GameObject currButtonGameObject = Instantiate(levelButtonGameObject, chapter2LevelContainer);
            
            LevelSelectButton currButton = currButtonGameObject.GetComponent<LevelSelectButton>();
            currButton.Init(chapter2LevelList[j]);
        }
        
        for (int j = 0; j < chapter3LevelList.Count; j++)
        {
            // Debug.Log(currChapter.name + " " + j);
            GameObject currButtonGameObject = Instantiate(levelButtonGameObject, chapter3LevelContainer);
            
            LevelSelectButton currButton = currButtonGameObject.GetComponent<LevelSelectButton>();
            currButton.Init(chapter3LevelList[j]);
        }
    }
    
    public void QuitApplication()
    {
        Application.Quit();
    }
}
