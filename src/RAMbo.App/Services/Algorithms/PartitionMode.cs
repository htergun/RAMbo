namespace RAMbo.App.Services.Algorithms;

public enum PartitionMode
{
    /// <summary>Her blok en fazla bir process alır; blokta artan boşluk internal fragmentation'dır.</summary>
    Fixed,

    /// <summary>Blok process boyutu kadar kullanılır, kalan boşluk başka process'lere açıktır (internal = 0).</summary>
    Variable
}