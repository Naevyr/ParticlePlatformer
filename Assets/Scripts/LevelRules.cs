using UnityEngine;





public abstract class LevelRules : ScriptableObject
{
    
    [SerializeField]
    bool _failOnFall;
    [SerializeField]
    int _maxBounces = -1;


    public bool FailOnFall 
    {
        get {return _failOnFall;}
        set {_failOnFall = value;}
    } 
    public uint MaxBounces {get;set;}


 
    public abstract void OnCompleteLevel();
    public abstract void OnFailLevel();
    public abstract bool CheckFailCondition(Player player);
}