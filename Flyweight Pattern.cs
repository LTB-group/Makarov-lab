using System;
using System.Collections.Generic;

public class Character
{
    // Легковесные данные (общие для всех экземпляров данного типа и имени)
    public string Name { get; private set; }
    public string Type { get; private set; }
    public string Image { get; private set; }

    // Тяжелые данные (уникальные для каждого экземпляра)
    public int Level { get; set; }
    public int Experience { get; set; }

    // Закрытый конструктор, чтобы запретить создание объекта напрямую
    private Character(string name, string type, string image)
    {
        Name = name;
        Type = type;
        Image = image;
        Level = 1;
        Experience = 0;
    }

    // Вложенная фабрика для управления созданием персонажей
    public static class CharacterFactory
    {
        private static readonly Dictionary<string, Character> _characters = new Dictionary<string, Character>();

        // Метод для создания или получения уже существующего персонажа
        public static Character GetCharacter(string name, string type, string image)
        {
            string key = name + ":" + type;

            if (_characters.ContainsKey(key))
            {
                return _characters[key];
            }

            var newCharacter = new Character(name, type, image);
            _characters[key] = newCharacter;
            return newCharacter;
        }
    }

    public override string ToString()
    {
        return $"Character [Name: {Name}, Type: {Type}, Image: {Image}, Level: {Level}, Experience: {Experience}]";
    }
}

class Program
{
    static void Main()
    {
        // Создание персонажей через фабрику
        var character1 = Character.CharacterFactory.GetCharacter("Arthur", "Warrior", "warrior.png");
        var character2 = Character.CharacterFactory.GetCharacter("Arthur", "Warrior", "warrior.png");
        var character3 = Character.CharacterFactory.GetCharacter("Luna", "Mage", "mage.png");

        // Демонстрация изменения уникальных данных
        character1.Level = 5;
        character1.Experience = 1200;

        character3.Level = 3;
        character3.Experience = 800;

        // Вывод данных персонажей
        Console.WriteLine(character1);
        Console.WriteLine(character2);
        Console.WriteLine(character3);

        // Проверка, что фабрика возвращает одинаковый объект для одного имени и типа
        Console.WriteLine("Are character1 and character2 the same instance? " + (character1 == character2));
    }
}
