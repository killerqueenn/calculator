using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {

            string line;  // переменная для хранения строки (выражение для калькулятора)
            List<string> final_res = new List<string>();
            System.IO.StreamReader file = new System.IO.StreamReader(@".\input.txt");
            while ((line = file.ReadLine()) != null)
            {
                line = line.Replace(" ", "");
                string output = convertToReversePolish(line);
                double result = Counting(output);
                final_res.Add(result.ToString());
            }
            file.Close();
            System.IO.File.WriteAllLines(@".\output.txt", final_res);
            // System.Console.ReadLine();
        }

        static public string[] GetNumStrs() //цифры
        {
            List<string> nums = new List<string>();

            for (int i = '0'; i <= '9'; i++) nums.Add(i.ToString());
            return nums.ToArray();
        }

        static public char[] GetSigns()  //знаки
        {
            return new char[] { '+', '-', '*', '/' };
        }

        static public void Count()
        {
            var nums = GetNumStrs();
            var signs = GetSigns();

            var bigArray = new List<string>();
            bigArray.AddRange(nums);
            bigArray.AddRange(signs.Select(x => x.ToString()));

            string expression;
            System.IO.StreamReader file = new System.IO.StreamReader(@"..\..\..\input.txt");
            expression = file.ReadLine();
            string[] parts = expression.Split(signs);  //разделили по знакам 

            List<string> newList = new List<string>(parts);
            List<string> newList2 = parts.ToList();

            //найти знак * или / и заменить цифры рядом с ним на произведение или деление

        }

        static public String convertToReversePolish(String exp)
        {
            if (exp == null)
            {
                return null;
            }
            String res = "";
            int len = exp.Length; // 0,1,2,3 => 4
            Stack<char> operStack = new Stack<char>(); // Операторы
            Stack<String> reversePolish = new Stack<String>(); // Числа (+изменения порядка записи за счет скобок)
            //не проверять пустой
            operStack.Push('#');
            for (int i = 0; i < len;)
            {
                /*
                // пробелы
                // https://stackoverflow.com/questions/3581741/c-sharp-equivalent-to-javas-charat
                while (i < len && exp[i] == ' ')
                {
                    i++;
                }
                */
                // символ в exp[i] не пробел
                if (i == len) // exp[i] не существует 
                {
                    break;
                }
                // Числа
                if (Helper.isNum(exp[i]))
                {
                    String num = ""; // Обязательно надо инициализировать созданную переменную
                    while (i < len && Helper.isNum(exp[i]))
                    {
                        num += exp[i++];
                    }
                    reversePolish.Push(num);
                }
                // Операторы
                else if (Helper.IsOperator(exp[i]))
                {
                    char op = exp[i];
                    switch (op)
                    {
                        case '(':
                            operStack.Push(op);
                            break; // Почитайте про break
                        case ')':
                            while (operStack.Peek() != '(')
                            {
                                reversePolish.Push(Char.ToString(operStack.Pop()));
                            }
                            operStack.Pop(); // Убирает открывающуюся скобку
                            break;
                        case '+':
                            if (operStack.Peek() == '(')
                            {
                                operStack.Push(op);
                            }
                            else
                            {
                                while (operStack.Peek() != '#' && operStack.Peek() != '(')
                                {
                                    reversePolish.Push(Char.ToString(operStack.Pop()));
                                }
                                operStack.Push(op);
                            }
                            break;
                        case '-':
                            int j = 0;
                            if (i > 0)
                            {
                                j = i - 1;
                                while (j > 0 && Helper.IsDelimeter(exp[j]))
                                {
                                    j--;
                                }
                            }
                            if (i == 0 || !Helper.isNum(exp[j]))
                            {
                                if (operStack.Peek() == '(')
                                {
                                    operStack.Push(op);
                                    operStack.Push('0');
                                }
                                else
                                {
                                    while (operStack.Peek() != '#' && operStack.Peek() != '(')
                                    {
                                        reversePolish.Push(Char.ToString(operStack.Pop()));
                                    }
                                    operStack.Push(op);
                                    operStack.Push('0');
                                }
                            }
                            else
                            {
                                if (operStack.Peek() == '(')
                                {
                                    operStack.Push(op);
                                }
                                else
                                {
                                    while (operStack.Peek() != '#' && operStack.Peek() != '(')
                                    {
                                        reversePolish.Push(Char.ToString(operStack.Pop()));
                                    }
                                    operStack.Push(op);
                                }
                            }
                            break;
                        case '*':
                        case '/':
                            if (operStack.Peek() == '(')
                            {
                                operStack.Push(op);
                            }
                            else
                            {
                                while (operStack.Peek() != '#' && operStack.Peek() != '+' &&
                                       operStack.Peek() != '-' && operStack.Peek() != '(')
                                {
                                    reversePolish.Push(Char.ToString(operStack.Pop()));
                                }
                                operStack.Push(op);
                            }
                            break;
                    }
                    i++;
                }
            }
            // Сливаем operStack и reversePolish
            // остается только reversePolish
            while (operStack.Peek() != '#')
            {
                reversePolish.Push(Char.ToString(operStack.Pop()));
            }
            // Создаем возвращаемую строку
            while (reversePolish.Count != 0)
            {
                res = res.Length == 0 ? reversePolish.Pop() + res : reversePolish.Pop() + " " + res;
            }
            return res;
        }

        // Метод, вычисляющий значение выражения, уже преобразованного в постфиксную запись
        static public double Counting(string input)
        {
            double result = 0.0; //Результат
            Stack<double> temp = new Stack<double>(); // стек для решения
            for (int i = 0; i < input.Length; i++) //Для каждого символа в строке
            {
                //Если символ - цифра, то читаем все число и записываем на вершину стека
                if (Helper.isNum(input[i]))
                {
                    string a = string.Empty;
                    while (!Helper.IsDelimeter(input[i]) && !Helper.IsOperator(input[i])) //Пока не разделитель
                    {
                        a += input[i]; //Добавляем
                        i++;
                        if (i == input.Length) break;
                    }
                    temp.Push(double.Parse(a)); //Записываем в стек
                    i--;
                }
                else if (Helper.IsOperator(input[i])) //Если символ - оператор
                {
                    //Берем два последних значения из стека
                    double a = temp.Pop();
                    double b = temp.Pop();
                    switch (input[i]) //И производим над ними действие, согласно оператору
                    {
                        case '+': result = a + b; break;
                        case '-': result = a - b; break;
                        case '*': result = a * b; break;
                        case '/': result = a / b; break;
                    }
                    temp.Push(result); //Результат вычисления записываем обратно в стек
                }
            }
            return temp.Peek(); //Забираем результат всех вычислений из стека и возвращаем его
        }
    }
}
