using Data.Models;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Services.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _repository;

        public StudentService(IRepository<Student> repository)
        {
            _repository = repository;
        }

        public Student Add(Student entity)
        {
            return _repository.Add(entity);
        }

        public IQueryable<Student> GetAll()
        {
            return _repository.GetAll();
        }

        public IQueryable<Student> GetAll(Expression<Func<Student, bool>> predicate)
        {
            return _repository.GetAll(predicate);
        }

        public Student GetById(object id)
        {
            return _repository.GetById(id);
        }
    }
}
