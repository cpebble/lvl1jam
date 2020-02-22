﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelLoadState
{
    UNLOADED,
    PRELOAD,
    LOAD,
    POSTLOAD,
    PREUNLOAD,
    UNLOAD,
    POSTUNLOAD
}

[CreateAssetMenu]
public class LevelLoader : ScriptableObject
{
    //   ___                        
    //  / _ \ _   _  ___ _   _  ___ 
    // | | | | | | |/ _ \ | | |/ _ \
    // | |_| | |_| |  __/ |_| |  __/
    //  \__\_\\__,_|\___|\__,_|\___|
    private List<ILevelEventHandler> handlers = new List<ILevelEventHandler>();

    public void RegisterEventHandler(ILevelEventHandler handler)
    {
        handlers.Add(handler);
    }
    public void UnRegisterEventHandler(ILevelEventHandler handler)
    {
        handlers.Remove(handler);
    }
    public void RaiseEvent(LevelLoadState state)
    {
        foreach (ILevelEventHandler handler in handlers)
        {
            handler.HandleEvent(state);
        }
    }
    //  _                   _     
    // | |    _____   _____| |___ 
    // | |   / _ \ \ / / _ \ / __|
    // | |__|  __/\ V /  __/ \__ \
    // |_____\___| \_/ \___|_|___/

    public int currentLevel;
    public List<Level> Levels;

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex >= Levels.Count)
        {
            throw new KeyNotFoundException();
        }
        // Level unloading/cleanup
        RaiseEvent(LevelLoadState.PREUNLOAD);
        RaiseEvent(LevelLoadState.UNLOAD);
        // The old level isn't accessible after current level has been loaded
        UnRegisterEventHandler(Levels[currentLevel]);
        RaiseEvent(LevelLoadState.POSTUNLOAD);
        currentLevel = levelIndex;
        // Level Loading
        RaiseEvent(LevelLoadState.PRELOAD);
        // Load the level
        RegisterEventHandler(Levels[levelIndex]);
        RaiseEvent(LevelLoadState.LOAD);
        RaiseEvent(LevelLoadState.POSTLOAD);

    }
    public void LoadNextLevel()
    {
        LoadLevel((currentLevel + 1) % Levels.Count);
    }

}
