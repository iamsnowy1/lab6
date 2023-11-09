using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    public class Calculator<T>
    {
        // Делегати для арифметичних операцій
        public delegate T AdditionDelegate(T a, T b);
        public delegate T SubtractionDelegate(T a, T b);
        public delegate T MultiplicationDelegate(T a, T b);
        public delegate T DivisionDelegate(T a, T b);

        // Делегати для виведення результатів
        public delegate void DisplayResultDelegate(T result);

        // Поля для зберігання делегатів
        public AdditionDelegate Add { get; set; }
        public SubtractionDelegate Subtract { get; set; }
        public MultiplicationDelegate Multiply { get; set; }
        public DivisionDelegate Divide { get; set; }

        // Конструктор, в якому ініціалізуються делегати
        public Calculator()
        {
            Add = (a, b) => (dynamic)a + b;
            Subtract = (a, b) => (dynamic)a - b;
            Multiply = (a, b) => (dynamic)a * b;
            Divide = (a, b) => (dynamic)a / b;
        }

        // Метод для виведення результату
        public void DisplayResult(T result)
        {
            Console.WriteLine($"Result: {result}");
        }
    }

    class Program
    {
        static void Main()
        {
            // Використання дженеричного класу Calculator для цілих чисел
            Calculator<int> intCalculator = new Calculator<int>();
            int a = 10, b = 5;

            intCalculator.DisplayResult(intCalculator.Add(a, b));
            intCalculator.DisplayResult(intCalculator.Subtract(a, b));
            intCalculator.DisplayResult(intCalculator.Multiply(a, b));
            intCalculator.DisplayResult(intCalculator.Divide(a, b));

            // Використання дженеричного класу Calculator для чисел з плаваючою точкою
            Calculator<double> doubleCalculator = new Calculator<double>();
            double x = 10.5, y = 3.2;

            doubleCalculator.DisplayResult(doubleCalculator.Add(x, y));
            doubleCalculator.DisplayResult(doubleCalculator.Subtract(x, y));
            doubleCalculator.DisplayResult(doubleCalculator.Multiply(x, y));
            doubleCalculator.DisplayResult(doubleCalculator.Divide(x, y));
        }
    }
}
