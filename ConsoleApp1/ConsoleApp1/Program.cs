// Вариант 1
// Создаём массив из 10 объектов 

Student<int>[] students = new Student<int>[10];


for (int i = 0; i < students.Length; i++)
{

    Console.Write("Фамилия и инициалы: ");
    string name = Console.ReadLine()!;
   
    Console.Write("Номер группы: ");
    int group = int.Parse(Console.ReadLine()!);
    
    int[] grades = new int[5];
    
    Console.WriteLine("5 оценок:");
    // Цикл для ввода каждой из 5 оценок
    for (int j = 0; j < 5; j++)
    {
        grades[j] = int.Parse(Console.ReadLine()!);
    }

    students[i] = new Student<int>(name, group, grades);
}


Array.Sort(students);


Console.WriteLine();

bool found = false;

foreach (var s in students)
{
    // Проверяем что средний балл больше 4.0
    if (s.Average() > 4.0)
    {
        
        Console.WriteLine($"{s.Name} - группа {s.Group}");
      
        found = true;
    }
}

if (!found) Console.WriteLine("Таких студентов нет");

Console.WriteLine();

Array.Sort(students, new StudentByName<int>());

foreach (var s in students) Console.WriteLine(s);


class Student<T> : ICloneable, IComparable<Student<T>> where T : IComparable<T>
{
    
    public string Name { get; set; }

    public T Group { get; set; }

    public int[] Grades { get; set; }

    public Student(string name, T group, int[] grades)
    {
        Name = name;
        Group = group;
        Grades = grades;
    }

    // Метод для вычисления среднего балла
    public double Average() => Grades.Average();

    public object Clone()
    {
        return new Student<T>(Name, Group, (int[])Grades.Clone());
    }

    //сравнение по номеру группы
    public int CompareTo(Student<T>? other)
    {
        if (other == null) return 1;
       
        return Group.CompareTo(other.Group);
    }

    public override string ToString() => $"{Name}, группа {Group}, средний балл {Average():F2}";
}

class StudentByName<T> : IComparer<Student<T>> where T : IComparable<T>
{
    
    public int Compare(Student<T>? x, Student<T>? y)
    {
        if (x == null || y == null) throw new ArgumentException();
        // Сравниваем фамилии и инициалы
        return x.Name.CompareTo(y.Name);
    }
}
