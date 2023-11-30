using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstProgram
{
    /**
     * This class holds the methods to run probability problems through a command line
     */
    public class Probability 
    {
        public String[] input;
        private double both;
        private bool exclusive = false;

        /**
         * Standard constructor that sets both to zero
         */
        public Probability()
        {
            this.both = 0;
        }
        /**
         * Constructor to set the probability of both events happening
         */
        public Probability(double both)
        {
            this.both = both;
        }

        /**
         * Calculates a non-mutually exclusive or probability
         */
        public double Or(double event1, double event2)
        {
            return event1 + event2*100;
        }

        /**
         * Calculates an and probability
         */
        public double And (double event1, double event2)
        {
            return this.exclusive? 0: event1 * event2 * 100;
        }

        /**
         * Calculates the probability of one event happening and not the other
         */
        public double AndNot(double event1, double event2)
        {
            return event1 * (1 - event2) *100;
        }

        /**
         * Calculates the probability of an event not happening at all
         */
        public double Not(double event1)
        {
            return (1 - (event1))*100;
        }

        /**
         * Calculates the probability of two events not happening
         * EX: P ~ (A or B)
         */
        public double Not(double event1, double event2)
        {
            return (1 - (event1 + event2 + both))*100;
        }
        /**
         * Calculates the probability of an event happening, given
         * that another event has occured 
         */
        public double Given(double event1, double event2)
        {
            return And(event1, event2) / event2 ;
        }
        
        /**
         * Sets the both instance variable
         */
        public void SetBoth(double both)
        {
            this.both = both;
        }
        /**
         * Gets the both instance variable
         */
        public double GetBoth()
        {
            return this.both;
        }
        /**
         * Sets the function to being mutually exclusive.
         * This leaves an impact of calculating "and"
         * probability.
         * probability.
         */
        public void SetExclusive(bool exclusive)
        {
            this.exclusive = exclusive;
        }
        public bool GetExclusive()
        {
            return this.exclusive;
        }
        /**
         * This method prints out a welcome statement 
         */
        public void Start()
        {
            Console.WriteLine("Welcome to the Probability Calculator!");
        }
        /**
         * This method sets the current input
         **/
        public void SetInput(String input)
        {
            this.input = input.Split(" ");
        }
        /**
         * This method just returns the input
         */
        public String[] GetInput()
        {
            return this.input;
        }
    }
}
