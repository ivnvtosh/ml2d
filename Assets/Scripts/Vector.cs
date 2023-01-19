public class Vector
{
    public readonly int size;
    private readonly float[] _data;
    
    public Vector(float[] data)
    {
        size = data.Length;
        _data = data;
    }
    
    public Vector(int size)
    {
        this.size = size;
        _data = new float[size];
    }
    
    // Parse
    public Vector(string path)
    {
        var reader = new System.IO.StreamReader(path);
        var data = reader.ReadToEnd();
        var lines = data.Split("\n");
        reader.Close();

        size = int.Parse(lines[0]);
        _data = new float[size];

        for (var i = 0; i < size; i += 1)
        {
            _data[i] = float.Parse(lines[i + 1]);
        }
    }
    
    public float this[int i]
    {
        get => _data[i];
        set => _data[i] = value;
    }

    public int Argmax
    {
        get
        {
            var max = _data[0];
            var index = 0;

            for (var i = 1; i < size; i += 1)
            {
                if (max < _data[i])
                {
                    max = _data[i];
                    index = i;
                }
            }

            return index;
        }
    }
    
    public Matrix T
    {
        get
        {
            var c = new Matrix(size, 1);
            
            for (var i = 0; i < size; i += 1)
            {
                c[i, 0] = _data[i];
            }
            
            return c;
        }
    }
    
    public static Vector Random(int size)
    {
        var a = new Vector(size);
        
        for (var i = 0; i < size; i += 1)
        {
            a[i] = UnityEngine.Random.Range(-1.0f, 1.0f);
        }
        
        return a;
    }
    
    public override string ToString()
    {
        var description = "";
        
        description += "size: " + size + "\n";
        for (var i = 0; i < size - 1; i += 1)
        {
            description += _data[i] + " ";
        }
        
        description += _data[size - 1] + "\n";
        return description;
    }
    
    public static Vector operator *(float k, Vector b)
    {
        var size = b.size;
        var c = new Vector(size);
        
        for (var i = 0; i < size; i += 1)
        {
            c[i] = k * b[i];
        }
        
        return c;
    }
    
    public static Vector operator +(Vector a, Vector b)
    {
        if (a.size != b.size)
        {
            throw new System.ArgumentException();
        }
        
        var size = a.size;
        var c = new Vector(size);
        
        for (var i = 0; i < size; i += 1)
        {
            c[i] = a[i] + b[i];
        }
        
        return c;
    }
    
    public static Vector operator -(Vector a, Vector b)
    {
        if (a.size != b.size)
        {
            throw new System.ArgumentException();
        }
        
        var size = a.size;
        var c = new Vector(size);
        
        for (var i = 0; i < size; i += 1)
        {
            c[i] = a[i] - b[i];
        }
        
        return c;
    }
    
    public static Vector operator *(Vector a, Vector b)
    {
        if (a.size != b.size)
        {
            throw new System.ArgumentException();
        }
        
        var size = a.size;
        var c = new Vector(size);
        
        for (var i = 0; i < size; i += 1)
        {
            c[i] = a[i] * b[i];
        }
        
        return c;
    }
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        
        var b = (Vector) obj;
        
        if (size != b.size)
        {
            throw new System.ArgumentException();
        }
        
        for (var i = 0; i < size; i += 1)
        {
            if (UnityEngine.Mathf.Approximately(_data[i], b[i]) == false)
            {
                return false;
            }
        }
        
        return true;
    }

    public override int GetHashCode()
    {
        return System.Collections.Generic.EqualityComparer<float[]>.Default.GetHashCode(_data) +
               System.Collections.Generic.EqualityComparer<int>.Default.GetHashCode(size) + 
               System.Collections.Generic.EqualityComparer<Vector>.Default.GetHashCode(this);
    }
}
