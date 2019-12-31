using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace DesignPatterns_Interpreter
{
    class Program
    {
        //original source https://metanit.com/sharp/patterns/3.8.php
        static void Main(string[] args)
        {
            Console.WriteLine("Design Patterns - Interpreter!");

            Context context = new Context();
            int x = 5;
            int y = 8;
            int z = 2;

            context.SetVariable("x", x);
            context.SetVariable("y", y);
            context.SetVariable("z", z);

            Console.WriteLine($"x = {x}, y = {y}, z = {z}");

            Context.IExpression expression = new Context.SubtractExpression(
                new Context.AddExpression(new Context.NumberExpression("x"), new Context.NumberExpression("y")), new Context.NumberExpression("z"));

            int result = expression.Interpret(context);

            Console.WriteLine($"Result x + y - z = {result}");
        }
    }

    class Context
    {
        private Dictionary<string, int> variables;

        public Context()
        {
            variables = new Dictionary<string, int>();
        }

        public int GetVariable(string name)
        {
            return variables[name];
        }

        public void SetVariable(string name, int value)
        {
            if (variables.ContainsKey(name))
            {
                variables[name] = value;
            }
            else
            {
                variables.Add(name, value);
            }
        }


        public interface IExpression
        {
            int Interpret(Context context);
        }

        public class NumberExpression : IExpression
        {
            private string _name;

            public NumberExpression(string variableName)
            {
                _name = variableName;
            }

            public int Interpret(Context context)
            {
                return context.GetVariable(_name);
            }
        }

        public class AddExpression : IExpression
        {
            private IExpression _leftExpression;
            private IExpression _rightExpression;

            public AddExpression(IExpression leftExpression, IExpression rightExpression)
            {
                _leftExpression = leftExpression;
                _rightExpression = rightExpression;
            }

            public int Interpret(Context context)
            {
                return _leftExpression.Interpret(context) + _rightExpression.Interpret(context);
            }
        }

        public class SubtractExpression : IExpression
        {
            private IExpression _lefExpression;
            private IExpression _rightExpression;

            public SubtractExpression(IExpression leftExpression, IExpression rightExpression)
            {
                _lefExpression = leftExpression;
                _rightExpression = rightExpression;
            }

            public int Interpret(Context context)
            {
                return _lefExpression.Interpret(context) - _rightExpression.Interpret(context);
            }
        }
    }
}
