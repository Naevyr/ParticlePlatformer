using System;
using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "Arcade", menuName = "LevelRules/Arcade", order = 1)]
public class ArcadeGameMode : LevelRules
{

    

    public override bool CheckFailCondition(Player player)
    {
        if(player.Launched)
            return player.GetComponent<Rigidbody2D>().velocity.magnitude < 0.1;
        return false;
    }

    public override void OnFailLevel()
    {   
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().name,LoadSceneMode.Single);
    }

    public override void OnCompleteLevel()
    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().name,LoadSceneMode.Single);
    }
}