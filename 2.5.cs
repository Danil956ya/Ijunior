using System;

namespace _2._5
{
    class Program
    {
        static void Main(string[] args)
        {
            string exitInput = "exit";
            string userImput;
            bool isWork = true;
            float rub = 50;
            float usd = 0;
            float eur = 0;
            int usdToRub = 64;
            int eurToRub = 80;

            while (isWork)
            {
                Console.WriteLine("Ваш баланс: {0} рублей, {1} долларов, {2} евро", rub, usd, eur);
                Console.WriteLine("Добрый день, c какой валютой вы хотите выполнить операцию?");
                Console.WriteLine("1: Пополнение баланса.");
                Console.WriteLine("2: Доллар.");
                Console.WriteLine("3: Евро.");
                userImput = Console.ReadLine();

                switch (userImput)
                {
                    case "1":
                        Console.WriteLine("На какую сумму желаете пополнить счёт?");
                        userImput = Console.ReadLine();

                        if (userImput != "exit")
                        {
                            rub += Convert.ToInt32(userImput);
                        }

                        break;
                    case "2":
                        Console.WriteLine("Операции с долларом. 1 доллар = {0} рублей.\n1: Купить доллар.\n2: Продать доллар.", usdToRub);
                        userImput = Console.ReadLine();

                        if (userImput != "exit")
                        {
                            if (userImput == "1")
                            {

                                Console.WriteLine("Сколько вы хотите купить долларов?");
                                userImput = Console.ReadLine();

                                if (rub / usdToRub >= Convert.ToSingle(userImput))
                                {
                                    rub -= Convert.ToSingle(userImput) * usdToRub;
                                    usd += Convert.ToSingle(userImput);
                                }
                                else
                                {
                                    Console.WriteLine("Недостаточно средств\n");
                                }

                            }
                            else if (userImput == "2")
                            {

                                Console.WriteLine("Сколько вы хотите продать долларов?");
                                userImput = Console.ReadLine();

                                if (usd >= Convert.ToSingle(userImput))
                                {
                                    rub -= Convert.ToSingle(userImput) * usdToRub;
                                    usd += Convert.ToSingle(userImput);
                                }
                                else
                                {
                                    Console.WriteLine("Недостаточно средств\n");
                                }

                            }
                        }
                        break;
                    case "3":
                        Console.WriteLine("Операции с евро. 1 евро = {0} рублей.\n1: Купить евро.\n2: Продать евро.", eurToRub);
                        userImput = Console.ReadLine();

                        if (userImput != "exit")
                        {
                            if (userImput == "1")
                            {
                                Console.WriteLine("Сколько вы хотите купить евро?");
                                userImput = Console.ReadLine();

                                if (rub / eurToRub >= Convert.ToSingle(userImput))
                                {
                                    rub -= Convert.ToSingle(userImput) * eurToRub;
                                    eur += Convert.ToSingle(userImput);
                                }
                                else
                                {
                                    Console.WriteLine("Недостаточно средств\n");
                                }

                            }
                            else if (userImput == "2")
                            {

                                Console.WriteLine("Сколько вы хотите продать евро?");
                                userImput = Console.ReadLine();

                                if (eur >= Convert.ToSingle(userImput))
                                {
                                    rub -= Convert.ToSingle(userImput) * eurToRub;
                                    eur += Convert.ToSingle(userImput);
                                }
                                else
                                {
                                    Console.WriteLine("Недостаточно средств\n");
                                }

                            }
                        }
                        break;
                }
                isWork = userImput != exitInput;
            }

        }
    }
}

