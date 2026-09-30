using System;
using System.Collections.Generic;
using System.Linq;
using Fio.Core;

namespace Laboratorio1;

public static class Program
{
    private static readonly List<string> saved = new();
    private static string current = string.Empty;
    private static bool inInitials;
    private static int initCount;
    private static FioTemplate? template;

    public static void Main()
    {
        Console.WriteLine("Ingreso automático de FIO en consola");
        Console.WriteLine("Ejemplo: Novikov-Priboy A.S.");
        Console.WriteLine("La primera entrada define la plantilla para las siguientes.");
        Console.WriteLine("Presione Escape para salir.");

        while (true)
        {
            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                break;
            }

            if (key.Key == ConsoleKey.Backspace)
            {
                RemoveLastChar();
                continue;
            }

            if (key.Key == ConsoleKey.Enter)
            {
                SaveCurrent();
                continue;
            }

            if (key.KeyChar == ' ')
            {
                HandleSpace();
                continue;
            }

            if (key.KeyChar == '-')
            {
                HandleHyphen();
                continue;
            }

            if (!char.IsLetter(key.KeyChar))
            {
                continue;
            }

            HandleLetter(key.KeyChar);
        }
    }

    private static void HandleSpace()
    {
        if (string.IsNullOrEmpty(current) || current.Contains(' ') || inInitials)
        {
            return;
        }

        current += " ";
        inInitials = true;
        initCount = 0;
        Console.Write(' ');
    }

    private static void HandleHyphen()
    {
        if (string.IsNullOrEmpty(current) || inInitials || current.Contains(' ') || current.EndsWith('-'))
        {
            return;
        }

        current += '-';
        Console.Write('-');
    }

    private static void HandleLetter(char ch)
    {
        if (inInitials)
        {
            var letter = char.ToUpperInvariant(ch);
            current += letter;
            current += '.';
            initCount++;
            Console.Write(letter);
            Console.Write('.');

            if (initCount >= 2)
            {
                Console.WriteLine();
                SaveCurrent();
                ResetState();
            }

            return;
        }

        if (string.IsNullOrEmpty(current) || current.EndsWith('-'))
        {
            current += char.ToUpperInvariant(ch);
            Console.Write(char.ToUpperInvariant(ch));
        }
        else
        {
            current += char.ToLowerInvariant(ch);
            Console.Write(char.ToLowerInvariant(ch));
        }
    }

    private static void RemoveLastChar()
    {
        if (string.IsNullOrEmpty(current))
        {
            return;
        }

        current = current.Substring(0, current.Length - 1);
        Console.Write("\b \b");
    }

    private static void SaveCurrent()
    {
        var value = current.Trim();

        if (!FioValidator.TryParse(value, out var candidate) || candidate is null)
        {
            Console.WriteLine();
            Console.WriteLine($"Formato inválido: '{value}'");
            Console.WriteLine("Debe ser del tipo: Novikov-Priboy A.S.");
            ResetState();
            return;
        }

        if (template is not null && !template.Matches(candidate))
        {
            Console.WriteLine();
            Console.WriteLine($"Formato inválido: '{value}'");
            Console.WriteLine($"No coincide con la plantilla: {template}");
            ResetState();
            return;
        }

        template ??= candidate;

        saved.Add(value);
        Console.WriteLine();
        Console.WriteLine($"Guardado: {value}");
        Console.WriteLine($"Plantilla activa: {template}");
        Console.WriteLine("Entradas:");

        foreach (var entry in saved)
        {
            Console.WriteLine(entry);
        }

        ResetState();
    }

    private static void ResetState()
    {
        current = string.Empty;
        inInitials = false;
        initCount = 0;
    }

}
