using UnityEngine;

public class Obstacle : MonoBehaviour{
    private const float offScreenLimit = -100f;

    Player player;
    private void Awake(){
        //find object
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void FixedUpdate(){
        // Stop updating if the game is over or paused
         if(player.gameOver || player.gameIsPaused){ 
            return;
        }

        // Move the obstacle at the same speed as the platforms
        Vector2 pos = transform.position;
        pos.x -= player.velocity.x *Time.fixedDeltaTime; 

        // If the obstacle moves offscreen (past the threshold), destroy it
        if(pos.x < -100){ 
            Destroy (gameObject);
        }
        transform.position = pos;
        
    }
}
