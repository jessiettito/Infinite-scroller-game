using UnityEngine;

public class Ground : MonoBehaviour{
    Player player;

    public float groundHeight;
    private float groundR; // ground right side
    public float screenR; // screen right side (camera left side is at 0,0)
    public float velocity ;
    BoxCollider2D collider;
    bool generatedGround = false;
    public Obstacle obstacle;
    public Collectible collectible;
    private int platformCount;

    private void Awake(){
        //find objects
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        collider = GetComponent<BoxCollider2D>();
        velocity = player.velocity.x;

        groundHeight = transform.position.y + (collider.size.y /2);
        screenR = Camera.main.transform.position.x*2;
    }

    void FixedUpdate(){
        if(player.gameOver || player.gameIsPaused){
            return;
        }

        Vector2 pos = transform.position;     
        pos.x -= velocity *Time.fixedDeltaTime;        

       groundR = transform.position.x + (collider.size.x)/2 ;

        if (groundR < -2){
            Destroy(gameObject);
        }

        if(!generatedGround){
            if(groundR < screenR){
                generatedGround = true;
                createGround();
            }
        }

        transform.position = pos;
    }

    void createGround(){
        GameObject gameO = Instantiate(gameObject);
        BoxCollider2D gameOCollider = gameO.GetComponent<BoxCollider2D>();
        Vector2 pos;

        float time = player.jumpVelocity/-player.gravity;
        float maxJumpHeight = player.jumpVelocity* time + (0.5f*(player.gravity * (time*time)));

        float maxY = maxJumpHeight*0.7f;
        maxY += groundHeight;
        float minY = 1;

        float actualY = Random.Range(minY, maxY);// random height


        float totalTime = time + Mathf.Sqrt((2.0f *(maxY - actualY))/-player.gravity);
        float maxX = totalTime * player.velocity.x;
        maxX *= 0.7f;
        maxX += groundR;
        float minX = screenR + 5;

        float actualX = Random.Range(minX, maxX);


        pos.y = actualY - gameOCollider.size.y/2;
        if(pos.y > 2.7f){
            pos.y = 2.7f;
        }

        pos.x = actualX + gameOCollider.size.x/2 -4.05f;
       
        gameO.transform.position = pos;

        Ground newGround = gameO.GetComponent<Ground>();
        newGround.groundHeight = gameO.transform.position.y + (gameOCollider.size.y /2);


        //obstacle
        int obstacleNum = Random.Range(0,4);


        for(int i =0; i < obstacleNum; i++){
            GameObject box = Instantiate(obstacle.gameObject);

            float halfwidth = gameOCollider.size.x/2-1;
            float left = gameO.transform.position.x - halfwidth/2;
            float right = gameO.transform.position.x + halfwidth/2;

            float x = Random.Range(left,right);
            float y = newGround.groundHeight;

            Vector2 boxPos = new Vector2(x,y);
            box.transform.position = boxPos;
        }

        //collectible
        int collectibleNum = Random.Range(1,5);

        for(int i =0; i < collectibleNum; i++){
            GameObject cheeseCollectible = Instantiate(collectible.gameObject);

            float halfwidth = gameOCollider.size.x/2-1;
            float left = gameO.transform.position.x - halfwidth/2;
            float right = gameO.transform.position.x + halfwidth/2;

            float x = Random.Range(left,right);
            float y = newGround.groundHeight;

            int randomHeight = Random.Range(0, 2); // Returns 0 or 1 
            
            if (randomHeight == 0) {// Do something for 1
                y = newGround.groundHeight + 3;
               
            } 

            Vector2 collectiblePos = new Vector2(x,y);
            cheeseCollectible.transform.position = collectiblePos;

        }
    }
}