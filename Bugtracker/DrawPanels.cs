﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Drawing;

namespace Bugtracker
{
    class DrawPanels
    { // https://stackoverflow.com/questions/8763716/slowness-in-c-net-windows-form-resize-when-form-has-many-dropdownlist-controls
        Window display;
        public static List<Panel> masterPanelList = new List<Panel>();
        public static List<Panel> projectPanelList = new List<Panel>();
        /// <summary>
        /// this generates the large panels that the projects are attached to
        /// maybe there can be a check to see how many of these panels are getting drawn on a row
        /// if only 1 is being drawn, change location to somewhere more central
        /// in case usr is somewhere between normal size window and fullscreen
        /// </summary>
        /// <param name="toDrawTo"> the panel drawn on the form, master panel </param>
        /// <param name="sections"> a list of ProjectObject lists (see inside projectObject class) </param>
        /// <param name="window"> the display window </param>
        public void BasePanels (Panel masterPanel, List<List<ProjectObject>> sections, Window window)
        {// list of lists is used to allow us to easily make container panels by telling it how many lists
            //we need a panel for
            masterPanelList.Clear();
            int howManyPanels = sections.Count();
            display = window;
            masterPanel.Controls.Clear();
            // for each  project list, make a conatiner panel, draw that panel to the master panel on the form, 
            // then add the contents of that list to this panel. 
            // 
            foreach (List<ProjectObject> list in sections)
            {
                Panel Panel_Container = new Panel
                {
                    Location = new Point(16, 16), // this is updated in GridDraw, so redunadant here
                    BackColor = Properties.Settings.Default.containerPanel_Color,
                    //MaximumSize = new Size(((display.Width / 2) - 200), toDrawTo.Height),
                    //Width = 530, // i want this size to fill the master panel on projectsForm when window at min size
                    // 2 project panels take up 536 px with their separators (3 * 32px) + (2*220px))
                    Width = (masterPanel.Width / howManyPanels),
                    Height = masterPanel.Height - 20, //max height of panel to draw to
                    MaximumSize = new Size (536, masterPanel.Height),
                    MinimumSize = new Size (536, masterPanel.Height),
                    AutoScroll = true
                };

                masterPanel.Controls.Add(Panel_Container);
                //to use drawGrid, have to add container panel to the list then clear it after
                //panelList.Add(Panel_Container);
                // draw container panel to master
                //GridDraw(masterPanel, panelList); // to draw to is the master panel
               //panelList.Clear();
                // create a panel for each project in the list and add them to the panel list. GridDraw is called 
                // at the end of this function too, to draw those panels into Panel_Container
                ProjectPanels(Panel_Container, list, window);
                masterPanelList.Add(Panel_Container);
                //needs to add to panel list after creation but panelList needs to be used by the project panels
                //could make another panel list
                /*foreach (List<ProjectObject> dataset in sections )
                {
                    ProjectPanels(Panel_Container, dataset, window);
                    
                } */
            }
            GridDraw(masterPanel, masterPanelList); // to draw to is the master panel
            masterPanelList.Clear();
        }

