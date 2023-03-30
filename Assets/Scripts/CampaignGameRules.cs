using System;
using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "Level", menuName = "LevelRules/CampaignLevel", order = 1)]
public class CampaignGameRules : LevelRules
{
    [SerializeField]
    Scene _nextLevel;
    

    public Scene NextLevel 
    {
        get
        {
            return _nextLevel;
        }
        set
        {
            _nextLevel = value;
        }
        
    }


    public override bool CheckFailCondition(Player player)
    {
        if(player.Launched)
            return player.GetComponent<Rigidbody2D>().velocity.magnitude < 0.1;
        return false;
    }

    public override void OnFailLevel()
    {   
       
        LevelManager.Instance.GetEntity<Player>().Value.Result.Reinitialize();
    }

    public override void OnCompleteLevel()
    {   

        Debug.Log("Level Complete!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name,LoadSceneMode.Single);
    }
}