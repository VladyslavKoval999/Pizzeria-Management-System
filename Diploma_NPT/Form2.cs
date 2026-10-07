using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diploma_NPT
{
    public partial class formAuthorization : Form
    {
        public formAuthorization()
        {
            InitializeComponent();
            tbPassword.PasswordChar = '•';

            LoadEyeImages();
        }

        ClassDataBase db = new ClassDataBase();
        string file_db = "Diploma_NPT";
        public List<User> users = new List<User>();

        private bool isPasswordVisible = false;
        private Image eyeOpenImage;
        private Image eyeClosedImage;

        private void LoadEyeImages()
        {
            string projectPath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\.."));
            string currentPath = Path.Combine(projectPath, "Photo_cursach", "Authorization");

            if (!Directory.Exists(currentPath))
            {
                MessageBox.Show($"Директорія не знайдена за шляхом:\n{currentPath}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string eyeOpenPath = Path.Combine(currentPath, "eye_open.png");
                string eyeClosedPath = Path.Combine(currentPath, "eye_closed.png");

                if (!File.Exists(eyeOpenPath))
                {
                    MessageBox.Show($"Зображення 'eye_open.png' не знайдено: {eyeOpenPath}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!File.Exists(eyeClosedPath))
                {
                    MessageBox.Show($"Зображення 'eye_closed.png' не знайдено: {eyeClosedPath}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                eyeOpenImage = Image.FromFile(eyeOpenPath);
                eyeClosedImage = Image.FromFile(eyeClosedPath);
                pbEye.Image = eyeClosedImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при завантаженні зображень: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void formAuthorization_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!IsInputValid()) return;

            if (AuthorizeUser(tbLogin.Text.Trim(), tbPassword.Text.Trim()))
            {
                MessageBox.Show("Авторизація успішна!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();

                UserSession.CurrentUser = users[0];
                formHomePage form1 = new formHomePage();
                form1.Show();
            }

            else MessageBox.Show("Невірний логін або пароль.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool IsInputValid()
        {
            if (string.IsNullOrWhiteSpace(tbLogin.Text))
            {
                MessageBox.Show("Будь ласка, введіть логін.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                MessageBox.Show("Будь ласка, введіть пароль.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private bool AuthorizeUser(string login, string password)
        {
            string query = "select id_staff, login, password from personnel_authorization where login = '" + login + "' and password = '" + password + "'";

            try
            {
                db.Execute<User>(file_db, query, ref users);

                if (users.Count > 0) return true;
                else return false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void pbEye_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;

            if (isPasswordVisible)
            {
                tbPassword.PasswordChar = '\0';
                pbEye.Image = eyeOpenImage;
            }
            else
            {
                tbPassword.PasswordChar = '•';
                pbEye.Image = eyeClosedImage;
            }
        }
    }
}