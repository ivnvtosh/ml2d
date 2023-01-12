using UnityEngine;

public class Sensor
{
    private readonly int _numOfSensors;
    
    private readonly Ray2D[] _rays;
    private readonly RaycastHit2D[] _hits;
    
    private readonly Vector2[] _directions;
    
    private Ray2D _ray;
    private RaycastHit2D _hit;
    private Vector2 _origin;
    
    private readonly Vector _data;
    
    public Sensor(int numOfSensors)
    {
        _numOfSensors = numOfSensors;
        _data = new Vector(numOfSensors);
        _rays = new Ray2D[numOfSensors];
        _hits = new RaycastHit2D[numOfSensors];
        _directions = new Vector2[numOfSensors];
        var angle = Mathf.PI / numOfSensors * 2;
        for (var i = 0; i < numOfSensors; i += 1)
        {
            Vector2 direction;
            direction.x = Mathf.Cos(angle * i);
            direction.y = Mathf.Sin(angle * i);
            _directions[i] = direction;
        }
    }
    
    public Vector GetData(Vector2 position)
    {
        _origin = position;
        Ray();
        DrawRay();
        return _data;
    }
    
    private void Ray()
    {
        for (var i = 0; i < _numOfSensors; i += 1)
        {
            _ray.direction = _directions[i];
            _hits[i] = Physics2D.Raycast(_origin + _ray.direction * 0.3f, _ray.direction, 10);
            _data[i] = _hits[i].distance;
            _rays[i] = _ray;
        }
    }
    
    private void DrawRay()
    {
        for (var i = 0; i < _numOfSensors; i += 1)
        {
            _ray = _rays[i];
            _hit = _hits[i];
            
            if (_hit.collider == null)
            {
                continue;
            }
            
            switch (_hit.collider.tag)
            {
                case "Reward":
                    Debug.DrawRay(_origin + _ray.direction * 0.3f, _ray.direction * 0.3f, Color.green);
                    break;
            }
        }
    }
}
