using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class Level : ScriptableObject, ILevelEventHandler
{
    private GameObject levelGameObject;
    public GameObject PlacementGrid;
    private List<GameObject> items;
    public List<GameObject> NewItems;

    public void HandleEvent(LevelLoadState state)
    {
        switch (state)
        {
            case LevelLoadState.LOAD:
                levelGameObject = Instantiate(PlacementGrid);
                foreach (GameObject item in NewItems)
                {
                    items.Add(Instantiate(item));
                }

                break;
            case LevelLoadState.UNLOAD:
                Destroy(levelGameObject);
                levelGameObject = null;
                foreach (GameObject item in items)
                {
                    Destroy(item);
                }
                items.Clear();
                break;
        }
    }
}