using System;
using System.Collections.Generic;
using System.Globalization;

abstract class Product
{
    public string Name { get; set; }
    public virtual double Price { get; set; }
    public abstract string GetInformation();

    public Product(string name, double price)
    {
        Name = name;
        Price = price;
    }
}

class Toy : Product
{
    public string Material { get; set; }

    public Toy(string name, double price, string material) : base(name, price)
    {
        Material = material;
    }

    public override string GetInformation()
    {
        return $"Toy: {Name}, Price: {Price:C}, Material: {Material}";
    }
}

class Meat : Product
{
    public string Type { get; set; }

    public Meat(string name, double price, string type) : base(name, price)
    {
        Type = type;
    }

    public override string GetInformation()
    {
        return $"Meat: {Name}, Price: {Price:C}, Type: {Type}";
    }
}

class Drinks : Product
{
    public bool IsAlcoholic { get; set; }

    public Drinks(string name, double price, bool isAlcoholic) : base(name, price)
    {
        IsAlcoholic = isAlcoholic;
    }

    public override string GetInformation()
    {
        return $"Drinks: {Name}, Price: {Price:C}, Alcoholic: {IsAlcoholic}";
    }
}

class Clothing : Product
{
    public string Size { get; set; }

    public Clothing(string name, double price, string size) : base(name, price)
    {
        Size = size;
    }

    public override string GetInformation()
    {
        return $"Clothing: {Name}, Price: {Price:C}, Size: {Size}";
    }
}

class Electronics : Product
{
    public int WarrantyPeriod { get; set; }

    public Electronics(string name, double price, int warrantyPeriod) : base(name, price)
    {
        WarrantyPeriod = warrantyPeriod;
    }

    public override string GetInformation()
    {
        return $"Electronics: {Name}, Price: {Price:C}, Warranty: {WarrantyPeriod} months";
    }
}

class DiscountedProduct : Product
{
    private double _discountPercentage;

    public DiscountedProduct(Product product, double discountPercentage) : base(product.Name, product.Price)
    {
        _discountPercentage = discountPercentage;
    }

    public override double Price
    {
        get
        {
            return base.Price - (base.Price * _discountPercentage / 100);
        }
    }

    public override string GetInformation()
    {
        return $"{Name}, Original Price: {base.Price:C}, Discounted Price: {Price:C}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        double discount = 55;
        List<Product> products = new List<Product>
        {
            new Toy("Lego Set", 50.0, "Plastic"),
            new Meat("Chicken Breast", 12.0, "Chicken"),
            new Drinks("Coca-Cola", 2.0, false),
            new Clothing("T-Shirt", 15.0, "M"),
            new Electronics("Smartphone", 300.0, 24)
        };

        List<Product> discountedProducts = new List<Product>();
        foreach (var product in products)
        {
            discountedProducts.Add(new DiscountedProduct(product, discount));
        }

        foreach (var product in discountedProducts)
        {
            Console.WriteLine(product.GetInformation());
        }
    }
}
