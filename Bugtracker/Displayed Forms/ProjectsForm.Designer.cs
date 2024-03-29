﻿namespace Bugtracker
{
    partial class ProjectsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Panel_DisplayProjects = new System.Windows.Forms.Panel();
            this.Button_NewProject = new System.Windows.Forms.Button();
            this.Button_Back = new System.Windows.Forms.Button();
            this.DebugButton_Login = new System.Windows.Forms.Button();
            this.Label_loggedUser = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Panel_DisplayProjects.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_DisplayProjects
            // 
            this.Panel_DisplayProjects.AutoScroll = true;
            this.Panel_DisplayProjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.Panel_DisplayProjects.Controls.Add(this.label2);
            this.Panel_DisplayProjects.Location = new System.Drawing.Point(1, 70);
            this.Panel_DisplayProjects.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_DisplayProjects.Name = "Panel_DisplayProjects";
            this.Panel_DisplayProjects.Size = new System.Drawing.Size(630, 420);
            this.Panel_DisplayProjects.TabIndex = 3;
            // 
            // Button_NewProject
            // 
            this.Button_NewProject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(0)))));
            this.Button_NewProject.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_NewProject.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_NewProject.FlatAppearance.BorderSize = 0;
            this.Button_NewProject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_NewProject.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_NewProject.ForeColor = System.Drawing.Color.White;
            this.Button_NewProject.Location = new System.Drawing.Point(165, 17);
            this.Button_NewProject.Margin = new System.Windows.Forms.Padding(8, 8, 32, 16);
            this.Button_NewProject.Name = "Button_NewProject";
            this.Button_NewProject.Size = new System.Drawing.Size(108, 26);
            this.Button_NewProject.TabIndex = 4;
            this.Button_NewProject.Text = "+ Project";
            this.Button_NewProject.UseVisualStyleBackColor = false;
            this.Button_NewProject.Click += new System.EventHandler(this.Button_NewProject_Click);
            // 
            // Button_Back
            // 
            this.Button_Back.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(0)))));
            this.Button_Back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_Back.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Back.FlatAppearance.BorderSize = 0;
            this.Button_Back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Back.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Back.ForeColor = System.Drawing.Color.White;
            this.Button_Back.Location = new System.Drawing.Point(41, 17);
            this.Button_Back.Margin = new System.Windows.Forms.Padding(32, 8, 8, 16);
            this.Button_Back.Name = "Button_Back";
            this.Button_Back.Size = new System.Drawing.Size(108, 26);
            this.Button_Back.TabIndex = 5;
            this.Button_Back.Text = "Back";
            this.Button_Back.UseVisualStyleBackColor = false;
            // 
            // DebugButton_Login
            // 
            this.DebugButton_Login.Location = new System.Drawing.Point(319, 19);
            this.DebugButton_Login.Name = "DebugButton_Login";
            this.DebugButton_Login.Size = new System.Drawing.Size(114, 23);
            this.DebugButton_Login.TabIndex = 6;
            this.DebugButton_Login.Text = "LogOut";
            this.DebugButton_Login.UseVisualStyleBackColor = true;
            this.DebugButton_Login.Click += new System.EventHandler(this.DebugButton_Login_Click);
            // 
            // Label_loggedUser
            // 
            this.Label_loggedUser.AutoSize = true;
            this.Label_loggedUser.Location = new System.Drawing.Point(461, 28);
            this.Label_loggedUser.Name = "Label_loggedUser";
            this.Label_loggedUser.Size = new System.Drawing.Size(35, 13);
            this.Label_loggedUser.TabIndex = 7;
            this.Label_loggedUser.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "ALL projects";
            // 
            // ProjectsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(631, 493);
            this.Controls.Add(this.Label_loggedUser);
            this.Controls.Add(this.DebugButton_Login);
            this.Controls.Add(this.Button_Back);
            this.Controls.Add(this.Button_NewProject);
            this.Controls.Add(this.Panel_DisplayProjects);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(620, 480);
            this.Name = "ProjectsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BugTracker";
            this.Panel_DisplayProjects.ResumeLayout(false);
            this.Panel_DisplayProjects.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel Panel_DisplayProjects;
        private System.Windows.Forms.Button Button_NewProject;
        private System.Windows.Forms.Button Button_Back;
        private System.Windows.Forms.Button DebugButton_Login;
        private System.Windows.Forms.Label Label_loggedUser;
        private System.Windows.Forms.Label label2;
    }
}