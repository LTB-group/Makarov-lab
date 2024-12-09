using System;
using System.Collections;
using System.Collections.Generic;

public class Book
{
    public string Title { get; }
    public string Author { get; }
    public int Year { get; }

    public Book(string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
    }
}

public class Library : IEnumerable<Book>
{
    private readonly List<Book> books = new();

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public IEnumerator<Book> GetEnumerator()
    {
        return books.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

class Program
{
    static void Main()
    {
        Library library = new();
        library.AddBook(new Book("Book 1", "Author A", 2001));
        library.AddBook(new Book("Book 2", "Author B", 2002));
        library.AddBook(new Book("Book 3", "Author C", 2003));

        foreach (var book in library)
        {
            Console.WriteLine($"{book.Title} by {book.Author} ({book.Year})");
        }
    }
}