        /// <summary>
        /// this logic generates the clickable panels that display projects
        /// </summary>
        /// <param name="toDrawTo"></param>
        /// <param name="dataset"></param>
        /// <param name="window"></param>
        public void ProjectPanels (Panel containerPanel, List<ProjectObject> dataset, Window window)
        {
            projectPanelList.Clear(); // clear panel list 
            display = window;
            containerPanel.Controls.Clear();
            foreach (ProjectObject project in dataset)
            {
                Panel Panel_ProjectPanel = CreatePanel(Color.White, 220, 220);
                Label Label_ProjectName = CreateNameLabel(project, Panel_ProjectPanel);
                if (project.isPrivate == 1 && (project.user != UserObject.loggedUser.iduser)) // private projects
                {
                    Button Button_RequestAccess = new Button()
                    {
                        Location = new Point(100, 180),
                        Text = "Request Access",
                        Font = new Font("Arial", 8f, FontStyle.Bold),
                        AutoSize = true,
                        
                    };
                    Button_RequestAccess.Click += new EventHandler((sender, e) => RequestAccess(sender, e, project));
                    Panel_ProjectPanel.Controls.Add(Button_RequestAccess);
                    containerPanel.Controls.Add(Panel_ProjectPanel);
                    
                }

                else //if (project.isPrivate == 0)
                {
                    Label Label_ProjectDescription = CreateDescriptionLabel(project, Panel_ProjectPanel);

                    Panel_ProjectPanel.Click += new EventHandler((sender, e) => ProjectClicked(sender, e, project));
                    Label_ProjectName.Click += new EventHandler((sender, e) => ProjectClicked(sender, e, project));
                    Label_ProjectDescription.Click += new EventHandler((sender, e) => ProjectClicked(sender, e, project));

                    //Controls.Add(Panel_DisplayProjects); isn't the base panel added as a control from the design form?
                    containerPanel.Controls.Add(Panel_ProjectPanel);
                    Panel_ProjectPanel.Controls.Add(Label_ProjectName);
                    Panel_ProjectPanel.Controls.Add(Label_ProjectDescription);
                }
                //For each project object in the database, make a panel, display elements, give it an on click method
                





                //on click method is applied on each panel generated as dataset list is iterated over
                //we can move logic into drawpanels class

                

                //problem is here, how would you take this outside of the loop?

                //GridDraw(toDrawTo, Panel_ProjectPanel, i);

                //could use .Count of the dataset (to get how many projects need to be made)
                //list of panels?
                projectPanelList.Add(Panel_ProjectPanel);
            }
            GridDraw(containerPanel, projectPanelList);
            projectPanelList.Clear(); //needs to clear the panel list once projects are drawn to their panels so master panels can be 
            //located afterwards
        }
        private void ProjectClicked(object sender, EventArgs e, ProjectObject project)
        {
            // check if project is private, if it is then
           // if(project.isPrivate == 1)
            
               /* if ((project.user != UserObject.loggedUser.iduser) && project.isPrivate == 1) // if the user isn't the project owner
                {
                    // go to db, try to return a row from allowedusers which has this project id and this userid
                    // formname. showdialouge 

                    DataTable isUserAllowed = Connection.GetDbConn().GetDataTable(SqlProject.CheckUserAccess(UserObject.loggedUser.iduser, project.idproject)); 

                   if (isUserAllowed.Rows.Count == 0) // if no result
                   {
                        
                   }
                
                } */
            // checked if user is in this project's 'allowUsers' table
            // if so, open displaybugsform, if not then show message box advising of no access
            // post project - poster is added to allowedaccess table + project is created
            ProjectObject.Projects.Clear(); //clear the list, we will need to clear all lists when more are added
            // i dont want to have to pass the display instance all the way to here
            ProjectObject.UserProjects.Clear();
            display.DisplayBugsForm(project.idproject.ToString());
        }

        /// <summary>
        /// current logic used to determine location of passed in newPanel is below but this means
        /// resetting required vars such as rowWidth (needed to decide when to draw to a new row)
        /// firstColumnX and Y (needed to determine the location of the first columns and where to draw new panel from it)
        /// unless we make all the panels first THEN pass all of them into here 
        /// </summary>
        /// <param name="toDrawTo"></param>
        /// <param name="newPanel"></param>
        /// <param name="projectPosition"></param>
        public static void GridDraw(Panel toDrawTo, List<Panel> newPanels) //needs to know if its drawing 1st panel or no
        {

            int separatorDistance = 32,
              rowWidth = separatorDistance, //distance between rows
              totalRows = 0, //cant set to 0 here as this happens for each project (may explain why all drawing to 0th line)
              rowNumber = totalRows,
              projectPosition = 0,
              firstColumnX = separatorDistance, 
              firstColumnY = separatorDistance / 2, 
              lastColumnY, 
              lastX = firstColumnX,
              lastY = firstColumnY,
              newX,
              newY;
           
            
            foreach (Panel panel in newPanels)
            { 
                rowWidth += panel.Width + separatorDistance; //incrememnts by 1 panel and separator
                if (projectPosition == 0 && totalRows == 0)//first panel
                {
                    newX = firstColumnX;
                    newY = firstColumnY;
                    panel.Location = new Point(newX, newY);
                    lastX = newX;
                    lastY = newY;

                    rowNumber++;
                    totalRows++;
                }
                // First Column on Next Row
                else if (rowWidth > toDrawTo.Width) //if width would be wider than the panel, make a new row
                {
                    lastColumnY = ((firstColumnY + panel.Height) * totalRows) + separatorDistance;

                    newX = firstColumnX;
                    newY = lastColumnY;
                    panel.Location = new Point(newX, newY);
                    lastX = newX;
                    lastY = newY;

                    rowWidth = separatorDistance + panel.Width + separatorDistance;
                    rowNumber++;
                    totalRows++;

                }
                // Next Column on Current Row
                else if (rowWidth <= toDrawTo.Width) //if space, draw panel on same row 
                {
                    newX = lastX + panel.Width + separatorDistance;
                    newY = lastY;
                    panel.Location = new Point(newX, newY);
                    lastX = newX;
                    lastY = newY;
                }
            }

            
        }

