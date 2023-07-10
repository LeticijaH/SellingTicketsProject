using eBoxOffice.Domain;
using eBoxOffice.Domain.Domain_models;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBoxOffice.Service.Interface
{
    public interface IMovieService
    {
        IEnumerable<Movie> GetAll();
        Movie Get(int id);
    }
}
