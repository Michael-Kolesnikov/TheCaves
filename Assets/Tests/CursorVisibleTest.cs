using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CursorVisibleTest
{
    EcsSystems _systems;
    EcsWorld _world;
    EcsPool<PlayerInventoryComponent> _inventoryPool;
    EcsPool<PlayerTag> _playerTagPool;
    int _playerEntity;
    public void CreateNewWorldWithSystem()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
        _playerEntity = _world.NewEntity();
        _inventoryPool = _world.GetPool<PlayerInventoryComponent>();
        _playerTagPool = _world.GetPool<PlayerTag>();
        _inventoryPool.Add(_playerEntity);
        _playerTagPool.Add(_playerEntity);
        _systems.Add(new CursorLockSystem()).Init();
    }
    [Test]
    public void CursorVisibleTestSimplePasses()
    {
        CreateNewWorldWithSystem();
        ref var playerInventory = ref _inventoryPool.Get(_playerEntity);
        playerInventory.isInventoryOppened = true;
        _systems.Run();

        Assert.IsTrue(Cursor.visible);
    }
    [Test]
    public void CursorInvisibleTestSimplePasses()
    {
        CreateNewWorldWithSystem();
        ref var playerInventory = ref _inventoryPool.Get(_playerEntity);
        playerInventory.isInventoryOppened = false;
        _systems.Run();

        Assert.IsTrue(!Cursor.visible);
    }

}
