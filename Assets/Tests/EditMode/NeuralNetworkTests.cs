using NUnit.Framework;

namespace Tests.EditMode
{
    public class NeuralNetworkTests
    {
        [Test]
        public void ForwardPropagation_00()
        {
            var layers = new int[3];
            layers[0] = 2;
            layers[1] = 2;
            layers[2] = 1;
            
            var neuralNetwork = new NeuralNetwork(layers, 0.05f, NeuralNetwork.Activation.Relu);
            var x = new Vector(new float[2] { 2f, 3f });
            var d = new Vector(new float[1] { 0.21f });
            
            var W = new Matrix[2];
            
            W[0] = new Matrix(2, 2)
            {
                [0, 0] = 0.11f,
                [0, 1] = 0.12f,
                
                [1, 0] = 0.21f,
                [1, 1] = 0.08f,
            };
            
            W[1] = new Matrix(2)
            {
                [0, 0] = 0.14f,
                [1, 0] = 0.15f,
            };
            
            var b = new Vector[2];
            
            b[0] = new Vector(new float[2] { 0.07f, 0.05f });
            b[1] = new Vector(new float[1] { 0.0017f });
            
            neuralNetwork.GoSetWb(W, b);
            
            var z = neuralNetwork.ForwardPropagation(x);
            
            Assert.AreEqual(d, z);
        }
        
        [Test]
        public void BackPropagation_00()
        {
            var layers = new int[3];
            layers[0] = 2;
            layers[1] = 2;
            layers[2] = 1;
            
            var neuralNetwork = new NeuralNetwork(layers, 0.05f, NeuralNetwork.Activation.Relu);
            var x = new Vector(new float[2] { 2f, 3f });
            var d = new Vector(new float[1] { 0.32185965f });
            
            var W = new Matrix[2];
            
            W[0] = new Matrix(2, 2)
            {
                [0, 0] = 0.11f,
                [0, 1] = 0.12f,
                
                [1, 0] = 0.21f,
                [1, 1] = 0.08f,
            };
            
            W[1] = new Matrix(2)
            {
                [0, 0] = 0.14f,
                [1, 0] = 0.15f,
            };
            
            var b = new Vector[2];
            
            b[0] = new Vector(new float[2] { 0.07f, 0.05f });
            b[1] = new Vector(new float[1] { 0.0017f });
            
            var t = new Vector(new float[1] { 1f });
            
            neuralNetwork.GoSetWb(W, b);
            
            neuralNetwork.BackPropagation(x, t);
            
            var z = neuralNetwork.ForwardPropagation(x);
            
            Assert.AreEqual(d, z);
        }
    }
}
