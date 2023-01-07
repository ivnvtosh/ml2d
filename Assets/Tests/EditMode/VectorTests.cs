using NUnit.Framework;

namespace Tests.EditMode
{
    public class VectorTests
    {
        [Test]
        public void Transposed()
        {
            var a = new Vector(new float[] {2f, 3f});
            var D = new Matrix(2)
            {
                [0, 0] = 2f,
                [1, 0] = 3f
            };
            
            var C = a.T;
            
            Assert.AreEqual(D, C);
        }
        
        [Test]
        public void Addition()
        {
            var a = new Vector(new float[] {2f, 3f});
            var b = new Vector(new float[] {-1f, 6f});
            var d = new Vector(new float[] {1f, 9f});

            var c = a + b;
            
            Assert.AreEqual(d, c);
        }
        
        [Test]
        public void Subtraction()
        {
            var a = new Vector(new float[] {2f, 3f});
            var b = new Vector(new float[] {-1f, 6f});
            var d = new Vector(new float[] {3f, -3f});
            
            var c = a - b;
            
            Assert.AreEqual(d, c);
        }
        
        [Test]
        public void Multiplication_float_00()
        {
            var a = new Vector(new float[] {2f, 3f});
            const float k = 2f;
            var d = new Vector(new float[] {4f, 6f});
            
            var c = k * a;
            
            Assert.AreEqual(d, c);
        }
        
        [Test]
        public void Multiplication_Vector_00()
        {
            var a = new Vector(new float[] {2f, 3f});
            var b = new Vector(new float[] {-1f, 6f});
            var d = new Vector(new float[] {-2f, 18f});
            
            var c = a * b;
            
            Assert.AreEqual(d, c);
        }
    }
}
