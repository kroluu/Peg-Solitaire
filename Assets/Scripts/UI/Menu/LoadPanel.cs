using System;
using System.Linq;
using DG.Tweening;
using ScriptableObjects;
using TMPro;
using UI.Core;
using UnityEngine;

namespace UI.Menu
{
    public class LoadPanel : MonoBehaviour,IPanelViewController
    {
        [SerializeField] private AnimatedButton backButton;
        private CanvasGroup loadGroup;

        [SerializeField] private Transform loadContent;
        [SerializeField] private Transform loadPrefab;
        
        private Tweener singleTweener;
        void Awake()
        {
            loadGroup = GetComponent<CanvasGroup>();
            loadGroup.alpha = 0f;
            backButton.contextToSet = MainMenuContext.Main;
            
        }

        private void Start()
        {
            InstanceLoadPrefabs();
        }

        private void InstanceLoadPrefabs()
        {
            foreach (Transform child in loadContent)
            {
                Destroy(child.gameObject);
            }
            string[] saves = Settings.Instance.GetFileSaves().Select(x=>x.Remove(x.Length-5,5)).ToArray();
            foreach (string save in saves)
            {
                Transform loadSave = Instantiate(loadPrefab, loadContent);
                LoadElementButton loadElementButton = loadSave.GetComponent<LoadElementButton>();
                loadElementButton.loadSaveText.text = save;
                loadElementButton.saveName = save;
            }
        }

        public void ShowPanel()
        {
            singleTweener?.Kill();
            singleTweener = DOVirtual.Float(loadGroup.alpha, 1f, Config.TIME_FOR_SHOW_MENU_PANEL, (value) => loadGroup.alpha = value);
            loadGroup.blocksRaycasts = true;
        }

        public void HidePanel()
        {
            singleTweener?.Kill();
            singleTweener = DOVirtual.Float(loadGroup.alpha, 0f, Config.TIME_FOR_HIDE_MENU_PANEL,(value) =>
            {
                loadGroup.alpha = value;
            });
            loadGroup.blocksRaycasts = false;
        }
    }
}
