using System;
using System.IO;
using System.Threading;

class Program
{
    static int playerX;
    static int playerY;

    static int startX = 12;
    static int startY = 15;

    static int stepX = 4;
    static int stepY = 5;

    static int[] itemX = new int[4];
    static int[] itemY = new int[4];
    static char[] itemType = new char[4];
    static bool[] active = new bool[4];

    static int score;

    static DateTime startTime;
    static int gameDuration = 25;

    static Random rnd = new Random();
    static string logPath = "log.txt";

    static void Main()
    {
        Console.CursorVisible = false;

        while (true)
        {
            StartGame();
            RunGame();
            GameOverScreen();
        }
    }

    // ================= START =================
    static void StartGame()
    {
        playerX = startX;
        playerY = startY;

        score = 0;
        startTime = DateTime.Now;

        Log($"GAME START → score={score}");

        SpawnWave();
    }

    // ================= MAIN LOOP =================
    static void RunGame()
    {
        while (true)
        {
            // REAL TIMER
            int elapsed = (int)(DateTime.Now - startTime).TotalSeconds;
            int timeLeft = gameDuration - elapsed;

            if (timeLeft <= 0)
            {
                Log($"TIME END → score={score}");
                return;
            }

            // INPUT
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.LeftArrow) playerX -= stepX;
                if (key == ConsoleKey.RightArrow) playerX += stepX;
                if (key == ConsoleKey.UpArrow) playerY -= stepY;
                if (key == ConsoleKey.DownArrow) playerY += stepY;

                Log($"INPUT → key={key} playerX={playerX} playerY={playerY}");
            }

            // PLAYER LOG
            Log($"PLAYER → x={playerX} y={playerY}");

            // WRAP
            int maxX = Console.WindowWidth;

            if (playerX < 0) playerX = maxX - stepX;
            if (playerX >= maxX) playerX = 0;

            // ITEMS
            bool waveAlive = false;

            for (int i = 0; i < 4; i++)
            {
                if (!active[i]) continue;

                itemY[i] += 1;

                Log($"UPDATE → item{i} type={itemType[i]} x={itemX[i]} y={itemY[i]}");

                if (playerX == itemX[i] && playerY == itemY[i])
                {
                    if (itemType[i] == '*')
                        score += 1;
                    else if (itemType[i] == 'O')
                        score += 3;

                    Log($"COLLISION → type={itemType[i]} score={score}");

                    Log($"SCORE UPDATE → score={score}");

                    if (score >= 17)
                    {
                        Log($"SCORE LIMIT REACHED → score={score}");
                        return;
                    }

                    KillWave();
                    SpawnWave();
                    waveAlive = true;
                    break;
                }

                if (itemY[i] < Console.WindowHeight)
                    waveAlive = true;
            }

            if (!waveAlive)
            {
                Log($"WAVE LOST → score={score}");
                return;
            }

            // DRAW
            Console.Clear();

            Console.SetCursorPosition(playerX, playerY);
            Console.Write("@");

            for (int i = 0; i < 4; i++)
            {
                if (!active[i]) continue;

                Console.SetCursorPosition(itemX[i], itemY[i]);
                Console.Write(itemType[i]);
            }

            Console.SetCursorPosition(0, 0);
            Console.Write($"Skor: {score}  Süre: {timeLeft}");

            Thread.Sleep(80);
        }
    }

    // ================= GAME OVER =================
    static void GameOverScreen()
    {
        Console.Clear();
        Console.WriteLine("OYUN BİTTİ!");
        Console.WriteLine("Skorunuz: " + score);
        Console.WriteLine();
        Console.WriteLine("R: yeniden başlat");
        Console.WriteLine("ESC: çıkış");

        Log($"GAME OVER → score={score}");

        while (true)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;

                Log($"INPUT MENU → key={key}");

                if (key == ConsoleKey.R)
                    return;

                if (key == ConsoleKey.Escape)
                {
                    Log("EXIT GAME");
                    Environment.Exit(0);
                }
            }

            Thread.Sleep(50);
        }
    }

    // ================= SPAWN =================
    static void SpawnWave()
    {
        for (int i = 0; i < 4; i++)
        {
            itemX[i] = rnd.Next(0, Console.WindowWidth / stepX) * stepX;
            itemY[i] = 0;

            active[i] = true;

            itemType[i] = (i < 2) ? '*' : 'O';

            Log($"SPAWN → item{i} type={itemType[i]} x={itemX[i]} y={itemY[i]}");
        }
    }

    static void KillWave()
    {
        for (int i = 0; i < 4; i++)
            active[i] = false;
    }

    // ================= LOG SYSTEM =================
    static void Log(string msg)
    {
        File.AppendAllText(logPath, msg + "\n");
    }
}