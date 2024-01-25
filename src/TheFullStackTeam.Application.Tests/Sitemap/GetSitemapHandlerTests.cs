using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using SimpleMvcSitemap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFullStackTeam.Application.Model.Sitemap;
using TheFullStackTeam.Application.Sitemap;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Domain.Entities.Base;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Tests.Sitemap
{
    public class GetSitemapHandlerTests
    {

        //[Fact]
        //public async Task Handle_ShouldReturnSitemap_WhenCalled()
        //{
        //    // ARRANGE:
        //    var supportedLanguages = new List<Language>  { new Language { Name = "en", IsoCode = "en" }, new Language { Name = "es", IsoCode = "es" } };
        //    var professionals = new List<Professional> { new Professional { Moniker = "professional1", Modified = new DateTime(2022, 1, 1) } };
        //    var organizations = new List<Organization> { new Organization { Moniker = "organization1", Modified = new DateTime(2022, 1, 1) } };
        //    var jobs = new List<Domain.Entities.Jobs> { new Domain.Entities.Jobs { Moniker = "job1", Modified = new DateTime(2022, 1, 1) } };

        //    var languageDbSetMock = new Mock<DbSet<Language>>();
        //    var professionalDbSetMock = new Mock<DbSet<Professional>>();
        //    var organizationDbSetMock = new Mock<DbSet<Organization>>();
        //    var jobDbSetMock = new Mock<DbSet<Domain.Entities.Jobs>>();



        //    languageDbSetMock.As<IQueryable<Language>>().Setup(x => x.Provider).Returns(supportedLanguages.AsQueryable().Provider);
        //    languageDbSetMock.As<IQueryable<Language>>().Setup(x => x.Expression).Returns(supportedLanguages.AsQueryable().Expression);
        //    languageDbSetMock.As<IQueryable<Language>>().Setup(x => x.ElementType).Returns(supportedLanguages.AsQueryable().ElementType);
        //    languageDbSetMock.As<IQueryable<Language>>().Setup(x => x.GetEnumerator()).Returns(supportedLanguages.AsQueryable().GetEnumerator());

        //    professionalDbSetMock.As<IQueryable<Professional>>().Setup(x => x.Provider).Returns(professionals.AsQueryable().Provider);
        //    professionalDbSetMock.As<IQueryable<Professional>>().Setup(x => x.Expression).Returns(professionals.AsQueryable().Expression);
        //    professionalDbSetMock.As<IQueryable<Professional>>().Setup(x => x.ElementType).Returns(professionals.AsQueryable().ElementType);
        //    professionalDbSetMock.As<IQueryable<Professional>>().Setup(x => x.GetEnumerator()).Returns(professionals.AsQueryable().GetEnumerator());

        //    organizationDbSetMock.As<IQueryable<Organization>>().Setup(x => x.Provider).Returns(organizations.AsQueryable().Provider);
        //    organizationDbSetMock.As<IQueryable<Organization>>().Setup(x => x.Expression).Returns(organizations.AsQueryable().Expression);
        //    organizationDbSetMock.As<IQueryable<Organization>>().Setup(x => x.ElementType).Returns(organizations.AsQueryable().ElementType);
        //    organizationDbSetMock.As<IQueryable<Organization>>().Setup(x => x.GetEnumerator()).Returns(organizations.AsQueryable().GetEnumerator());

        //    jobDbSetMock.As<IQueryable<Domain.Entities.Jobs>>().Setup(x => x.Provider).Returns(jobs.AsQueryable().Provider);
        //    jobDbSetMock.As<IQueryable<Domain.Entities.Jobs>>().Setup(x => x.Expression).Returns(jobs.AsQueryable().Expression);
        //    jobDbSetMock.As<IQueryable<Domain.Entities.Jobs>>().Setup(x => x.ElementType).Returns(jobs.AsQueryable().ElementType);
        //    jobDbSetMock.As<IQueryable<Domain.Entities.Jobs>>().Setup(x => x.GetEnumerator()).Returns(jobs.AsQueryable().GetEnumerator());

        //    var dbContextMock = new Mock<ITheFullStackTeamDbContext>();
        //    dbContextMock.Setup(x => x.Languages).Returns(languageDbSetMock.Object);
        //    dbContextMock.Setup(x => x.Professionals).Returns(professionalDbSetMock.Object);
        //    dbContextMock.Setup(x => x.Organizations).Returns(organizationDbSetMock.Object);
        //    dbContextMock.Setup(x => x.Jobs).Returns(jobDbSetMock.Object);

        //    var objectToTest = new GetSitemapHandler(dbContextMock.Object);

        //    // ACT:
        //    var result = await objectToTest.Handle(new GetSitemap(), CancellationToken.None);

        //    // Assert
        //    // TODO: Perform assertions on the result
        //}
    }
}
