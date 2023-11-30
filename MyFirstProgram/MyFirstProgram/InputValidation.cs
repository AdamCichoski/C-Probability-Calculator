using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;


namespace MyFirstProgram
{
    /// <summary>
    /// This class controls the flow of information in the command line input
    /// </summary>
    public class InputValidation : Probability
    {

        private String user = "User";
        
        /// <summary>
        /// Checks the type of command that was inputted
        /// </summary>
        /// <param name="input"></param>
        public void CheckFunctionType(String[] input)
        {
            if (input[0] == "#")
            {
                Personalize(input);
            }
            else
            {
                ProbabilityCommands(input);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool ProbabilityCommands(String[] input)
        {
            bool length3 = input.Length == 3;
            bool length2 = input.Length == 2;
            double event1=-1;
            double event2=-1;
            if (length3)
            {
                try
                {
                    event1=Convert.ToDouble(input[1])/100.0;
                    event2=Convert.ToDouble(input[2])/100.0;
                }
                catch (FormatException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message+"\n");
                    return false;
                    
                }
            }
            else if(length2)
            {
                try
                {
                    event1 = Convert.ToDouble(input[1]) / 100.0;
                }
                catch (FormatException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message+"\n");

                    return false;

                }
            }
            Console.ForegroundColor= ConsoleColor.Yellow;
            switch (input[0])
            {
                case "or":
                    if (length3)
                    {
                        Console.WriteLine("P(A or B) = {0:###.##}%",Or(event1, event2));
                    }
                    else
                    {
                        PrintError(FunctionErrorString());
                    }
                    break;
                case "and":
                    if (length3)
                    {
                        Console.WriteLine("P(A and B) = {0:###.##}%",And(event1, event2));
                    }
                    else
                    {
                        PrintError(FunctionErrorString());
                    }
                    break;
                case "andn":
                    if (length3)
                    {
                        Console.WriteLine("P(A and ~B) = {0:###.##}%", AndNot(event1, event2));
                    }
                    else
                    {
                        PrintError(FunctionErrorString());
                    }
                    break;
                case "not":
                    Console.WriteLine( length3 ?"P~(A or B) = "+ Not(event1, event2)+"%" :length2 ? "P~(A) = " + Not(event1)+"%" : FunctionErrorString());
                    break;
                case "given":
                    if (length3)
                    {
                        Console.WriteLine("P(A | B) = {0:###.##}%",Given (event1, event2));
                    }
                    else
                    {
                        PrintError(FunctionErrorString());
                    } 
                    break;
                case "setx":
                    if (input.Length == 1)
                    {
                        SetExclusive(!GetExclusive());
                        Console.WriteLine(GetExclusive() ? "--> Function is exclusive" : "--> Function is not exclusive");
                    }
                    else
                    {
                        PrintError(FunctionErrorString());
                    }
                    break;
                case "help":
                    Help();
                    break;
                case "?":
                    Help();
                    break;
                default:
                    PrintError(FunctionErrorString());
                    break;

            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            return true;
        }

        /// <summary>
        /// Personalization functions:
        /// Change name, color themes, personal infomration, etc. 
        /// </summary>
        /// <param name="input"></param>
        private void Personalize(String[] input)
        {
            if(input.Length == 4)
            {
                switch (input[1])
                {
                case "set":
                    SetValue(input);
                    break;
                default:
                    PrintError(SetInputError()+" at personalize");
                    break;
                }
            }
            else
            {
                PrintError(SetLengthError());
            }
            
        }

        /// <summary>
        ///  Sets a personal value based on the users input
        /// </summary>
        /// <param name="input"></param>
        private void SetValue(String[] input)
        {
            String update = input[3];
            switch (input[2])
            {
                case "name":
                    SetUser(update);
                    break;
                case "background":
                    SetBackground(input[3]);
                    break;
                default: 
                    PrintError(SetInputError()+" at set value");
                    break;
            }
        }

        /// <summary>
        /// Takes in a string that determines what color the console background will be set to 
        /// </summary>
        /// <param name="color"></param>
        private void SetBackground(String color)
        {
            switch (color)
            {
                case "red":
                    Console.BackgroundColor = ConsoleColor.Red; 
                    break;
                case "white":
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
                case "black":
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                default:
                    Console.WriteLine("**Invalid Color Input**");
                    break;
            }
        }

        /// <summary>
        /// This method sets the foreground color based on a command input
        /// </summary>
        /// <param name="color"></param>
        public void SetForground(String color)
        {
            switch(color) 
            { 
            case "red":
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case "white":
                Console.ForegroundColor = ConsoleColor.White;
                break;
            case "black":
                Console.ForegroundColor = ConsoleColor.Black;
                break;
            default:
                Console.WriteLine("**Invalid Color Input**");
                break;
            }
        }

        /// <summary>
        ///  This prints out a list of all of the acceptable inputs
        /// </summary>
        public void Help()
        {
            String a = "[A: numeric] [B: numeric]";
            String b = "[A: numeric]";
            Console.WriteLine("Probability Calculator Commands: ");
            Console.WriteLine(""
                + "and   {0} ; Probability of A and B \n"
                + "or    {1} ; Probability of A or B \n"
                + "andn  {2} ; Probability of A and not B \n"
                + "not   {3}              ; Probability of not A\n"
                + "not   {4} ; Probability of not A or B \n"
                + "given {5} ; Probability of A given B\n"
                + "setx                            ; Sets the function to exclusive\n" +
                  "help / ?                        ; prints out a list of all valid inputs", a,a,a,b,a,a);

            Console.WriteLine("\n\n"
                + "Personalization and Set Up:\n"
                + "# set name [String]   ; Sets the users name");
        }

        /********************
         * Methods to make it more clear when the color of the foreground is changing
         *******************/
        private void ConsoleRed()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }

        private void ConsoleYellow()
        {
            Console.ForegroundColor = ConsoleColor.Yellow; 
        }

        private void ConsoleWhite()
        {
            Console.ForegroundColor= ConsoleColor.White;
        }
        
        /****************
         * End of color change
         * **************/

        /// <summary>
        /// Sets the users name 
        /// </summary>
        /// <param name="user"></param>
        public void SetUser(String user)
        {
            this.user = user;
        }

        /// <summary>
        /// Gets the users name 
        /// </summary>
        /// <returns></returns>
        public String GetUser()
        {
            return this.user;
        }

        /// <summary>
        /// An error to be printed when the length of a set command in invalid
        /// </summary>
        /// <returns>
        /// Error String
        /// </returns>
        private String SetLengthError()
        {
            return "**Set Command Length Invalid**";
        }

        /// <summary>
        /// An error to be printed when the input for a set command is invalid
        /// </summary>
        /// <returns>
        /// Error String
        /// </returns>
        private String SetInputError()
        {
            return "**Invalid Input For Setting Value**";
        }

        /// <summary>
        /// Prints an error on the screen for invalid functions 
        /// </summary>
        /// <returns></returns>
        private String FunctionErrorString()
        {
            return "**Invalid Input: Function Undefined**";
        }

        /// <summary>
        /// This prints out that there was an error in the input
        /// </summary>
        /// <param name="error">
        /// The specific error that will be printed
        /// </param>
        private void PrintError(String error)
        {
            ConsoleRed();
            Console.WriteLine(error);
            ConsoleWhite();
        }

        /// <summary>
        /// This method creates a new line to write on
        /// </summary>
        public void NewLine()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\n"+GetUser());
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("$ ");
            ConsoleWhite();
        }

    }
}
