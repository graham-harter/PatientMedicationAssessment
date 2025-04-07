using System;
using Microsoft.EntityFrameworkCore;
using Moq;
using PatientMedication.Infrastructure.DbContexts;
using PatientMedication.Infrastructure.Factories.Interfaces;
using PatientMedication.Infrastructure.Models;

namespace PatientMedication.Application.Tests;

/// <summary>
/// Unit tests for the <see cref="MedicationRequestUpdater"/> class.
/// </summary>
[TestClass]
public class MedicationRequestUpdaterTests
{
    private const short ActiveStatusId = 1;
    private const short OnHoldStatusId = 2;
    private const short CancelledStatusId = 3;
    private const short CompletedStatusId = 4;

    private static readonly IList<MedicationRequestStatus> _medicationRequestStatuses = new[]
    {
        new MedicationRequestStatus(1, "Active"),
        new MedicationRequestStatus(2, "On hold"),
        new MedicationRequestStatus(3, "Cancelled"),
        new MedicationRequestStatus(4, "Completed"),
    };

    #region Ctor tests

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Ctor_IPatientMedicationContextFactory_Null_ArgumentNullException()
    {
        // Arrange

        // Act
        var updater = new MedicationRequestUpdater((IPatientMedicationContextFactory)null!);

        // Assert
    }

    [TestMethod]
    public void Ctor_IPatientMedicationContextFactory_Instance_ObjectCreated()
    {
        // Arrange
        var factory = CreateMockPatientMedicationContextFactory();

        // Act
        var updater = new MedicationRequestUpdater(factory.Object);

        // Assert
        Assert.IsInstanceOfType<MedicationRequestUpdater>(updater);
    }

    #endregion // #region Ctor tests

    #region Private methods

    private static Mock<DbSet<MedicationRequestStatus>> CreateMockMedicationRequestStatusDbSet(int returnsMedicationRequestStatusId)
    {
        var mockDbSet = new Mock<DbSet<MedicationRequestStatus>>();

        return mockDbSet;
    }

    private static Mock<PatientMedicationContext> CreateMockPatientMedicationContext(
        Mock<DbSet<MedicationRequestStatus>> mockMedicationRequestStatusDbSet)
    {
        var mockContext = new Mock<PatientMedicationContext>();

        mockContext.Setup(ctx => ctx.MedicationRequestStatuses)
            .Returns(mockMedicationRequestStatusDbSet.Object);

        return mockContext;
    }

    private static Mock<IPatientMedicationContextFactory> CreateMockPatientMedicationContextFactory(
        PatientMedicationContext patientMedicationDbContext)
    {
        var mockFactory = new Mock<IPatientMedicationContextFactory>();

        mockFactory.Setup(fc => fc.Create())
            .Returns(patientMedicationDbContext);

        return mockFactory;
    }

    private static Mock<IPatientMedicationContextFactory> CreateMockPatientMedicationContextFactory()
    {
        var dbSet = CreateMockMedicationRequestStatusDbSet(OnHoldStatusId);
        var context = CreateMockPatientMedicationContext(dbSet);
        var factory = CreateMockPatientMedicationContextFactory(context.Object);
        return factory;
    }

    #endregion // #region Private methods
}
