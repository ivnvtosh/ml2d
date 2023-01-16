using System;

/*
 * multilayer perceptron
 */
public class NeuralNetwork
{
    private readonly int[] _layers;                           // the number of neurons in each layer
    private readonly float _alpha;                            // hyper parameter - learning rate
    private readonly Func<Vector, Vector> _activation;        // σ
    private readonly Func<Vector, Vector> _derivedActivation; // σ'
    private Matrix[] W;                                       // matrix of weights
    private Vector[] b;                                       // vector of bias
    
    private readonly Vector[] t;
    private readonly Vector[] h;
    private readonly Matrix[] dE_dW;
    private readonly Vector[] dE_db;
    
    public enum Activation
    {
        Relu
    }
    
    public NeuralNetwork(int[] layers, float learningRate, Activation activation)
    {
        _layers = layers;
        _alpha = learningRate;
        
        switch (activation)
        {
            case Activation.Relu:
                _activation = Relu;
                _derivedActivation = DerivedRelu;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(activation), activation, null);
        }

        var numOfLayers = _layers.Length - 1;
        
        W = new Matrix[numOfLayers];
        b = new Vector[numOfLayers];
        
        t = new Vector[_layers.Length - 2];
        h = new Vector[_layers.Length - 2];
        dE_dW = new Matrix[numOfLayers];
        dE_db = new Vector[numOfLayers];
        
        for (var i = 1; i < _layers.Length; i += 1)
        {
            W[i - 1] = Matrix.Random(_layers[i - 1], _layers[i]);
            b[i - 1] = Vector.Random(_layers[i]);
        }
    }

    public void GoSetWb(Matrix[] W, Vector[] b)
    {
        this.W = W;
        this.b = b;
    }
    
    /*
     * x - first layer input
     * z - last layer output
     * 
     * h = σ(Wx + b)
     * W - weights
     * x - layer
     * b - bias
     * σ - activation function
     * h - next layer
     * the predictions are a linear function followed by a sigmoidal nonlinearity.
     * the activation function does not apply to the last layer.
     */
    public Vector ForwardPropagation(Vector x)
    {
        t[0] = W[0].T * x + b[0];
        h[0] = _activation(t[0]);
        
        var i = 0;
        while (i < _layers.Length - 3)
        {
            t[i + 1] = W[i + 1].T * h[i] + b[i + 1];
            h[i + 1] = _activation(t[i + 1]);
            i += 1;
        }
        
        var z = W[i + 1].T * h[i] + b[i + 1];
        return Softmax(z);
    }

    /*
     * BackPropagation (for short backward propagation of errors)
     */
    public void BackPropagation(Vector x, Vector t)
    {
        var y = ForwardPropagation(x);
        
        var size = _layers.Length - 3;
        var dE_dt = y - t;
        Vector dE_dh;
        
        dE_dW[size + 1] = h[size].T * dE_dt.T.T;
        dE_db[size + 1] = dE_dt;
        size -= 1;
        for (var i = size; i > 0; i -= 1)
        {
            dE_dh = W[i + 1].T * dE_dt;
            dE_dt = dE_dh * _derivedActivation(this.t[i]);
            dE_dW[i] = h[i].T * dE_dt.T.T;
            dE_db[i] = dE_dt;
        }
        dE_dh = W[1] * dE_dt;
        dE_dt = dE_dh * _derivedActivation(this.t[0]);
        dE_dW[0] = x.T * dE_dt.T.T;
        dE_db[0] = dE_dt;

        // Update
        for (var i = 0; i < _layers.Length - 1; i += 1)
        {
            W[i] -= _alpha * dE_dW[i];
            b[i] -= _alpha * dE_db[i];
        }
    }
    
    private static float Relu(float t)
    {
        return t > 0 ? t : 0;
    }
    
    private static float DerivedRelu(float t)
    {
        return t >= 0 ? 1 : 0;
    }
    
    private static Vector Relu(Vector t)
    {
        var c = new Vector(t.size);
        
        for (var i = 0; i < t.size; i += 1)
        { 
            c[i] = Relu(t[i]);
        }
        
        return c;
    }
    
    private static Vector DerivedRelu(Vector a)
    {
        var c = new Vector(a.size);
        
        for (var i = 0; i < a.size; i += 1)
        { 
            c[i] = DerivedRelu(a[i]);
        }
        
        return c;
    }
    
    private static Vector Softmax(Vector a)
    {
        var c = new Vector(a.size);
        var sum = 0.0f;
        
        for (var i = 0; i < a.size; i += 1)
        {
            c[i] = UnityEngine.Mathf.Exp(a[i]);
            sum += c[i];
        }
        
        for (var i = 0; i < a.size; i += 1)
        {
            c[i] /= sum;
        }
        
        return c;
    }
}
