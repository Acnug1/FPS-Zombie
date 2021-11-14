using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class SwitchLevelButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;

    private const string Level = "Level {0}";
    private Button _button;
    private int _levelNumber;

    public event UnityAction<int, SwitchLevelButton> SwitchLevelButtonClick;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void Render(int levelNumber)
    {
        _label.text = string.Format(Level, levelNumber);
        _levelNumber = levelNumber;
    }

    private void OnButtonClick()
    {
        SwitchLevelButtonClick?.Invoke(_levelNumber, this);
    }
}
