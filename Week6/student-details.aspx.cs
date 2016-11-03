using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// reference the model binding library
using System.Web.ModelBinding;

namespace Week6
{
    public partial class student_details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                // Check the URL whether we are Adding or Editing
                if (!String.IsNullOrEmpty(Request.QueryString["StudentID"]))
                {
                    // Get ID from the URL
                    Int32 StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

                    // Connect to db
                    var conn = new sharpshooterEntities();

                    // Selected Department
                    var objDep = (from s in conn.Students
                                  where s.StudentID == StudentID
                                  select s).FirstOrDefault();

                    // The form
                    txtLastName.Text = objDep.LastName;
                    txtFirstMidName.Text = objDep.FirstMidName;
                    txtEnrollmentDate.Text = objDep.EnrollmentDate.ToString();

                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Check the URL whether we are Adding or Editing
            Int32 StudentID = 0;

            if (!String.IsNullOrEmpty(Request.QueryString["StudentID"]))
            {
                StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);
            }

            // Connect to db
            var conn = new sharpshooterEntities();

            // Create a new Student object by Student Class
            Student s = new Student();

            // New student object's properties
            s.LastName = txtLastName.Text;
            s.FirstMidName = txtFirstMidName.Text;
            s.EnrollmentDate  = Convert.ToDateTime(txtEnrollmentDate.Text);

            // Save the new object to the database
            conn.Students.Add(s);
            conn.SaveChanges();

            // Redirect to the students page
            Response.Redirect("students.aspx");
        }
    }
}