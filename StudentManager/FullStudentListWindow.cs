using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Media;

namespace StudentManager
{
    public static class FullStudentListWindow
    {
        private static Window _currentWindow = null;

        public static void Show(List<Student> studentsDatabase)
        {
            // Если окно уже открыто — просто выводим его на передний план
            if (_currentWindow != null && _currentWindow.IsLoaded)
            {
                _currentWindow.Activate();
                return;
            }

            Window window = new Window
            {
                Title = "Актуальный состав всех групп",
                Width = 450,
                Height = 500,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Background = SystemColors.ControlBrush
            };

            _currentWindow = window;

            Grid grid = new Grid { Margin = new Thickness(15) };
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            TextBlock header = new TextBlock
            {
                Text = $"Всего студентов в базе данных: {studentsDatabase.Count}",
                FontSize = 14,
                FontWeight = FontWeights.SemiBold,
                Margin = new Thickness(0, 0, 0, 10),
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1D1D1F"))
            };
            Grid.SetRow(header, 0);
            grid.Children.Add(header);

            // Создаем ListView для красивого вывода с группировкой
            ListView listView = new ListView
            {
                Background = Brushes.White,
                FontSize = 13,
                DisplayMemberPath = "FullName" // Отображаем только ФИО студента
            };

            // Настраиваем группировку и сортировку данных
            ICollectionView view = CollectionViewSource.GetDefaultView(studentsDatabase);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription("GroupName", ListSortDirection.Ascending)); // Сначала по группам
            view.SortDescriptions.Add(new SortDescription("FullName", ListSortDirection.Ascending));  // Внутри группы — по алфавиту

            view.GroupDescriptions.Clear();
            view.GroupDescriptions.Add(new PropertyGroupDescription("GroupName")); // Группируем по свойству GroupName

            listView.ItemsSource = view;

            // Создаем визуальный шаблон для заголовков групп (Названия папок/групп)
            GroupStyle groupStyle = new GroupStyle();
            DataTemplate headerTemplate = new DataTemplate();
            FrameworkElementFactory headerFactory = new FrameworkElementFactory(typeof(TextBlock));

            headerFactory.SetValue(TextBlock.TextProperty, new Binding("Name") { StringFormat = "📁 Группа {0}" });
            headerFactory.SetValue(TextBlock.FontWeightProperty, FontWeights.Bold);
            headerFactory.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0071E3")));
            headerFactory.SetValue(TextBlock.MarginProperty, new Thickness(5, 8, 0, 3));
            headerFactory.SetValue(TextBlock.FontSizeProperty, 14.0);

            headerTemplate.VisualTree = headerFactory;
            groupStyle.HeaderTemplate = headerTemplate;
            listView.GroupStyle.Add(groupStyle);

            Grid.SetRow(listView, 1);
            grid.Children.Add(listView);

            Button btnClose = new Button
            {
                Content = "Закрыть",
                Height = 30,
                Width = 100,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0, 12, 0, 0),
                IsCancel = true,
                Background = Brushes.LightGray
            };
            btnClose.Click += (s, e) => window.Close();
            Grid.SetRow(btnClose, 2);
            grid.Children.Add(btnClose);

            window.Content = grid;
            window.Show();
        }
    }
}