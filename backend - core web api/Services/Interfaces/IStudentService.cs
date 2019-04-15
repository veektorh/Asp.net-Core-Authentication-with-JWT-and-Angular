using Data.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Services.Services
{
    public interface IStudentService
    {
        Student Add(Student entity);
        IQueryable<Student> GetAll();

        Student GetById(object id);

        IQueryable<Student> GetAll(Expression<Func<Student, bool>> predicate);
    }
}