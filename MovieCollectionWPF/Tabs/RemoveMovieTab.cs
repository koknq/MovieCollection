using MovieCollection.Models;
using MovieCollectionWPF.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace MovieCollectionWPF
{
    public partial class MainWindow : Fluent.RibbonWindow
    {
        private void InitializeRemoveMovieTab()
        {
            FillIdBox();
            FillNameBox();
        }

        private void FillNameBox()
        {
            List<Movie> allMovies = unitOfWork.MovieManager.GetAll().ToList();
            List<string> movies = allMovies.Select(x => x.MovieName).ToList();
            txtBoxRemoveMovieName.ItemsSource = movies;
        }

        private void FillIdBox()
        {
            List<Movie> allMovies = unitOfWork.MovieManager.GetAll().ToList();
            List<int> numbers = allMovies.Select(x => x.ID).ToList();
            txtBoxMovieId.ItemsSource = numbers;
        }
        private void removeMovieButton_Click(object sender, RoutedEventArgs e)
        {
            tabControlRemoveMovie.IsSelected = true;
            searchMovieButton.Visibility = Visibility.Collapsed;
            sortMovieButton.Visibility = Visibility.Collapsed;
        }

        private string CheckIfMovieExist(int id)
        {
            movie = unitOfWork.MovieManager.Find(id);
            if (movie != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"'{movie.MovieName}' ({movie.Year})");
                sb.AppendLine();
                sb.AppendLine($"Director:  {movie.Director.Name}");
                sb.AppendLine();
                sb.AppendLine($"Genre:  {movie.Genre}");
                sb.AppendLine();
                sb.AppendLine($"Status:  {movie.Status}");
                return sb.ToString();
            }
            else
            {
                return $"Movie with this ID:{id} does not exist.";
            }
        }

        private void txtBoxMovieId_TextChanged(object sender, RoutedEventArgs e)
        {
            if (txtBoxMovieId.Text != "")
            {
                int id = int.Parse(txtBoxMovieId.Text);
                lblRemovedMovie.Content = CheckIfMovieExist(id);
            }
            else
            {
                lblRemovedMovie.Content = "";
            }
        }
        private void txtBoxRemoveMovieName_TextChanged(object sender, RoutedEventArgs e)
        {
            movie = unitOfWork.MovieManager.GetAll().Where(x => x.MovieName == txtBoxRemoveMovieName.Text).FirstOrDefault();

            if (unitOfWork.MovieManager.GetAll().Contains(movie))
            {
                txtBoxMovieId.Text = movie.ID.ToString();
            }
        }

        private void saveMovieRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (txtBoxMovieId.Text != "")
            {
                if (unitOfWork.MovieManager.GetAll().Contains(movie))
                {
                    completeMessage = new CompleteMessage();
                    RemoveMovie(int.Parse(txtBoxMovieId.Text));
                    //MessageBox.Show($"You successfully removed '{movie.MovieName}' from the collection !");
                    completeMessage.Show($"You successfully removed '{movie.MovieName}' from the collection !");
                    txtBoxMovieId.Text = Properties.Settings.Default.MovieID;
                    txtBoxRemoveMovieName.Text = Properties.Settings.Default.MovieNameRemove;
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
                errorMessage.Show("You need to enter ID !");
            }
        }

        private void discardMovieRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            txtBoxMovieId.Text = Properties.Settings.Default.MovieID;
            txtBoxRemoveMovieName.Text = Properties.Settings.Default.MovieNameRemove;
        }

        void RemoveMovie(int id)
        {
            movie = unitOfWork.MovieManager.Find(id);
            if (movie != null)
            {
                moviesCount--;
                collection.MoviesCount = moviesCount;
                unitOfWork.MovieManager.Remove(id);
                unitOfWork.SaveChanges();
            }


        }

        private void saveMovieRemoveButton_MouseEnter(object sender, MouseEventArgs e)
        {
            saveMovieRemoveButton.Width = 230;
        }

        private void saveMovieRemoveButton_MouseLeave(object sender, MouseEventArgs e)
        {
            saveMovieRemoveButton.Width = 200;
        }

        private void discardMovieRemoveButton_MouseEnter(object sender, MouseEventArgs e)
        {
            discardMovieRemoveButton.Width = 230;
        }

        private void discardMovieRemoveButton_MouseLeave(object sender, MouseEventArgs e)
        {
            discardMovieRemoveButton.Width = 200;
        }
    }

}

