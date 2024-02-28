using UnityEngine;
using UnityEngine.EventSystems;

public class LanguageButton : MonoBehaviour, IPointerClickHandler
{
    public Language Language;

    public void OnPointerClick(PointerEventData eventData)
    {
        Localizer.SetLanguage(Language);
    }
}
