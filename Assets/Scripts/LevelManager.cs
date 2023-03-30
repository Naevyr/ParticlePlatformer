using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LevelManager : MonoBehaviour
{



    SingletonHandle<Player> _player;
    Map _map;

   
    [SerializeField]  
    ScriptableObject _levelRules;
    



    public LevelRules Rules 
    {
        get {return _levelRules as LevelRules;}
        set { _levelRules = value;}
    }
    
    public LevelEntityManager Entities{get;private set;} = new LevelEntityManager();
    public static LevelManager Instance {get; private set;}

    void Awake ()
    {

        Instance = this;
    }


    void Start()
    {
        Debug.Assert(_levelRules != null && _levelRules is LevelRules, "No level rules present on Current Level");
        
        _player =  GetEntity<Player>();
        
        _player.Value.ContinueWith(
            (t) =>
            {
                
                t.Result.OnComplete +=  (_,_) => Rules.OnCompleteLevel();
                t.Result.OnFail += (_,_) => Rules.OnFailLevel();
                return t.Result;

            }
        );
        

        var lowerBound = GameObject.FindGameObjectWithTag("LowerBound").GetComponent<Collider2D>();
        
        //Works because the player is checking for trigger collision, if bounces it doesn't call the fail event 
        lowerBound.isTrigger = Rules.FailOnFall;
    }

    void Update()
    {
        
        if(_player.Value.IsCompleted)
        {
            if(Rules.CheckFailCondition(_player.Value.Result))
            {
                Rules.OnFailLevel();
            }

        }
    }
    

    public void RegisterEntity<T>(T entity) where T : SingletonLevelEntity
    {
        Entities.RegisterEntity<T>(entity);
    }

    public SingletonHandle<T> GetEntity<T>() where T : SingletonLevelEntity
    {
        return Entities.GetEntity<T>();
    }
    void OnDestroy()
    {
        //Removing reference so if a loaded scene doesn't have a LevelManager it doesn't refer to a destroyed instance
        Instance = null;
    }
    
}