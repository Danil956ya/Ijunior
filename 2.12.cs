using System;

namespace _2._12
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            Console.WriteLine("- Привет, герой, как звать то тебя?");
            Console.Write("Ваше имя: ");
            string name = Console.ReadLine();
            int healt = 100;
            int damage = 5;
            int multiplyDamage = 15;
            int luckyDamage = random.Next(5, 10);
            int spitDamage = 25;
            int attackDamage = 10;
            Console.Clear();
            Console.WriteLine("- {0}, Перед тобой САМЫЙ сильный человек в мире, покажи ему!", name);
            int enemyHealt = 100;
            int enemyDamage = 15;
            Console.WriteLine("- А вот и его статистика! \nЗдоровье: {0}. \nУрон: {1}.", enemyHealt, enemyDamage);
            string command = "default";
            int healCooldown = 0;
            int troweCooldown = 0;
            bool spitReady = false;
            bool inStun = false;
            string enemyStats = "*Здоворье САМОГО сильного: ";
            string heroStats = "*Ваше здоворье: ";

            for(int i = 0; healt >= 0 && enemyHealt >= 0; i++)
            {
                healCooldown--;
                troweCooldown--;
                Console.WriteLine("- Что будем делать? *(Для помощи напиши - help)*");
                command = Console.ReadLine();

                switch(command)
                {
                    case "heal":

                        if(healCooldown <= 0)
                        {
                            Console.WriteLine("- Зачем ты достал еду? Аааааа, приятного аппетита!\n*Пока ты жевал бутерброд, враг смачно ударил тебя по лицу*");
                            healt -= enemyDamage;
                            healt += 30;
                            Console.WriteLine(enemyStats + enemyHealt);
                            Console.WriteLine(heroStats + healt);
                            healCooldown = 3;
                        }
                        else
                        {
                            Console.WriteLine("- Ты же только лечился, так не пойдёт! \n- Получи в лицо!");
                            Console.WriteLine(enemyStats + enemyHealt);
                            healt -= enemyDamage;
                            Console.WriteLine(heroStats + healt);
                        }
                        break;
                    case "trowe":

                        if(troweCooldown <= 0)
                        {
                            if(inStun)
                            {
                                Console.WriteLine("- Ты бросил в него камень!\n- Он упал, из-за твоих слюней!");
                                enemyHealt -= luckyDamage + multiplyDamage;
                                Console.WriteLine(enemyStats + enemyHealt);
                                Console.WriteLine(heroStats + healt);
                                inStun = false;
                                troweCooldown = 3;
                            }
                            else
                            {
                                Console.WriteLine("- Ты бросил в него камень!\n- О нет, он его поднимает, и бросает обратно в тебя!");
                                enemyHealt -= luckyDamage;
                                Console.WriteLine(enemyStats + enemyHealt);
                                healt -= enemyDamage;
                                Console.WriteLine(heroStats + healt);
                                troweCooldown = 3;
                            }
                        }
                        else
                        {
                            Console.WriteLine("- Эй! ТЫ ЖЕ ТОЛЬКО ЧТО КИНУЛ В НЕГО КАМЕНЬ! Получи в лицо!");
                            Console.WriteLine(enemyStats + enemyHealt);
                            healt -= enemyDamage;
                            Console.WriteLine(heroStats + healt);
                        }
                        break;
                    case "attack":
                        if(inStun)
                        {
                            Console.WriteLine("- Ты бьёшь его пока он вытераеться! Грязная стратегия...\n- Он упал, бей его скорее!");
                            enemyHealt -= attackDamage + multiplyDamage;
                            Console.WriteLine(enemyStats + enemyHealt);
                            Console.WriteLine(heroStats + healt);
                            inStun = false;
                        }
                        else
                        {
                            Console.WriteLine("- Вот это удар! Стоп, а он что? ОН ТОЖЕ ТЕБЯ УДАРИЛ!");
                            enemyHealt -= attackDamage;
                            Console.WriteLine(enemyStats + enemyHealt);
                            healt -= enemyDamage;
                            Console.WriteLine(heroStats + healt);
                        }
                        break;
                    case "bite":
                        Console.WriteLine("- Ого, ты укусил его! \nНо он тоже не промах - получи в лицо!\n");
                        enemyHealt -= damage;
                        Console.WriteLine(enemyStats + enemyHealt);
                        healt -= enemyDamage;
                        Console.WriteLine(heroStats + healt);
                        break;
                    case "getspit":
                        spitReady = true;
                        Console.WriteLine("- Ты готовишься... Что? Плюнуть? Ну окей.... \nВраг, удерживая смех, бьёт тебя!");
                        healt -= enemyDamage;
                        Console.WriteLine(enemyStats + enemyHealt);
                        Console.WriteLine(heroStats + healt);
                        break;
                    case "spit":

                        if(spitReady)
                        {
                            Console.WriteLine("- Ты плюнул в него! Так держать!\nВраг, вытерает твои слюни, время для удара!");
                            enemyHealt -= spitDamage;
                            Console.WriteLine(enemyStats + enemyHealt);
                            Console.WriteLine(heroStats + healt);
                            inStun = true;
                        }
                        else
                        {
                            Console.WriteLine("- Правда? ты решил плюнуть в него? Ну попробуй. \n*Ваши слюни не долетают до врага.*\nВас бьют...");
                            healt -= enemyDamage;
                            Console.WriteLine(enemyStats + enemyHealt);
                            Console.WriteLine(heroStats + healt);
                        }
                        spitReady = false;
                        break;
                    case "help":
                        Console.WriteLine("- Ты уже забыл что умеешь делать? Запоминай - 'bite', 'heal', 'attack', 'trowe', 'getspit', 'spit'.");
                        break;
                }

            }

            if(healt >= 0 && enemyHealt >= 0)
            {
                Console.WriteLine("Красивая ничья! {0}, приходи ещё подраться!", name);
            }
            else if(enemyHealt <= 0)
            {
                Console.WriteLine("{0}, молодец! Теперь ты САМЫЙ сильный человек!");
            }
            

        }
    }
}
