using ModernWinFormsUI.Controls;
using ModernWinFormsUI.Tokens;

namespace ModernWinFormsUI.Demo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadStudentGridDemoData();


            comboPrinter.AddItems(new object[]
            {
                "ZDesigner ZD420-203dpi ZPL",
                "Microsoft Print to PDF",
                "Canon LBP6030"
            });

            comboPrinter.SelectedIndex = 0;
        }

        private void btnPrimaryDemo_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        private void LoadStudentGridDemoData()
        {
            gridStudents.Columns.Clear();
            gridStudents.Rows.Clear();

            gridStudents.Columns.Add("StudentNo", "Student No");
            gridStudents.Columns.Add("FirstName", "First Name");
            gridStudents.Columns.Add("LastName", "Last Name");
            gridStudents.Columns.Add("Faculty", "Faculty");
            gridStudents.Columns.Add("Department", "Department");
            gridStudents.Columns.Add("Year", "Year");
            gridStudents.Columns.Add("Status", "Status");

            gridStudents.Rows.Add("12345678910", "Carl", "Johnson", "Faculty of Engineering", "Computer Engineering", "3", "Active");
            gridStudents.Rows.Add("12345678911", "Lebron", "James", "Faculty of Economics", "Business Administration", "2", "Active");
            gridStudents.Rows.Add("12345678912", "Yoshikage", "Kira", "Faculty of Science", "Mathematics", "1", "Active");
            gridStudents.Rows.Add("12345678913", "Naruto", "Uzumaki", "Faculty of Education", "Guidance and Counseling", "4", "Active");
            gridStudents.Rows.Add("12345678914", "Gon", "Freecss", "Faculty of Engineering", "Electrical Engineering", "2", "Active");

            gridStudents.Columns["StudentNo"].FillWeight = 120;
            gridStudents.Columns["FirstName"].FillWeight = 90;
            gridStudents.Columns["LastName"].FillWeight = 90;
            gridStudents.Columns["Faculty"].FillWeight = 150;
            gridStudents.Columns["Department"].FillWeight = 160;
            gridStudents.Columns["Year"].FillWeight = 60;
            gridStudents.Columns["Status"].FillWeight = 80;

            gridStudents.ClearSelection();
        }

        private void cardStudents_Paint(object sender, PaintEventArgs e)
        {

        }
    }


}

