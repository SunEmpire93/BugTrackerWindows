﻿using System;
using System.Windows.Forms;

namespace Bugtracker
{
    public partial class Window : Form
    {

        Form1 f1;
        ProjectsForm projForm;
        NewProjectForm newProjForm;
        BugsForm bugsForm;
        BugReportForm bugReportForm;

        string currentForm;
        public Window()
        {
            
            InitializeComponent();
            DisplayProjectsForm();
        }
        public void DisplayForm1()
        {
            currentForm = "Form1";
            Controls.Clear();
            f1 = new Form1(this)
            {
                TopLevel = false
            };
            Controls.Add(f1);
            f1.Show();
        }

        public  void DisplayProjectsForm()
        {
            currentForm = "DisplayProjectsForm";
            Controls.Clear();
            projForm = new ProjectsForm(this)
            {
                TopLevel = false
            };
            Controls.Add(projForm);
            projForm.Show();
        }
        public void DisplayNewProjectForm()
        {
            currentForm = "DisplayNewProjectForm";
            Controls.Clear();
            newProjForm = new NewProjectForm(this)
            {
                TopLevel = false
            };
            Controls.Add(newProjForm);
            newProjForm.Show();
        }
        public void DisplayBugsForm(string id)
        {
            currentForm = "DisplayBugsForm";
            Controls.Clear();
            bugsForm = new BugsForm(this, id)
            {
                TopLevel = false
            };
            Controls.Add(bugsForm);
            bugsForm.Show();
        }
        public void DisplayBugReportForm(string id)
        {
            currentForm = "DisplayBugReportForm";
            Controls.Clear();
            bugReportForm = new BugReportForm(this, id)
            {
                TopLevel = false
            };
            Controls.Add(bugReportForm);
            bugReportForm.Show();
        }


        public void Window_Resize(object sender, EventArgs e)
        {
            switch (currentForm)
            {
                case "Form1":
                    
                    DisplayForm1();
                    
                    break;

                case "DisplayProjectsForm":

                    projForm.doResize();

                    break;

                case "DisplayNewProjectForm":
                    DisplayNewProjectForm();
                    break;
            }

        }
    }
}
