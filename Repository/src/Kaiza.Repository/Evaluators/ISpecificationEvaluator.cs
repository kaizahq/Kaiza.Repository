﻿namespace Kaiza.Repository.Evaluators;

public interface ISpecificationEvaluator
{
    IQueryable<T> GetQuery<T>(IQueryable<T> inputQuery, ISpecification<T> specification, bool evaluateCriteriaOnly = false) where T : class;
}