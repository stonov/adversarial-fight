using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum behaviorCategory {player, random};

public class ShipControls : MonoBehaviour{
    public behaviorCategory thisBehavior = behaviorCategory.player;
    public GameObject laserPrefab;
    public GameObject AIObject;
    
    Vector3 targetPos;
    GenericAI thisAI;
    float maxHealth = 300.0f;
    float currentHealth;
    Quaternion currentRotation;

    void getBehavior(){
        if(thisBehavior == behaviorCategory.player){
            thisAI = AIObject.GetComponent<PlayerAI>();
        }
        else if(thisBehavior == behaviorCategory.random){
            thisAI = AIObject.GetComponent<RandomAI>();
        }else{
            thisAI = AIObject.GetComponent<RandomAI>();
        }
    }
    
    int ToInt(bool val){
        return val ? 1 : 0;
    }
    
    void updateOrientation(){
        targetPos = thisAI.getTargetPosInput();
        var objectPos = Camera.main.WorldToScreenPoint(transform.position);
        var targetAngle = Mathf.Atan2(targetPos.y - objectPos.y, targetPos.x - objectPos.x) * Mathf.Rad2Deg;
        
        var currentQuat = currentRotation;
        var goalQuat = Quaternion.Euler(new Vector3(0, 0, targetAngle));
        var transAngle = Quaternion.Angle(currentQuat, goalQuat);
        var turnSpeed = 30.0f;
        
        var newRotation = Quaternion.RotateTowards(currentQuat, goalQuat, transAngle * turnSpeed * Time.deltaTime);
        transform.rotation = newRotation;
        currentRotation = transform.rotation;
    }

    Vector3 getMoveDir(ref int shouldMove){
        // Up, Down, Left, Right
        int[] inputDir = {0, 0, 0, 0};
        inputDir = thisAI.getMovementInput();
        int verticalDir = inputDir[0] - inputDir[1];
        int horizontalDir = inputDir[3] - inputDir[2];

        // if the sum from all keys is zero, then we should not move
        if(verticalDir == 0 && horizontalDir == 0){
            shouldMove = 0;
        }else{
            shouldMove = 1;
        }

        // gets the angle between a unit vector on the x-axis and the current direction vector
        float angle = Vector3.Angle(new Vector3(1.0f, 0.0f, 0.0f), new Vector3(horizontalDir, verticalDir, 0.0f))*Mathf.Deg2Rad;

        // the angle has to be inverted if the vertical direction is negative
        if(verticalDir < 0){
            angle *= -1.0f;
        }

        // Debug.Log("Hello: " + angle*Mathf.Rad2Deg + " inputDir " + inputDir[0] + " "+ inputDir[1] + " "+ inputDir[2] + " "+ inputDir[3] + " ");
        return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f);
    }

    void updatePosition(){
        int shouldMove = 1;
        Vector3 moveDir = getMoveDir(ref shouldMove);
        if(shouldMove == 0){
            return;
        }
        float speed = 2.0f;
        transform.Translate(moveDir * speed * Time.deltaTime);
    }

    void fireLaser(){
        GameObject newLaser = Instantiate(laserPrefab, transform.position, currentRotation);
        LaserController newLaserScript = newLaser.GetComponent<LaserController>();
        newLaserScript.parent = gameObject.GetInstanceID();
    }

    void shootHandler(){
        if(thisAI.getShootingInput()){
            fireLaser();
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "LaserShot"){
            GameObject hitLaser = col.gameObject;
            LaserController newLaserScript = hitLaser.GetComponent<LaserController>();
            if(newLaserScript.parent != gameObject.GetInstanceID()){
                newLaserScript.destroySelf();
                currentHealth -= newLaserScript.damage;
                if(currentHealth <= 0.0f){
                    destroySelf();
                }
            }
        }
    }

    void destroySelf(){
        Destroy(gameObject);
        Destroy(this);
    }

    void Start(){
        getBehavior();
        Quaternion currentRotation = transform.rotation;
        currentHealth = maxHealth;
    }

    void Update(){
        // reset the rotation to independently re-position ship
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
        updatePosition();

        // restore the rotation
        updateOrientation();

        // shoot if conditions are met
        shootHandler();

        thisAI.shout();
    }
}
