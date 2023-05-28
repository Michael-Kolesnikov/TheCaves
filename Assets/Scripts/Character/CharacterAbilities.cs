using UnityEngine;

public static class CharacterAbilities
{
    public static bool canCharacterMove;
    public static bool canCameraMove;
    public static bool canOpenInventory;
    public static bool isInventoryOpened;
    public static void SetState(bool state)
    {
        canCameraMove = state;
        canOpenInventory = state;
        canCharacterMove = state;
    }
    public static void ChangeCursor(bool state)
    {
        if (state)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
