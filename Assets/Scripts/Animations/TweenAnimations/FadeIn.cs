using DG.Tweening;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    [SerializeField] private Color startColor;

    [SerializeField] private Color endColor;

    [SerializeField] private float fadeDuration;

    [SerializeField] private UnityEngine.UI.Image imageToFade;

    [SerializeField] private bool doOnStart;

    void Start()
    {
        if (doOnStart)
        {
            DoFade();
        }
    }

    public void DoFade()
    {
        imageToFade.color = startColor;
        imageToFade.DOColor(endColor, fadeDuration);
    }
}