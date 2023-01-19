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
            neuralNetwork.LoadFromFile("Assets/Tests/EditMode/NeuralNetwork_00/");
            var x = new Vector(new float[2] { 2f, 3f });
            var d = new Vector(new float[1] { 0.21f });
            
            var y = neuralNetwork.ForwardPropagation(x);
            
            Assert.AreEqual(d, y);
        }
        
        [Test]
        public void BackPropagation_00()
        {
            var layers = new int[3];
            layers[0] = 2;
            layers[1] = 2;
            layers[2] = 1;
            var neuralNetwork = new NeuralNetwork(layers, 0.05f, NeuralNetwork.Activation.Relu);
            neuralNetwork.LoadFromFile("Assets/Tests/EditMode/NeuralNetwork_00/");
            var x = new Vector(new float[2] { 2f, 3f });
            var t = new Vector(new float[1] { 1f });
            var d = new Vector(new float[1] { 0.32185965f });
            
            neuralNetwork.BackPropagation(x, t);
            var y = neuralNetwork.ForwardPropagation(x);
            
            Assert.AreEqual(d, y);
        }
        
        [Test]
        public void ForwardPropagation_01()
        {
            var layers = new int[3];
            layers[0] = 32;
            layers[1] = 128;
            layers[2] = 4;
            var neuralNetwork = new NeuralNetwork(layers, 0.05f, NeuralNetwork.Activation.Relu);
            neuralNetwork.LoadFromFile("Assets/Tests/EditMode/NeuralNetwork_01/");
            var x = new Vector(new float[32] { 0.86f, 0.05f, 0.1f,  0.06f, 0.31f, 0.26f, 0.42f, 0.56f, 0.5f, 0.22f,
                0.02f, 0.18f, 0.96f, 0.23f, 0.52f, 0.95f, 0.46f, 0.19f, 0.84f, 0.91f, 0.38f, 0.92f, 0.53f, 0.58f, 0.74f,
                0.89f, 0.35f, 0.41f, 0.56f, 0.42f, 0.01f, 0.46f });
            var d = new Vector(new float[4] { 512.69f, 486.03f, 472.04f, 494.63f });
            
            var y = neuralNetwork.ForwardPropagation(x);
            
            Assert.AreEqual(d, y);
        }
    }
}
