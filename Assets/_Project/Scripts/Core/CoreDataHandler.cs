using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreDataHandler : MonoBehaviour
{
    private string _gameUID;
    private MapData _mapData;

    public string GameUID => _gameUID;
    public string Scene => _mapData != null ? _mapData.sceneName : null;
    public float MapSize => _mapData.mapSize;

    public void SetGameUID(MapData data)
    { 
        _gameUID = $"{data.sceneName}__{System.Guid.NewGuid().ToString()}";
    }
    public void SetGameUID(string uid)
    {
        _gameUID = uid;
    }

    public void SetMapData(MapData data)
    {
        _mapData = data;
    }
}
