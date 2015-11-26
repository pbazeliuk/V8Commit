using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V8Commit.Repositories
{
    public class FileV8CommitRepository : IV8CommitRepository
    {
        private readonly string _connectionString;

        public FileV8CommitRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }
    }
}
