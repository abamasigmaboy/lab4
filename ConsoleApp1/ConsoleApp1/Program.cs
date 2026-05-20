//вариант 1
Student[] students = new Student[10];

for (int i = 0; i < students.Length; i++)
{
    Console.Write("Фамилия и инициалы: ");
    string name = Console.ReadLine()!;
    Console.Write("Номер группы: ");
    int group = int.Parse(Console.ReadLine()!);
    int[] grades = new int[5];
    Console.WriteLine("5 оценок:");
    for (int j = 0; j < 5; j++)
    {
        grades[j] = int.Parse(Console.ReadLine()!);
    }
    students[i] = new Student(name, group, grades);
}

Array.Sort(students);

Console.WriteLine();
bool found = false;
foreach (var s in students)
{
    if (s.Average() > 4.0)
    {
        Console.WriteLine($"{s.Name} - группа {s.Group}");
        found = true;
    }
}
if (!found) Console.WriteLine("Таких студентов нет");

Console.WriteLine();
Array.Sort(students, new StudentByName());
foreach (var s in students) Console.WriteLine(s);

class Student : ICloneable, IComparable
{
    public string Name { get; set; }
    public int Group { get; set; }
    public int[] Grades { get; set; }

    public Student(string name, int group, int[] grades)
    {
        Name = name;
        Group = group;
        Grades = grades;
    }

    public double Average() => Grades.Average();

    public object Clone()
    {
        return new Student(Name, Group, (int[])Grades.Clone());
    }

    public int CompareTo(object? obj)
    {
        if (obj is Student s) return Group.CompareTo(s.Group);
        throw new ArgumentException();
    }

    public override string ToString() => $"{Name}, группа {Group}, средний балл {Average():F2}";
}

class StudentByName : IComparer<Student>
{
    public int Compare(Student? x, Student? y)
    {
        if (x == null || y == null) throw new ArgumentException();
        return x.Name.CompareTo(y.Name);
    }
}
