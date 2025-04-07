# PatientMedicationAssessment

To run this application, ensure that PatientMedicationApi is set to be the startup project.
Run using option "https" in Visual Studio. (Execution within Dockerfile container has not yet been made to work. Exercise still to do.)

The API has the following endpoints:-

[POST] /PatientMedication

This is used to create new medication requests.


[GET] /PatientMedication

Retrieve a list of medication requests by some or all of the following filter criteria:-
 - Medication request status description. If specified, this must match (case-insensitively) a valid medication request status description.
 - Start date at which the medication was prescribed. If the finish prescription date has been specified, this value must also be specified.
 - Finish date at which the medication was prescribed. If the start prescription date has been specified, this value must also be specified.
 
It is also considered an error to specify no filter criteria.


[PATCH] /PatientMedication/{medicationRequestId:int}

This is used to update the specified mediation request. The following properties can be updated:-
 - EndDate
 - FrequencyNumberOfTimes and FrequencyUnit (Note: These values must be specified together, or neither be specified.)
 - Status

Any properties not specified in the request will be left with their existing values.


============
Unit tests
============

There is a small unit test project in the solution, written using MSTest and Moq.
To run the unit tests, rebuild the solution and then right-click in Solution Explorer, either on the solution, or on the test project,
and choose "Run Tests" in the context menu.


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

** (3.) **
Currently the GetMedicationRequests(...) API endpoint is applying the filter criteria client-side, not server-side.
This should be improved to apply the filter criteria server-side for efficient querying.

** (4.) **
Non-clustered indices should also be added to the columns queried by the filter criteria, namely:-
 - [medication].[MedicationRequest].[PrescribedDate]

** (5.) **
On the CreateMedicationRequest(...) and UpdateMedicationRequest(...) API endpoints, there is no valdation as yet
to ensure that the EndDate, if specified, is equal to or later than the StartDate.
