using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewsAggregatorApp
{
    public partial class AddNewsForm : Form
    {
        public AddNewsForm()
        {
            InitializeComponent();
        }

        private void LoadAuthorsAndCategories()
        {
            // Загрузка авторов
            string authorsQuery = "SELECT author_id, name FROM Authors";
            DataTable authors = DatabaseHelper.ExecuteQuery(authorsQuery);
            comboBoxAuthor.DataSource = authors;
            comboBoxAuthor.DisplayMember = "name";
            comboBoxAuthor.ValueMember = "author_id";

            // Загрузка категорий
            string categoriesQuery = "SELECT category_id, name FROM Categories";
            DataTable categories = DatabaseHelper.ExecuteQuery(categoriesQuery);
            comboBoxCategory.DataSource = categories;
            comboBoxCategory.DisplayMember = "name";
            comboBoxCategory.ValueMember = "category_id";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Введите заголовок!");
                return;
            }

            string query = @"INSERT INTO News (title, content, author_id, category_id) 
                     VALUES (@title, @content, @author, @category)";
            SqlParameter[] parameters =
            {
        new SqlParameter("@title", txtTitle.Text),
        new SqlParameter("@content", txtContent.Text),
        new SqlParameter("@author", comboBoxAuthor.SelectedValue),
        new SqlParameter("@category", comboBoxCategory.SelectedValue)
    };

            DatabaseHelper.ExecuteNonQuery(query, parameters);
            MessageBox.Show("Новость добавлена!");
            this.Close();
        }
    }
}
