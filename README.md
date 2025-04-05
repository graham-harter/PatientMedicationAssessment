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

