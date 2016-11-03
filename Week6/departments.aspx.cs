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
    public partial class departments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the departments and Display in the gridview
            getDepartments();
        }

        protected void getDepartments()
        {
            // Connect to db
            var conn = new sharpshooterEntities();

            // Run the query using LINQ
            var Departments = from d in conn.Departments
                              select d;

            // Display query result in gridview
            grdDepartments.DataSource = Departments.ToList();
            grdDepartments.DataBind();

        }

        protected void grdDepartments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Following function will allow us to delete a Department from gridview

            // 1. Checks that which row is clicked by the user
            Int32 gridIndex = e.RowIndex;

            // 2. Find the value of DepartmentID in the selected row
            Int32 DepartmentID = Convert.ToInt32(grdDepartments.DataKeys[gridIndex].Value);

            // 3. Connect to the db
            var conn = new sharpshooterEntities();

            Department d = new Department();
            d.DepartmentID = DepartmentID;
            conn.Departments.Attach(d);
            conn.Departments.Remove(d);
            conn.SaveChanges();

            // 5. Refresh The Gridview
            getDepartments();
        }
    }
}