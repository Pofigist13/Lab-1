﻿using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;

namespace Календарь
{

    public partial class Form1 : Form
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
                    eventsListBox.Items.Add($"{e.Date.ToString("yyyy-MM-dd")} - {e.Description}");
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
                if (DateTime.TryParse(selectedItem.Split(new[] { '-' }, StringSplitOptions.None)[0], out
                date))
                {
                    var eventToRemove = calendarManager.Events.Find(e => e.Date == date);
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
        }
    }
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

