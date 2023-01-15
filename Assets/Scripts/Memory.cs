using System.Collections.Generic;

public class Memory
{
    private (Vector state, int action, float reward, Vector nextState)[] _store;
    private readonly int _capacity;
    private readonly int _batchSize;
    private int _size;
    private int _everyNTimes;
    
    public Memory(int capacity, int batchSize)
    {
        _capacity = capacity;
        _batchSize = batchSize;
        _store = new (Vector state, int action, float reward, Vector nextState)[capacity];
        _size = 0;
        _everyNTimes = 4;
    }
    
    public void Append((Vector state, int action, float reward, Vector nextState) store)
    {
        if (_everyNTimes < 4)
        {
            _everyNTimes += 1;
            return;
        }
        
        if (_size == _capacity)
        {
            Clear();
        }
        
        _everyNTimes = 0;
        _store[_size] = store;
        _size += 1;
    }
    
    private void Clear()
    {
        _store = new (Vector state, int action, float reward, Vector nextState)[_capacity];
        _size = 0;
    }
    
    public (Vector state, int action, float reward, Vector nextState)[] GetBatch()
    {
        var size = this._size < _batchSize ? this._size : _batchSize;
        var indexes = new int[size];
        
        for (var i = 0; i < size; i += 1)
        {
            indexes[i] = i;
        }
        
        for (var i = 0; i < size; i += 1)
        {
            var j = UnityEngine.Random.Range(0, size);
            (indexes[j], indexes[i]) = (indexes[i], indexes[j]);
        }
        
        var result = new (Vector state, int action, float reward, Vector nextState)[size];
        for (var i = 0; i < size; i += 1)
        {
            result[i] = _store[indexes[i]];
        }
        
        return result;
    }
}
