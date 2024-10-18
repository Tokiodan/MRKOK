using UnityEngine;

public class ShowUnlockUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject spellUnlockPanel;  // The panel UI for spell unlocks

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals) || (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Equals)))
        {
            TogglePanel();
        }
    }

    void TogglePanel()
    {
        bool isActive = !spellUnlockPanel.activeSelf;
        spellUnlockPanel.SetActive(isActive);
        Cursor.visible = isActive;
        Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
