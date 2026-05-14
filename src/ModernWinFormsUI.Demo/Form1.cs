using ModernWinFormsUI.Controls;
using ModernWinFormsUI.Tokens;

namespace ModernWinFormsUI.Demo
{
    public partial class MainForm : Form
    {
        private SearchMode _currentSearchMode = SearchMode.Number;
        public MainForm()
        {
            InitializeComponent();
            SetSearchMode(SearchMode.Number);
        }

        private void btnPrimaryDemo_Click(object sender, EventArgs e)
        {

        }

        private void segmentedByNumber_Click(object sender, EventArgs e)
        {
            SetSearchMode(SearchMode.Number);
        }

        private void segmentedByName_Click(object sender, EventArgs e)
        {
            SetSearchMode(SearchMode.Name);
        }

        private void SetSearchMode(SearchMode mode)
        {
            _currentSearchMode = mode;  
            bool isNumberMode = mode == SearchMode.Number;

            segmentedByNumber.Selected = isNumberMode;
            segmentedByName.Selected = !isNumberMode;

            segmentedByNumber.IconText = isNumberMode ? "●" : "○";
            segmentedByName.IconText = isNumberMode ? "○" : "●";

            inputSearch.InputText = string.Empty;
            inputSearch.ErrorText = string.Empty;

            if (isNumberMode)
            {
                inputSearch.LabelText = "TC / Öğrenci No";
                inputSearch.PlaceholderText = "TC kimlik no veya öğrenci no giriniz";
            }
            else
            {
                inputSearch.LabelText = "Ad / Soyad";
                inputSearch.PlaceholderText = "Ad veya soyad giriniz";
            }
        }

        private void btnFindStudent_Click(object sender, EventArgs e)
        {
            string value = inputSearch.InputText.Trim();

            if (string.IsNullOrWhiteSpace(value))
            {
                inputSearch.ErrorText = _currentSearchMode == SearchMode.Number
                    ? "TC kimlik no veya öğrenci no giriniz."
                    : "Ad veya soyad giriniz.";

                return;
            }

            inputSearch.ErrorText = string.Empty;

            MessageBox.Show(
                $"Arama yapılıyor: {value}",
                "Öğrenci Arama",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        public enum SearchMode
        {
            Number,
            Name
        }


    }


}

