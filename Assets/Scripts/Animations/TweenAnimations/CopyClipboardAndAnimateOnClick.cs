using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

public class CopyClipboardAndAnimateOnClick : MonoBehaviour, IPointerClickHandler
{
    private TextMeshProUGUI tmpText;
    private Vector3 originalScale;

    void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
        originalScale = transform.localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ClipboardHelper.CopyToClipboard(CleanTextMeshProTextRegex.GetCleanString(tmpText.text));

        transform.DOScale(originalScale * 0.8f, 0.1f).OnComplete(() => { transform.DOScale(originalScale, 0.1f); });
    }
}