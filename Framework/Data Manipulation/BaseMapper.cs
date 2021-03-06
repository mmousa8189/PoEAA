﻿using System;
using Framework.Domain;
using System.Collections;

namespace Framework.Data_Manipulation
{
    public delegate void SuccessfulInvocationDelegate(IDomainObject domainObject, Hashtable results);
    public delegate void FailedInvocationDelegate(IDomainObject domainObject, Hashtable results);

    public interface IBaseMapper_Instantiator<TEntity>
     where TEntity : IDomainObject
    {
    }

    public interface IBaseMapper
    {
        string GetEntityTypeName();
        bool Update(ref IDomainObject entity, SuccessfulInvocationDelegate successfulInvocation, FailedInvocationDelegate failedInvocation);
        bool Insert(ref IDomainObject entity, SuccessfulInvocationDelegate successfulInvocation, FailedInvocationDelegate failedInvocation);
        bool Delete(ref IDomainObject entity, SuccessfulInvocationDelegate successfulInvocation, FailedInvocationDelegate failedInvocation);
    }

    public interface IBaseMapper<TEntity> : IBaseMapper, IBaseMapper_Instantiator<TEntity>
        where TEntity: IDomainObject
    {
        bool Update(ref TEntity entity, SuccessfulInvocationDelegate successfulInvocation, FailedInvocationDelegate failedInvocation);
        bool Insert(ref TEntity entity, SuccessfulInvocationDelegate successfulInvocation, FailedInvocationDelegate failedInvocation);
        bool Delete(ref TEntity entity, SuccessfulInvocationDelegate successfulInvocation, FailedInvocationDelegate failedInvocation);
    }

    public abstract class BaseMapper<TEntity> : IBaseMapper<TEntity>
        where TEntity : IDomainObject
    {
        public string GetEntityTypeName()
        {
            return typeof(TEntity).Name;
        }

        public abstract bool Update(ref TEntity entity, SuccessfulInvocationDelegate successfulInvocation, FailedInvocationDelegate failedInvocation);
        public abstract bool Insert(ref TEntity entity, SuccessfulInvocationDelegate successfulInvocation, FailedInvocationDelegate failedInvocation);
        public abstract bool Delete(ref TEntity entity, SuccessfulInvocationDelegate successfulInvocation, FailedInvocationDelegate failedInvocation);

        public bool Update(ref IDomainObject entity, SuccessfulInvocationDelegate successfulInvocation, FailedInvocationDelegate failedInvocation)
        {
            TEntity instance = (TEntity) entity;

            SetSafeSuccessfulInvocator(ref successfulInvocation);
            SetSafeFailureInvocator(ref failedInvocation);

            return Update(ref instance, successfulInvocation, failedInvocation);
        }

        public bool Insert(ref IDomainObject entity, SuccessfulInvocationDelegate successfulInvocation, FailedInvocationDelegate failedInvocation)
        {
            TEntity instance = (TEntity)entity;

            SetSafeSuccessfulInvocator(ref successfulInvocation);
            SetSafeFailureInvocator(ref failedInvocation);

            return Insert(ref instance, successfulInvocation, failedInvocation);
        }

        public bool Delete(ref IDomainObject entity, SuccessfulInvocationDelegate successfulInvocation, FailedInvocationDelegate failedInvocation)
        {
            TEntity instance = (TEntity)entity;

            SetSafeSuccessfulInvocator(ref successfulInvocation);
            SetSafeFailureInvocator(ref failedInvocation);

            return Delete(ref instance, successfulInvocation, failedInvocation);
        }

        void SetSafeSuccessfulInvocator(ref SuccessfulInvocationDelegate successfulInvocation)
        {
            if (successfulInvocation == null)
                successfulInvocation = (domainObject, results) => { };
        }

        void SetSafeFailureInvocator(ref FailedInvocationDelegate failedInvocation)
        {
            if (failedInvocation == null)
                failedInvocation = (domainObject, results) => { };
        }
    }
}
