using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : GenericAI
{
    int ToInt(bool val){
        return val ? 1 : 0;
    }
    
    int[] getInputFromArrowKeys(){
        int upPressed = ToInt(Input.GetKey(KeyCode.W));
        int downPressed = ToInt(Input.GetKey(KeyCode.S));
        int leftPressed = ToInt(Input.GetKey(KeyCode.A));
        int rightPressed = ToInt(Input.GetKey(KeyCode.D));
        int[] inputDir = {upPressed, downPressed, leftPressed, rightPressed};
        return inputDir;
    }
    
    public override Vector3 getTargetPosInput(){
        return Input.mousePosition;
    }

    public override int[] getMovementInput(){
        return getInputFromArrowKeys();
    }

    public override bool getShootingInput(){
        return Input.GetMouseButton(0);
    }
}
