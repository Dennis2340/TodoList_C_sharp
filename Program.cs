using System;
using System.Collections.Generic;
using System.IO;

class ToDoItem
{
    public string Task { get; set; }
    public bool IsCompleted { get; set; }
}

class Program
{
    static List<ToDoItem> tasks = new List<ToDoItem>();
    static string dataFilePath = "tasks.txt";

    static void Main()
    {
        LoadTasksFromFile();

        Console.WriteLine("Welcome to the To-Do List Application!");

        while (true)
        {
            Console.WriteLine("\nPlease select an option:");
            Console.WriteLine("1. Add a task");
            Console.WriteLine("2. View tasks");
            Console.WriteLine("3. Mark task as completed");
            Console.WriteLine("4. Save and Exit");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        AddTask();
                        break;
                    case 2:
                        ViewTasks();
                        break;
                    case 3:
                        MarkTaskAsCompleted();
                        break;
                    case 4:
                        SaveTasksToFile();
                        Console.WriteLine("Tasks saved successfully. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }

    static void AddTask()
    {
        Console.WriteLine("Enter the task:");
        string task = Console.ReadLine();

        tasks.Add(new ToDoItem { Task = task, IsCompleted = false });
        Console.WriteLine("Task added successfully!");
    }

    static void ViewTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks found.");
            return;
        }

        Console.WriteLine("Tasks:");

        for (int i = 0; i < tasks.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {(tasks[i].IsCompleted ? "[Done] " : "")}{tasks[i].Task}");
        }
    }

    static void MarkTaskAsCompleted()
    {
        ViewTasks();

        Console.WriteLine("Enter the task number you want to mark as completed:");
        if (int.TryParse(Console.ReadLine(), out int taskNumber))
        {
            if (taskNumber >= 1 && taskNumber <= tasks.Count)
            {
                tasks[taskNumber - 1].IsCompleted = true;
                Console.WriteLine("Task marked as completed!");
            }
            else
            {
                Console.WriteLine("Invalid task number. Please try again.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a number.");
        }
    }

    static void LoadTasksFromFile()
    {
        if (File.Exists(dataFilePath))
        {
            try
            {
                string[] lines = File.ReadAllLines(dataFilePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 2)
                    {
                        string task = parts[0];
                        bool isCompleted = bool.Parse(parts[1]);
                        tasks.Add(new ToDoItem { Task = task, IsCompleted = isCompleted });
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Error loading tasks from file.");
            }
        }
    }

    static void SaveTasksToFile()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(dataFilePath))
            {
                foreach (var task in tasks)
                {
                    writer.WriteLine($"{task.Task}|{task.IsCompleted}");
                }
            }
        }
        catch (IOException)
        {
            Console.WriteLine("Error saving tasks to file.");
        }
    }
}
