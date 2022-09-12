using MovieCollection.Enumerators;
using MovieCollection.Models;
using MovieCollectionWPF.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace MovieCollectionWPF
{
    public partial class MainWindow : Fluent.RibbonWindow
    {
        private void InitializeUpdateMovieTab()
        {
            FillUpdateIdBox();
            FillUpdateStatusBox();
        }

        private void updateMovieButton_Click(object sender, RoutedEventArgs e)
        {
            tabControlUpdateMovie.IsSelected = true;
            searchMovieButton.Visibility = Visibility.Collapsed;
            sortMovieButton.Visibility = Visibility.Collapsed;
        }

        private void txtBoxUpdateMovieId_TextChanged(object sender, RoutedEventArgs e)
        {
            if (txtBoxUpdateMovieId.Text != "")
            {
                int id = int.Parse(txtBoxUpdateMovieId.Text);
                lblUpdatedMovie.Content = CheckIfMovieExist(id);
            }
            else
            {
                lblUpdatedMovie.Content = "";
            }
        }

        private void FillUpdateIdBox()
        {
            List<Movie> allMovies = unitOfWork.MovieManager.GetAll().ToList();
            List<int> numbers = allMovies.Select(x => x.ID).ToList();
            txtBoxUpdateMovieId.ItemsSource = numbers;
        }

        private void FillUpdateStatusBox()
        {
            List<string> suggestions = Enum.GetNames(typeof(Status)).ToList();
            txtBoxUpdateMovieStatus.ItemsSource = suggestions;
        }

        private void saveMovieUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (txtBoxUpdateMovieId.Text != "")
            {
                if (txtBoxUpdateMovieStatus.Text != "")
                {
                    if (unitOfWork.MovieManager.GetAll().Contains(movie))
                    {
                        completeMessage = new CompleteMessage();
                        UpdateStatus(int.Parse(txtBoxUpdateMovieId.Text), txtBoxUpdateMovieStatus.Text);
                        //MessageBox.Show($"You successfully updated '{movie.MovieName}' status to {txtBoxUpdateMovieStatus.Text} !");
                        completeMessage.Show($"You successfully updated '{movie.MovieName}' status to '{txtBoxUpdateMovieStatus.Text}' !");
                        txtBoxUpdateMovieId.Text = Properties.Settings.Default.MovidIDUpdate;
                        txtBoxUpdateMovieStatus.Text = Properties.Settings.Default.MovieStatusUpdate;
                        FillDataGrid();
                    }
                    else
                    {
                        errorMessage = new ErrorMessage();
                        errorMessage.Show("You need to enter correct ID !");
                    }
                }
                else
                {
                    errorMessage = new ErrorMessage();
                    errorMessage.Show("You need to enter movie status !");
                }
            }
            else
            {
                errorMessage = new ErrorMessage();
                errorMessage.Show("You need to enter ID !");
            }

        }

        private void discardMovieUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            txtBoxUpdateMovieId.Text = Properties.Settings.Default.MovidIDUpdate;
            txtBoxUpdateMovieStatus.Text = Properties.Settings.Default.MovieStatusUpdate;
        }

        void UpdateStatus(int id, string status)
        {
            Movie movie = unitOfWork.MovieManager.Find(id);
            movie.Status = status;

            unitOfWork.MovieManager.Update(movie);
            unitOfWork.SaveChanges();
        }

        private void saveMovieUpdateButton_MouseEnter(object sender, MouseEventArgs e)
        {
            saveMovieUpdateButton.Width = 230;
        }

        private void saveMovieUpdateButton_MouseLeave(object sender, MouseEventArgs e)
        {
            saveMovieUpdateButton.Width = 200;
        }

        private void discardMovieUpdateButton_MouseEnter(object sender, MouseEventArgs e)
        {
            discardMovieUpdateButton.Width = 230;
        }

        private void discardMovieUpdateButton_MouseLeave(object sender, MouseEventArgs e)
        {
            discardMovieUpdateButton.Width = 200;
        }
    }
}
