/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

ALTER DATABASE AssureCore SET READ_COMMITTED_SNAPSHOT ON;

:r .\InsertCountries.sql
:r .\InsertCurrencies.sql

-- Below this line is deployment specific
:r .\InsertProducts.sql
:r .\InsertBenefits.sql
:r .\InsertProductBenefits.sql

-- Below this line is test data
:r .\InsertTestData.sql

