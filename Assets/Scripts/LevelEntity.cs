using UnityEngine;


/// <summary>
/// Signleton level entity, remember to call  "Register<T>()" during initialization for it to be easily accessed;
/// </summary>
public abstract class SingletonLevelEntity : MonoBehaviour
{
    protected virtual void Register<T>() where T : SingletonLevelEntity
    {
        
        LevelManager.Instance.RegisterEntity<T>(this as T);
    }
}