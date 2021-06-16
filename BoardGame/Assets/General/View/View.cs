using System;
using System.Collections;
using UnityEngine;

namespace General.View
{
    public class View : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeDuration = 0.25f;
        private Coroutine fadeCoroutine;

        public void Show(Action callback = null)
        {
            gameObject.SetActive(true);
            StopFadeCoroutine();
            fadeCoroutine = StartCoroutine(FadeCoroutine(0f, 1f, () =>
            {
                callback?.Invoke();
                gameObject.SetActive(true);
            }));
        }

        public void Hide(Action callback = null)
        {
            StopFadeCoroutine();
            fadeCoroutine = StartCoroutine(FadeCoroutine(1f, 0f, () =>
            {
                callback?.Invoke();
                gameObject.SetActive(false);
            }));
        }

        public void SetAlpha(float alpha)
        {
            gameObject.SetActive(true);
            StopFadeCoroutine();
            canvasGroup.alpha = alpha;
        }

        public void ShowInstantly()
        {
            canvasGroup.alpha = 1f;
            gameObject.SetActive(true);
        }

        public void HideInstantly()
        {
            canvasGroup.alpha = 0f;
            gameObject.SetActive(false);
        }

        private IEnumerator FadeCoroutine(float start, float end, Action callback = null)
        {
            var t = 0.0f;

            while(t < 1f){
                t += Time.deltaTime / fadeDuration;
                canvasGroup.alpha = Mathf.Lerp(start, end, t);
                yield return null;
            }
            canvasGroup.alpha = end;
            callback?.Invoke();
        }

        private void StopFadeCoroutine()
        {
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);
        }
    }
}
