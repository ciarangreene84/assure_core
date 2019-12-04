CREATE SCHEMA [StaticFacade]
    AUTHORIZATION [dbo];






















GO
GRANT UPDATE
    ON SCHEMA::[StaticFacade] TO [AssureSystemAdministrator];


GO
GRANT SELECT
    ON SCHEMA::[StaticFacade] TO [AssureUser];


GO
GRANT SELECT
    ON SCHEMA::[StaticFacade] TO [AssureSystemAdministrator];


GO
GRANT INSERT
    ON SCHEMA::[StaticFacade] TO [AssureSystemAdministrator];


GO
GRANT EXECUTE
    ON SCHEMA::[StaticFacade] TO [AssureSystemAdministrator];


GO
GRANT DELETE
    ON SCHEMA::[StaticFacade] TO [AssureSystemAdministrator];


GO
GRANT SELECT
    ON SCHEMA::[StaticFacade] TO [AssureCustomer];

