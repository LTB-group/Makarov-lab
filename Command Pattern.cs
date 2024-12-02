using System;
using System.Collections.Generic;

public interface ICommand
{
    void Execute();
    void Undo();
}

public class TextEditor
{
    public string Text { get; private set; } = "";

    public void InsertText(string text)
    {
        Text += text;
    }

    public void DeleteText(int length)
    {
        if (length > Text.Length) length = Text.Length;
        Text = Text.Substring(0, Text.Length - length);
    }
}

public class InsertTextCommand : ICommand
{
    private readonly TextEditor _editor;
    private readonly string _text;

    public InsertTextCommand(TextEditor editor, string text)
    {
        _editor = editor;
        _text = text;
    }

    public void Execute()
    {
        _editor.InsertText(_text);
    }

    public void Undo()
    {
        _editor.DeleteText(_text.Length);
    }
}

public class DeleteTextCommand : ICommand
{
    private readonly TextEditor _editor;
    private readonly int _length;
    private string _deletedText;

    public DeleteTextCommand(TextEditor editor, int length)
    {
        _editor = editor;
        _length = length;
    }

    public void Execute()
    {
        if (_length > _editor.Text.Length) _deletedText = _editor.Text;
        else _deletedText = _editor.Text.Substring(_editor.Text.Length - _length);
        _editor.DeleteText(_length);
    }

    public void Undo()
    {
        _editor.InsertText(_deletedText);
    }
}

public class CommandManager
{
    private readonly Stack<ICommand> _commandHistory = new Stack<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _commandHistory.Push(command);
    }

    public void UndoLastCommand()
    {
        if (_commandHistory.Count > 0)
        {
            var command = _commandHistory.Pop();
            command.Undo();
        }
    }
}

class Program
{
    static void Main()
    {
        var editor = new TextEditor();
        var manager = new CommandManager();

        var insertCommand1 = new InsertTextCommand(editor, "Hello ");
        var insertCommand2 = new InsertTextCommand(editor, "World!");
        var deleteCommand = new DeleteTextCommand(editor, 6);

        manager.ExecuteCommand(insertCommand1);
        Console.WriteLine(editor.Text);

        manager.ExecuteCommand(insertCommand2);
        Console.WriteLine(editor.Text);

        manager.ExecuteCommand(deleteCommand);
        Console.WriteLine(editor.Text);

        manager.UndoLastCommand();
        Console.WriteLine(editor.Text);

        manager.UndoLastCommand();
        Console.WriteLine(editor.Text);
    }
}
