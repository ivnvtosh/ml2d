using System;
using UnityEngine;

[Serializable] public class Agent : MonoBehaviour
{
    [Header("NeuralNetwork")]
    [SerializeField] private int[] layers;
    [SerializeField] private float learningRate;
    
    [Header("Memory")]
    [SerializeField] private int capacity;
    [SerializeField] private int batchSize;
    
    private Memory _memory;
    private NeuralNetwork _neuralNetwork;
    
    private Vector2 position;
    
    private void Start()
    {
        _memory = new Memory(capacity, batchSize);
        _neuralNetwork = new NeuralNetwork(layers, learningRate, NeuralNetwork.ActivationFunction.Relu);
    }

    public Vector GetAction(Vector state)
    {
        var action = _neuralNetwork.ForwardPropagation(state);
        return action;
    }

    public void Remember(Vector state, Vector action, float reward, Vector nextState)
    {
        
    }
    
    public void Train()
    {
        foreach (var (state, action, reward, nextState) in _memory.GetBatch())
        {
            _neuralNetwork.BackPropagation(state, nextState);
        }
    }
}
