using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour{
    Player player;
    Text scoreText;
    Text distanceText;
    Text counterText;
    Text collectiblesCounterText;

    GameObject panel;

    void Start(){
        //finding objects
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        scoreText= GameObject.FindGameObjectWithTag("Text").GetComponent<Text>();
        panel = GameObject.Find("Panel1"); //panel1 has all of the "game over" features
        distanceText = GameObject.Find("DistanceText").GetComponent<Text>();
        counterText = GameObject.Find("CounterText").GetComponent<Text>();
        collectiblesCounterText = GameObject.Find("Collectibles").GetComponent<Text>();

        panel.SetActive(false); //deactivated by default
        
    }

    void Update(){
        int distance = Mathf.FloorToInt(player.distance); //distance the player has achieved (extracted from player gameObject)
        int collectiblesCounter = player.cheeseCollected;

        scoreText.text = distance + "m";
        counterText.text = collectiblesCounter + "";
        
        
        if(player.gameOver){
            panel.SetActive(true); //show game over window
            distanceText.text = distance + "m";

            if(collectiblesCounter == 1){
            collectiblesCounterText.text = "You colllected " + collectiblesCounter + " coin!";
            }else{
            collectiblesCounterText.text = "You colllected " + collectiblesCounter + " coins!";
            }
        }
    }

    public void Exit(){
        SceneManager.LoadScene("Menu");
    }

    public void PlayAgain(){
        SceneManager.LoadScene("SampleScene");
    }
}
