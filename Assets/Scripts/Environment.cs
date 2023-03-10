using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable] public class Environment : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject agentPrefab;
    [SerializeField] private GameObject rewardPrefab;

    [Header("State")]
    [SerializeField] [Range(0.1f, 8f)] private float agentSpeed;
    [SerializeField] [Range(2.0f, 8f)] private int timeout;
    
    [Header("Sensor")]
    [SerializeField] private int numOfSensors;
    
    private Sensor _sensor;
    
    private float _timeExpired;
    
    private int _success = 0;
    private int _fail = 0;
    
    private GameObject _agent;
    private GameObject _reward;
    
    private float pastDistance = 3f;
    
    public Vector Reset()
    {
        _sensor = new Sensor(numOfSensors);
        _agent = Instantiate(agentPrefab, new Vector3(-1,-1), Quaternion.identity);
        _reward = Instantiate(rewardPrefab, new Vector3(1,1), Quaternion.identity);
        var position = _agent.transform.position;
        var state = _sensor.GetData(position);
        _timeExpired = Time.time + timeout;
        rb = _agent.GetComponent<Rigidbody2D>();
        return state;
    }
    
    private Rigidbody2D rb;
    
    public (Vector nextState, float reward) Step(int action)
    {
        var direction = action switch
        {
            0 => Vector2.up,
            1 => Vector2.down,
            2 => Vector2.left,
            3 => Vector2.right,
            _ => Vector2.zero
        };
        
        rb.MovePosition(rb.position + direction * agentSpeed * Time.fixedDeltaTime);
        
        var position = rb.position;

        float reward;
        
        var mixX = position.x - 0.5;
        var maxX = position.x + 0.5;
        var minY = position.y - 0.5;
        var maxY = position.y + 0.5;
        
        var distance = Vector2.Distance(position, _reward.transform.position);
        
        if (mixX <= _reward.transform.position.x && maxX >= _reward.transform.position.x &&
            minY <= _reward.transform.position.y && maxY >= _reward.transform.position.y)
        {
            Success();
            reward = 10f;
        }
        else if (Time.time > _timeExpired)
        {
            Fail();
            reward = -10f;
        }
        else if (distance < pastDistance)
        {
            reward = +1f;
        }
        else
        {
            reward = -1f;
        }
        
        pastDistance = distance;
        
        var state = _sensor.GetData(position);
        return (state, reward);
    }
    
    public void Success()
    {
        _success += 1;
        _reward.transform.position = new Vector3(Random.Range(-3, +3),  Random.Range(-3f, 3f));
        _timeExpired = Time.time + timeout;
    }
    
    private void Fail()
    {
        _fail += 1;
        _agent.transform.position = new Vector3(Random.Range(-3f, +3f),  Random.Range(-3f, +3f));
        _reward.transform.position = new Vector3(Random.Range(-3f, +3f),  Random.Range(-3f, +3f));
        _timeExpired = Time.time + timeout;
    }
}
