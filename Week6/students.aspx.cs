using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// add reference to access the database
using System.Web.ModelBinding;

namespace Week6
{
    public partial class students : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // Get the students and Display in the gridview
            getStudents();
        }

        protected void getStudents()
        {
            // Connect to db
            var conn = new sharpshooterEntities();

            // Run the query using LINQ
            var Students = from s in conn.Students
                           select s;

            // Display query result in gridview
            grdStudents.DataSource = Students.ToList();
            grdStudents.DataBind();

        }

        protected void grdStudents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Following function will allow us to delete a Student from gridview

            // 1. Checks that which row is clicked by the user
            Int32 gridIndex = e.RowIndex;

            // 2. Find the value of StudentID in the selected row
            Int32 StudentID = Convert.ToInt32(grdStudents.DataKeys[gridIndex].Value);

            // 3. Connect to the db
            var conn = new sharpshooterEntities();

            Student s = new Student();
            s.StudentID = StudentID;
            conn.Students.Attach(s);
            conn.Students.Remove(s);
            conn.SaveChanges();

            // 5. Refresh The Gridview
            getStudents();
        }
    }
}