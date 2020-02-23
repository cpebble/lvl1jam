using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransistionSystem : MonoBehaviour, ILevelEventHandler
{
    
    public Animation transistionAnimation;
    public LevelLoader levelManager;
    void Start(){
        levelManager.RegisterEventHandler(this);
    }
    public void SetAnimationValue(int value){
        levelManager.AnimationWaiting = value != 1 ? true : false;
    }
    public void StartTransistion(){
        transistionAnimation.Play();
    }
    public void EndTransistion(){

    }

    public void HandleEvent(LevelLoadState state)
    {
        switch (state)
        {
            case LevelLoadState.PREUNLOAD:
                StartTransistion();
                break;
        }
    }
}
