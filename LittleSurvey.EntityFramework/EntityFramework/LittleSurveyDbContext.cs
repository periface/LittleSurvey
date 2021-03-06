﻿using System;
using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using LittleSurvey.Authorization.Roles;
using LittleSurvey.MultiTenancy;
using LittleSurvey.Users;

namespace LittleSurvey.EntityFramework
{
    public class LittleSurveyDbContext : AbpZeroDbContext<Tenant, Role, User>, IDisposable
    {
        //TODO: Define an IDbSet for your Entities...
        public IDbSet<Survey.Core.Entities.Survey> Surveys { get; set; }
        public IDbSet<Survey.Core.Entities.SurveyQuestion> SurveyQuestions { get; set; }
        public IDbSet<Survey.Core.Entities.Answer> Answers { get; set; }
        public IDbSet<Survey.Core.Entities.OfferedAnswer> OfferedAnswers { get; set; }
        public IDbSet<Survey.Core.Entities.SelectedAnswer> SelectedAnswers { get; set; }
        public IDbSet<Survey.Core.Entities.SurveyQuestionAnswer> SurveyQuestionAnswers { get; set; }
        public IDbSet<Survey.Core.Entities.Question> Questions { get; set; }
        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public LittleSurveyDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in LittleSurveyDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of LittleSurveyDbContext since ABP automatically handles it.
         */
        public LittleSurveyDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public LittleSurveyDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public LittleSurveyDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
