using KebabCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabCasePlayground {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Sample App to conver text to a Kebab Case");
            Console.WriteLine();
            goto GetInput;


            GetInput:
            Console.Write("Enter any text: ");
            string input = Console.ReadLine();
            Console.WriteLine(input.ToKebabCase() + "\n\n");
            goto GetInput;
        }
    }
}
