# PatientMedicationAssessment


===========================
Notes on implementation:—
===========================

** (1.) **
CREATE TABLE [medication].[Medication](
	[MedicationId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](10) NOT NULL,
	[CodeName] [nvarchar](100) NOT NULL,
	[MedicationCodeSystemId] [int] NOT NULL,
	[StrengthValue] [smallint] NOT NULL,
	[StrengthUnitId] [int] NOT NULL,
	[FormId] [int] NOT NULL,
	
The intention was for [StrengthUnitId] and [FormId] to be foreign keys to other reference data tables containing values such as "g/ml" and "tablet" (respectively).
However, this has been left as an exercise still to do. For now, the API will just write zeroes into these fields, since there are no primary key tables for these columns
to refer to.

** (2.) **
CREATE TABLE [medication].[MedicationRequest](
	[MedicationRequestId] [int] IDENTITY(1,1) NOT NULL,
	[PatientReference] [bigint] NOT NULL,
	[ClinicianReference] [bigint] NOT NULL,
	[MedicationReference] [varchar](10) NOT NULL,
	[ReasonText] [nvarchar](4000) NOT NULL,
	[PrescribedDate] [datetime2](7) NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NULL,
	[FrequencyNumberOfTimes] [int] NOT NULL,
	[FrequencyUnitId] [int] NOT NULL,
	[MedicationRequestStatusId] [smallint] NOT NULL,
	
The intention was for [FrequencyUnitId] to be a foreign key to a reference data table, [medication].[FrequencyUnit] containing the possible frequency units
such as "daily", "weekly", "fortnightly", "monthly". This has been left as an exercise still to do. For now, the API will just write zeroes into these fields,
since there is no primary key table for this column to refer to.
