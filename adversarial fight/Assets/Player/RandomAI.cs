using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAI : GenericAI{
    Vector3 targetPos = new Vector3(0.0f, 0.0f, 0.0f);
    int[] movementInput = {0, 0, 0, 0};
    bool shoot = true;

    // void Start(){
    //     Debug.Log("override start");
    // }
    
    // void Update(){
    //     // if(Random.Range(0.0f, 100.0f) > 99.0f){
    //     //     shoot = true;
    //     // }
    //     Debug.Log("overridden update");
    //     targetPos = new Vector3(Random.Range(0.0f,10.0f), 0.0f, 0.0f);
    //     Debug.Log(shoot);
    //     shoot = true;
    // }
    
    public override Vector3 getTargetPosInput(){
        return targetPos;
    }

    public override int[] getMovementInput(){
        return movementInput;
    }

    public override bool getShootingInput(){
        if(shoot){
            shoot = false;
            return true;
        }
        return false;
    }

    public override void shout(){
        // Debug.Log("fuck you");
    }
}
