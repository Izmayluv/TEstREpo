using System.Windows.Forms;
using WindowsFormsApp1.Entity;
using System.Linq;
using System;
using System.Drawing;
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1
{
    public partial class Catalog : Form
    {
        public Catalog()
        {
            InitializeComponent();

            FillGrid();
            LoadCbDiscount();
            LoadCbPrice();

            PrintCountOfRows();
        }

        private void LoadCbDiscount()
        {
            comboBoxDiscount.Items.Add("Любая скидка");
            comboBoxDiscount.Items.Add("10-15");
            comboBoxDiscount.Items.Add("50-99");
            comboBoxDiscount.Items.Add("15-30");

            comboBoxDiscount.SelectedItem = comboBoxDiscount.Items[0];
        }

        private void LoadCbPrice()
        {
            comboBoxPrice.Items.Add("Без фильтра");
            comboBoxPrice.Items.Add("По возрастанию");
            comboBoxPrice.Items.Add("По убыванию");

            comboBoxPrice.SelectedItem = comboBoxPrice.Items[0];
        }

        private void PrintCountOfRows()
        {
            using (DataModel context = new DataModel())
            {
                label1.Text = $"{dataGridView.RowCount} из {context.Product.Count()}";
            }
        }

        Bitmap bitmap;
        string path = @"C:\Users\pixam\source\repos\WindowsFormsApp1\WindowsFormsApp1\Resources\";
        private void FillGrid()
        {
            dataGridView.Rows.Clear();

            using (DataModel context = new DataModel())
            {
                var products = context.Product.ToList();

                switch (comboBoxPrice.SelectedIndex)
                {
                    default:
                        break;

                    case 1:
                        products = products
                            .OrderBy(x=> x.productCost).ToList();
                        break;

                    case 2:
                        products = products
                            .OrderByDescending(x => x.productCost).ToList();
                        break;
                }

                switch (comboBoxDiscount.SelectedIndex)
                {
                    default:
                        break;

                    case 1:
                        products = products
                            .Where(x=> x.productActiveDiscountAmount >= 10 
                            && x.productActiveDiscountAmount <= 15).ToList();
                        break;

                    case 2:
                        products = products
                            .Where(x => x.productActiveDiscountAmount >= 50
                            && x.productActiveDiscountAmount <= 99).ToList();
                        break;

                    case 3:
                        products = products
                            .Where(x => x.productActiveDiscountAmount >= 15
                            && x.productActiveDiscountAmount <= 30).ToList();
                        break;
                }

                for (int i = 0; i < products.Count(); i++)
                {
                    if (i != products.Count() - 1)
                        dataGridView.Rows.Add();

                    if (products.Select(x => x.productActiveDiscountAmount).ToArray()[i] >= 15)
                    {
                        dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(0x96, 0x81, 0x9B); ;
                    }

                    var picName = products.Select(x => x.productPicture).ToArray()[i];                    

                    if (!String.IsNullOrEmpty(picName))
                    {
                        bitmap = new Bitmap(path + picName + ".png");
                        bitmap = new Bitmap(bitmap, 128, 128);
                    }

                    dataGridView.Rows[i].Cells[0].Value = bitmap;

                    byte? discount = products.Select(x => x.productActiveDiscountAmount).ToArray()[i];

                    if (!discount.Equals(null))
                    {
                        dataGridView.Rows[i].Cells[1].Value =
                            $"Наименование: {products.Select(x => x.productName).ToArray()[i]}\n" +
                            $"Цена: {Math.Round(products.Select(x => x.productCost).ToArray()[i], 0)}\n" +
                            $"Описание: {products.Select(x => x.productDescription).ToArray()[i]}\n" +
                            $"Скидка: {Math.Round((decimal)products.Select(x => x.productActiveDiscountAmount).ToArray()[i], 0)}\n" +
                            $"Цена со скидкой: {Math.Round((decimal)(products.Select(x => x.productCost).ToArray()[i] - products.Select(x => x.productCost).ToArray()[i] * products.Select(x => x.productActiveDiscountAmount).ToArray()[i] / 100), 0)}\n";

                        dataGridView.Rows[i].Cells[2].Style.Font = new Font(dataGridView.Font, FontStyle.Strikeout);
                        dataGridView.Rows[i].Cells[2].Value = $"Цена: {Math.Round(products.Select(x => x.productCost).ToArray()[i], 0)}\n";

                    }
                    else
                    {
                        dataGridView.Rows[i].Cells[1].Value =
                            $"Наименование: {products.Select(x => x.productName).ToArray()[i]}\n" +
                            $"Описание: {products.Select(x => x.productDescription).ToArray()[i]}\n" +
                            $"Цена: {Math.Round(products.Select(x => x.productCost).ToArray()[i], 0)}\n";
                    }

                }
                // Я НАСРАЛ КОДА
                PrintCountOfRows();
            }
        }

        private void comboBoxPrice_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            FillGrid();
        }

        private void comboBoxDiscount_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            FillGrid();
        }
    }
}
