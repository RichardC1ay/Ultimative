using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultimative.Universal.Net
{
    public class MultiFileTask
    {
        private bool _isConsumed = false;

        public string Guid;
        public int Total;
        public volatile int Done;
        public bool IsConsumed { get { return _isConsumed; } }

        public MultiFileTask(string guid, int total)
        {
            Guid = guid;
            Total = total;
            Done = 0;
        }

        public void Consume()
        {
            _isConsumed = true;
        }
    }
}
