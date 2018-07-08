using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.MongoFillingService.Expense;
using MyDiary.MongoFillingService.Income;
using MyDiary.MongoFillingService.Parser;

namespace MyDiary.MongoFillingService
{
    class Program
    {
        static void Main(string[] args)
        {
           

          //string input =  Console.ReadLine();

          //string[] inputArgs = input.Split(' ').ToArray<string>();

           GetParsedOtions(args);

           Console.WriteLine(args);

            string userInput;
            Console.WriteLine("-Run Income/Expense I/E ?");
            userInput = Console.ReadLine();
            switch (userInput.ToUpper().Trim())
            {
                case "I":
                    Console.WriteLine("Are you sure you want to run the Income Initializer Y/N ?");
                    userInput = Console.ReadLine();
                    switch (userInput.ToUpper().Trim())
                    {
                        case "Y":
                            Income.Income.Fill();
                            break;
                        case "N":
                            break;
                        default:
                            break;
                    }
                    break;
                case "E":
                    Console.WriteLine("Are you sure you want to run the Expense Initializer Y/N ?");
                    userInput = Console.ReadLine();
                    switch (userInput.ToUpper().Trim())
                    {
                        case "Y":
                            Expense.Expense.Fill();
                            break;
                        case "N":
                            break;
                        default:
                            break;
                    }
                    break;
            }
        }

        private static void GetParsedOtions(string[] args)
        {
            if (args.Length > 0)
            {
                // create a generic parser for the ApplicationArguments type
                var parser = new Fclp.FluentCommandLineParser<MyDiaryOptions>();

                // specify which property the value will be assigned too.
                parser.Setup<FileTypeEnum>(arg => arg.FileType)
                      .As('f', "fileType") ;// define the short and long option name
                      //.Required()// using the standard fluent Api to declare this Option as required.
                      // .SetDefault("Production");

                parser.Setup(arg => arg.Option)
                      .As('r', "Run")
                      .Required();
                      
                var result = parser.Parse(args);

                parser.SetupHelp("?", "help")
                      .Callback(text => Console.WriteLine(text));

                // triggers the SetupHelp Callback which writes the text to the console
                parser.HelpOption.ShowHelp(parser.Options);

                var myDiaryOptions = parser.Object;

                switch (myDiaryOptions.FileType)
                {
                    case FileTypeEnum.Production:
                        var a = 1;

                        break;

                    case FileTypeEnum.Submission:
                        var b = 2;
                        break;


                }
                
               
                if (result.HasErrors == false)
                {
                    // use the instantiated ApplicationArguments object from the Object property on the parser.
                    // application.Run(p.Object);
                }

            }
        }
    }
}
