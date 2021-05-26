using System;
using System.Collections;
using UnityEngine;

namespace General.View
{
    public class View : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeDuration;
        private Coroutine fadeCoroutine;

        public void Show(Action callback = null){
            StopFadeCoroutine();
            fadeCoroutine = StartCoroutine(FadeCoroutine(0f, 1f, callback));
        }

        public void Hide(Action callback = null){
            StopFadeCoroutine();
            fadeCoroutine = StartCoroutine(FadeCoroutine(1f, 0f, callback));
        }

        public void ShowInstantly(){
            canvasGroup.alpha = 1f;
        }

        public void HideInstantly(){
            canvasGroup.alpha = 0f;
        }

        private IEnumerator FadeCoroutine(float start, float end, Action callback = null){
            var t = 0.0f;

            while(t < 1f){
                t += Time.deltaTime / fadeDuration;
                canvasGroup.alpha = Mathf.Lerp(start, end, t);
                yield return null;
            }
            canvasGroup.alpha = end;
            callback?.Invoke();
        }

        private void StopFadeCoroutine(){
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);
        }
    }
}
