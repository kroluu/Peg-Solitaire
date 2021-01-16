using System;
using DG.Tweening;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Menu
{
    
    public class MenuRadialController : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        private bool enableDetecting;

        public int amountOfRadial;

        private Image radialToFillImage;
        private TextMeshProUGUI radialFillText;

        private int GetRadialAmountFromSettings()
        {
            return Settings.Instance.GameSound;
        }

        void Start()
        {
            amountOfRadial = GetRadialAmountFromSettings();
            radialToFillImage = GetComponent<Image>();
            radialFillText = GetComponentInChildren<TextMeshProUGUI>();
            SetRadialState(amountOfRadial,true);
        }
        
        void Update()
        {
            if(!enableDetecting) return;
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
            {
                SetRadialState(++amountOfRadial);
            }
            else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
            {
                SetRadialState(--amountOfRadial);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            enableDetecting = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            enableDetecting = false;
        }

        private void SetRadialState(int _amount,bool _calledOnStart=false)
        {
            amountOfRadial = Mathf.Clamp(_amount, 0, 100);
            DOVirtual.Float(radialToFillImage.fillAmount, amountOfRadial / 100f, _calledOnStart?0f:0.5f,
                (_value) => radialToFillImage.fillAmount = _value);
            radialFillText.text = amountOfRadial.ToString();
            SetSettingsState();
        }

        private void SetSettingsState()
        {
            Settings.Instance.GameSound = amountOfRadial;
        }
    }
}
