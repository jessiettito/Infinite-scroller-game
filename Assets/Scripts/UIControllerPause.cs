using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControllerPause : MonoBehaviour{
    Player player;
    GameObject pausePanel;
    GameObject pauseButton;
    void Start(){
        //find objects 
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        pausePanel = GameObject.Find("Panel2"); //panel2 has the pause components
        pausePanel.SetActive(false); //deactivated by default
        pauseButton = GameObject.Find("Pause");
        
    }

    void Update(){

        if(player.gameOver){
            pauseButton.SetActive(false);
        }else if(player.gameIsPaused){
            pausePanel.SetActive(true);

        }else{
            pausePanel.SetActive(false);
        }  
    }
    public void PauseGame(){//pause game
        player.gameIsPaused = true;
    }

    public void ContinueGame(){//continue with the game
        player.gameIsPaused = false;
    }


    public void Exit(){
       SceneManager.LoadScene("Menu");
    }

    public void Reset(){
        SceneManager.LoadScene("SampleScene");
    }
}
