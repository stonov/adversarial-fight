using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAI : GenericAI{
    Vector3 targetPos = new Vector3(0.0f, 0.0f, 0.0f);
    int[] movementInput = {0, 0, 0, 0};
    bool shoot = true;
    float movementAlarm = 2.0f;
    float currentMovementAlarm = 0.0f;

    int zeroOrOne(){
        if(Random.Range(0.0f, 1.0f) > 0.5f){
            return 1;
        }else{
            return 0;
        }
    }

    void randomMovement(){
        movementInput[0] = zeroOrOne();
        movementInput[1] = zeroOrOne();
        movementInput[2] = zeroOrOne();
        movementInput[3] = zeroOrOne();
        targetPos = new Vector3(Random.Range(0.0f, Screen.width), Random.Range(0.0f, Screen.height), 0.0f);
    }

    public override void Start(){
        movementAlarm = 2.0f;
        currentMovementAlarm = 0.0f;
    }
    
    public override void Update(){
        if(Random.Range(0.0f, 100.0f) > 99.0f){
            shoot = true;
        }
        if(currentMovementAlarm >= movementAlarm){
            currentMovementAlarm = 0.0f;
            randomMovement();
        }
        currentMovementAlarm += 1.0f * Time.deltaTime;
        // Debug.Log("movement: " + currentMovementAlarm + "/" + movementAlarm + " input: " + movementInput[0] + " " + movementInput[1] + " " + movementInput[2] + " " + movementInput[3]);
    }
    
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
}
