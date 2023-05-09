using System.Linq;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    public Transform campfirePanel;
    public Transform inventoryPanel;
    private bool isOpened = false;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isOpened)
        {
            ChangeCursorMode(false);
            campfirePanel.gameObject.SetActive(false);
            inventoryPanel.gameObject.SetActive(false);
            isOpened = false;
            CharacterAbilities.canCameraMove = true;
            CharacterAbilities.canCharacterMove = true;
            CharacterAbilities.canOpenInventory = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && !isOpened)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
            {
                if (hit.collider.gameObject.GetComponent<Campfire>() != null)
                {
                    ChangeCursorMode(true);
                    var onlyInactive = FindObjectsOfType<Transform>(true).Where(sr => !sr.gameObject.activeInHierarchy).ToArray();
                    campfirePanel = onlyInactive.Select(obj => obj).Where(obj => obj.tag == "Campfire").First();
                    inventoryPanel = onlyInactive.Select(obj => obj).Where(obj => obj.tag == "Inventory").First();
                    campfirePanel.gameObject.SetActive(true);
                    inventoryPanel.gameObject.SetActive(true);
                    isOpened = true;
                    CharacterAbilities.canCameraMove = false;
                    CharacterAbilities.canCharacterMove = false;
                    CharacterAbilities.canOpenInventory = false;
                }
            }
        }

    }
    private void ChangeCursorMode(bool state)
    {
        if (state)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        Cursor.visible = state;
    }
}
