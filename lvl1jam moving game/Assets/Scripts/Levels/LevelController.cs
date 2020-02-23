using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour, ILevelEventHandler
{
    int amountOfTimesPressed = 1;
    [SerializeField]
    Animation trashAnimation;

    public bool animationIsFinished;
    [SerializeField]
    LevelLoader loader;
    
    private bool buttonIsActive = true;

    private bool loadNext = true;
    void Start()
    {
        loader.RegisterEventHandler(this);
    }
    public void FinishAnimation(){
        animationIsFinished = true;
    }
    public void ChooseToMove(){
        if(!buttonIsActive)
            return;
        if(!loadNext)
        {
            if(trashAnimation == null)
                trashAnimation = GameObject.Find("Placement grid and trashcan(Clone)").GetComponent<Animation>();
                

            trashAnimation.Play();
        }
        if(loadNext){
            loader.LoadNextLevel();
        }
        loadNext = !loadNext;
    }
    
    public void HandleEvent(LevelLoadState state)
    {
        switch (state)
        {
            case LevelLoadState.PREUNLOAD:
                buttonIsActive = false;
                break;
            case LevelLoadState.POSTLOAD:
                buttonIsActive = true;
                break;
            
        }
    }
}
