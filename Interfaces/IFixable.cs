using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal_Engines.Interfaces
{
    public interface IFixable
    {
        string Name { get; set; }
        float Condition { get; set; } 
        void Repair(float amount);
        string GetStatusReport(); 
        bool IsFine { get; }

        IEnumerable<string> GetActions();
        void ExecuteAction(string action);
    }
}
