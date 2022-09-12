using MovieCollection;
using MovieCollection.Models;
using MovieCollection.UnitOfWork;
using MovieCollectionWPF.Messages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace MovieCollectionWPF
{
    public partial class MainWindow : Fluent.RibbonWindow
    {
        private DbContextFactory dbContextFactory;
        private readonly IUnitOfWork unitOfWork;
        private SqlConnection con;
        private Movie movie;
        private Director director;
        private MoviesCollection collection;
        private int moviesCount;
        private int directorsCount;
        private List<string> genres;
        private string connectionString;
        private CompleteMessage completeMessage;
        private ErrorMessage errorMessage;
        public MainWindow()
        {
            dbContextFactory = new DbContextFactory();
            unitOfWork = new UnitOfWork(dbContextFactory.CreateDbContext(null));
            moviesCount = unitOfWork.MovieManager.Count();
            directorsCount = unitOfWork.DirectorManager.Count();
            connectionString = "Data Source=SV-APP-014\\IMOSSQL2016;Initial Catalog=FirstDB;Integrated Security=True;User ID=;Password=";

            MoviesCollection getCollection = unitOfWork.MovieCollectionManager.GetAll().FirstOrDefault();
            if (getCollection == null)
            {
                collection = new MoviesCollection();
                unitOfWork.MovieCollectionManager.Add(collection);
            }
            else
            {
                collection = getCollection;
            }
            unitOfWork.SaveChanges();

            InitializeComponent();
            InitializeAddMovieTab();
            InitializeUpdateMovieTab();
            InitializeRemoveMovieTab();
            InitializeCollectionTab();
            InitializeDirectorsTab();
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Image image = sender as Image;
            image.Margin = new Thickness(0, 10, 0, 10);
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            Image image = sender as Image;
            image.Margin = new Thickness(0, 20, 0, 20);
        }

    }
}