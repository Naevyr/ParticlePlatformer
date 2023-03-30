using System;
using System.Collections.Generic;
using UnityEngine;
public class LevelEntityManager
{
    Dictionary<Type,object> _entityHandleCoordinators = new Dictionary<Type, object>();

    public void RegisterEntity<T>(T entity) where T : SingletonLevelEntity
    {

        

        if(!_entityHandleCoordinators.ContainsKey(typeof(T)))
            _entityHandleCoordinators.Add(typeof(T), new SingletonCoordinator<T>());
           
       
        (_entityHandleCoordinators[typeof(T)] as SingletonCoordinator<T>).SetValue(entity);
        

        
    }
    
    public SingletonHandle<T> GetEntity<T>() where T : SingletonLevelEntity
    {
        
        if(_entityHandleCoordinators.ContainsKey(typeof(T)))
            return (_entityHandleCoordinators[typeof(T)] as SingletonCoordinator<T>).Handle; 
        else
        {
            var coordinator = new SingletonCoordinator<T>();
            _entityHandleCoordinators.Add(typeof(T), coordinator);
            return coordinator.Handle;

        }

      
    }
}