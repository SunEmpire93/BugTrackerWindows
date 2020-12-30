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
        BugInfoForm bugInfoForm;
        PostUpdateForm postUpdateForm;
        public static int heightOffset;
        public static int widthOffset; // These will be calculated in the resize function, static so that other forms
                                        //can use their value to position forms on the FormContent Panel

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
            //Panel_FormContent.Controls.Clear();
            //Panel_FormContent.Controls.Add(f1);
        }

        public  void DisplayProjectsForm()
        {
            
            currentForm = "DisplayProjectsForm";
            string t = Panel_FormContent.Size.ToString();
            Panel_FormContent.Controls.Clear();
            //Controls.Clear();
            projForm = new ProjectsForm(this)
            {
                TopLevel = false
            };
            Panel_FormContent.Controls.Add(projForm);
            //Controls.Add(projForm);
            //Panel_FormContent.Size = Size;
            //projForm.Size = Panel_FormContent.Size;
            projForm.Show();
        }
        public void DisplayNewProjectForm()
        {
            
            currentForm = "DisplayNewProjectForm";
            //Controls.Clear();
            Panel_FormContent.Controls.Clear();
            newProjForm = new NewProjectForm(this)
            {
                TopLevel = false
            };
            Panel_FormContent.Controls.Add(newProjForm);
            //Controls.Add(newProjForm);
            newProjForm.Show();
        }
        public void DisplayBugsForm(string id)
        {
            currentForm = "DisplayBugsForm";
            Panel_FormContent.Controls.Clear();
            //Controls.Clear();
            bugsForm = new BugsForm(this, id)
            {
                TopLevel = false
            };
            Panel_FormContent.Controls.Add(bugsForm);
            //Controls.Add(bugsForm);
            bugsForm.Show();
        }
        public void DisplayBugReportForm(string id)
        {
            currentForm = "DisplayBugReportForm";
            Panel_FormContent.Controls.Clear();
            //Controls.Clear();
            bugReportForm = new BugReportForm(this, id)
            {
                TopLevel = false
            };
            Panel_FormContent.Controls.Add(bugReportForm);
            //Controls.Add(bugReportForm);
            bugReportForm.Show();
        }
        public void DisplayBugInfoForm(string id)
        {
            currentForm = "DisplayBugInfoForm";
            Panel_FormContent.Controls.Clear();
            //Controls.Clear();
            bugInfoForm = new BugInfoForm(this, id)
            {
                TopLevel = false
            };
            Panel_FormContent.Controls.Add(bugInfoForm);
            //Controls.Add(bugReportForm);
            bugInfoForm.Show();
        }
        public void DisplayPostUpdateForm(string id)
        {
            currentForm = "DisplayPostUpdateForm";
            Panel_FormContent.Controls.Clear();
            //Controls.Clear();
            postUpdateForm = new PostUpdateForm(this, id) //,id
            {
                TopLevel = false
            };
            Panel_FormContent.Controls.Add(postUpdateForm);
            //Controls.Add(bugReportForm);
            postUpdateForm.Show();
        }


        public void Window_Resize(object sender, EventArgs e)
        {
            //Panel_FormContent.Size = Size; panel now outsizes window
             heightOffset = Panel_Management.Height + 40; //I guess 40 and 20 is the size of the windows
             widthOffset = Panel_Navigation.Width + 20;   //toolbar and margins
            Panel_FormContent.Height = Height - heightOffset;
            Panel_FormContent.Width = Width - widthOffset ;
            //Here the form content panel needs to be resized if the window ever changes size. However
            //content draws outside of the panel if panel.size = size is used - because the panel
            //gets resized but doesnt start from 0,0 it starts from where the nav bar and toolbar
            //meet. needs to take nav bar (left hand bar's) width for panel.width = width - navbar.width
            // and toolbar (top bar - orange) for panel.height = height = toolbar.height
            
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

                case "DisplayReportBugForm":

                    break;
                case "DisplayBugsForm":
                    bugsForm.DoResize();
                    //when screen resized, needs to redraw all the panels containing bug info. I dont want it to requery 
                    //the DB as resize should only focus on redrawing  (and it's hard to pass the project id around)
                    //id stored in currentProject variable in bugsForm - no need to pass id
                    break;
            }

        }
    }
}
