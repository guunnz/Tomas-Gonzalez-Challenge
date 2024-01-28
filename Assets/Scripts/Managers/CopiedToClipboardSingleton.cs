using DG.Tweening;
using TMPro;
using UnityEngine;

public class CopiedToClipboardSingleton : MonoBehaviour
{
    public static CopiedToClipboardSingleton instance;

    [SerializeField] private TextMeshProUGUI CopiedToClipboardText;
    [SerializeField] private float moveDistance = 30f;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float stayDuration = 2f;

    private Vector2 originalPosition;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
            return;
        }

        instance = this;
        originalPosition = CopiedToClipboardText.rectTransform.anchoredPosition;
    }

    public void PopText()
    {
        // Ensure the text is initially transparent
        CopiedToClipboardText.color = new Color(CopiedToClipboardText.color.r, CopiedToClipboardText.color.g,
            CopiedToClipboardText.color.b, 0);

        // Fade in and move up
        Sequence animationSequence = DOTween.Sequence();
        animationSequence.Append(CopiedToClipboardText.DOFade(1f, fadeDuration))
            .Join(CopiedToClipboardText.rectTransform.DOAnchorPosY(originalPosition.y + moveDistance, fadeDuration)
                .SetRelative(true))
            .AppendInterval(stayDuration)
            .Append(CopiedToClipboardText.DOFade(0f, fadeDuration))
            .Join(CopiedToClipboardText.rectTransform.DOAnchorPosY(originalPosition.y, fadeDuration)
                .SetRelative(false));

        animationSequence.OnComplete(() => {
            CopiedToClipboardText.rectTransform.anchoredPosition = originalPosition;
        });
    }
}