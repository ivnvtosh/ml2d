using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable] public class Agent : MonoBehaviour
{
    [Header("NeuralNetwork")]
    [SerializeField] private int[] layers;
    [SerializeField] private float learningRate;
    
    [SerializeField] private float _gamma;
    [SerializeField] private float _epsilon;
    [SerializeField] private float _epsilonMin;
    [SerializeField] private float _epsilonDecay;
    
    [Header("Memory")]
    [SerializeField] private int capacity;
    [SerializeField] private int batchSize;
    
    private Memory _memory;
    private NeuralNetwork _neuralNetwork;
    
    private Vector2 position;
    
    private void Start()
    {
        _memory = new Memory(capacity, batchSize);
        _neuralNetwork = new NeuralNetwork(layers, learningRate, NeuralNetwork.Activation.Relu);
    }

    public int GetAction(Vector state)
    {
        if (Random.Range(0, 1) <= _epsilon)
        {
            return Random.Range(0, layers.Last());
        }
        
        var action = _neuralNetwork.ForwardPropagation(state);
        return action.Argmax;
    }

    public void Remember(Vector state, int action, float reward, Vector nextState)
    {
        var experience = (state, action, reward, nextState);
        _memory.Append(experience);
    }
    
    public void Train()
    {
        foreach (var (state, action, reward, nextState) in _memory.GetBatch())
        {
            var target = _neuralNetwork.ForwardPropagation(state);
            // var targetNext = _neuralNetwork.ForwardPropagation(nextState);
            
            target[action] = reward;
            
            _neuralNetwork.BackPropagation(state, target);
        }
        
        if (_epsilon > _epsilonMin)
        {
            _epsilon *= _epsilonDecay;
        }
    }
}
