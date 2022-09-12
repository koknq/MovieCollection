using Microsoft.EntityFrameworkCore.Internal;
using MovieCollection;
using MovieCollection.Enumerators;
using MovieCollection.Models;
using MovieCollection.UnitOfWork;
using MovieCollectionWPF.Messages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace MovieCollectionWPF
{
    public partial class MainWindow : Fluent.RibbonWindow
    {
        private void InitializeAddMovieTab()
        {
            FillSuggestionBoxForGenres();
            FillSuggestionBoxForStatus();
            FillSuggestionsForDirectorsNameBox();
        }

        private void addMovieButton_Click(object sender, RoutedEventArgs e)
        {
            tabControlAddMovie.IsSelected = true;
            searchMovieButton.Visibility = Visibility.Collapsed;
            sortMovieButton.Visibility = Visibility.Collapsed;
        }

        private void FillSuggestionBoxForGenres()
        {
            List<string> suggestions = Enum.GetNames(typeof(Genres)).ToList();
            txtBoxMovieGenre.ItemsSource = suggestions;
            txtBoxMovieGenre2.ItemsSource = suggestions;
            txtBoxMovieGenre3.ItemsSource = suggestions;
        }

        private void FillSuggestionBoxForStatus()
        {
            List<string> suggestions = Enum.GetNames(typeof(Status)).ToList();
            txtBoxMovieStatus.ItemsSource = suggestions;
        }

        private void FillSuggestionsForDirectorsNameBox()
        {
            List<Director> allDirectors = unitOfWork.DirectorManager.GetAll().ToList();
            List<string> directors = allDirectors.Select(x => x.Name).ToList();
            txtBoxMovieDirector.ItemsSource = directors;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void saveMovieButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFields() == true && CheckGenres() == true)
            {
                completeMessage = new CompleteMessage();
                AddMovie(txtBoxMovieName.Text, int.Parse(txtBoxMovieYear.Text), txtBoxMovieDirector.Text, string.Join(",", genres), txtBoxMovieStatus.Text, collection.ID);
                //MessageBox.Show($"You successfully added '{txtBoxMovieName.Text}' to the collection !");
                completeMessage.Show($"You successfully added '{txtBoxMovieName.Text}' to the collection !");
                txtBoxMovieName.Text = Properties.Settings.Default.MovieName;
                txtBoxMovieYear.Text = Properties.Settings.Default.Year;
                txtBoxMovieGenre.Text = Properties.Settings.Default.Genre;
                txtBoxMovieGenre2.Text = Properties.Settings.Default.Genre2;
                txtBoxMovieGenre3.Text = Properties.Settings.Default.Genre3;
                txtBoxMovieDirector.Text = Properties.Settings.Default.DirectorName;
                txtBoxMovieStatus.Text = Properties.Settings.Default.Status;
                FillDataGrid();
            }
        }

        private void AddMovie(string movieName, int year, string director, string genre, string status, int collectionId)
        {
            movie = new Movie();
            movie.MovieName = movieName;
            movie.Year = year;
            movie.Director = CheckForTheDirector(director);
            movie.Genre = genre;
            movie.Status = status;
            movie.MovieCollectionId = collectionId;
            moviesCount++;
            collection.MoviesCount = moviesCount;
            collection.DirectorsCount = directorsCount;
            unitOfWork.MovieManager.Add(movie);
            unitOfWork.SaveChanges();
        }
        private Director CheckForTheDirector(string name)
        {
            Director getDirector = unitOfWork.DirectorManager.GetAll().Where(x => x.Name == name).FirstOrDefault();
            if (getDirector == null)
            {
                director = new Director(name);
                directorsCount++;
                unitOfWork.DirectorManager.Add(director);
                return director;
            }
            else
            {
                director = getDirector;
                return director;
            }
        }

        private bool CheckGenres()
        {
            genres = new List<string>();
            if (txtBoxMovieGenre.Text != "" || txtBoxMovieGenre2.Text != "" || txtBoxMovieGenre3.Text != "")
            {
                genres.Add(txtBoxMovieGenre.Text);
                genres.Add(txtBoxMovieGenre2.Text);
                genres.Add(txtBoxMovieGenre3.Text);
                for (int i = 0; i < genres.Count; i++)
                {
                    if (genres[i] == "")
                    {
                        genres.Remove(genres[i]);
                    }
                }
                return true;
            }
            else
            {
                //MessageBox.Show("You need to enter at least one genre !");
                errorMessage.Show("You need to enter at least one genre !");
                return false;
            }
        }

        private bool CheckFields()
        {
            errorMessage = new ErrorMessage();
            if (txtBoxMovieName.Text == "")
            {
                //MessageBox.Show("You need to enter a movie name !");
                errorMessage.Show("You need to enter a movie name !");
                return false;
            }
            else if (txtBoxMovieDirector.Text == "")
            {
                //MessageBox.Show("You need to enter a director name !");
                errorMessage.Show("You need to enter a director name !");
                return false;
            }
            else if (txtBoxMovieYear.Text.Length < 4)
            {
                //MessageBox.Show("You need to enter a correct year !");
                errorMessage.Show("You need to enter a correct year !");
                return false;
            }
            else if (txtBoxMovieYear.Text == "")
            {
                //MessageBox.Show("You need to enter a movie year !");
                errorMessage.Show("You need to enter a movie year !");
                return false;
            }
            else if (txtBoxMovieStatus.Text == "")
            {
                //MessageBox.Show("You need to enter a movie status !");
                errorMessage.Show("You need to enter a movie status !");
                return false;
            }
            else
            {
                return true;
            }

        }
        private void discardMovieButton_Click(object sender, RoutedEventArgs e)
        {
            txtBoxMovieName.Text = Properties.Settings.Default.MovieName;
            txtBoxMovieYear.Text = Properties.Settings.Default.Year;
            txtBoxMovieGenre.Text = Properties.Settings.Default.Genre;
            txtBoxMovieGenre2.Text = Properties.Settings.Default.Genre2;
            txtBoxMovieGenre3.Text = Properties.Settings.Default.Genre3;
            txtBoxMovieDirector.Text = Properties.Settings.Default.DirectorName;
            txtBoxMovieStatus.Text = Properties.Settings.Default.Status;
        }

        private void addMovieButton_MouseEnter(object sender, MouseEventArgs e)
        {
            addMovieButton.Width = 120;
        }

        private void addMovieButton_MouseLeave(object sender, MouseEventArgs e)
        {
            addMovieButton.Width = 100;
        }

        private void removeMovieButton_MouseEnter(object sender, MouseEventArgs e)
        {
            removeMovieButton.Width = 120;
        }

        private void removeMovieButton_MouseLeave(object sender, MouseEventArgs e)
        {
            removeMovieButton.Width = 100;
        }

        private void searchMovieButton_Click(object sender, RoutedEventArgs e)
        {
            panelGenres.Visibility = Visibility.Visible;
            panelSort.Visibility = Visibility.Collapsed;
        }

        private void searchMovieButton_MouseEnter(object sender, MouseEventArgs e)
        {
            searchMovieButton.Width = 120;
        }

        private void searchMovieButton_MouseLeave(object sender, MouseEventArgs e)
        {
            searchMovieButton.Width = 100;
        }

        private void sortMovieButton_Click(object sender, RoutedEventArgs e)
        {
            panelGenres.Visibility = Visibility.Collapsed;
            panelSort.Visibility = Visibility.Visible;
        }

        private void sortMovieButton_MouseEnter(object sender, MouseEventArgs e)
        {
            sortMovieButton.Width = 120;
        }

        private void sortMovieButton_MouseLeave(object sender, MouseEventArgs e)
        {
            sortMovieButton.Width = 100;
        }

        private void updateMovieButton_MouseEnter(object sender, MouseEventArgs e)
        {
            updateMovieButton.Width = 120;
        }

        private void updateMovieButton_MouseLeave(object sender, MouseEventArgs e)
        {
            updateMovieButton.Width = 100;
        }

        private void saveMovieButton_MouseEnter(object sender, MouseEventArgs e)
        {
            saveMovieButton.Width = 230;
        }

        private void saveMovieButton_MouseLeave(object sender, MouseEventArgs e)
        {
            saveMovieButton.Width = 200;
        }

        private void discardMovieButton_MouseEnter(object sender, MouseEventArgs e)
        {
            discardMovieButton.Width = 230;
        }

        private void discardMovieButton_MouseLeave(object sender, MouseEventArgs e)
        {
            discardMovieButton.Width = 200;
        }
    }
}
