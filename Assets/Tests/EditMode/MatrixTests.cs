using NUnit.Framework;

namespace Tests.EditMode
{
    public class MatrixTest
    {
        [Test]
        public void Transposed()
        {
            var A = new Matrix(2, 2)
            {
                [0, 0] = 1f,
                [0, 1] = 3f,
            
                [1, 0] = -6f,
                [1, 1] = 2f
            };
        
            var D = new Matrix(2, 2)
            {
                [0, 0] = 1f,
                [0, 1] = -6f,
            
                [1, 0] = 3f,
                [1, 1] = 2f
            };
        
            var C = A.T;
        
            Assert.AreEqual(D, C);
        }
    
        [Test]
        public void Addition()
        {
            var A = new Matrix(2, 2)
            {
                [0, 0] = 1f,
                [0, 1] = 3f,
            
                [1, 0] = -6f,
                [1, 1] = 2f
            };

            var B = new Matrix(2, 2)
            {
                [0, 0] = 7f,
                [0, 1] = -2f,
            
                [1, 0] = 4f,
                [1, 1] = 7f
            };
        
            var D = new Matrix(2, 2)
            {
                [0, 0] = 8f,
                [0, 1] = 1f,
            
                [1, 0] = -2f,
                [1, 1] = 9f
            };

            var C = A + B;
        
            Assert.AreEqual(D, C);
        }
    
        [Test]
        public void Subtraction()
        {
            var A = new Matrix(2, 2)
            {
                [0, 0] = 1f,
                [0, 1] = 3f,
            
                [1, 0] = -6f,
                [1, 1] = 2f
            };

            var B = new Matrix(2, 2)
            {
                [0, 0] = 7f,
                [0, 1] = -2f,
            
                [1, 0] = 4f,
                [1, 1] = 7f
            };
        
            var D = new Matrix(2, 2)
            {
                [0, 0] = -6f,
                [0, 1] = 5f,
            
                [1, 0] = -10f,
                [1, 1] = -5f
            };

            var C = A - B;
        
            Assert.AreEqual(D, C);
        }

        [Test]
        public void Multiplication_float_00()
        {
            var A = new Matrix(2, 2)
            {
                [0, 0] = 1f,
                [0, 1] = 3f,
            
                [1, 0] = -6f,
                [1, 1] = 2f
            };
        
            const float k = 2f;
            var D = new Matrix(2, 2)
            {
                [0, 0] = 2f,
                [0, 1] = 6f,
            
                [1, 0] = -12f,
                [1, 1] = 4f
            };
        
            var C = k * A;
        
            Assert.AreEqual(D, C);
        }
    
        [Test]
        public void Multiplication_Vector_00()
        {
            var A = new Matrix(2, 2)
            {
                [0, 0] = 1f,
                [0, 1] = 3f,
            
                [1, 0] = -6f,
                [1, 1] = 2f
            };
        
            var b = new Vector(new float[] { 3f, 2f });
            var d = new Vector(new float[] { 9f, -14f });
        
            var c = A * b;
        
            Assert.AreEqual(d, c);
        }
        
        [Test]
        public void Multiplication_Matrix_00()
        {
            var A = new Matrix(2, 2)
            {
                [0, 0] = 1f,
                [0, 1] = 3f,
            
                [1, 0] = -6f,
                [1, 1] = 2f
            };

            var B = new Matrix(2, 2)
            {
                [0, 0] = 7f,
                [0, 1] = -2f,
            
                [1, 0] = 4f,
                [1, 1] = 7f
            };
        
            var D = new Matrix(2, 2)
            {
                [0, 0] = 19f,
                [0, 1] = 19f,
            
                [1, 0] = -34f,
                [1, 1] = 26f
            };

            var C = A * B;
        
            Assert.AreEqual(D, C);
        }
    }
}
