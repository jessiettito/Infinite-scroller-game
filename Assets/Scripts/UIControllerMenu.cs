using UnityEngine;

public class UIControllerMenu : MonoBehaviour{
    void Start(){}
    void Update(){}

    public void play(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}
