using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StudentManager
{
    public static class StudentListDialog
    {
        public static string Show(List<string> allowedStudents, string groupName)
        {
            Window window = new Window
            {
                Title = $"Актуальный состав: {groupName}",
                Width = 380,
                Height = 360,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.NoResize,
                Background = SystemColors.ControlBrush
            };

            StackPanel sp = new StackPanel { Margin = new Thickness(15) };

            TextBlock lbl = new TextBlock
            {
                Text = "Выберите студента из списка для добавления:",
                Margin = new Thickness(0, 0, 0, 10),
                FontWeight = FontWeights.SemiBold
            };
            sp.Children.Add(lbl);

            ListBox listBox = new ListBox
            {
                Height = 180,
                ItemsSource = allowedStudents,
                Background = Brushes.White
            };
            if (listBox.Items.Count > 0) listBox.SelectedIndex = 0;
            sp.Children.Add(listBox);

            // Закрытие диалога по двойному щелчку мыши на студента
            listBox.MouseDoubleClick += (s, e) => {
                if (listBox.SelectedItem != null) { window.DialogResult = true; window.Close(); }
            };

            Button btnAdd = new Button
            {
                Content = "Добавить в архив",
                Height = 35,
                Margin = new Thickness(0, 15, 0, 0),
                IsDefault = true,
                Background = SystemColors.ActiveCaptionBrush,
                FontWeight = FontWeights.Medium,
                Cursor = System.Windows.Input.Cursors.Hand
            };
            btnAdd.Click += (s, e) => { window.DialogResult = true; window.Close(); };
            sp.Children.Add(btnAdd);

            window.Content = sp;

            if (window.ShowDialog() == true && listBox.SelectedItem != null)
            {
                return listBox.SelectedItem.ToString() ?? string.Empty;
            }

            return string.Empty;
        }
    }
}