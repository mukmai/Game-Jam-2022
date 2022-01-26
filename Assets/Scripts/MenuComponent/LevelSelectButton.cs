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
    
    public void Init()
    {
        _levelIndex = transform.GetSiblingIndex() + 1;
        nameText.text = _levelIndex.ToString();
        button.onClick.AddListener(OnButtonClicked);
    }
    
    public virtual void OnButtonClicked()
    {
        ApplicationManager.Instance.EnterGameplay(_levelIndex);
    }

    public void RefreshButton()
    {
        button.interactable = true;
    }
}
