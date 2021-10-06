using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Calculator
    {
        public Calculator() { }

        public static double Calculate(string sum)
        {
            string leftBracket = "(";
            string rightBracket = ")";
            char[] high_prio_operators = { '*', '/' };
            char[] low_prio_operators = { '+', '-' };
            char[] search_operators;
            char[] operators = { '*', '/', '+', '-' };
            char[] valid_character = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '-', '*', '/', '(', ')', '.' };

            int idxFound = 0;
            int idxEnd;
            int idxStart;
            int idxTemp;
            int bracketControl;

            //Remove all spaces
            sum = sum.Replace(" ", null);

            //Checks for invalid character
            for (int i = 0; i < sum.Length; i++)
            {
                if (sum.Substring(i, 1).IndexOfAny(valid_character) == -1)
                    throw new Exception("Error: Invalid character was found.");
            }

            //checks for open bracket (add * for it)
            sum = sum.Replace("(", "*(")
                .Replace("**", "*").Replace("/*", "/")
                .Replace("-*", "-").Replace("+*", "+");

            //replace *- (multiply negative value)
            sum = sum.Replace("*-", "a");
            //checks for close bracket ( add * for it)
            sum = sum.Replace(")", ")*")
                .Replace("**", "*").Replace("*/", "/")
                .Replace("*-", "-").Replace("*+", "+")
                .Replace("*a", "a");
            //brings back *-
            sum = sum.Replace("a", "*-");

            //Cater for starting and ending brackets
            if (sum.Substring(0, 2) == "*(")
                sum = sum.Remove(0, 1);
            if (sum.Substring(sum.Length - 2, 2) == ")*")
                sum = sum.Remove(sum.Length - 1, 1);

            if (sum.Substring(0, 1) == "*" || sum.Substring(0, 1) == "/" || sum.Substring(0, 1) == "+" ||
                sum.Substring(sum.Length - 1, 1) == "*" || sum.Substring(sum.Length - 1, 1) == "/" || sum.Substring(sum.Length - 1, 1) == "+")
                throw new Exception("Error: Cannot start or end with operators.");

            //Loop until no more brackets priotize backet calculation
            while (idxFound != -1)
            {
                idxFound = sum.IndexOf(leftBracket, idxFound);

                //If bracket is found, start finding its close bracket
                if (idxFound != -1)
                {
                    bracketControl = 1;
                    idxTemp = idxFound;
                    while (bracketControl > 0)
                    {
                        idxStart = sum.IndexOf(leftBracket, idxTemp + 1);
                        idxEnd = sum.IndexOf(rightBracket, idxTemp + 1);

                        if (idxStart != -1 && idxStart < idxEnd)
                        {
                            bracketControl += 1;
                            idxTemp = idxStart;
                        }
                        else if (idxEnd != -1)
                        {
                            bracketControl -= 1;
                            idxTemp = idxEnd;
                        }
                        //When extra open bracket was found
                        else if( idxEnd == -1 && bracketControl > 0)
                        {
                            throw new Exception("Error: Extra '(' was found.");
                        }
                    }

                    //Call this function again with the contents inside bracket
                    //Replace the bracket value with exact value
                    double temp = Calculate(sum.Substring(idxFound + 1, idxTemp - idxFound - 1));
                    sum = sum.Replace(sum.Substring(idxFound, idxTemp - idxFound + 1), temp.ToString());
                }
            }

            if (sum.Contains(")"))
                throw new Exception("Error: Extra ')' was found.");

            //Calculation part
            for (int i=0; i <2; i++)
            {
                idxFound = 1;

                //Search operators based on priorities
                if (i == 0)
                    search_operators = high_prio_operators;
                else
                    search_operators = low_prio_operators;

                //Loop until no more operators (starting with multiplication and division, then add and minus)
                while (idxFound != -1 && idxFound < sum.Length)
                {
                    idxFound = sum.IndexOfAny(search_operators, 1);

                    //If operator is found, start finding the value before it and after it
                    if (idxFound != -1)
                    {
                        //Finding the operator before 'this' operator
                        idxStart = sum.Substring(0, idxFound - 1).LastIndexOfAny(operators);
                        idxStart = idxStart == -1 ? 0 : idxStart + 1;

                        //To cater negative value before 'this' operator
                        //First value is negative
                        if (idxStart == 1 && sum[0] == '-')
                            idxStart = 0;
                        //Value after operator before 'this' operator is negative
                        else if (idxStart > 1 && sum.Substring(idxStart - 2, 1).IndexOfAny(operators) == 0)
                            idxStart -= 1;

                        //Finding the operator after 'this' operator
                        idxEnd = sum.Substring(idxFound + 2, sum.Length - (idxFound + 2)).IndexOfAny(operators);
                        idxEnd = idxEnd == -1 ? sum.Length - 1 : idxEnd + idxFound + 1;

                        double temp = 0;
                        switch(sum[idxFound])
                        {
                            case '*':
                                temp = Convert.ToDouble(sum.Substring(idxStart, idxFound - idxStart)) * Convert.ToDouble(sum.Substring(idxFound + 1, idxEnd - idxFound));
                                break;
                            case '/':
                                if (Convert.ToDouble(sum.Substring(idxFound + 1, idxEnd - idxFound)) == 0)
                                    throw new Exception("Error: Cannot devide by zero.");
                                temp = Convert.ToDouble(sum.Substring(idxStart, idxFound - idxStart)) / Convert.ToDouble(sum.Substring(idxFound + 1, idxEnd - idxFound));
                                break;
                            case '+':
                                temp = Convert.ToDouble(sum.Substring(idxStart, idxFound - idxStart)) + Convert.ToDouble(sum.Substring(idxFound + 1, idxEnd - idxFound));
                                break;
                            case '-':
                                temp = Convert.ToDouble(sum.Substring(idxStart, idxFound - idxStart)) - Convert.ToDouble(sum.Substring(idxFound + 1, idxEnd - idxFound));
                                break;
                        }
                        sum = sum.Replace(sum.Substring(idxStart, idxEnd - idxStart + 1), temp.ToString());
                    }
                }
            }
            return Convert.ToDouble(sum);
        }
    }
}
