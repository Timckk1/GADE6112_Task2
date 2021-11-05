using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6112_Task_1.Classes
{
    public interface View
    {
        
            void Info(string text);
            void DeleteTile(Tile t);
            void EndGame();
        
    }
}
