using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : SingletonLevelEntity
{

    

    

    
    Rigidbody2D _rigidbody;
    Indicator _indicator;
    SingletonHandle<Map> _mapRenderer;
    Vector3 _initialPosition;
    Vector3 _oldPosition;


    [SerializeField]
    public bool Launched {get;set;} = false;


    public event EventHandler OnFail;
    public event EventHandler OnComplete;

    void Start()
    {
        
        Register<Player>();
        
        _initialPosition = transform.position;
        
        _mapRenderer = LevelManager.Instance.GetEntity<Map>();


        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0;
        _indicator = transform.GetChild(0).GetComponent<Indicator>();
        
    }

    
    void Update()
    {
        if(Launched)
            _rigidbody.gravityScale = 1;

    }
 
    

    void OnMouseDown()
    {
        if(!Launched)
            _indicator.gameObject.SetActive(true);
    }
    void OnMouseDrag()
    {   
        if(!Launched)
        {   
            var localTarget = -( Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position );
            localTarget.z = 0;
         
            _indicator.RelativeTarget = localTarget;
        }
    }
    
    void OnMouseUp()
    {
        if(!Launched)
        {

            _indicator.gameObject.SetActive(false);
            _rigidbody.AddForce(new Vector2(_indicator.RelativeTarget.x,_indicator.RelativeTarget.y) * 10, ForceMode2D.Impulse);
            Launched = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {   
        var screenPosition = Camera.main.WorldToScreenPoint(other.contacts[0].point);
        
        _mapRenderer.Value.Result.Hit(new Vector2Int(
            Mathf.Clamp((int)screenPosition.x,0 , Screen.width),
            Mathf.Clamp((int)screenPosition.y,0 , Screen.height)));
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            
            case "EndTrigger":
                
                OnComplete?.Invoke(this,null); 
                break;

            case "LowerBound":
                OnFail?.Invoke(this,null);
                break;


            default:
                break;
        }
        
    }

    public void Register()
    {
        LevelManager.Instance.RegisterEntity<Player>(this);
    }


    public void Reinitialize()
    {
        Launched = false;
        _rigidbody.velocity = Vector2.zero;
        this.transform.position = _initialPosition;
        _rigidbody.gravityScale = 0;
    }
}
