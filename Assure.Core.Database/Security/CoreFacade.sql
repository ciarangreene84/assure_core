CREATE SCHEMA [CoreFacade]
    AUTHORIZATION [dbo];






















































GO
GRANT UPDATE
    ON SCHEMA::[CoreFacade] TO [AssureUser];


GO
GRANT UPDATE
    ON SCHEMA::[CoreFacade] TO [AssureSystemAdministrator];


GO
GRANT SELECT
    ON SCHEMA::[CoreFacade] TO [AssureUser];


GO
GRANT SELECT
    ON SCHEMA::[CoreFacade] TO [AssureSystemAdministrator];


GO
GRANT INSERT
    ON SCHEMA::[CoreFacade] TO [AssureUser];


GO
GRANT INSERT
    ON SCHEMA::[CoreFacade] TO [AssureSystemAdministrator];


GO
GRANT EXECUTE
    ON SCHEMA::[CoreFacade] TO [AssureUser];


GO
GRANT EXECUTE
    ON SCHEMA::[CoreFacade] TO [AssureSystemAdministrator];


GO
GRANT DELETE
    ON SCHEMA::[CoreFacade] TO [AssureUser];


GO
GRANT DELETE
    ON SCHEMA::[CoreFacade] TO [AssureSystemAdministrator];


GO
GRANT SELECT
    ON SCHEMA::[CoreFacade] TO [AssureCustomer];

