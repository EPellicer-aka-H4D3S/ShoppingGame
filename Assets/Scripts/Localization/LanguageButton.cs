using UnityEngine;
using UnityEngine.EventSystems;

public class LanguageButton : MonoBehaviour, IPointerClickHandler
{
    public Language language;

    public void OnPointerClick(PointerEventData eventData)
    {
        Localizer.SetLanguage(language);
    }
}