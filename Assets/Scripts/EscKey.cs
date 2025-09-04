using UnityEngine;

public class EscKey : MonoBehaviour{
    void Update(){
        if(Input.GetKeyDown("escape")){
            Application.Quit();
        }
    }
}
