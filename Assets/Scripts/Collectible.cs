using UnityEngine;

public class Collectible : MonoBehaviour{
    private const float offScreenLimit = -100f;
    Player player;
    bool isBadCollectible; // Boolean to check if the collectible is bad 
    private SpriteRenderer spriteRenderer;  // Reference to the SpriteRenderer for the collectible

    private void Awake(){
        //find objects
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    void FixedUpdate(){
        // Stop updating if the game is over or paused
         if(player.gameOver || player.gameIsPaused){ 
            return;
        }

        // Move the collectible at the same speed as the ground
        Vector2 pos = transform.position;
        pos.x -= player.velocity.x *Time.fixedDeltaTime; //move as same speed as player

        // If the collectible moves offscreen (past the threshold), destroy it
        if(pos.x < offScreenLimit){ 
            Destroy (gameObject);
        }

        transform.position = pos;       

    }
    
}
