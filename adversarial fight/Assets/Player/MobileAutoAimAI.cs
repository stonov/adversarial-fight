using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileAutoAimAI : GenericAI
{
    Vector3 targetPos;
    int[] movementInput = {0, 0, 0, 0};
    float movementAlarm;
    float currentMovementAlarm;
    GameObject target;

    public override void Start(){
        target = null;
        targetPos = new Vector3(0.0f, 0.0f, 0.0f);
        movementAlarm = 2.0f;
        currentMovementAlarm = 0.0f;
    }
    
    public override void Update(){
        if(target == null){
            findTarget();
        }else{
            adjustAim();
        }

        if(currentMovementAlarm >= movementAlarm){
            currentMovementAlarm = 0.0f;
            randomMovement();
        }
        currentMovementAlarm += 1.0f * Time.deltaTime;
    }

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

    void findTarget(){

    }

    void adjustAim(){
        
    }
    
    public override Vector3 getTargetPosInput(){
        return targetPos;
    }

    public override int[] getMovementInput(){
        return movementInput;
    }

    public override bool getShootingInput(){
        return true;
    }
}
