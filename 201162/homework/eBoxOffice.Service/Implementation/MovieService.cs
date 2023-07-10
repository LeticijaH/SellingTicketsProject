using eBoxOffice.Domain;
using eBoxOffice.Domain.Domain_models;
using eBoxOffice.Repository.Interface;
using eBoxOffice.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBoxOffice.Service.Implementation
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;

        public MovieService(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public IEnumerable<Movie> GetAll()
        {
            return _movieRepository.GetAll();
        }

        public Movie Get(int id)
        {
            return _movieRepository.Get(id);
        }

     
    }
}
