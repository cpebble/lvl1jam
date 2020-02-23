using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class Level : ScriptableObject, ILevelEventHandler
{
    public LevelLoader loader;
    public GameObject levelGameObject;
    public GameObject PlacementGrid;
    private List<GameObject> items;
    public List<GameObject> NewItems;
    public GameObject Decoration;
    private GameObject decorationContainer;
    public GameObject RolloverItemLocation;
    private GameObject rolloverItemObject;

    public void HandleEvent(LevelLoadState state)
    {
        switch (state)
        {
            case LevelLoadState.LOAD:
                onLoad();
                break;
            case LevelLoadState.UNLOAD:
                onUnload();
                break;
        }
    }
    void onLoad()
    {
        levelGameObject = Instantiate(PlacementGrid);
        rolloverItemObject = Instantiate(RolloverItemLocation);
        foreach (GameObject item in NewItems)
        {
            items.Add(Instantiate(item));
        }
        float xOff = 0.0f;
        foreach (GameObject item in loader.ItemCarryOver)
        {
            item.transform.parent = rolloverItemObject.transform;
            item.transform.position = new Vector3(
                xOff + rolloverItemObject.transform.position.x,
                rolloverItemObject.transform.position.y,
                rolloverItemObject.transform.position.z);
            xOff += item.GetComponent<SpriteRenderer>().bounds.size.x;
            items.Add(item);
        }
        loader.ItemCarryOver.Clear();
        decorationContainer = Instantiate(Decoration);


    }
    void onUnload()
    {
        // This handles object carryover
        PlacementGrid pgrid = levelGameObject.GetComponent<PlacementGrid>();
        foreach (DragAbleObject item in pgrid.heldObjects)
        {
            loader.ItemCarryOver.Add(item.gameObject);
        }
        foreach (GameObject item in items)
        {
            // Only destroy items where neccesary
            if (!loader.ItemCarryOver.Contains(item))
                Destroy(item);
            else
            {
                item.transform.parent = null;
            }
        }
        items.Clear();
        Destroy(levelGameObject);
        levelGameObject = null;
        Destroy(decorationContainer);
        Destroy(rolloverItemObject);
    }
}