using System.Collections.Generic;
namespace CoreLibrary.Cores
{
    public class CounterModel<T>
    {
        public int Total { get; set; }
        public IEnumerable<T> Items { get; set; }
        
    }
}
