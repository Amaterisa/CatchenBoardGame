using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace General.View
{

    public class View3D : MonoBehaviour
    {
        [SerializeField] private List<MeshRenderer> meshRenderers = new List<MeshRenderer>();
        [SerializeField] private List<TextMeshPro> textMeshPros = new List<TextMeshPro>();
        [SerializeField] private float fadeDuration = 0.25f;
        private Coroutine fadeCoroutine;
        private static readonly int Color = Shader.PropertyToID("_Color");
        private static readonly int Alpha = Shader.PropertyToID("Alpha");
        private Color color;
        private Color32 faceColor;

        public void Show(Action callback = null)
        {
            StopFadeCoroutine();
            SetObjectsActive(true);
            fadeCoroutine = StartCoroutine(FadeCoroutine(0f, 1f, callback));
        }

        public void Hide(Action callback = null)
        {
            StopFadeCoroutine();
            fadeCoroutine = StartCoroutine(FadeCoroutine(1f, 0f, () => {
                SetObjectsActive(false);
                callback?.Invoke();
            }));
        }

        public void ShowInstantly()
        {
            StopFadeCoroutine();
            SetObjectsActive(true);
            SetRenderersAlpha(1f);
        }

        public void HideInstantly()
        {
            StopFadeCoroutine();
            SetObjectsActive(false);
            SetRenderersAlpha(0f);
        }

        private IEnumerator FadeCoroutine(float start, float end, Action callback = null)
        {
            var t = 0.0f;

            while(t < 1f){
                t += Time.deltaTime / fadeDuration;
                SetRenderersAlpha(Mathf.Lerp(start, end, t));
                yield return null;
            }
            SetRenderersAlpha(end);
            callback?.Invoke();
        }

        private void StopFadeCoroutine()
        {
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);
        }

        private void SetRenderersAlpha(float alpha)
        {
            var faceColorAlpha = (int) (alpha * 255);

            foreach(var meshRenderer in meshRenderers)
            {
                if (meshRenderer.material.HasProperty(Color))
                {
                    color = meshRenderer.material.color;
                    color.a = alpha;
                    meshRenderer.material.SetColor(Color, color);
                }
                else
                {
                    meshRenderer.material.SetFloat(Alpha, alpha);
                }
            }

            foreach (var textMeshPro in textMeshPros)
            {
                faceColor = textMeshPro.faceColor;
                faceColor.a = (byte) faceColorAlpha;
                textMeshPro.faceColor = faceColor;
            }
        }

        private void SetObjectsActive(bool active)
        {
            foreach(var meshRenderer in meshRenderers)
            {
                meshRenderer.gameObject.SetActive(active);
            }

            foreach (var textMeshPro in textMeshPros)
            {
                textMeshPro.gameObject.SetActive(active);
            }
        }

        public void PopulateInInspector()
        {
            meshRenderers = GetComponentsInChildren<MeshRenderer>(true).Where(m => m.sharedMaterial.HasProperty(Color)).ToList();
            textMeshPros = GetComponentsInChildren<TextMeshPro>(true).ToList();
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(View3D), true)]
    public class View3DEditor : Editor {
        public override void OnInspectorGUI() 
        {
            base.OnInspectorGUI();
            
            View3D editorObj = target as View3D;
            if (editorObj == null)
                return;

            if (GUILayout.Button("Populate lists in inspector"))
            {
                editorObj.PopulateInInspector();
                EditorUtility.SetDirty(editorObj);
            }
        }
    }
#endif
}