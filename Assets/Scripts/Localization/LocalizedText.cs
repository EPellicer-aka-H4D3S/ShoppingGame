using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    public string textKey;
    private TMP_Text textValue;

    void Start()
    {
        textValue = GetComponent<TMP_Text>();
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
        textValue.text = Localizer.GetText(textKey);
    }
}