using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable] public class Agent : MonoBehaviour
{
    [Header("NeuralNetwork")]
    [SerializeField] private int[] layers;        // the number of layers and neurons in the perceptron
    [SerializeField] private float learningRate;  // alpha - learning rate
    
    [SerializeField] private float _gamma;        // discount - how much future states affect rewards
    [SerializeField] private float _epsilon;      // chance of picking random action
    [SerializeField] private float _epsilonMin;   // min chance value
    [SerializeField] private float _epsilonDecay; // chance decay amount for each update
    
    [Header("Memory")]
    [SerializeField] private int capacity;        // memory capacity
    [SerializeField] private int batchSize;       // 
    
    private Memory _memory;                       // memory - array of past actions and rewards
    private NeuralNetwork _neuralNetwork;         // neural network - multilayer perceptron
    
    private Vector2 position;
    
    private void Start()
    {
        _memory = new Memory(capacity, batchSize);
        _neuralNetwork = new NeuralNetwork(layers, learningRate, NeuralNetwork.Activation.Relu);
    }

    public int GetAction(Vector state)
    {
        if (Random.Range(0f, 1f) <= _epsilon)
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
            var targetNext = _neuralNetwork.ForwardPropagation(nextState);
            
            target[action] = reward + _gamma * targetNext[targetNext.Argmax];
            // target[action] = reward;
            
            _neuralNetwork.BackPropagation(state, target);
        }
        
        if (_epsilon > _epsilonMin)
        {
            _epsilon *= _epsilonDecay;
        }
    }
}
