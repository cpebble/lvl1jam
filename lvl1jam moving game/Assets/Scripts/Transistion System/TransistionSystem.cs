using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransistionSystem : MonoBehaviour, ILevelEventHandler
{
    
    public Animation transistionAnimation;
    public LevelLoader levelManager;
    void Start(){
        levelManager.RegisterEventHandler(this);
        GameObject.Find("LevelController");
    }
    public void SetAnimationValue(int value){
        levelManager.AnimationWaiting = value != 1 ? true : false;
    }
    public void StartTransition(){
        transistionAnimation.Play();
    }
    public void EndTransition(){

    }

    public void HandleEvent(LevelLoadState state)
    {
        switch (state)
        {
            case LevelLoadState.PREUNLOAD:
                StartTransition();
                break;
        }
    }
}
