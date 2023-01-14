using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Game Rule")]
    [SerializeField] [Range(0.1f, 2f)] private float gameSpeed;
    
    private Environment _environment;
    private Agent _agent;
    
    private Vector _state;
    
    private void Start()
    {
        Screen.fullScreen = false;
        Application.runInBackground = true;
    }
    
    private void Awake()
    {
        _environment = GetComponent<Environment>();
        _agent = GetComponent<Agent>();
        _state = _environment.Reset();
    }
    
    private void Update()
    {
        Time.timeScale = gameSpeed;
    }
    
    private void FixedUpdate()
    {
        var action = _agent.GetAction(_state);
        var (nextState, reward) = _environment.Step(action);
        _agent.Remember(_state, action, reward, nextState);
        _agent.Train();
        _state = nextState;
    }
}
