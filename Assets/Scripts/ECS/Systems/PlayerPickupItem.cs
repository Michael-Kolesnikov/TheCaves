using Leopotam.EcsLite;
using UnityEngine;
public class PlayerPickupItem : IEcsRunSystem
{
    public void Run(EcsSystems systems)
    {
        var filter = systems.GetWorld().Filter<PlayerTag>().Inc<PlayerInventoryComponent>().End();
        foreach(var entity in filter)
        {
            if (Input.GetKey(KeyCode.E))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100))
                {
                    InventoryItem pickup = hit.collider.gameObject.GetComponent<InventoryItem>();
                    var inventoryPool = systems.GetWorld().GetPool<PlayerInventoryComponent>().Get(entity);
                    ref var inventory = ref inventoryPool.inventory;
                    var adding =  inventory.TryToAddItem(pickup);
                    Debug.Log(adding.ToString());
                    //получить инвентарь добавть в нго пикап предмет, иконку и тд. 
                    //inventory.AddItem(pickup.itemName, pickup.icon, pickup.description, pickup.quantity);
                    // Удалить объект
                    //Destroy(hit.collider.gameObject.gameObject);
                }
            }
        }
    }
}
