using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericAI : MonoBehaviour
{
    public virtual void Start(){
    }
    
    public virtual void Update(){
    }

    public virtual Vector3 getTargetPosInput(){
        return new Vector3(0.0f, 0.0f, 0.0f);
    }

    public virtual int[] getMovementInput(){
        int[] defaultVal = {0, 0, 0, 0};
        return defaultVal;
    }

    public virtual bool getShootingInput(){
        Debug.Log("generic!!");
        return false;
    }
}
