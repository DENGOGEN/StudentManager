using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace StudentManager
{
    public class GroupDisplay
    {
        public string GroupName { get; set; } = string.Empty;
        public ObservableCollection<StudentNode> Students { get; set; } = new ObservableCollection<StudentNode>();
    }

    public class StudentNode
    {
        public string StudentName { get; set; } = string.Empty;
        public string GroupName { get; set; } = string.Empty;
    }

    public class MainViewModel
    {
        // Расширенная актуальная база данных студентов
        // Полная база данных: от 10 до 20 студентов в каждой группе
        private readonly List<Student> _allowedStudentsDatabase = new List<Student>
        {
            // ===== ГРУППА ИСП-11 =====
            new Student("Иванов Иван Иванович", "ИСП-11"),
            new Student("Макаров Максим Андреевич", "ИСП-11"),
            new Student("Петров Петр Петрович", "ИСП-11"),
            new Student("Астахов Илья Сергеевич", "ИСП-11"),
            new Student("Борисов Денис Олегович", "ИСП-11"),
            new Student("Волкова Алина Дмитриевна", "ИСП-11"),
            new Student("Григорьев Антон Юрьевич", "ИСП-11"),
            new Student("Пабло Эскобар ", "ИСП-11"),
            new Student("Егорова Елена Николаевна", "ИСП-11"),
            new Student("Зимин Максим Игоревич", "ИСП-11"),
            new Student("Игнатов Владислав Олегович", "ИСП-11"),
            new Student("Кириллов Артем Андреевич", "ИСП-11"),
            new Student("Котова Марина Викторовна", "ИСП-11"),

            // ===== ГРУППА ИСП-21 =====
            new Student("Васильев Василий Васильевич", "ИСП-21"),
            new Student("Кузнецов Алексей Николаевич", "ИСП-21"),
            new Student("Михайлов Михаил Михайлович", "ИСП-21"),
            new Student("Николаев Николай Николаевич", "ИСП-21"),
            new Student("Орлов Олег Петрович", "ИСП-21"),
            new Student("Павлов Павел Сергеевич", "ИСП-21"),
            new Student("Романов Роман Александрович", "ИСП-21"),
            new Student("Степанов Степан Игоревич", "ИСП-21"),
            new Student("Тарасов Денис Владимирович", "ИСП-21"),
            new Student("Ушаков Кирилл Алексеевич", "ИСП-21"),
            new Student("Филатов Андрей Дмитриевич", "ИСП-21"),
            new Student("Хромов Илья Евгеньевич", "ИСП-21"),

            // ===== ГРУППА ИСП-22 =====
            new Student("Барак Хусейн Обама", "ИСП-22"),
            new Student("Попова Елена Владимировна", "ИСП-22"),
            new Student("Федоров Дмитрий Александрович", "ИСП-22"),
            new Student("Андреева Ольга Борисовна", "ИСП-22"),
            new Student("Белов Кристиан Эдуардович", "ИСП-22"),
            new Student("Виноградов Максим Юрьевич", "ИСП-22"),
            new Student("Гаврилов Данила Денисович", "ИСП-22"),
            new Student("Данилов Никита Олегович", "ИСП-22"),
            new Student("Ермаков Арсений Маратович", "ИСП-22"),
            new Student("Жуков Илья Вячеславович", "ИСП-22"),
            new Student("Зайцев Матвей Романович", "ИСП-22"),
            new Student("Ильин Захар Артемович", "ИСП-22"),
            new Student("Казаков Лев Никитич", "ИСП-22"),

            // ===== ГРУППА ОДЛ-11 =====
            new Student("Морозов Егор Павлович", "ОДЛ-11"),
            new Student("Козлов Михаил Дмитриевич", "ОДЛ-11"),
            new Student("Лебедева Ольга Игоревна", "ОДЛ-11"),
            new Student("Макарова София Руслановна", "ОДЛ-11"),
            new Student("Новиков Артемий Егорович", "ОДЛ-11"),
            new Student("Пономарев Глеб Маркович", "ОДЛ-11"),
            new Student("Джо Робинетт Байден", "ОДЛ-11"),
            new Student("Савельев Лев Даниилович", "ОДЛ-11"),
            new Student("Тихонов Михаил Кириллович", "ОДЛ-11"),
            new Student("Фролова Варвара Алексеевна", "ОДЛ-11"),
            new Student("Чернов Ярослав Русланович", "ОДЛ-11"),
            new Student("Шаров Федор Маркович", "ОДЛ-11"),

            // ===== ГРУППА ЖКХ-11 =====
            new Student("Новикова Дарья Артемовна", "ЖКХ-11"),
            new Student("Волков Александр Олегович", "ЖКХ-11"),
            new Student("Соловьев Илья Андреевич", "ЖКХ-11"),
            new Student("Баранов Тимофей Алексеевич", "ЖКХ-11"),
            new Student("Кудрявцева Василиса Михайловна", "ЖКХ-11"),
            new Student("Александрова Милана Даниэльевна", "ЖКХ-11"),
            new Student("Самойлов Даниил Никитич", "ЖКХ-11"),
            new Student("Королев Роман Маркович", "ЖКХ-11"),
            new Student("Журавлев Артемий Давидович", "ЖКХ-11"),
            new Student("Аль Капоне", "ЖКХ-11"),
            new Student("Карпов Семен Максимович", "ЖКХ-11"),
            new Student("Крылов Михаил Георгиевич", "ЖКХ-11"),

            // ===== ГРУППА Э-21 =====
            new Student("Сидоров Сидор Сидорович", "Э-21"),
            new Student("Алексеев Алексей Алексеевич", "Э-21"),
            new Student("Новый Студент Свежий", "Э-21"),
            new Student("Кузнецова Мария Дмитриевна", "Э-21"),
            new Student("Васильева Анна Львовна", "Э-21"),
            new Student("Соколов Федор Максимович", "Э-21"),
            new Student("Саддам Хусейн", "Э-21"),
            new Student("Федорова Кира Егоровна", "Э-21"),
            new Student("Яковлев Тимофей Русланович", "Э-21"),
            new Student("Панфилов Марк Богданович", "Э-21"),
            new Student("Винокуров Дмитрий Данилович", "Э-21"),
            new Student("Савина Виктория Кирилловна", "Э-21"),
            new Student("Ковалева Елизавета Тимофеевна", "Э-21")
        };

        private readonly string _rootStoragePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StudentGroups");
        public ObservableCollection<GroupDisplay> DisplayedGroups { get; } = new ObservableCollection<GroupDisplay>();

        public ICommand AddStudentCommand { get; }
        public ICommand DeleteStudentCommand { get; }
        public ICommand ShowFullStudentListCommand { get; }

        public MainViewModel()
        {
            AddStudentCommand = new RelayCommand(OnAddStudent);
            DeleteStudentCommand = new RelayCommand(OnDeleteStudent);
            ShowFullStudentListCommand = new RelayCommand(OnShowFullStudentList);

            if (!Directory.Exists(_rootStoragePath)) Directory.CreateDirectory(_rootStoragePath);
            RefreshUIFromDisk();
        }

        // Передаем в окно чистый список студентов из базы данных
        private void OnShowFullStudentList(object obj)
        {
            FullStudentListWindow.Show(_allowedStudentsDatabase);
        }

        private void RefreshUIFromDisk()
        {
            DisplayedGroups.Clear();

            var groupsInDb = _allowedStudentsDatabase.Select(s => s.GroupName).Distinct();
            foreach (var gName in groupsInDb)
            {
                string path = Path.Combine(_rootStoragePath, gName);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            }

            var groupDirectories = Directory.GetDirectories(_rootStoragePath);
            foreach (var dir in groupDirectories)
            {
                string groupName = Path.GetFileName(dir);
                var groupUI = new GroupDisplay { GroupName = groupName };

                var files = Directory.GetFiles(dir, "*.txt");
                foreach (var file in files)
                {
                    string studentName = Path.GetFileNameWithoutExtension(file);
                    groupUI.Students.Add(new StudentNode { StudentName = studentName, GroupName = groupName });
                }

                var sorted = groupUI.Students.OrderBy(s => s.StudentName).ToList();
                groupUI.Students.Clear();
                foreach (var node in sorted) groupUI.Students.Add(node);

                DisplayedGroups.Add(groupUI);
            }
        }

        private void OnAddStudent(object obj)
        {
            if (obj is GroupDisplay targetGroup)
            {
                var allowedNames = _allowedStudentsDatabase
                    .Where(s => s.GroupName == targetGroup.GroupName)
                    .Select(s => s.FullName)
                    .ToList();

                if (allowedNames.Count == 0)
                {
                    MessageBox.Show("В актуальной базе данных нет студентов для этой группы.", "Архив", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                string selectedStudent = StudentListDialog.Show(allowedNames, targetGroup.GroupName);

                if (!string.IsNullOrEmpty(selectedStudent))
                {
                    string filePath = Path.Combine(_rootStoragePath, targetGroup.GroupName, $"{selectedStudent}.txt");

                    if (File.Exists(filePath))
                    {
                        MessageBox.Show("Личное дело этого студента уже заведено на диске!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    File.WriteAllText(filePath, $"Личное дело: {selectedStudent}\nГруппа: {targetGroup.GroupName}\nДата создания: {DateTime.Now}");
                    RefreshUIFromDisk();
                }
            }
        }

        private void OnDeleteStudent(object obj)
        {
            if (obj is StudentNode studentToDelete)
            {
                var result = MessageBox.Show($"Вы уверены, что хотите удалить файл личного дела студента {studentToDelete.StudentName}?",
                    "Удаление файла", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    string filePath = Path.Combine(_rootStoragePath, studentToDelete.GroupName, $"{studentToDelete.StudentName}.txt");
                    if (File.Exists(filePath)) File.Delete(filePath);
                    RefreshUIFromDisk();
                }
            }
        }
    }
}