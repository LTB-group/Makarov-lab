using System;
using System.Collections.Generic;


public interface IDocumentComponent
{
    void Add(IDocumentComponent component);
    void Remove(IDocumentComponent component);
    void Display(int depth = 0);
}


public class Paragraph : IDocumentComponent
{
    private readonly string _text;

    public Paragraph(string text)
    {
        _text = text;
    }

    public void Add(IDocumentComponent component)
    {
        throw new InvalidOperationException("Cannot add a component to a paragraph.");
    }

    public void Remove(IDocumentComponent component)
    {
        throw new InvalidOperationException("Cannot remove a component from a paragraph.");
    }

    public void Display(int depth = 0)
    {
        Console.WriteLine(new string(' ', depth) + "- " + _text);
    }
}


public class Section : IDocumentComponent
{
    private readonly string _title;
    private readonly List<IDocumentComponent> _components = new List<IDocumentComponent>();

    public Section(string title)
    {
        _title = title;
    }

    public void Add(IDocumentComponent component)
    {
        _components.Add(component);
    }

    public void Remove(IDocumentComponent component)
    {
        _components.Remove(component);
    }

    public void Display(int depth = 0)
    {
        Console.WriteLine(new string(' ', depth) + "* " + _title);
        foreach (var component in _components)
        {
            component.Display(depth + 2);
        }
    }
}


public class Document : IDocumentComponent
{
    private readonly List<IDocumentComponent> _sections = new List<IDocumentComponent>();

    public void Add(IDocumentComponent component)
    {
        _sections.Add(component);
    }

    public void Remove(IDocumentComponent component)
    {
        _sections.Remove(component);
    }

    public void Display(int depth = 0)
    {
        Console.WriteLine("Document Structure:");
        foreach (var section in _sections)
        {
            section.Display(depth + 2);
        }
    }
}


class Program
{
    static void Main()
    {
        // Создаем документ
        Document document = new Document();

        // Создаем разделы и параграфы
        Section section1 = new Section("Introduction");
        section1.Add(new Paragraph("This is the introduction paragraph."));

        Section section2 = new Section("Main Content");
        section2.Add(new Paragraph("This is the first paragraph of the main content."));

        Section subsection = new Section("Subsection 1.1");
        subsection.Add(new Paragraph("This is a paragraph in the subsection."));
        section2.Add(subsection);

        Section section3 = new Section("Conclusion");
        section3.Add(new Paragraph("This is the conclusion paragraph."));

        // Добавляем разделы в документ
        document.Add(section1);
        document.Add(section2);
        document.Add(section3);

        // Отображаем структуру документа
        document.Display();
    }
}
