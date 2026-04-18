using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class CalculatorNCalc
{
    public List<object?> Calculate(List<string> equations, double xValue)
    {
        return equations.Select(eq =>
        {
            try
            {
                // Convert '^' to Pow(a,b)
                string processedEq = ConvertExponentiation(eq);

                var expression = new Expression(processedEq);

                // Set parameter x
                expression.Parameters["x"] = xValue;

                // Handle constants like pi and e
                expression.EvaluateParameter += (name, args) =>
                {
                    if (name.Equals("pi", StringComparison.OrdinalIgnoreCase))
                        args.Result = Math.PI;
                    else if (name.Equals("e", StringComparison.OrdinalIgnoreCase))
                        args.Result = Math.E;
                };

                // Handle lowercase math functions and Pow
                expression.EvaluateFunction += (name, args) =>
                {
                    string func = name.ToLower();

                    if (func == "pow")
                    {
                        if (args.Parameters.Length != 2)
                            throw new ArgumentException("Function 'Pow' requires 2 arguments");
                        double a = Convert.ToDouble(args.Parameters[0].Evaluate());
                        double b = Convert.ToDouble(args.Parameters[1].Evaluate());
                        args.Result = Math.Pow(a, b);
                        return;
                    }

                    if (args.Parameters.Length != 1)
                        throw new ArgumentException($"Function '{name}' requires 1 argument");

                    double param = Convert.ToDouble(args.Parameters[0].Evaluate());

                    switch (func)
                    {
                        case "sin": args.Result = Math.Sin(param); break;
                        case "cos": args.Result = Math.Cos(param); break;
                        case "tan": args.Result = Math.Tan(param); break;
                        case "asin": args.Result = Math.Asin(param); break;
                        case "acos": args.Result = Math.Acos(param); break;
                        case "atan": args.Result = Math.Atan(param); break;
                        case "sqrt": args.Result = Math.Sqrt(param); break;
                        case "log": args.Result = Math.Log(param); break; // natural log
                        case "log10": args.Result = Math.Log10(param); break;
                        case "abs": args.Result = Math.Abs(param); break;
                        default:
                            throw new ArgumentException($"Unknown function '{name}'");
                    }
                };

                return expression.Evaluate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error evaluating equation '{eq}': {ex.Message}");
                return null;
            }
        }).ToList();
    }

    // Convert "a^b" into "Pow(a,b)" recursively
    private string ConvertExponentiation(string eq)
    {
        string pattern = @"([0-9a-zA-Z\)\.]+)\s*\^\s*([0-9a-zA-Z\(\)\.]+)";

        while (Regex.IsMatch(eq, pattern))
        {
            eq = Regex.Replace(eq, pattern, "Pow($1,$2)");
        }

        return eq;
    }
}