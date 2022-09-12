using MovieCollection.ContextManager;
using MovieCollection.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCollection.Managers
{
    public class DirectorManager : ContextManager<Director>, IDirectorManager
    {
        public DirectorManager(MyContext context)
            : base(context)
        {

        }
    }
}
