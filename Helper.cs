using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Helper
    {
        // Разделитель, который мы используем при чтении из файла 
        public const string separator = ",";
        // Метод возвращает true, если проверяемый символ - разделитель
        // ("пробел" или "равно")
        static public bool IsDelimeter(char c)
        {
            if ((" =".IndexOf(c) != -1))
            {
                return true;
            }
            return false;
        }
        // Метод возвращает true, если проверяемый символ - оператор
        // К операторам относятся 
        // ! (факториал) 
        // + (плюс) 
        // - (минус)
        // * (умножить) 
        // / (разделить) 
        // ^ (возвести в степень)
        // ( (открывающаяся скобка) 
        // ) (закрывающаяся скобка)
        static public bool IsOperator(char с)
        {
            // -1 Знак не найден
            if (("+-/*^()!".IndexOf(с) != -1))
            {
                return true;
            }
            return false;
        }

        // Метод возвращает приоритет оператора
        static public byte GetPriority(char s)
        {
            switch (s)
            {
                case '(':
                case ')': return 0;
                case '+':
                case '-': return 1;
                case '*':
                case '/': return 2;
                case '^': return 3;
                case '!': return 4;
                default: return 5;
            }
        }
        // Проверяет, что переданный символ является числом
        // Не используем стандартные методы, из-за того, что 
        // реалиована поддержка чисел с плавающей точкой и
        // в нашем случае разделителем является запятая

        // Разделитель хранится в константе separator

        // Возможно, лучше реализовывать разделитель с помощью
        // пользовательского ввода и форматных функций 
        // встроенных в C#
        static public bool isNum(char c)
        {
            // https://stackoverflow.com/questions/894263/identify-if-a-string-is-a-number
            // https://stackoverflow.com/questions/6733652/how-can-i-check-if-a-string-is-a-number
            if (("1234567890" + separator).IndexOf(c) != -1)
            {
                return true;
            }
            return false;
        }
        // Функция вычисляет факториал числа
        // https://ru.wikipedia.org/wiki/%D0%A4%D0%B0%D0%BA%D1%82%D0%BE%D1%80%D0%B8%D0%B0%D0%BB
        public static int Factorial(int N)
        {
            int factotial = 1;
            if (N != 0)
            {
                for (int i = 2; i <= N; i++)
                {
                    factotial *= i;
                }
            }
            return factotial;
        }
    }
}
