using MovieCollection.Enumerators;
using MovieCollection.Models;
using MovieCollection.UnitOfWork;
using System.Data;
using System.Linq;

namespace MovieCollection
{
    public class StartUp
    {
        static MoviesCollection collection;
        static Director director;
        static Movie movie;
        static void Main(string[] args)
        {
            DbContextFactory dbContextFactory = new DbContextFactory();
            IUnitOfWork unitOfWork = new UnitOfWork.UnitOfWork(dbContextFactory.CreateDbContext(null));
            int moviesCount = unitOfWork.MovieManager.Count();
            int directorsCount = unitOfWork.DirectorManager.Count();

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

            //AddMovie("Tenet", 2020,"Christopher Nolan", $"{Genres.SciFi},{Genres.Action},{Genres.Thriller}", Status.Watched.ToString(), collection.ID);
            //RemoveMovie(22);
            //UpdateGenre(18, $"{Genres.Drama},{Genres.Action},{Genres.Historical}");

            void AddMovie(string movieName, int year, string director, string genre, string status, int collectionId)
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

            Director CheckForTheDirector(string name)
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

            void RemoveDirector(string name)
            {
                director = unitOfWork.DirectorManager.FindByName(name);
                if(director != null)
                {
                    unitOfWork.DirectorManager.RemoveByName(name);
                    directorsCount--;
                    collection.DirectorsCount = directorsCount;
                    unitOfWork.SaveChanges();
                }
            }

            void UpdateStatus(int id, string status)
            {
                Movie movie = unitOfWork.MovieManager.Find(id);
                movie.Status = status;

                unitOfWork.MovieManager.Update(movie);
                unitOfWork.SaveChanges();
            }

            void UpdateGenre(int id, string genre)
            {
                Movie movie = unitOfWork.MovieManager.Find(id);
                movie.Genre = genre;

                unitOfWork.MovieManager.Update(movie);
                unitOfWork.SaveChanges();
            }
        }


    }
}