        private Panel CreatePanel( Color backColor, int height, int width)
        {
            Panel Panel_ProjectPanel = new Panel
            {
                
                BackColor = backColor,
                Width = width,
                Height = height,
            };

            return Panel_ProjectPanel;
        }

           
        
        private Label CreateNameLabel  (ProjectObject project, Panel toAddTo)
        {
            Label Label_ProjectName = new Label
            {
                
                Location = new Point(16, 16),
                Font = new Font("Arial", 14f, FontStyle.Bold),
                ForeColor = Color.FromArgb(82, 82, 82),
                MaximumSize = new Size(toAddTo.Width - 32, toAddTo.Height / 4),
                AutoSize = true,
                Text = project.projName
            };
            return Label_ProjectName;

        }
        private Label CreateDescriptionLabel (ProjectObject project, Panel toAddTo)
        {
            Label Label_ProjectDescription = new Label
            {
                
                Location = new Point(16, (toAddTo.Height / 4) + 16),
                Font = new Font("Arial", 8f, FontStyle.Bold),
                ForeColor = Color.FromArgb(82, 82, 82),
                MaximumSize = new Size(toAddTo.Width - 32, toAddTo.Height / 2),
                AutoSize = true,
                Text = project.description
            };
            return Label_ProjectDescription;
        }

        private void RequestAccess (object sender, EventArgs e, ProjectObject project) 
        {
            // create a follow project object which will notify the project poster that a user wants access
            try
            { // creates follow table row, as this is a private project (public projects are followed on bugsform)
                // sends a notification to the poster of the project to advise the user wants access.
                DateTime now = DateTime.Now;
                string timestamp = now.ToString("yyyy-MM-dd HH:mm:ss"); 
                string timestampTo = now.AddSeconds(5).ToString("yyyy-MM-dd HH:mm:ss");
                SqlProject sq = new SqlProject();
                SqlNotifications notif = new SqlNotifications();

                Connection.GetDbConn().CreateCommand(SqlFollow.FollowProject(UserObject.loggedUser.iduser, project.idproject));
               
                notif.InsertNotification(UserObject.loggedUser.iduser, project.idproject, "", "", "request access", "", now);

                DataSet getNotifId = Connection.GetDbConn().GetDataSet($"SELECT idnotification FROM notification" +
                $" WHERE usernotif = {UserObject.loggedUser.iduser} AND project = {project.idproject} AND timestamp BETWEEN '{timestamp}' AND '{timestampTo}'");
                string notifId = getNotifId.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                DataSet getProjectOwner = Connection.GetDbConn().GetDataSet($"SELECT user FROM project WHERE idproject = { project.idproject}");
                string projOwner = getProjectOwner.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                notif.InsertToNotify(notifId, projOwner, "0");
                MessageBox.Show("Request sent to project creator");
            }
            catch (Exception)
            {
                MessageBox.Show("Request already sent");
            }
            
        }
    }
    
}
