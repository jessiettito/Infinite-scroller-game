using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour{
    Player player;
    public float velocity = 5;//change according to selected background object

    private void Awake(){
        //find object
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
    }

    void FixedUpdate(){

         if(player.gameOver || player.gameIsPaused){//stop effect if game is over or if game is paused
            return;
        }
        Vector2 pos = transform.position;
        pos.x -= velocity *Time.fixedDeltaTime;//move to the left

        if(pos.x <= -20){
            pos.x = 40;//"respawn" it (move it to the right)
        }

        transform.position = pos;
    }
}
