using System;


namespace MyFirstProgram
{
    internal class Program
    {
        static void Main(string[] args)
        {

            InputValidation iv = new InputValidation();
            iv.Start();
            // Creates a loop to run the code
            while (true)
            {
                iv.NewLine();
                iv.SetInput(Console.ReadLine());
                iv.CheckFunctionType(iv.GetInput());
            }

        }
    }
}