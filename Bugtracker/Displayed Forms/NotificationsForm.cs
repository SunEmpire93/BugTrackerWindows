﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bugtracker
{
    public partial class NotificationsForm : Form
    {
        Window display;
        public NotificationsForm(Window window)
        {
            InitializeComponent();
            display = window;
            Size = new Size(window.Width, window.Height);
            LoadNotifications(UserObject.loggedUser.iduser);
        }



        private void LoadNotifications(string userid)
        {
            // get all results from notification table where user = userid
            // join notiftype AND notification tables where notification.idnotification = notiftype.toNotifyId
            // order by unread first and timestamp
            // depending on notif type, do something with related id
            // types are: reqUserAccess, newBug, newPost, newFollowProj, newFollowBug, bugStatus, projStatus
            // may need type to advise user of a comment written on their update
            ///// ///// ///// ///// ///// ///// ///// ///// ///// ///// ///// ///// ///// /////
            // example: if notifType == reqUserAccess: panelText = USER foo HAS REQUESTED ACCESS TO PROJECT bar
            // foo = SELECT user.username WHERE iduser = notiftype.relatedId
            // bar = notification.project
            // example: if notifType == newBug: panelText = USER foo HAS POSTED NEW BUG bar IN PROJECT wiz 
            // foo = SELECT user.username FROM user WHERE userid = notiftype.relatedid
            // bar = notification.bug
            // wiz = notification.project
            ///// ///// ///// ///// ///// ///// ///// ///// ///// ///// ///// ///// ///// /////
            // inserting a new bug needs to create a new line for all userids
            // users = select users, isfollow, isallow, idnotification from notification where project == thisProject AND follow == 1
            // for each user in users
            // INSERT INTO notiftype VALUES toNotify, type, relatedId, bug, timestamp, read (default as false)
            // idnotification, newBug, newbug.poster, bug'sID (will have to insert then deref id), datetime.now
            //  ///// ///// ///// ///// ///// ///// ///// ///// ///// ///// ///// ///// /////
            // a new update on a bug will need to notify all users who are following the bug
            // INSERT INTO update (postedBy, comment, bug, status, timestamp)
            // usersToNotify = SELECT * FROM notifications WHERE project == thisProject 
            // for user in usersToNotify
            // INSERT INTO notiftype VALUES toNotify, type, relatedId, bug, timestamp, read
            // user.idnotification, newPost, update's.id, bug, timestamp
            // if it's a new post then user will want to see the bug's updated status - can deref relatedid for status

            DataTable table = Connection.GetDbConn().GetDataTable($"SELECT * FROM tonotify INNER JOIN notification as n " +
                $"on n.idnotification = tonotify.notifid WHERE tonotify.userid = {UserObject.loggedUser.iduser} " +
                $"ORDER BY n.timestamp ASC");
            int panelYpos = 16;
            foreach (DataRow row in table.Rows)
            {
                string labeltext = "";
                string userNotifFrom = row[4].ToString();
                string projId = row[5].ToString();
                string bugId = row[6].ToString();
                string timestamp = row[10].ToString();
                string status = row[9].ToString();
                // display the notification differently depending on type, add on click to show related info
                // on click function will differ for each notification however the .click function
                // has to be defined when the label text is created
                switch (row[8])
                {
                    case "new bug":
                        labeltext = "New bug: " +  bugId + " on project: " + projId + " from user: " + userNotifFrom + " at: " + timestamp + ".";
                        BugObject newbug = GetBug(bugId);
                        Label notifLabel = new Label
                        {
                            Location = new Point(16, panelYpos),
                            Font = new Font("Arial", 8f, FontStyle.Bold),
                            ForeColor = Color.FromArgb(82, 82, 82),
                            //MaximumSize = new Size(Panel_BugPanel.Width - 32, Panel_BugPanel.Height / 4),
                            AutoSize = true,
                            Text = labeltext
                        };
                        notifLabel.Click += new System.EventHandler((sender, e) => BugClicked(sender, e, newbug));
                        Panel_MasterPanel.Controls.Add(notifLabel);
                        break;

                    case "follow":
                        labeltext = "New follower: " + userNotifFrom + " on project: " + projId + " at: " + timestamp;
                        Label followLabel = new Label
                        {
                            Location = new Point(16, panelYpos),
                            Font = new Font("Arial", 8f, FontStyle.Bold),
                            ForeColor = Color.FromArgb(82, 82, 82),
                            //MaximumSize = new Size(Panel_BugPanel.Width - 32, Panel_BugPanel.Height / 4),
                            AutoSize = true,
                            Text = labeltext
                        };
                        followLabel.Click += new System.EventHandler((sender, e) => ProjectClicked(sender, e, projId));
                        Panel_MasterPanel.Controls.Add(followLabel);

                        break;

                    case "request access":

                        BugObject accessbug = GetBug(bugId);
                        labeltext = "User: " + userNotifFrom + " has requested access to private project: " 
                            + projId + " at: " + timestamp;
                        Label requestLabel = new Label
                        {
                            Location = new Point(16, panelYpos),
                            Font = new Font("Arial", 8f, FontStyle.Bold),
                            ForeColor = Color.FromArgb(82, 82, 82),
                            //MaximumSize = new Size(Panel_BugPanel.Width - 32, Panel_BugPanel.Height / 4),
                            AutoSize = true,
                            Text = labeltext
                        };
                        // needs to make 2 buttons, accept/ decline / click for user profile
                        requestLabel.Click += new System.EventHandler((sender, e) => BugClicked(sender, e, accessbug));
                        Panel_MasterPanel.Controls.Add(requestLabel);
                        break;

                    case "bugfollow":
                        labeltext = "User: " + userNotifFrom + " has followed bug: "
                            + bugId + " at: " + timestamp;
                        BugObject followBug = GetBug(bugId);
                        Label bugFollowLabel = new Label
                        {
                            Location = new Point(16, panelYpos),
                            Font = new Font("Arial", 8f, FontStyle.Bold),
                            ForeColor = Color.FromArgb(82, 82, 82),
                            //MaximumSize = new Size(Panel_BugPanel.Width - 32, Panel_BugPanel.Height / 4),
                            AutoSize = true,
                            Text = labeltext
                        };
                        // needs to make 2 buttons, accept/ decline / click for user profile
                        bugFollowLabel.Click += new System.EventHandler((sender, e) => BugClicked(sender, e, followBug));
                        Panel_MasterPanel.Controls.Add(bugFollowLabel);

                        break;

                    case "new update":

                        labeltext = "User: " + userNotifFrom + " has posted an update on bug: "
                            + bugId + " at: " + timestamp + ", its new status is: " + status;
                        BugObject updateBug = GetBug(bugId);
                        Label updateLabel = new Label
                        {
                            Location = new Point(16, panelYpos),
                            Font = new Font("Arial", 8f, FontStyle.Bold),
                            ForeColor = Color.FromArgb(82, 82, 82),
                            //MaximumSize = new Size(Panel_BugPanel.Width - 32, Panel_BugPanel.Height / 4),
                            AutoSize = true,
                            Text = labeltext
                        };
                        // needs to make 2 buttons, accept/ decline / click for user profile
                        updateLabel.Click += new System.EventHandler((sender, e) => BugClicked(sender, e, updateBug));
                        Panel_MasterPanel.Controls.Add(updateLabel);

                        break;


                }

                //add height of panel plus 10 pixel gap
                panelYpos += 56;
 
            }
            
            
        }
        private void BugClicked(object sender, EventArgs e, BugObject bug)
        {
            display.DisplayBugInfoForm(bug);
        }
        private void ProjectClicked(object sender, EventArgs e, string project)
        {
            display.DisplayBugsForm(project);

            
        }
        private BugObject GetBug(string bugId) // this should be a public function in bugobject class
        {
            DataTable followset = Connection.GetDbConn().GetDataTable(SqlBug.GetOneBug(bugId));
            BugObject followbug = new BugObject(followset.Rows[0]["idbug"].ToString(),
        followset.Rows[0]["title"].ToString(), followset.Rows[0]["description"].ToString(), followset.Rows[0]["location"].ToString(),
        followset.Rows[0]["status"].ToString(),
         followset.Rows[0]["poster"].ToString(), followset.Rows[0]["project"].ToString(), followset.Rows[0]["priority"].ToString(),
          followset.Rows[0]["referencedBug"].ToString(), Convert.ToDateTime(followset.Rows[0]["timePosted"]));
            return followbug;
        }
    }
}
