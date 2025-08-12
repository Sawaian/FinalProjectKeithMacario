using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

public class SettingsMenu : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI headerText;
    public RectTransform listRoot;
    public Button buttonPrefab;
    public Button backButton;
 
    Stack<MenuNode> nav = new Stack<MenuNode>();
    MenuNode current;

    void Awake()
    {
        current = BuildTree();
        Render();
        if (backButton) backButton.onClick.AddListener(GoBack);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) GoBack();
    }

    void GoBack()
{
    if (nav.Count > 0)
    {
        current = nav.Pop();
        Render();
    }
    else
    {
        if (Inventory_Manager.instance != null)
            Inventory_Manager.instance.CloseSettings();
        else
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

    void Render()
    {
        foreach (Transform child in listRoot) Destroy(child.gameObject);
        if (headerText) headerText.text = current.Title;
        //really cool
        for (int i = 0; i < current.Items.Count; i++)
        {
            var item = current.Items[i];
            var button = Instantiate(buttonPrefab, listRoot);
            var label = button.GetComponentInChildren<TextMeshProUGUI>();
            if (label) label.text = item.GetLabel();
            int index = i;
            button.onClick.AddListener(() => OnItemPressed(index));
        }
    }

    void OnItemPressed(int index)
    {
        var item = current.Items[index];
        if (item.IsLeaf)
        {
            item.OnSelect?.Invoke();
            Render();
        }
        else
        {
            nav.Push(current);
            current = item;
            Render();
        }
    }

    MenuNode BuildTree()
    {
        var root = new MenuNode("Settings");

        //adjusts volume. Callback function.
        var audio = new MenuNode("Audio");
        audio.AddLeaf(
            () => $"Master Volume: {Mathf.RoundToInt(Settings.MasterVolume * 100)}%",
            () => Settings.CycleMasterVolume()
        );

        //adjusts video size
        var video = new MenuNode("Video");
        video.AddLeaf(
            () => $"Fullscreen: {(Screen.fullScreen ? "On" : "Off")}",
            () => Screen.fullScreen = !Screen.fullScreen
        );


        root.AddChild(audio);
        root.AddChild(video);
        return root;
    }

    static class Settings
    {
        public static float MasterVolume
        {
            get => PlayerPrefs.GetFloat("MasterVolume", 1f);
            set { PlayerPrefs.SetFloat("MasterVolume", value); AudioListener.volume = value; }
        }

        public static void CycleMasterVolume()
        {
            float[] steps = { 0f, 0.5f, 1f };
            MasterVolume = GetNextPreset(MasterVolume, steps);
        }

        static float GetNextPreset(float currentValue, float[] presets)
        {
            if (presets == null || presets.Length == 0) return currentValue;

            int closestPresetIndex = 0;
            float smallestGap = Mathf.Abs(currentValue - presets[0]);

            for (int presetIndex = 1; presetIndex < presets.Length; presetIndex++)
            {
                float gap = Mathf.Abs(currentValue - presets[presetIndex]);
                if (gap < smallestGap)
                {
                    smallestGap = gap;
                    closestPresetIndex = presetIndex;
                }
            }


            int nextPresetIndex = (closestPresetIndex + 1) % presets.Length;
            return presets[nextPresetIndex];
        }
    }
}
