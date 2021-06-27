using System;

namespace BreakBirds
{
    enum Operations
    {
        Sum,
        Substract,
        Multiply,
        Divide,
        Functional,
        Hello,
        Space,
        Quit,
        Err_
    };

    enum Result
    {
        Error_,
        Ok
    };

    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            PrintHello();
            PrintFunctional();
            while (true)
            {
                PrintPrompt();
                var oper = TakeOperation(Console.ReadLine());
                if (oper == Operations.Quit)
                    return;
                DoOperations(oper);
            }
        }

        static double Sum(double a, double b) { return a + b; }

        static double Substract(double a, double b) { return a - b; }

        static double Multiple(double a, double b) { return a * b; }

        static double Divide(double a, double b) { return a / b; }

        static Result ReadNumber(ref double a, ref double b)
        {
            Console.WriteLine("Пожалуйста, введите два числа: ");

            string[] vs = Console.ReadLine().Split();
            string[] str = vs;

            while (str.Length < 2)
            {
                var t = Console.ReadLine();
                if (t != "")
                {
                    if (str[0] == "")
                        str[0] = t;
                    else
                    {
                        Array.Resize(ref str, str.Length +1);
                        if (str[1] == null)
                            str[1] = t;
                    }
                }
            }
            try
            {
                a = double.Parse(str[0]);
                b = double.Parse(str[1]);
            }
            catch
            {
                a = 0.0;
                b = 0.0;
                return Result.Error_;
            }

            return Result.Ok;
        }

        /*
        static Result ReadNumber(ref double a)
        {
            Console.WriteLine("Пожалуйста, введите число: ");
            try
            {
                a = double.Parse(Console.ReadLine());
            }
            catch
            {
                a = 0.0;
                return Result.Error_;
            }

            return Result.Ok;
        }
        */

        static void DoOperations(Operations op)
        {
            var (a, b) = (0.0, 0.0);
            (string, ConsoleColor) output;

            switch (op)
            {
                case Operations.Sum:
                    output = ReadNumber(ref a, ref b) switch
                    {
                        Result.Error_ => ("Некорректный ввод", ConsoleColor.Red),
                        Result.Ok => (Sum(a, b).ToString(), ConsoleColor.Green),
                        _ => ("Неизвестная ошибка", ConsoleColor.Red),
                    };

                    Console.ForegroundColor = output.Item2;
                    Console.WriteLine(output.Item1);
                    Console.ResetColor();
                    return;

                case Operations.Substract:
                    output = ReadNumber(ref a, ref b) switch
                    {
                        Result.Error_ => ("Некорректный ввод", ConsoleColor.Red),
                        Result.Ok => (Substract(a, b).ToString(), ConsoleColor.Green),
                        _ => ("Неизвестная ошибка", ConsoleColor.Red),
                    };

                    Console.ForegroundColor = output.Item2;
                    Console.WriteLine(output.Item1);
                    Console.ResetColor();
                    return;

                case Operations.Multiply:
                    output = ReadNumber(ref a, ref b) switch
                    {
                        Result.Error_ => ("Некорректный ввод", ConsoleColor.Red),
                        Result.Ok => (Multiple(a, b).ToString(), ConsoleColor.Green),
                        _ => ("Неизвестная ошибка", ConsoleColor.Red),
                    };

                    Console.ForegroundColor = output.Item2;
                    Console.WriteLine(output.Item1);
                    Console.ResetColor();
                    return;

                case Operations.Divide:
                    output = ReadNumber(ref a, ref b) switch
                    {
                        Result.Error_ => ("Некорректный ввод", ConsoleColor.Red),
                        Result.Ok => (b != 0)
                            ? ( Divide(a, b).ToString(), ConsoleColor.Green)
                            : ("Знаменатель равен нулю!", ConsoleColor.Red),
                        _ => ("Неизвестная ошибка", ConsoleColor.Red),
                    };

                    Console.ForegroundColor = output.Item2;
                    Console.WriteLine(output.Item1);
                    Console.ResetColor();
                    return;

                case Operations.Functional:
                    PrintFunctional();
                    return;

                case Operations.Hello:
                    PrintHello();
                    return;

                case Operations.Err_:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Неизвестная операция");
                    Console.ResetColor();
                    return;

                default:
                    return;
            }
        }

        static Operations TakeOperation(string input)
        {
            return input switch
            {
                "+" => Operations.Sum,
                "-" => Operations.Substract,
                "*" => Operations.Multiply,
                "/" => Operations.Divide,
                "f" => Operations.Functional,
                "h" => Operations.Hello,
                ""  => Operations.Space,
                "q" => Operations.Quit,
                _ => Operations.Err_,
            };
        }

        static void PrintPrompt()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("-( ");

            Console.ResetColor();
            Console.Write("user");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" )-: ");

            Console.ResetColor();
        }

        static void PrintFunctional()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" Наши функции:");
            Console.ResetColor();

            (string, string, string)[] functions =
            {
                  ("Сумма",         "(+)", "Суммирует два числа")
                , ("Вычитание",     "(-)", "Вычитает из первого числа второе")
                , ("Умножение",     "(*)", "Умножает одно число на другое")
                , ("Деление",       "(/)", "Делит первое число на второе")
                , ("Функционал",    "(f)", "Показывает функционал Калькулятора")
                , ("Hello",         "(h)", "Показывает экран приветствия")
                , ("Выйти",         "(q)", "Позволяет выйти из Калькулятора")
            };

            var maxLength = 0;
            foreach (var f in functions)
                if (f.Item1.Length > maxLength)
                    maxLength = f.Item1.Length;
            maxLength++;

            foreach (var f in functions)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" -> " + f.Item1);

                for (var i = 0; i < maxLength - f.Item1.Length; i++)
                    Console.Write(" ");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(f.Item2);

                Console.ResetColor();
                Console.WriteLine(": " + f.Item3);
            }

            Console.WriteLine();
        }

        static void PrintHello()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            var helloString = "Добро пожаловать в Калькулятор!";
            var neededSpaces = Console.WindowWidth - helloString.Length;

            for (var i = 0; i < Console.WindowWidth; i++)
                Console.Write("-");

            Console.WriteLine("\n");

            for (var i = 0; i < neededSpaces / 2; i++)
                Console.Write(" ");

            Console.WriteLine(helloString + "\n");

            for (var i = 0; i < Console.WindowWidth; i++)
                Console.Write("-");

            Console.WriteLine("\n");
            Console.ResetColor();
        }

    }
}
