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
        private void InitializeCollectionTab()
        {
            FillDataGrid();
            FillSuggestionsBoxInsSearch();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tabControlMovieCollection.IsSelected = true;
            FillDataGrid();
            ShowMoviesCount();
            panelSort.Visibility = Visibility.Visible;
            panelGenres.Visibility = Visibility.Collapsed;
            searchMovieButton.Visibility = Visibility.Visible;
            sortMovieButton.Visibility = Visibility.Visible;
            dockPanelSort.Visibility = Visibility.Visible;
        }

        void FillDataGrid()
        {
            //var connectionString = "Data Source=SV-APP-014\\IMOSSQL2016;Initial Catalog=FirstDB;Integrated Security=True;User ID=;Password=";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM [FirstDB].[dbo].[Movies] ORDER BY MovieName ASC", con);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            moviesInTheCollection.ItemsSource = dt.DefaultView;


            cmd.Dispose();
            con.Close();

        }

        private void moviesInTheCollection_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string columnName = e.Column.Header.ToString();
            if (columnName == "MovieCollectionId")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
            if (columnName == "MoviesCollectionID")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
            if (columnName == "DirectorName")
            {
                e.Column.Header = "Director";
                e.Column.DisplayIndex = 3;
                e.Column.Width = 300;
            }
            if (columnName == "MovieName")
            {
                e.Column.Header = "Movie";
                e.Column.Width = 400;
            }
            if (columnName == "ID")
            {
                e.Column.Width = 40;
            }
            if (columnName == "Year")
            {
                e.Column.Width = 50;
            }
            if (columnName == "Genre")
            {
                e.Column.Width = 300;
            }
            if (columnName == "Status")
            {
                e.Column.Width = 150;
            }
        }
        private void sortNameMovieButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM [FirstDB].[dbo].[Movies] ORDER BY MovieName ASC", con);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            moviesInTheCollection.ItemsSource = dt.DefaultView;


            cmd.Dispose();
            con.Close();
        }

        private void sortYearMovieButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM [FirstDB].[dbo].[Movies] ORDER BY Year DESC", con);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            moviesInTheCollection.ItemsSource = dt.DefaultView;


            cmd.Dispose();
            con.Close();
        }

        private void sortDirectorMovieButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM [FirstDB].[dbo].[Movies] ORDER BY DirectorName ASC", con);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            moviesInTheCollection.ItemsSource = dt.DefaultView;


            cmd.Dispose();
            con.Close();
        }

        private void sortStatusMovieButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM [FirstDB].[dbo].[Movies] ORDER BY Status ASC", con);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            moviesInTheCollection.ItemsSource = dt.DefaultView;


            cmd.Dispose();
            con.Close();
        }

        private void actionButton_Click(object sender, RoutedEventArgs e)
        {
            SearchByGenre("Action");
        }

        private void adventureButton_Click(object sender, RoutedEventArgs e)
        {
            SearchByGenre("Adventure");
        }

        private void animationButton_Click(object sender, RoutedEventArgs e)
        {
            SearchByGenre("Animation");
        }
        private void crimeButton_Click(object sender, RoutedEventArgs e)
        {
            SearchByGenre("Crime");
        }

        private void comedyButton_Click(object sender, RoutedEventArgs e)
        {
            SearchByGenre("Comedy");
        }

        private void documentaryButton_Click(object sender, RoutedEventArgs e)
        {
            SearchByGenre("Documentary");
        }

        private void dramaButton_Click(object sender, RoutedEventArgs e)
        {
            SearchByGenre("Drama");
        }

        private void fantasyButton_Click(object sender, RoutedEventArgs e)
        {
            SearchByGenre("Fantasy");
        }

        private void historicalButton_Click(object sender, RoutedEventArgs e)
        {
            SearchByGenre("Historical");
        }

        private void horrorButton_Click(object sender, RoutedEventArgs e)
        {
            SearchByGenre("Horror");
        }

        private void misteryButton_Click(object sender, RoutedEventArgs e)
        {
            SearchByGenre("Mystery");
        }

        private void romanceButton_Click(object sender, RoutedEventArgs e)
        {
            SearchByGenre("Romance");
        }

        private void westernButton_Click(object sender, RoutedEventArgs e)
        {
            SearchByGenre("Western");
        }

        private void scifiButton_Click(object sender, RoutedEventArgs e)
        {
            SearchByGenre("SciFi");
        }

        private void thrillerButton_Click(object sender, RoutedEventArgs e)
        {
            SearchByGenre("Thriller");
        }

        private void FillSuggestionsBoxInsSearch()
        {
            List<Movie> allMovies = unitOfWork.MovieManager.GetAll().ToList();
            List<Director> allDirectors = unitOfWork.DirectorManager.GetAll().ToList();
            List<string> movies = allMovies.Select(x => x.MovieName).ToList();
            List<string> directors = allDirectors.Select(x => x.Name).ToList();
            var suggestions = movies.Concat(directors);
            txtBoxSearch.ItemsSource = suggestions;
        }

        private void txtBoxSearch_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string search = txtBoxSearch.Text;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand($"SELECT * FROM[FirstDB].[dbo].[Movies] Where MovieName Like '%{search}%' OR DirectorName Like '%{search}%'", con);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                moviesInTheCollection.ItemsSource = dt.DefaultView;


                cmd.Dispose();
                con.Close();
            }
        }

        private void ShowMoviesCount()
        {
            int unwatched = unitOfWork.MovieManager.GetAll().Where(x => x.Status == "Unwatched").Count();
            int watched = unitOfWork.MovieManager.GetAll().Where(x => x.Status == "Watched").Count();
            txtBlockMoviesCount.Text = $"Unwatched: {unwatched}  Watched: {watched}";
        }

        private void SearchByGenre(string genre)
        {
            con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand($"SELECT * FROM[FirstDB].[dbo].[Movies] Where Genre Like '%{genre}%'", con);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            moviesInTheCollection.ItemsSource = dt.DefaultView;


            cmd.Dispose();
            con.Close();
        }

        private void moviesInTheCollection_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (moviesInTheCollection.SelectedItem != null)
            {
                DataRowView view = moviesInTheCollection.SelectedItem as DataRowView;
                DataRow row = view.Row;
                int text = (int)row.ItemArray[0];

                tabControlUpdateMovie.IsSelected = true;
                searchMovieButton.Visibility = Visibility.Collapsed;
                sortMovieButton.Visibility = Visibility.Collapsed;
                txtBoxUpdateMovieId.Text = text.ToString();
            }
        }

        //Small buttons width change when mouse is over the button
        private void sortNameMovieButton_MouseEnter(object sender, MouseEventArgs e)
        {
            sortNameMovieButton.Width = 60;
        }

        private void sortNameMovieButton_MouseLeave(object sender, MouseEventArgs e)
        {
            sortNameMovieButton.Width = 50;
        }

        private void sortYearMovieButton_MouseEnter(object sender, MouseEventArgs e)
        {
            sortYearMovieButton.Width = 60;
        }

        private void sortYearMovieButton_MouseLeave(object sender, MouseEventArgs e)
        {
            sortYearMovieButton.Width = 50;
        }

        private void sortDirectorMovieButton_MouseEnter(object sender, MouseEventArgs e)
        {
            sortDirectorMovieButton.Width = 60;
        }

        private void sortDirectorMovieButton_MouseLeave(object sender, MouseEventArgs e)
        {
            sortDirectorMovieButton.Width = 50;
        }

        private void sortStatusMovieButton_MouseEnter(object sender, MouseEventArgs e)
        {
            sortStatusMovieButton.Width = 60;
        }

        private void sortStatusMovieButton_MouseLeave(object sender, MouseEventArgs e)
        {
            sortStatusMovieButton.Width = 50;
        }

        private void actionButton_MouseEnter(object sender, MouseEventArgs e)
        {
            actionButton.Width = 60;
        }

        private void actionButton_MouseLeave(object sender, MouseEventArgs e)
        {
            actionButton.Width = 50;
        }

        private void adventureButton_MouseEnter(object sender, MouseEventArgs e)
        {
            adventureButton.Width = 60;
        }

        private void adventureButton_MouseLeave(object sender, MouseEventArgs e)
        {
            adventureButton.Width = 50;
        }
        private void animationButton_MouseEnter(object sender, MouseEventArgs e)
        {
            animationButton.Width = 60;
        }

        private void animationButton_MouseLeave(object sender, MouseEventArgs e)
        {
            animationButton.Width = 50;
        }

        private void crimeButton_MouseEnter(object sender, MouseEventArgs e)
        {
            crimeButton.Width = 60;
        }

        private void crimeButton_MouseLeave(object sender, MouseEventArgs e)
        {
            crimeButton.Width = 50;
        }

        private void comedyButton_MouseEnter(object sender, MouseEventArgs e)
        {
            comedyButton.Width = 60;
        }

        private void comedyButton_MouseLeave(object sender, MouseEventArgs e)
        {
            comedyButton.Width = 50;
        }

        private void documentaryButton_MouseEnter(object sender, MouseEventArgs e)
        {
            documentaryButton.Width = 60;
        }

        private void documentaryButton_MouseLeave(object sender, MouseEventArgs e)
        {
            documentaryButton.Width = 50;
        }

        private void dramaButton_MouseEnter(object sender, MouseEventArgs e)
        {
            dramaButton.Width = 60;
        }

        private void dramaButton_MouseLeave(object sender, MouseEventArgs e)
        {
            dramaButton.Width = 50;
        }

        private void fantasyButton_MouseEnter(object sender, MouseEventArgs e)
        {
            fantasyButton.Width = 60;
        }

        private void fantasyButton_MouseLeave(object sender, MouseEventArgs e)
        {
            fantasyButton.Width = 50;
        }

        private void historicalButton_MouseEnter(object sender, MouseEventArgs e)
        {
            historicalButton.Width = 60;
        }

        private void historicalButton_MouseLeave(object sender, MouseEventArgs e)
        {
            historicalButton.Width = 50;
        }

        private void horrorButton_MouseEnter(object sender, MouseEventArgs e)
        {
            horrorButton.Width = 60;
        }

        private void horrorButton_MouseLeave(object sender, MouseEventArgs e)
        {
            horrorButton.Width = 50;
        }

        private void misteryButton_MouseEnter(object sender, MouseEventArgs e)
        {
            misteryButton.Width = 60;
        }

        private void misteryButton_MouseLeave(object sender, MouseEventArgs e)
        {
            misteryButton.Width = 50;
        }

        private void romanceButton_MouseEnter(object sender, MouseEventArgs e)
        {
            romanceButton.Width = 60;
        }

        private void romanceButton_MouseLeave(object sender, MouseEventArgs e)
        {
            romanceButton.Width = 50;
        }

        private void scifiButton_MouseEnter(object sender, MouseEventArgs e)
        {
            scifiButton.Width = 60;
        }

        private void scifiButton_MouseLeave(object sender, MouseEventArgs e)
        {
            scifiButton.Width = 50;
        }

        private void thrillerButton_MouseEnter(object sender, MouseEventArgs e)
        {
            thrillerButton.Width = 60;
        }

        private void thrillerButton_MouseLeave(object sender, MouseEventArgs e)
        {
            thrillerButton.Width = 50;
        }

        private void westernButton_MouseEnter(object sender, MouseEventArgs e)
        {
            westernButton.Width = 60;
        }

        private void westernButton_MouseLeave(object sender, MouseEventArgs e)
        {
            westernButton.Width = 50;
        }
    }
}
