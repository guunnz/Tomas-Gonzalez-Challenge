using UnityEngine;
using DG.Tweening;

public class WaveAnimation : MonoBehaviour
{
    public RectTransform[] rectTransforms;

    public float moveDistance = 50f;
    public float duration = 1f;
    public float delayBetweenWaves = 0.2f;

    void Start()
    {
        for (int i = 0; i < rectTransforms.Length; i++)
        {
            if (rectTransforms[i] != null)
            {
                AnimateWave(rectTransforms[i], delayBetweenWaves * i);
            }
        }
    }

    void AnimateWave(RectTransform rectTransform, float delay)
    {
        rectTransform.DOMoveY(rectTransform.position.y + moveDistance, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .SetDelay(delay);
    }
}