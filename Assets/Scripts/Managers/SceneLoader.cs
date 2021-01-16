using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    
    public static class SceneLoader
    {
        public static bool loadGameFromSave;
        public static string saveNameToLoad;
        public static void LoadScene(Scenes _sceneToLoad)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            SceneManager.LoadScene(_sceneToLoad.ToString());
        }
        
        public static void Fade(Image fadeImage,FadeOption _fadeOption,Action _onCompleteFadeAction=null)
        {
            float fadeAmount = 0f;
            float fadeTime = 0f;
            switch (_fadeOption)
            {
                case FadeOption.In:
                    fadeAmount = 1f;
                    fadeTime = Config.TIME_FOR_FADE_IN;
                    break;
                case FadeOption.Out:
                    fadeTime = Config.TIME_FOR_FADE_OUT;
                    break;
            }

            DOVirtual.Float(fadeImage.color.a, fadeAmount, fadeTime, (_value) =>
            {
                Color color = fadeImage.color;
                color.a = _value;
                fadeImage.color = color;
            }).OnComplete(() =>
            {
                _onCompleteFadeAction?.Invoke();
            });
    }
    
    

            
}
}
