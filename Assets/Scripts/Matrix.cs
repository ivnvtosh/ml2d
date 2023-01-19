public class Matrix
{
    public readonly int m;
    public readonly int n;
    private readonly float[,] _data;
    
    public Matrix(int m, int n = 1)
    {
        _data = new float[m, n];
        this.m = m;
        this.n = n;
    }
    
    // Parse
    public Matrix(string path)
    {
        var reader = new System.IO.StreamReader(path);
        var data = reader.ReadToEnd();
        var lines = data.Split("\n");
        reader.Close();

        m = int.Parse(lines[0]);
        n = int.Parse(lines[1]);
        _data = new float[m, n];
        
        for (var i = 0; i < m; i += 1)
        {
            var words = lines[i + 2].Split(" ");
            var k = 0;
            for (var j = 0; j < n; j += 1)
            {
                while (words[j + k] == "")
                {
                    k += 1;
                }
                
                _data[i, j] = float.Parse(words[j + k]);
            }
        }
    }
    
    public float this[int i, int j]
    {
        get => _data[i, j];
        set => _data[i, j] = value;
    }
    
    public Matrix T
    {
        get
        {
            var c = new Matrix(n, m);
            
            for (var i = 0; i < m; i += 1)
            {
                for (var j = 0; j < n; j += 1)
                {
                    c[j, i] = _data[i, j];
                }
            }
            
            return c;
        }
    }
    
    public static Matrix Random(int m, int n)
    {
        var a = new Matrix(m, n);
        
        for (var i = 0; i < m; i += 1)
        {
            for (var j = 0; j < n; j += 1)
            {
                a[i, j] = UnityEngine.Random.Range(-1.0f, 1.0f);
            }
        }
        
        return a;
    }
    
    public override string ToString()
    {
        var description = "";
        
        description += "m: " + m + "\n";
        description += "n: " + n + "\n";
        for (var i = 0; i < m; i += 1)
        {
            for (var j = 0; j < n - 1; j += 1)
            {
                description += _data[i, j] + " ";
            }
            
            description += _data[i, n - 1] + "\n";
        }
        
        return description;
    }

    public static Matrix operator *(float k, Matrix a)
    {
        var m = a.m;
        var n = a.n;
        var c = new Matrix(m, n);
        
        for (var i = 0; i < m; i += 1)
        {
            for (var j = 0; j < n; j += 1)
            {
                c[i, j] = k * a[i, j];
            }
        }
        
        return c;
    }
    
    public static Vector operator *(Vector b, Matrix a)
    {
        if (a.n != b.size)
        {
            throw new System.ArgumentException();
        }
        
        var size = b.size;
        var c = new Vector(size);
        
        for (var i = 0; i < size; i += 1)
        {
            for (var k = 0; k < size; k += 1)
            {
                c[i] += b[k] * a[k, i];
            }
        }
        
        return c;
    }
    
    public static Vector operator *(Matrix a, Vector b)
    {
        if (a.n != b.size)
        {
            throw new System.ArgumentException();
        }
        
        var size = a.m;
        var c = new Vector(size);
        
        for (var i = 0; i < size; i += 1)
        {
            for (var k = 0; k < b.size; k += 1)
            {
                c[i] += a[i, k] * b[k];
            }
        }
        
        return c;
    }
    
    public static Matrix operator +(Matrix a, Matrix b)
    {
        if (a.n != b.n || a.m != b.m)
        {
            throw new System.ArgumentException();
        }
        
        var m = a.m;
        var n = a.n;
        var c = new Matrix(m, n);
        
        for (var i = 0; i < m; i += 1)
        {
            for (var j = 0; j < n; j += 1)
            {
                c[i, j] = a[i, j] + b[i, j];
            }
        }
        
        return c;
    }
    
    public static Matrix operator -(Matrix a, Matrix b)
    {
        if (a.n != b.n || a.m != b.m)
        {
            throw new System.ArgumentException();
        }
        
        var m = a.m;
        var n = a.n;
        var c = new Matrix(m, n);
        
        for (var i = 0; i < m; i += 1)
        {
            for (var j = 0; j < n; j += 1)
            {
                c[i, j] = a[i, j] - b[i, j];
            }
        }
        
        return c;
    }

    public static Matrix operator *(Matrix a, Matrix b)
    {
        if (a.n != b.m)
        {
            throw new System.ArgumentException();
        }
        
        var m = a.m;
        var n = b.n;
        var c = new Matrix(m, n);
        
        for (var i = 0; i < m; i += 1)
        {
            for (var j = 0; j < n; j += 1)
            {
                for (var k = 0; k < a.n; k += 1)
                {
                    c[i, j] += a[i, k] * b[k, j];
                }
            }
        }
        
        return c;
    }
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        
        var b = (Matrix) obj;
        
        if (m != b.m || n != b.n)
        {
            throw new System.ArgumentException();
        }
        
        for (var i = 0; i < m; i += 1)
        {
            for (var j = 0; j < n; j += 1)
            {
                if (UnityEngine.Mathf.Approximately(_data[i, j], b[i, j]) == false)
                {
                    return false;
                }
            }
        }
        
        return true;
    }

    public override int GetHashCode()
    {
        return System.Collections.Generic.EqualityComparer<float[,]>.Default.GetHashCode(_data) + 
               System.Collections.Generic.EqualityComparer<int>.Default.GetHashCode(n) +
               System.Collections.Generic.EqualityComparer<int>.Default.GetHashCode(m) +
               System.Collections.Generic.EqualityComparer<Matrix>.Default.GetHashCode(this);
    }
}
