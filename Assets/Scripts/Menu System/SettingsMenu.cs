using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using UnityEditor;

public class SettingsMenu : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI headerText;
    public RectTransform listRoot;
    public Button buttonPrefab;
    public Button backButton;
 
    Stack<MenuNode> nav = new Stack<MenuNode>();
    MenuNode currentNode;


    void Awake()
    {

        currentNode = BuildTree();
        Render();
        if (backButton)
        {
            backButton.onClick.AddListener(GoBack);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) GoBack();
    }

    void GoBack()
{
    if (nav.Count > 0)
    {
        currentNode = nav.Pop();
        Render();
    }
    else
    {
            if (Inventory_Manager.instance != null)
            {
            Inventory_Manager.instance.CloseSettings();
            }
            
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
        if (headerText) headerText.text = currentNode.Title;
        //really cool
        for (int i = 0; i < currentNode.Items.Count; i++)
        {
            var item = currentNode.Items[i];
            var button = Instantiate(buttonPrefab, listRoot);
            var label = button.GetComponentInChildren<TextMeshProUGUI>();
            if (label) label.text = item.GetLabel();
            int index = i;
            button.onClick.AddListener(() => OnItemPressed(index));
        }
    }

    void OnItemPressed(int index)
    {
        var item = currentNode.Items[index];
        if (item.IsLeaf)
        {
            item.OnSelect?.Invoke();
            Render();
        }
        else
        {
            nav.Push(currentNode);
            currentNode = item;
            Render();
        }
    }

    MenuNode BuildTree()
    {
        var root = new MenuNode("Settings");

        //adjusts volume. Callback function. Rebuild with proper implementation.
        var audio = new MenuNode("Audio");

        //adjusts video size
        var video = new MenuNode("Video");

        root.AddChild(audio);
        root.AddChild(video);
        return root;
    }
}
