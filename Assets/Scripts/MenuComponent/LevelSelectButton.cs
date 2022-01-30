using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI nameText;
    private int _levelIndex;
    private string _levelName;
    
    public void Init(string levelName)
    {
        _levelIndex = transform.GetSiblingIndex() + 1;
        nameText.text = _levelIndex.ToString();
        button.onClick.AddListener(OnButtonClicked);
        _levelName = levelName;
    }
    
    public virtual void OnButtonClicked()
    {
        ApplicationManager.Instance.EnterGameplay(_levelName);
    }

    public void RefreshButton()
    {
        button.interactable = true;
    }
}
