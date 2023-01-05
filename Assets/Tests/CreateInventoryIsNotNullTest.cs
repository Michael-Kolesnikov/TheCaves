using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CreateInventoryIsNotNullTest
{
    EcsSystems _systems;
    EcsWorld _world;
    EcsPool<PlayerInventoryComponent> _inventoryPool;
    EcsPool<PlayerTag> _playerTagPool;
    int _playerEntity;

    [Test]
    public void CreateInventoryIsNotNullTestSimplePasses()
    {
        CreateNewWorldWithSystem();
        ref var playerInventory = ref _inventoryPool.Get(_playerEntity);
        var playerInventoryUIPanel = new GameObject();
        var childCanvas = new GameObject();
        childCanvas.transform.SetParent(playerInventoryUIPanel.transform);
        for (var i = 0; i < 10; i++)
        {
            new GameObject().transform.SetParent(childCanvas.transform);
        }
        playerInventory.playerInventoryUIPanel = playerInventoryUIPanel.transform;
        _systems.Init();
        Assert.IsNotNull(playerInventory.inventory);
    }
    public void CreateNewWorldWithSystem()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
        _playerEntity = _world.NewEntity();
        _inventoryPool = _world.GetPool<PlayerInventoryComponent>();
        _playerTagPool = _world.GetPool<PlayerTag>();
        _inventoryPool.Add(_playerEntity);
        _playerTagPool.Add(_playerEntity);
        _systems.Add(new PlayerInitInventorySystem());
    }
}
