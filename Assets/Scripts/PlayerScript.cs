using UnityEngine;

public class Player: MonoBehaviour{

    public Vector2 velocity;

    //private variables
    private bool isGrounded;
    private float groundHeight;
    
    
    //public variables
    public float jumpVelocity;
    public float gravity;
    public float distance; //for game score
    public int cheeseCollected;
    public float maxVelocity;
    public float acceleration; //speed of player and/or platforms moving
    public bool gameOver;
    public bool gameIsPaused;

    public LayerMask groundLayerMask; 
    public LayerMask obstacleLayerMask;

    void   Awake(){
        //initializing variables
        isGrounded = false;
        groundHeight = 0;
        jumpVelocity =30;
        gravity = -100;
        distance = 0; //for game score
        cheeseCollected = 0;
        maxVelocity = 50f;
        acceleration= 0.2f; //speed of player and/or platforms moving
        gameOver = false;
        gameIsPaused = false;
        velocity.x = 15;
    }
    
    void Update(){
        if(!gameOver && !gameIsPaused ){ //stop counting distance if game is over or game is paused

            distance += velocity.x*Time.fixedDeltaTime;

            if(isGrounded){
                if(Input.GetKeyDown(KeyCode.Space)){//jump
                    isGrounded = false;
                    velocity.y = jumpVelocity;
                }
            }

            // Gradually increase velocity over time (if game is not paused or over)
            if(velocity.x < maxVelocity) {
                velocity.x += acceleration * Time.deltaTime;  // Gradually increase velocity.x
            }
        }
    }

    void FixedUpdate(){
        Vector2 pos = transform.position;
        RaycastHit2D hit; 

        if(gameOver || gameIsPaused){ //stop moving if game is over or game is paused
            return;
        }

        if(pos.y < - 10){//if mouse fell, game is over
            gameOver = true;
        }

        if (!isGrounded){ //collider for when player lands in a platform
            pos.y += velocity.y *Time.fixedDeltaTime;
            velocity.y += gravity * Time.fixedDeltaTime;
            hit = raycast(new Vector2(pos.x + 2, pos.y), Vector2.up, velocity.y *Time.fixedDeltaTime, groundLayerMask);//giving player buffer space for landing in platform 

            if(hit.collider != null){
                Ground ground = hit.collider.GetComponent<Ground>();

                if(ground != null){
                    if(pos.y >= ground.groundHeight){
                        groundHeight = ground.groundHeight;
                        pos.y = groundHeight;
                        velocity.y = 0; //resetting for when player falls in platform
                        isGrounded = true;
                    }
                    
                } 
            }
            
            //raycast for hitting wall
            Vector2 wallOrigin = new Vector2(pos.x, pos.y);
            RaycastHit2D wallHit = raycast(wallOrigin, Vector2.right, velocity.x * Time.fixedDeltaTime, default);//check if null applies or not

            if(wallHit.collider != null){
                Ground ground= wallHit.collider.GetComponent<Ground>();

                if(ground != null){
                    if(pos.y < ground.groundHeight){//making sure
                        velocity.x = 0;
                    }
                }
            }
            
        }else { //collider looking for platform below us
            hit = raycast(new Vector2(pos.x - 1, pos.y), Vector2.up, velocity.y *Time.fixedDeltaTime, groundLayerMask);//giving player buffer space for jumping from platform.

            if(hit.collider == null){
                isGrounded = false;
            }
        }

        // obstacleCollision(pos);
        // collectibleCollision(pos);

        //collision with obstacles
        Vector2 obstacleOrigin = new Vector2(pos.x, pos.y);

        //collision x axis
        RaycastHit2D obstacleHitX = Physics2D.Raycast(obstacleOrigin, Vector2.right, velocity.x*Time.fixedDeltaTime, obstacleLayerMask);
        if(obstacleHitX.collider != null){
            Obstacle obstacle = obstacleHitX.collider.GetComponent<Obstacle>();
            if(obstacle != null){
                hitObstacle(obstacle);
                cheeseCollected++;
            }
        }

        //collision y axis
        RaycastHit2D obstacleHitY = Physics2D.Raycast(obstacleOrigin, Vector2.up, velocity.x*Time.fixedDeltaTime, obstacleLayerMask);
        if(obstacleHitY.collider != null){
            Obstacle obstacle = obstacleHitY.collider.GetComponent<Obstacle>();
            if(obstacle != null){
                hitObstacle(obstacle);
                cheeseCollected++;
            }
        }


        //collision with collectibles
        Vector2 collectibleOrigin = new Vector2(pos.x, pos.y);
        float rayLength = 2.5f;

        // 1. Collision on the X-axis (right direction)
            RaycastHit2D collectibleHitX = Physics2D.Raycast(collectibleOrigin, Vector2.right, rayLength, obstacleLayerMask);
            if (collectibleHitX.collider != null) {
                Collectible collectible = collectibleHitX.collider.GetComponent<Collectible>();
                if (collectible != null) {
                    hitCollectible(collectible);
                }
            }

            // 2. Collision on the Y-axis (up direction)
            RaycastHit2D collectibleHitY = Physics2D.Raycast(collectibleOrigin, Vector2.up, rayLength, obstacleLayerMask);
            if (collectibleHitY.collider != null) {
                Collectible collectible = collectibleHitY.collider.GetComponent<Collectible>();
                if (collectible != null) {
                    hitCollectible(collectible);
                }
            }

            // 3. Collision on the X-axis (left direction)
            RaycastHit2D collectibleHitXLeft = Physics2D.Raycast(collectibleOrigin, Vector2.left, rayLength, obstacleLayerMask);
            if (collectibleHitXLeft.collider != null) {
                Collectible collectible = collectibleHitXLeft.collider.GetComponent<Collectible>();
                if (collectible != null) {
                    hitCollectible(collectible);
                }
            }

            // 4. Collision on the Y-axis (down direction)
            RaycastHit2D collectibleHitYDown = Physics2D.Raycast(collectibleOrigin, Vector2.down, rayLength, obstacleLayerMask);
            if (collectibleHitYDown.collider != null) {
                Collectible collectible = collectibleHitYDown.collider.GetComponent<Collectible>();
                if (collectible != null) {
                    hitCollectible(collectible);
                }
            }

        transform.position = pos;
    }


    private RaycastHit2D raycast (Vector2 origin, Vector2 rayDirection, float rayDistance, LayerMask layerMask){
        return Physics2D.Raycast(origin, rayDirection, rayDistance, groundLayerMask);
    }

    private void hitObstacle(Obstacle obstacle){
        Destroy(obstacle.gameObject);
        gameOver = true;
        
    }

    private void hitCollectible(Collectible collectible){
        Destroy(collectible.gameObject);
        cheeseCollected++;
        
    }

    private void hitBadCollectible(Collectible collectible){
        Destroy(collectible.gameObject);
        velocity.x *= 0.7f; 

    }
}

