using System;

public interface IWeapon
{
    void UseWeapon();
}

public class Sword : IWeapon
{
    public void UseWeapon()
    {
        Console.WriteLine("Swinging the sword!");
    }
}

public class Bow : IWeapon
{
    public void UseWeapon()
    {
        Console.WriteLine("Shooting an arrow!");
    }
}

public class Axe : IWeapon
{
    public void UseWeapon()
    {
        Console.WriteLine("Swinging the axe!");
    }
}

public class Player
{
    private IWeapon currentWeapon;

    public Player(IWeapon weapon)
    {
        currentWeapon = weapon;
    }

    public void SetWeapon(IWeapon weapon)
    {
        currentWeapon = weapon;
    }

    public void Attack()
    {
        currentWeapon.UseWeapon();
    }
}

public class Game
{
    public void Start()
    {
        Player player = new(new Sword());

        while (true)
        {
            Console.WriteLine("Choose a weapon: 1. Sword  2. Bow  3. Axe  4. Exit");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    player.SetWeapon(new Sword());
                    break;
                case "2":
                    player.SetWeapon(new Bow());
                    break;
                case "3":
                    player.SetWeapon(new Axe());
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    continue;
            }

            Console.WriteLine("Attacking with the chosen weapon:");
            player.Attack();
        }
    }
}

class Program
{
    static void Main()
    {
        Game game = new();
        game.Start();
    }
}
