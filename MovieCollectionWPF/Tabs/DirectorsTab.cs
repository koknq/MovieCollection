using MovieCollection.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MovieCollectionWPF
{
    public partial class MainWindow : Fluent.RibbonWindow
    {
        private void InitializeDirectorsTab()
        {
            FillDataGridDirectors();
            FillSuggestionBoxSearchDirector();
        }

        private void directorsButton_Click(object sender, RoutedEventArgs e)
        {
            tabControlDirectors.IsSelected = true;
            searchMovieButton.Visibility = Visibility.Collapsed;
            sortMovieButton.Visibility = Visibility.Collapsed;
            FillDataGridDirectors();
        }

        private void directorsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            directorsButton.Width = 120;
        }

        private void directorsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            directorsButton.Width = 100;
        }
        private void FillDataGridDirectors()
        {
            SqlConnection con = new SqlConnection(connectionString);

            //All Directors from SQL Table
            //SqlCommand cmd = new SqlCommand("SELECT * FROM [FirstDB].[dbo].[Directors] ORDER BY Name ASC", con);

            //Directors that exist in the SQL Table "Movies"
            SqlCommand cmd = new SqlCommand($"SELECT Name FROM[FirstDB].[dbo].[Directors] Where Name In(Select DirectorName FROM[FirstDB].[dbo].[Movies])", con);

            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            directorsInTheCollection.ItemsSource = dt.DefaultView;


            cmd.Dispose();
            con.Close();
        }

        private void directorsInTheCollection_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string columnName = e.Column.Header.ToString();
            if (columnName == "Name")
            {
                e.Column.Width = 468;
                
            }
        }

        private void txtBoxSearchDirector_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string search = txtBoxSearchDirector.Text;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand($"SELECT * FROM[FirstDB].[dbo].[Directors] Where Name Like '%{search}%'", con);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                directorsInTheCollection.ItemsSource = dt.DefaultView;


                cmd.Dispose();
                con.Close();
            }
        }

        private void FillSuggestionBoxSearchDirector()
        {
            List<Director> allDirectors = unitOfWork.DirectorManager.GetAll().ToList();
            List<string> suggestions = allDirectors.Select(x => x.Name).ToList();
            txtBoxSearchDirector.ItemsSource = suggestions;
        }

        private void directorsInTheCollection_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (directorsInTheCollection.SelectedItem != null)
            {
                DataRowView view = directorsInTheCollection.SelectedItem as DataRowView;
                DataRow row = view.Row;
                string text = Convert.ToString(row.ItemArray[0]);

                tabControlMovieCollection.IsSelected = true;
                panelGenres.Visibility = Visibility.Collapsed;
                panelSort.Visibility = Visibility.Visible;
                dockPanelSort.Visibility = Visibility.Collapsed;
                txtBlockMoviesCount.Visibility = Visibility.Collapsed;

                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand($"SELECT * FROM [FirstDB].[dbo].[Movies] Where DirectorName = '{text}' Order By DirectorName ASC", con);
                con.Open();             
                
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                moviesInTheCollection.ItemsSource = dt.DefaultView;

                cmd.Dispose();
                con.Close();
            }
        }
    }
}
