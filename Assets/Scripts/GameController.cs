using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject agentPrefab;
    [SerializeField] private GameObject rewardPrefab;
    
    [Header("Game Rule")]
    [SerializeField] [Range(0.1f, 2f)] private float gameSpeed;
    [SerializeField] [Range(0.1f, 8f)] private float agentSpeed;
    [SerializeField] [Range(2.0f, 8f)] private int timeout;

    private GameObject _agent;
    private GameObject _reward;
    
    private int _success = 0;
    private int _fail = 0;
    
    private float _timeExpired;

    // Start is called before the first frame update
    private void Start()
    {
        _agent = Instantiate(agentPrefab, new Vector3(-1,-1), Quaternion.identity);
        _reward = Instantiate(rewardPrefab, new Vector3(1,1), Quaternion.identity);
        _timeExpired = Time.time + timeout;
    }
    
    // Update is called once per frame
    private void Update()
    {
        Time.timeScale = gameSpeed;
        
        if (_timeExpired < Time.time)
        {
            Fail();
        }
    }
    
    public void Success()
    {
        Debug.Log("success");
        _success += 1;
        ReSpawn();
    }
    
    private void Fail()
    {
        Debug.Log("fail");
        _fail += 1;
        ReSpawn();
    }
    
    private void ReSpawn()
    {
        _agent.transform.position = new Vector3(Random.Range(-3, +3),  Random.Range(-3f, 3f));
        _reward.transform.position = new Vector3(Random.Range(-3, +3),  Random.Range(-3f, 3f));
        _timeExpired = Time.time + timeout;
    }
}
