using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    public string TextKey;
    private Text _textValue;
    //private TMP_Text _textValue;

    void Start()
    {
        _textValue = GetComponent<Text>();
        //_textValue = GetComponent<TMP_Text>();
        SetText();
    }

    private void OnEnable()
    {
        Localizer.OnLanguageChangeDelegate += OnLanguageChanged;
    }

    private void OnDisable()
    {
        Localizer.OnLanguageChangeDelegate -= OnLanguageChanged;
    }

    private void OnLanguageChanged()
    {
        SetText();
    }

    private void SetText()
    {
        _textValue.text = Localizer.GetText(TextKey);
    }
}
