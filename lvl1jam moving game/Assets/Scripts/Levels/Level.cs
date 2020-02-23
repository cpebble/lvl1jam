using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class Level : ScriptableObject, ILevelEventHandler
{
    public GameObject levelGameObject;
    public GameObject PlacementGrid;
    private List<GameObject> items;
    public List<GameObject> NewItems;
    public GameObject Decoration;
    private GameObject decorationContainer; 

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
                decorationContainer = Instantiate(Decoration);

                break;
            case LevelLoadState.UNLOAD:
                Destroy(levelGameObject);
                levelGameObject = null;
                foreach (GameObject item in items)
                {
                    Destroy(item);
                }
                items.Clear();
                Destroy(decorationContainer);
                break;
        }
    }
}