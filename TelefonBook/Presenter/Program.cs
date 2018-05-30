using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelefonBook;

namespace ConsoleView {
    
        class Program {
            static void Main(string[] args) {
                ConsoleAction view = new ConsoleAction();
            Presenter presenter = new Presenter(view);
            view.Run();
            }
        

    }
}
