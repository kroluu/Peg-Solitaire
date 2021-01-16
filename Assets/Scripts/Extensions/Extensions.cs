using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

public static class Extensions
    {
        public static TweenerCore<string, string, StringOptions> DOTMPText(this string target, string endValue, float duration, bool richTextEnabled = true, ScrambleMode scrambleMode = ScrambleMode.None, string scrambleChars = null)
        {
            if (endValue == null) {
                if (Debugger.logPriority > 0) Debugger.LogWarning("You can't pass a NULL string to DOText: an empty string will be used instead to avoid errors");
                endValue = "";
            }
            TweenerCore<string, string, StringOptions> t = DOTween.To(() => target, x => target = x, endValue, duration);
            t.SetOptions(richTextEnabled, scrambleMode, scrambleChars)
                .SetTarget(target);
            return t;
        }

        public static SavableVector3 ToSavableVector3(this Vector3 _vector3)
        {
            return new SavableVector3()
            {
                x = _vector3.x,
                y = _vector3.y,
                z = _vector3.z
            };
        }

        public static Vector3 ConvertToVector3(this SavableVector3 _savableVector3)
        {
            return new Vector3(_savableVector3.x,_savableVector3.y,_savableVector3.z);
        }
        
        public static int GetEnumCount<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Length;
        }
        
        [Serializable]
        public class SavableVector3
        {
            public float x;
            public float y;
            public float z;
        }
    }

