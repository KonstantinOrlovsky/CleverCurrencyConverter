using System;

namespace DataRetrieval
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Введите выражение для вычисления");
            string expression = "$70:ToRub + 50eur:ToDollar - 5r:ToDollar, ToEuro:";
            // var sourceExpression = Console.ReadLine();
            string resultMessage = "";

            Initializer initializer = new Initializer(expression);

            try
            {
               // resultMessage = initializer.InitializingFromXml(Constants.Local);
               resultMessage = initializer.InitializingFromJson(Constants.Url);
               // resultMessage = initializer.InitializingFromDataBase();
            }
            catch (Exception exception)
            {
                resultMessage = exception.Message;
            }
            finally
            {
                initializer.SaveToLog(resultMessage);
            }

            Console.WriteLine($"Результат: {resultMessage}");
            Console.Read();
        }
    }
    
}
