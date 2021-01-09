using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public int parent;
    public float damage = 50.0f;
    float speed = 15.0f;

    public void destroySelf(){
        Destroy(gameObject);
        Destroy(this);
    }

    // Update is called once per frame
    void Update(){
        transform.Translate(new Vector3(speed * Time.deltaTime, 0.0f, 0.0f));
        Vector3 currentPosition = Camera.main.WorldToScreenPoint(transform.position);
        if(currentPosition.x > Screen.width + 64.0f ||
            currentPosition.x < -64.0f ||
            currentPosition.y > Screen.height + 64.0f ||
            currentPosition.y < -64.0f){

            destroySelf();
        }
        // Debug.Log("x: " + currentPosition.x + " y: " + currentPosition.y);
    }
}
