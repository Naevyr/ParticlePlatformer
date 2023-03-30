using System;
using System.Threading.Tasks;
using System.Diagnostics;
using UnityEngine;
public interface SingletonHandle<T>
{
    Task<T> Value {get;}
}
public class SingletonCoordinator<T>
{   

    bool _valueSet;




    public SingletonHandle<T> Handle {get;private set;}


    public SingletonCoordinator()
    {
        Handle = new SingletonHandle_class();
    }



   
  
    class SingletonHandle_class : SingletonHandle<T>
    {
        TaskCompletionSource<T> _value = new TaskCompletionSource<T>();

        public void SetValue(T value)
        {
            
            _value.SetResult(value);
        }
        public Task<T> Value 
        {
            get
            {
                
                
                return _value.Task;

           
            }
        }
        
    }

    public void SetValue(T value)
    {
        System.Diagnostics.Debug.Assert(_valueSet,$"ERR: Multiple instances of {value.GetType().ToString()}. It can cause unwanted behaviours.");
        
        (Handle as SingletonHandle_class).SetValue(value);
    }
}   