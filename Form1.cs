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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadNews();
        }

        private void LoadNews()
        {
            string query = "SELECT news_id, title, publication_date FROM News";
            dataGridViewNews.DataSource = DatabaseHelper.ExecuteQuery(query);
            dataGridViewNews.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewsForm addForm = new AddNewsForm();
            addForm.ShowDialog();
            LoadNews(); // Обновить список после закрытия формы
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewNews.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите новость для удаления!");
                return;
            }

            int newsId = (int)dataGridViewNews.SelectedRows[0].Cells["news_id"].Value;
            string query = "DELETE FROM News WHERE news_id = @id";
            SqlParameter[] parameters = { new SqlParameter("@id", newsId) };

            DatabaseHelper.ExecuteNonQuery(query, parameters);
            LoadNews(); // Обновить список
        }
        private void LoadCategories()
        {
            string query = "SELECT category_id, name FROM Categories";
            DataTable categories = DatabaseHelper.ExecuteQuery(query);
            comboBoxFilter.DataSource = categories;
            comboBoxFilter.DisplayMember = "name";
            comboBoxFilter.ValueMember = "category_id";
        }
        private void comboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            int categoryId = (int)comboBoxFilter.SelectedValue;
            string query = $"SELECT news_id, title, publication_date FROM News WHERE category_id = {categoryId}";
            dataGridViewNews.DataSource = DatabaseHelper.ExecuteQuery(query);
        }
    }
}
