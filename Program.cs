using System;
using System.Threading;

namespace ConsoleGameProject
{
    internal class Program
    {
        public enum Scene { Select, Confirm, Room, Kitchen, Park, Shop, BathRoom, PlayRoom, Inventory }
        public enum Puppy { 말티즈 = 1, 시츄, 비숑, 치와와 }

        public enum Clothes { None, 셔츠, 드레스, 턱시도 }

        public enum Toy { None, 스틱, 퍼즐, 인형 }

        struct Inventory
        {
            public Clothes clothes;
            public Toy toy;
        }

        static Inventory[] inventories = new Inventory[10];

        static int EmptyInventory(Inventory[] inventories)
        {
            for (int i = 0; i < inventories.Length; i++)
            {
                if (inventories[i].clothes == 0 && inventories[i].toy == 0)
                {
                    return i;
                }
            }
            return -1;
        }

        static bool NoMoney()
        {
            if (data.money < 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("더 이상 사용할 수 있는 돈이 없습니다.");
                Console.WriteLine();
                Console.WriteLine("파산하셨습니다.");
                Console.ResetColor();
                Wait(2);
                End();

                return false;
            }
            return true;
        }

        static bool ByeByePuppy()
        {
            if (data.feeling <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine($"{data.name}(이)의 기분 스택이 0이하가 되었습니다.");
                Console.WriteLine($"{data.name}(이)와 더이상 함께 할 수 없습니다.");
                Wait(2);
                Console.WriteLine();
                Console.WriteLine($"{data.name}(이)는 기분이 좋지않아 집을 떠났습니다.");
                Wait(3);
                Console.ResetColor();
                Console.WriteLine();
                End();
                return false;
            }
            else if (data.energy <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine($"{data.name}(이)의 에너지 스택이 0이하가 되었습니다.");
                Console.WriteLine($"{data.name}(이)와 더이상 함께 할 수 없습니다.");
                Wait(2);
                Console.WriteLine();
                Console.WriteLine($"{data.name}(이)는 너무 지루해 집을 떠났습니다.");
                Wait(3);
                Console.ResetColor();
                Console.WriteLine();
                End();
                return false;
            }
            else if (data.hungry >= 100)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine($"{data.name}(이)의 배고픔 스택이 100이상이 되었습니다.");
                Console.WriteLine($"{data.name}(이)와 더이상 함께 할 수 없습니다.");
                Wait(2);
                Console.WriteLine();
                Console.WriteLine($"{data.name}(이)는 배가고파 집을 떠났습니다.");
                Wait(3);
                Console.ResetColor();
                Console.WriteLine();
                End();
                return false;
            }
            else if (data.dirty >= 100)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine($"{data.name}(이)의 더러움 스택이 100이상이 되었습니다.");
                Console.WriteLine($"{data.name}(이)와 더이상 함께 할 수 없습니다.");
                Wait(2);
                Console.WriteLine();
                Console.WriteLine($"{data.name}(이)는 씻겨주지 않아 집을 떠났습니다.");
                Wait(3);
                Console.ResetColor();
                Console.WriteLine();
                End();
                return false;
            }
            return true;
        }

        static bool HappyEnding()
        {
            if (data.feeling >= 100)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine($"{data.name}(이)의 기분이 MAX 상태가 되었습니다.");
                Wait(2);
                Console.WriteLine();
                Console.WriteLine($"{data.name}(이)와 영원히 함께할 수 있습니다.");
                Wait(2);
                Console.ResetColor();
                Console.WriteLine();
                Clear();
                return false;
            }
            else if (data.energy >= 100)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine($"{data.name}(이)의 에너지가 MAX 상태가 되었습니다.");
                Wait(2);
                Console.WriteLine();
                Console.WriteLine($"{data.name}(이)와 영원히 함께할 수 있습니다.");
                Wait(2);
                Console.ResetColor();
                Console.WriteLine();
                Clear();
                return false;
            }
            return true;
        }

        static bool AgainBuy()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("1. 상품 더 구매하기 | 2. 인벤토리 확인하기 | 3. 거실로 돌아가기");
            Console.ResetColor();
            Console.Write("선택하기: ");
            string intput = Console.ReadLine();
            Console.WriteLine();

            switch (intput)
            {
                case "1":
                    Console.Clear();
                    ShopScene();
                    break;

                case "2":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("인벤토리를 열고 있습니다.");
                    Console.ResetColor();
                    Wait(2);
                    data.scene = Scene.Inventory;
                    break;

                case "3":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("거실로 돌아갑니다.");
                    Console.ResetColor();
                    Wait(2);
                    data.scene = Scene.Room;
                    break;

            }


            return false;
        }

        static bool AlreadyBuyClothes(Clothes clothes)
        {
            foreach (var item in inventories)
            {
                if (item.clothes == clothes)
                {
                    return true;
                }
            }
            return false;
        }

        static bool AlreadyBuyToy(Toy toy)
        {
            foreach (var item in inventories)
            {
                if (item.toy == toy)
                {
                    return true;
                }
            }
            return false;
        }

        struct GameData
        {
            public bool running;
            public Scene scene;
            public string name;
            public Puppy puppy;

            public int feeling;
            public int hungry;
            public int energy;
            public int dirty;
            public int money;
        }

        static GameData data;
        static void Main(string[] args)
        {
            Start();

            while (data.running)
            {
                Run();
            }

            End();
        }

        static void Start()
        {

            data = new GameData();

            data.running = true;

            Console.Clear();
            Console.WriteLine("                        ★ 강아지 육성 게임★                   ");
            Console.WriteLine();
            Console.WriteLine("           ＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿");
            Console.WriteLine("                                                          ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("                   주인님 저를 끝까지 키워주세요!!       ");
            Console.ResetColor();
            Console.WriteLine("          ＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿");
            Console.WriteLine();
            Console.WriteLine("     ￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("                             게임규칙                          ");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("        1. 강아지의 기분, 에너지 스택이 100이 되면 승리입니다.   ");
            Console.WriteLine("        2. 강아지의 배고픔, 더러움 스택이 100이 되면 패배입니다. ");
            Console.WriteLine();
            Console.WriteLine("                                                             ");
            Console.WriteLine("     ￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣￣ ");
            Console.Write("                  게임을 시작하려면 아무키나 누르세요");
            Console.ReadKey();
        }
        static void Clear()
        {
            Console.Clear();
            Console.ForegroundColor= ConsoleColor.Red;
            Console.WriteLine("           。　♡ 。　　♡。　　♡");
            Console.WriteLine("           ♡。　＼　　｜　　／。　♡");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("           　    Game Clear!!       ");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("           ♡。　／　　｜　　＼。　♡");
            Console.WriteLine("           。　♡。 　　。　　♡。");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Environment.Exit(0);

        }

        static void End()
        {
            Console.Clear();
            Console.WriteLine("           。　♡ 。　　♡。　　♡");
            Console.WriteLine("           ♡。　＼　　｜　　／。　♡");
            Console.WriteLine();
            Console.WriteLine("           　    Game Over!!       ");
            Console.WriteLine();
            Console.WriteLine("           ♡。　／　　｜　　＼。　♡");
            Console.WriteLine("           。　♡。 　　。　　♡。");
            Console.WriteLine();
            Environment.Exit(0);
        }

        static void Run()
        {
            Console.Clear();

            switch (data.scene)
            {
                case Scene.Select:
                    SelectScene();
                    break;

                case Scene.Confirm:
                    ConfirmScene();
                    break;

                case Scene.Room:
                    HomeScene();
                    break;

                case Scene.Kitchen:
                    KitchenScene();
                    break;

                case Scene.Park:
                    ParkScene();
                    break;

                case Scene.BathRoom:
                    BathRoomScene();
                    break;

                case Scene.Shop:
                    ShopScene();
                    break;

                case Scene.PlayRoom:
                    PlayRoomScene();
                    break;

                case Scene.Inventory:
                    InventoryScene();
                    break;


            }
        }

        static void PuppyProfile()
        {
            Console.WriteLine("==============================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"          이름: ");
            Console.ResetColor();
            Console.WriteLine($"{data.name}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"          종류: ");
            Console.ResetColor();
            Console.WriteLine($"{data.puppy}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"          기분: ");
            Console.ResetColor();
            Console.WriteLine($"{data.feeling}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"          에너지: ");
            Console.ResetColor();
            Console.WriteLine($"{data.energy}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"          배고픔지수: ");
            Console.ResetColor();
            Console.WriteLine($"{data.hungry}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"          더러움지수: ");
            Console.ResetColor();
            Console.WriteLine($"{data.dirty}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"          사용할 수 있는 돈: ");
            Console.ResetColor();
            Console.WriteLine($"{data.money}");
            Console.WriteLine("==============================================");
        }

        static void Wait(float seconds)
        {
            Thread.Sleep((int)(seconds * 1000));
        }

        static void SelectScene()
        {
            Console.Write("강아지의 이름을 정해주세요: ");
            data.name = Console.ReadLine();
            Console.WriteLine();

            if (data.name == "")
            {
                Console.WriteLine("잘못된 입력입니다.");
                return;
            }

            Console.WriteLine("강아지 종류를 선택하세요.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("1. 말티즈  | 2. 시츄 | 3. 비숑 | 4. 치와와");
            Console.ResetColor();
            Console.Write("선택하기: ");

            if (int.TryParse(Console.ReadLine(), out int select) == false)
            {
                Console.WriteLine("잘못된 입력입니다.");
                return;
            }
            else if (Enum.IsDefined(typeof(Scene), select) == false)
            {
                Console.WriteLine("잘못된 입력입니다.");
                return;
            }

            switch ((Puppy)select)
            {
                case Puppy.말티즈:
                    data.puppy = Puppy.말티즈;
                    data.feeling = 50;
                    data.energy = 50;
                    data.hungry = 50;
                    data.dirty = 50;
                    data.money = 5000;
                    break;

                case Puppy.시츄:
                    data.puppy = Puppy.시츄;
                    data.feeling = 50;
                    data.energy = 50;
                    data.hungry = 50;
                    data.dirty = 50;
                    data.money = 5000;
                    break;

                case Puppy.비숑:
                    data.puppy = Puppy.비숑;
                    data.feeling = 50;
                    data.energy = 50;
                    data.hungry = 50;
                    data.dirty = 50;
                    data.money = 5000;
                    break;

                case Puppy.치와와:
                    data.puppy = Puppy.치와와;
                    data.feeling = 50;
                    data.energy = 50;
                    data.hungry = 50;
                    data.dirty = 50;
                    data.money = 5000;
                    break;
            }
            data.scene = Scene.Confirm;
        }

        static void ConfirmScene()
        {
            Console.WriteLine("==============================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"          이름: ");
            Console.ResetColor();
            Console.WriteLine($"{data.name}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"          종류: ");
            Console.ResetColor();
            Console.WriteLine($"{data.puppy}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"          기분: ");
            Console.ResetColor();
            Console.WriteLine($"{data.feeling}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"          에너지: ");
            Console.ResetColor();
            Console.WriteLine($"{data.energy}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"          배고픔지수: ");
            Console.ResetColor();
            Console.WriteLine($"{data.hungry}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"          더러움지수: ");
            Console.ResetColor();
            Console.WriteLine($"{data.dirty}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"          사용할 수 있는 돈: ");
            Console.ResetColor();
            Console.WriteLine($"{data.money}");
            Console.WriteLine("==============================================");
            Console.WriteLine();
            Console.Write("이 강아지를 키우시겠습니까? [네(Y)/아니오(N)]: ");



            string input = Console.ReadLine();

            switch (input)
            {
                case "Y":
                case "y":
                case "네":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("강아지와 함께 생활 할 집의 거실로 이동합니다.");
                    Console.ResetColor();
                    Wait(3);
                    data.scene = Scene.Room;
                    break;

                case "N":
                case "n":
                case "아니오":
                    data.scene = Scene.Select;
                    break;

                default:
                    data.scene = Scene.Confirm;
                    break;
            }
        }

        static void HomeScene()
        {
            PuppyProfile();
            Console.WriteLine();
            Console.WriteLine($"{data.name}(이)에게 무엇을 해 주시겠습니까?");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("1. 밥주기 | 2. 산책나가기 | 3. 샤워시키기 | 4. 쇼핑가기 | 5. 놀아주기 | 6. 인벤토리 열기");
            Console.ResetColor();
            Console.Write("선택하기: ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("주방으로 이동합니다.");
                    Console.ResetColor();
                    Wait(2);
                    data.scene = Scene.Kitchen;
                    break;

                case "2":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("공원으로 이동합니다.");
                    Console.ResetColor();
                    Wait(2);
                    data.scene = Scene.Park;
                    break;

                case "3":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("화장실로 이동합니다.");
                    Console.ResetColor();
                    Wait(2);
                    data.scene = Scene.BathRoom;
                    break;

                case "4":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("상점으로 이동합니다.");
                    Console.ResetColor();
                    Wait(2);
                    data.scene = Scene.Shop;
                    break;

                case "5":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("놀이방으로 이동합니다.");
                    Console.ResetColor();
                    Wait(2);
                    data.scene = Scene.PlayRoom;
                    break;

                case "6":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("인벤토리를 열고 있습니다.");
                    Console.ResetColor();
                    Wait(2);
                    data.scene = Scene.Inventory;
                    break;
            }
        }

        static void KitchenScene()
        {
            Console.WriteLine("강아지 사료 종류가 너무 많은데? 어떤 걸 줘야 하지?");
            Wait(2);
            Console.WriteLine("내가 보기엔 강아지 사료가 아닌 것도 섞여 있는 거 같아... 잘 골라서 줘야겠는데?");
            Wait(2);

            Console.WriteLine();
            Console.WriteLine($"{data.name}에게 줄 사료를 선택하세요");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("1. 퍼피딜라이트 | 2. 와글와글푸드 | 3. 푸르펫츠 | 4. 피쉬프렌지");
            Console.ResetColor();
            Console.Write("선택하기: ");

            string input = Console.ReadLine();
            Console.WriteLine();

            switch (input)
            {
                case "1":
                    Console.WriteLine("말티즈와 시츄가 좋아하는 사료를 선택하셨습니다.");
                    Wait(2);
                    Console.WriteLine();

                    if (data.puppy == Puppy.말티즈 || data.puppy == Puppy.시츄)
                    {
                        Console.WriteLine($"{data.name}(이)가 사료를 맛있게 먹고 있습니다.");
                        Console.WriteLine();
                        Wait(2);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("기분 +20, 배고픔 -20, 에너지 +20을 얻었습니다!");
                        Console.ResetColor();
                        Wait(3);
                        data.feeling += 20;
                        data.hungry -= 20;
                        data.energy += 20;
                        ByeByePuppy();
                        HappyEnding();

                    }
                    else
                    {
                        Console.WriteLine($"{data.name}(이)가 사료를 마음에 들어하지 않습니다.");
                        Console.WriteLine();
                        Wait(2);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("기분 -10, 배고픔 +20, 에너지 -10을 얻었습니다!");
                        Console.ResetColor();
                        Wait(3);
                        data.feeling -= 20;
                        data.hungry += 20;
                        data.energy -= 20;
                        ByeByePuppy();
                        HappyEnding();
                    }
                    break;

                case "2":
                    Console.WriteLine("강아지들 사이에서 인기가 제일 많은 사료를 선택하셨습니다.");
                    Wait(2);
                    Console.WriteLine();
                    Console.WriteLine($"{data.name}(이)가 사료를 굉장히 만족스러워 하고 있습니다.");
                    Console.WriteLine();
                    Wait(2);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("기분 +20, 배고픔 -30, 에너지 +20을 얻었습니다!");
                    Console.ResetColor();
                    Wait(3);
                    data.feeling += 20;
                    data.hungry -= 30;
                    data.energy += 20;
                    ByeByePuppy();
                    HappyEnding();
                    break;

                case "3":
                    Console.WriteLine("비숑과 치와와가 좋아하는 사료를 선택하셨습니다.");
                    Wait(2);
                    Console.WriteLine();

                    if (data.puppy == Puppy.비숑 || data.puppy == Puppy.치와와)
                    {
                        Console.WriteLine($"{data.name}(이)가 사료를 맛있게 먹고 있습니다.");
                        Console.WriteLine();
                        Wait(2);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("기분 +20, 배고픔 -20, 에너지 +20을 얻었습니다!");
                        Console.ResetColor();
                        Wait(3);
                        data.feeling += 20;
                        data.hungry -= 20;
                        data.energy += 20;
                        ByeByePuppy();
                        HappyEnding();
                    }
                    else
                    {
                        Console.WriteLine($"{data.name}(이)가 사료를 마음에 들어하지 않습니다.");
                        Console.WriteLine();
                        Wait(2);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("기분 -10, 배고픔 +20, 에너지 -10을 얻었습니다!");
                        Console.ResetColor();
                        Wait(3);
                        data.feeling -= 20;
                        data.hungry += 20;
                        data.energy -= 20;
                        ByeByePuppy();
                        HappyEnding();
                    }
                    break;

                case "4":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("큰일났습니다!! 이 사료는 강아지 사료가 아닙니다!!");
                    Wait(2);
                    Console.WriteLine();
                    Console.WriteLine($"{data.name}(이)가 매우 화가났습니다!!");
                    Wait(2);
                    Console.WriteLine();
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("기분 -30, 배고픔 +30, 에너지 -30을 얻었습니다!");
                    Console.ResetColor();
                    Wait(3);
                    data.feeling -= 30;
                    data.hungry += 30;
                    data.energy -= 30;
                    ByeByePuppy();
                    HappyEnding();
                    break;

                default:
                    break;
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("거실로 돌아갑니다.");
            Console.ResetColor();
            Wait(2);
            data.scene = Scene.Room;
        }

        static void ParkScene()
        {
            Console.WriteLine("갈림길이 너무 많아 어느 방향으로 산책해야하지?");
            Wait(2);
            Console.WriteLine("저 쪽은 너무 어두워 무언가 나타날 거 같아... 조심하는 게 좋겠어...");
            Wait(2);
            Console.WriteLine();

            Console.WriteLine("산책할 방향을 선택해 주세요.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("1. 오른쪽 | 2. 왼쪽 | 3. 직진 | 4. 되돌아가기");
            Console.ResetColor();
            Console.Write("선택하기: ");

            string input = Console.ReadLine();
            Console.WriteLine();

            switch (input)
            {
                case "1":
                case "3":
                    Console.WriteLine("무사하게 산책을 마쳤습니다.");
                    Wait(2);
                    Console.WriteLine();
                    Console.WriteLine($"{data.name}(이)가 매우 신나합니다. 기분이 매우 좋습니다.");
                    Console.WriteLine();
                    Wait(2);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("기분 +20, 에너지 +20, 배고픔 +20, 더러움 +20을 얻었습니다!");
                    Console.ResetColor();
                    Wait(3);
                    data.feeling += 20;
                    data.energy += 20;
                    data.hungry += 20;
                    data.dirty += 20;
                    ByeByePuppy();
                    HappyEnding();
                    break;

                case "2":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("큰일났습니다!! 골목에서 큰 강아지를 마주쳤습니다!!");
                    Console.WriteLine();
                    Wait(2);
                    Console.ResetColor();
                    Console.WriteLine($"{data.name}(이)가 도망칩니다. 기분이 안 좋은 상태로 산책을 마쳤습니다.");
                    Console.WriteLine();
                    Wait(2);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("기분 -10, 에너지 -10, 배고픔 +20, 더러움 +20을 얻었습니다!");
                    Console.ResetColor();
                    Wait(3);
                    data.feeling -= 10;
                    data.energy -= 10;
                    data.hungry += 20;
                    data.dirty += 20;
                    ByeByePuppy();
                    HappyEnding();
                    break;

                case "4":
                    Console.WriteLine("산책을 하지 않고 집에 돌아갑니다.");
                    Console.WriteLine();
                    Wait(2);
                    Console.WriteLine($"{data.name}(이)가 매우 실망합니다.");
                    Console.WriteLine();
                    Wait(2);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("기분 -20, 에너지 -20를 얻었습니다!");
                    Console.ResetColor();
                    Wait(3);
                    data.feeling -= 20;
                    data.energy -= 20;
                    ByeByePuppy();
                    HappyEnding();
                    break;

                default:
                    break;
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("집으로 돌아갑니다.");
            Console.ResetColor();
            Wait(2);
            data.scene = Scene.Room;
        }

        static void BathRoomScene()
        {
            Console.Write($"{data.name}(이)를 씻기겠습니까? [네(Y)/아니오(N)]: ");

            string input = Console.ReadLine();
            Console.WriteLine();

            switch (input)
            {
                case "Y":
                case "y":
                case "네":
                    Console.WriteLine($"{data.name}(이)가 깨끗해졌습니다!!");
                    Wait(2);
                    Console.WriteLine();
                    Console.WriteLine("하지만 강아지는 씻는 걸 별로 좋아하지 않아 기분은 언짢은 것 같습니다.");
                    Wait(2);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("기분 -20, 배고픔 +20, 에너지 -20, 깨끗함 +40을 얻었습니다!");
                    Console.ResetColor();
                    Wait(3);
                    data.feeling -= 20;
                    data.energy -= 20;
                    data.hungry += 20;
                    data.dirty -= 40;
                    ByeByePuppy();
                    HappyEnding();
                    break;

                case "N":
                case "n":
                case "아니오":
                    Console.WriteLine("샤워를 하지 않았습니다.");
                    Wait(2);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("더러움 +20을 얻었습니다!");
                    Console.ResetColor();
                    Wait(3);
                    data.dirty += 20;
                    ByeByePuppy();
                    HappyEnding();
                    break;

                default:
                    break;
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("거실로 돌아갑니다.");
            Console.ResetColor();
            Wait(2);
            data.scene = Scene.Room;
        }

        static void ShopScene()
        {
            Console.WriteLine("상점에선 옷과 장난감을 살 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("무엇을 사시겠습니까?");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("1. 옷 | 2. 장난감");
            Console.ResetColor();
            Console.Write("선택하기: ");

            string input = Console.ReadLine();
            Console.WriteLine();

            int empty = EmptyInventory(inventories);

            if (empty == -1)
            {
                Console.WriteLine("인벤토리가 가득 찼습니다. 더이상 물건을 살 수 없습니다.");
                return;
            }

            switch (input)
            {
                case "1":
                    BuyClothes(empty);
                    break;
                case "2":
                    BuyToy(empty);
                    break;
            }
        }

        static void BuyClothes(int empty)
        {
            Console.WriteLine($"{data.name}(이)를 위한 옷을 구매할 수 있습니다.");
            Console.WriteLine();
            Wait(2);
            Console.WriteLine("어떤 옷을 구매하시겠습니까?");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("1. 해피 퍼피 셔츠: 1500원");
            Console.WriteLine("2. 로얄 프린센스 드레스: 2500원");
            Console.WriteLine("3. 엘레강트 턱시도: 3000원");
            Console.ResetColor();
            Console.Write("선택하기: ");

            string input1 = Console.ReadLine();
            Console.WriteLine();

            switch (input1)
            {
                case "1":
                    if (AlreadyBuyClothes(Clothes.셔츠))
                    {
                        Console.WriteLine("이미 구매한 상품입니다. 다른 상품을 선택해주세요.");
                        Wait(2);
                        break;
                    }
                    else
                    {
                        data.money -= 1500;
                        if (!NoMoney())
                        {
                            break;
                        }
                        Console.WriteLine("해피 퍼피 셔츠를 구매하셨습니다.");
                        Wait(2);
                        Console.WriteLine();
                        Console.WriteLine("옷이 인벤토리에 저장됩니다.");
                        Wait(2);
                        inventories[empty].clothes = Clothes.셔츠;
                        AgainBuy();
                        break;
                    }



                case "2":
                    if (AlreadyBuyClothes(Clothes.셔츠))
                    {
                        Console.WriteLine("이미 구매한 상품입니다. 다른 상품을 선택해주세요.");
                        Wait(2);
                        break;
                    }
                    else
                    {
                        data.money -= 2500;
                        if (!NoMoney())
                        {
                            break;
                        }
                        Console.WriteLine("로얄 프린센스 드레스를 구매하셨습니다.");
                        Wait(2);
                        Console.WriteLine();
                        Console.WriteLine("옷이 인벤토리에 저장됩니다.");
                        Wait(2);
                        inventories[empty].clothes = Clothes.드레스;
                        AgainBuy();
                        break;
                    }

                case "3":
                    if (AlreadyBuyClothes(Clothes.턱시도))
                    {
                        Console.WriteLine("이미 구매한 상품입니다. 다른 상품을 선택해주세요.");
                        Wait(2);
                        break;
                    }
                    else
                    {
                        data.money -= 3000;
                        if (!NoMoney())
                        {
                            break;
                        }
                        Console.WriteLine("엘레강트 턱시도를 구매하셨습니다.");
                        Wait(2);
                        Console.WriteLine();
                        Console.WriteLine("옷이 인벤토리에 저장됩니다.");
                        Wait(2);
                        inventories[empty].clothes = Clothes.턱시도;
                        AgainBuy();
                        break;
                    }

                default:
                    break;
            }
        }

        static void BuyToy(int empty)
        {
            Console.WriteLine($"{data.name}(이)를 위한 장난감을 구매할 수 있습니다.");
            Console.WriteLine();
            Wait(2);
            Console.WriteLine("어떤 장난감을 구매하시겠습니까?");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("1. 휘리릭 스틱: 1000원");
            Console.WriteLine("2. 지그재그 퍼즐: 2000원");
            Console.WriteLine("3. 러블리 토끼 인형: 2500원");
            Console.ResetColor();
            Console.Write("선택하기: ");

            string input2 = Console.ReadLine();
            Console.WriteLine();

            switch (input2)
            {

                case "1":
                    if (AlreadyBuyToy(Toy.스틱))
                    {
                        Console.WriteLine("이미 구매한 상품입니다. 다른 상품을 선택해주세요.");
                        Wait(2);
                        break;
                    }
                    else
                    {
                        data.money -= 1000;
                        if (!NoMoney())
                        {
                            break;
                        }
                        Console.WriteLine("휘리릭 스틱을 구매하셨습니다.");
                        Wait(2);
                        Console.WriteLine();
                        Console.WriteLine("장난감이 인벤토리에 저장됩니다.");
                        Wait(2);
                        inventories[empty].toy = Toy.스틱;
                        AgainBuy();
                        break;
                    }

                case "2":
                    if (AlreadyBuyToy(Toy.퍼즐))
                    {
                        Console.WriteLine("이미 구매한 상품입니다. 다른 상품을 선택해주세요.");
                        Wait(2);
                        break;
                    }
                    else
                    {
                        data.money -= 2000;
                        if (!NoMoney())
                        {
                            break;
                        }
                        Console.WriteLine("지그재그 퍼즐을 구매하셨습니다.");
                        Wait(2);
                        Console.WriteLine();
                        Console.WriteLine("장난감이 인벤토리에 저장됩니다.");
                        Wait(2);
                        inventories[empty].toy = Toy.퍼즐;
                        AgainBuy();
                        break;
                    }

                case "3":
                    if (AlreadyBuyToy(Toy.인형))
                    {
                        Console.WriteLine("이미 구매한 상품입니다. 다른 상품을 선택해주세요.");
                        Wait(2);
                        break;
                    }
                    else
                    {
                        data.money -= 2500;
                        if (!NoMoney())
                        {
                            break;
                        }
                        Console.WriteLine("러블리 토끼 인형을 구매하셨습니다.");
                        Wait(2);
                        Console.WriteLine();
                        Console.WriteLine("장난감이 인벤토리에 저장됩니다.");
                        Wait(2);
                        inventories[empty].toy = Toy.인형;
                        AgainBuy();
                        break;
                    }
                default:
                    break;
            }

        }

        static void InventoryScene()
        {
            Console.Clear();
            Console.WriteLine("인벤토리 목록입니다.");
            Console.WriteLine();

            bool isEmpty = true;

            for (int i = 0; i < inventories.Length; i++)
            {
                if (inventories[i].clothes != Clothes.None || inventories[i].toy != Toy.None)
                {
                    isEmpty = false;

                    if (inventories[i].clothes != Clothes.None)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("옷: ");
                        Console.ResetColor();
                        Console.WriteLine($"{inventories[i].clothes}");
                        Console.WriteLine();
                    }

                    if (inventories[i].toy != Toy.None)
                    {
                        if (inventories[i].clothes != Clothes.None)
                        {
                            Console.Write(", ");
                        }

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("장난감: ");
                        Console.ResetColor();
                        Console.WriteLine($"{inventories[i].toy} ");
                        Console.WriteLine();
                    }
                }
            }

            if (isEmpty)
            {
                Wait(2);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("인벤토리가 비어있습니다.");
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.WriteLine("인벤토리 확인을 마쳤으면 아무 키나 누르세요");
            Console.ReadKey();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("거실로 돌아갑니다.");
            Console.ResetColor();
            Wait(2);
            data.scene = Scene.Room;
        }

        static void PlayRoomScene()
        {
            Console.WriteLine($"구매하신 장난감으로 {data.name}(이)를 놀아줄 수 있습니다.");
            Wait(2);
            Console.WriteLine("어떤 장난감으로 놀아주시겠습니까?");
            Wait(2);
            Console.WriteLine();
            Console.WriteLine("장난감 목록입니다.");
            Console.WriteLine();

            while (true)
            {
                bool isEmpty_toy = true;

                for (int i = 0; i < inventories.Length; i++)
                {
                    if (inventories[i].toy != Toy.None)
                    {

                        isEmpty_toy = false;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("장난감: ");
                        Console.ResetColor();
                        Console.WriteLine($"{inventories[i].toy} ");
                        Console.WriteLine();
                    }
                }

                if (isEmpty_toy)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("장난감 목록이 비어있습니다.");
                    Wait(3);
                    Console.WriteLine();
                    Console.ResetColor();
                    Console.WriteLine();

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("놀아 줄 장난감이 없어 거실로 돌아갑니다.");
                    Wait(3);
                    Console.ResetColor();
                    data.scene = Scene.Room;
                    return;
                }

                Console.WriteLine();
                Console.Write("목록에 존재하는 장난감 이름을 입력해 주세요: ");
                string toyChoice = Console.ReadLine();
                Console.WriteLine();

                switch (toyChoice)
                {
                    case "doll":
                    case "stick":
                    case "puzzle":
                    case "DOLL":
                    case "STICK":
                    case "PUZZLE":
                    case "Doll":
                    case "Stick":
                    case "Puzzle":
                    case "인형":
                    case "스틱":
                    case "퍼즐":
                        Console.WriteLine($"{data.name}(이)가 장난감({toyChoice})으로 재미있게 놀고 있습니다.");
                        Console.WriteLine();
                        Wait(2);
                        Console.WriteLine($"{data.name}(이)가 신나합니다.");
                        Console.WriteLine();
                        Wait(2);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("기분 +20, 에너지 +20, 배고픔 +20을 얻었습니다!");
                        Console.ResetColor();
                        Wait(3);
                        data.feeling += 20;
                        data.energy += 20;
                        data.hungry += 20;
                        ByeByePuppy();
                        HappyEnding();

                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("거실로 돌아갑니다.");
                        Console.ResetColor();
                        Wait(2);
                        data.scene = Scene.Room;
                        return;

                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("없는 장난감입니다. 다시 입력해 주세요");
                        Console.ResetColor();
                        Console.WriteLine();
                        continue;

                }
            }
        }
       
    }
}
