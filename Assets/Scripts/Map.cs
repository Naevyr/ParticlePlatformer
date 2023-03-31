
using System.Linq;
using UnityEngine;
public class Map : SingletonLevelEntity
{

  
    [SerializeField]
    Color _hitColor;



    [SerializeField] 
    int _hitDimension;
    Texture2D _hitTexture;


    [SerializeField]
    RenderTexture _mapTexture;
    [SerializeField]
    Material _overlayMaterial;
    SpriteRenderer _overlayLayer;


    
    
    void Start()
    {
        Register<Map>();


        Debug.Assert(_overlayMaterial != null);
        Debug.Assert(_mapTexture != null);


        var overlayLayerGameObject = new GameObject();
        
        overlayLayerGameObject.transform.parent = this.transform;

        overlayLayerGameObject.AddComponent<SpriteRenderer>();
        
        _overlayLayer = overlayLayerGameObject.GetComponent<SpriteRenderer>();
        var sprite = Sprite.Create(new Texture2D(Screen.width,Screen.height),new Rect(0,0,Screen.width,Screen.height), Vector2.one / 2);
        _overlayLayer.sprite = sprite;
        _overlayLayer.material = _overlayMaterial;



        var width = _overlayLayer.sprite.bounds.size.x;
        var height = _overlayLayer.sprite.bounds.size.y;
        
        var worldScreenHeight = Camera.main.orthographicSize * 2.0;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        
        overlayLayerGameObject.transform.localScale = new Vector3((float)worldScreenWidth / width, (float)worldScreenHeight / height,1);
    
        _hitTexture = new Texture2D(Screen.width,Screen.height,TextureFormat.R16,0,true,true);
        
        var col = Color.black;
        
        
        
        _hitTexture.SetPixels(Enumerable.Repeat(col,Screen.width * Screen.height).ToArray());
        _hitTexture.Apply();
        _mapTexture.Release();
        _mapTexture.width = Screen.width;
        _mapTexture.height = Screen.height;
        _mapTexture.Create();


        _overlayMaterial.SetTexture("_MapTexture",_mapTexture);
        _overlayMaterial.SetTexture("_HitTexture",_hitTexture);

    }
    

    public void Hit(Vector2Int pixelPosition)
    {
      
        for (int deltaX = -_hitDimension; deltaX < _hitDimension; deltaX++)
        {

            var maxY = Mathf.Sqrt(_hitDimension * _hitDimension - deltaX * deltaX);
            
            for (int deltaY =  - (int)maxY; deltaY < maxY; deltaY++)
            {
                var x = Mathf.Clamp(pixelPosition.x + deltaX,0,Screen.width);
                var y = Mathf.Clamp(pixelPosition.y + deltaY,0,Screen.height);
               
                
                var hitSample = _hitTexture.GetPixel(x,y);

                var hit  = 1 - Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY) / _hitDimension;    
                
                hitSample.r = Mathf.Max(Mathf.Clamp(hitSample.r + hit,0,1),hitSample.r);

                _hitTexture.SetPixel(x,y,hitSample);
                
               
            }
        }


        _hitTexture.Apply();
        
    }

}



