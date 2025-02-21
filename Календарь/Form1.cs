﻿using System;
using System.Windows.Forms;
using КалендарьКласс;

namespace Календарь
{
    public class CalendarForm : Form
    {
        private CalendarManager calendarManager;
        private DateTimePicker datePicker;
        private TextBox descriptionTextBox;
        private Button addEventButton;
        private ListBox eventsListBox;
        private Button removeEventButton;
        public CalendarForm()
        {
            this.Text = "Календарь событий";
            this.Width = 400;
            this.Height = 400;
            datePicker = new DateTimePicker
            {
                Location = new System.Drawing.Point(10, 10)
            };
            descriptionTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 40),
                Width = 200
            };
            addEventButton = new Button
            {
                Location = new System.Drawing.Point(10, 70),
                Text = "Добавить",
                Width = 100
            };
            addEventButton.Click += AddEventButton_Click;
            eventsListBox = new ListBox
            {
                Location = new System.Drawing.Point(10, 100),
                Width = 200,
                Height = 200
            };
            removeEventButton = new Button
            {
                Location = new System.Drawing.Point(220, 100),
                Text = "Удалить",
                Width = 80,
                Height = 20
            };
            removeEventButton.Click += RemoveEventButton_Click;
            this.Controls.Add(datePicker);
            this.Controls.Add(descriptionTextBox);
            this.Controls.Add(addEventButton);
            this.Controls.Add(eventsListBox);
            this.Controls.Add(removeEventButton);
            calendarManager = new CalendarManager();
            UpdateEventsList();
        }
        private void UpdateEventsList()
        {
            eventsListBox.Items.Clear();
            foreach (var e in calendarManager.Events)
            {
                eventsListBox.Items.Add($"{e.Date.ToString("yyyy/MM/dd")} - {e.Description}");
            }
        }
        private void AddEventButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(descriptionTextBox.Text))
            {
                MessageBox.Show("Введите описание события!");
                return;
            }
            Event newEvent = new Event(datePicker.Value, descriptionTextBox.Text);
            try
            {
                calendarManager.AddEvent(newEvent);
                descriptionTextBox.Clear();
                UpdateEventsList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void RemoveEventButton_Click(object sender, EventArgs e)
        {
            if (eventsListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите событие для удаления!");
                return;
            }
            string selectedItem = eventsListBox.SelectedItem.ToString();
            DateTime date;
            if (DateTime.TryParse(selectedItem.Split(new char[]{ '-' }, StringSplitOptions.None)[0], out date))
            {
                var eventToRemove = calendarManager.Events.Find(x => x.Date == date);
                if (eventToRemove != null)
                {
                    try
                    {
                        calendarManager.RemoveEvent(eventToRemove);
                        UpdateEventsList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CalendarForm
            // 
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "CalendarForm";
            this.ResumeLayout(false);

        }
    }
}
