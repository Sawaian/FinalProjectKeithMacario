using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
public class Inventory_Manager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject inventoryRowPrefab;
    [SerializeField] private Transform itemContent;


    [SerializeField] private Button openSettingsButton;
    [SerializeField] private Button closeSettingsButton;
    [SerializeField] private KeyCode modifierKey = KeyCode.LeftControl;
    [SerializeField] private InputActionReference toggleSettingsAction;


    public static Inventory_Manager instance;
    public CanvasGroup inventoryGroup, settingsGroup;

    private bool isActive = false;
    public List<Item> items = new List<Item>();
    void OnEnable()
    {
        if (toggleSettingsAction)
        {
            toggleSettingsAction.action.performed += OnToggleSettings;
            toggleSettingsAction.action.Enable();
        }
    }
    void OnDisable()
    {
        if (toggleSettingsAction)
        {
            toggleSettingsAction.action.performed -= OnToggleSettings;
            toggleSettingsAction.action.Disable();
        }
    }

#if ENABLE_INPUT_SYSTEM
    void OnToggleSettings(InputAction.CallbackContext _)
    {
        ToggleSettings();
    }
#endif

    void ToggleSettings()
    {
        bool open = settingsGroup && settingsGroup.gameObject.activeSelf;
        if (open) CloseSettings(); else OpenSettings();
    }


    void Awake()
    {
        instance = this;
        inventoryPanel.SetActive(false);

        if (settingsGroup) Set(settingsGroup, false);
        if (inventoryGroup) Set(inventoryGroup, false);

        if (openSettingsButton)
            openSettingsButton.onClick.AddListener(() =>
            {
                if (Input.GetKey(modifierKey) || Input.GetKey(KeyCode.RightControl))
                    OpenSettings();
            });

        if (closeSettingsButton)
            closeSettingsButton.onClick.AddListener(() =>
            {
                if (Input.GetKey(modifierKey) || Input.GetKey(KeyCode.RightControl))
                    CloseSettings();
            });

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isActive = !isActive;
            inventoryPanel.SetActive(isActive);

            if (isActive)
                ListItems();
        }
    }

    public void AddItem(Item newItem)
    {
        items.Add(newItem);
        if (isActive) ListItems();
    }

    public void RemoveItem(Item itemToRemove)
    {
        items.Remove(itemToRemove);
        if (isActive) ListItems();
    }

    private void ListItems()
    {
        if (inventoryRowPrefab == null)
        {
            Debug.LogError("UI Row Prefab not assigned!");
            return;
        }

        for (int i = itemContent.childCount - 1; i >= 0; i--)
            Destroy(itemContent.GetChild(i).gameObject);

        foreach (var it in items)
        {
            GameObject row = Instantiate(inventoryRowPrefab, itemContent);

            var nameText = row.transform.Find("ItemName")?.GetComponent<TextMeshProUGUI>();
            var iconImage = row.transform.Find("ItemIcon")?.GetComponent<Image>();

            if (nameText != null) nameText.text = it.ItemName;
            if (iconImage != null)
            {
                iconImage.color = Color.white;
                iconImage.sprite = it.ItemIcon;
                iconImage.enabled = iconImage.sprite != null;
            }
        }
    }

    public void OpenSettings()
    {
        Set(settingsGroup, true);
        Set(inventoryGroup, false);
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void CloseSettings()
    {
        Set(settingsGroup, false);
        Set(inventoryGroup, true);
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    
     void Set(CanvasGroup group, bool on)
        {
            group.gameObject.SetActive(on);
            group.alpha = on ? 1 : 0; group.interactable = on; group.blocksRaycasts = on;
        }
}
