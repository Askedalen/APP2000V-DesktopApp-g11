-- MySQL
-- Database: Workflow

DROP TABLE IF EXISTS EmployeeLeave;
DROP TABLE IF EXISTS AssignedTask;
DROP TABLE IF EXISTS ProjectParticipant;
DROP TABLE IF EXISTS PTask;
DROP TABLE IF EXISTS TaskList;
DROP TABLE IF EXISTS Report;
DROP TABLE IF EXISTS Project;
DROP TABLE IF EXISTS User;


CREATE TABLE User
(
    UserId       INT(11) NOT NULL AUTO_INCREMENT,
    Username     VARCHAR(55) NOT NULL,
    Password     VARCHAR(55) NOT NULL,
    FirstName    VARCHAR(55) NOT NULL,
    LastName     VARCHAR(55) NOT NULL,
    Email        VARCHAR(255) NOT NULL,
    PhoneNumber  VARCHAR(11),
    Role         INT,
    About        TEXT,

    CONSTRAINT UserPK PRIMARY KEY (UserId)
);


CREATE TABLE Project
(
    ProjectId           INT(11) NOT NULL AUTO_INCREMENT,
    ProjectName         VARCHAR(255) NOT NULL,
    ProjectDescription  VARCHAR(255),
    ProjectStart        DATE,
    ProjectDeadline     DATE,
    CompletionDate      DATE,
    ProjectManager      INT(11),
    MarkedAsFinished    BOOLEAN,

    CONSTRAINT ProjectPK PRIMARY KEY (ProjectId),
    CONSTRAINT ProjectUserFK FOREIGN KEY (ProjectManager) REFERENCES User (UserId) ON UPDATE CASCADE
);

CREATE TABLE Report
(
    ReportId        INT(11) NOT NULL AUTO_INCREMENT,
    ProjectId       INT(11),
    CompletionDate  DATETIME DEFAULT CURRENT_TIMESTAMP,
    Comment         TEXT,

    CONSTRAINT ReportPK PRIMARY KEY (ReportId),
    CONSTRAINT ReportProjectFK FOREIGN KEY (ProjectId) REFERENCES Project (ProjectId)
);


CREATE TABLE TaskList
(
    TaskListId     INT(11) NOT NULL AUTO_INCREMENT,
    ProjectId      INT NOT NULL,
    ListName       VARCHAR(55),

    CONSTRAINT TaskListPK PRIMARY KEY (TaskListId),
    CONSTRAINT TaskListFK FOREIGN KEY (ProjectId) REFERENCES Project (ProjectId)
);


CREATE TABLE PTask
(
    TaskId            INT(11) NOT NULL AUTO_INCREMENT,
    TaskName          VARCHAR(255) NOT NULL,
    Description       VARCHAR(255),
    Priority          ENUM ('low', 'normal', 'high'),
    TaskCreationDate  DATETIME DEFAULT CURRENT_TIMESTAMP,
    TaskDeadline      DATE,
    CompletionDate    DATE,
    TaskProjectId     INT(11),
    TaskListId        INT(11),

    CONSTRAINT PTaskPK PRIMARY KEY (TaskId),
    CONSTRAINT PTaskProjectFK FOREIGN KEY (TaskProjectId) REFERENCES Project (ProjectId),
    CONSTRAINT PTaskTaskListFK FOREIGN KEY (TaskListId) REFERENCES TaskList (TaskListId)
);



CREATE TABLE ProjectParticipant
(
    ProjectId     INT(11) NOT NULL,
    UserId        INT(11) NOT NULL,

    CONSTRAINT ProjectParticipantPK PRIMARY KEY (ProjectId, UserId),
    CONSTRAINT ProjectParticipantProjectFK FOREIGN KEY (ProjectId) REFERENCES Project (ProjectId),
    CONSTRAINT ProjectParticipantUserFK FOREIGN KEY (UserId) REFERENCES User (UserId)
);


CREATE TABLE AssignedTask
(
    AssignedTaskId  INT(11) NOT NULL AUTO_INCREMENT,
    ProjectId       INT(11) NOT NULL,
    UserId          INT(11) NOT NULL,
    TaskId          INT(11) NOT NULL,

    CONSTRAINT AssignedTaskPK PRIMARY KEY (AssignedTaskId),
    -- Får ikke lov til dette i EF Core
    -- Legg inn manuelt i databasen etter scaffolding HVIS det ikke føkker for "Database-Update"
    CONSTRAINT AssignedTaskProjectParticipantFK FOREIGN KEY (ProjectId, UserId) REFERENCES ProjectParticipant (ProjectId, UserId),
    CONSTRAINT AssignedTaskPTaskFK FOREIGN KEY (TaskId) REFERENCES PTask (TaskId)
);

-- La til etter scaffolding (models created) og etter InitialCreate (migrations) 
-- ALTER TABLE AssignedTask ADD FOREIGN KEY (ProjectId, UserId) REFERENCES ProjectParticipant (ProjectId, UserId);


CREATE TABLE EmployeeLeave
(
    LeaveId       INT(11) NOT NULL AUTO_INCREMENT,
    UserId        INT(11) NOT NULL,
    FromDate      DATE,
    ToDate        DATE,

    CONSTRAINT EmployeeLeavePK PRIMARY KEY (LeaveId),
    CONSTRAINT EmployeeLeaveUserFK FOREIGN KEY (UserId) REFERENCES User (UserId)
);


INSERT INTO User VALUES
(1, 'pernille', '123', 'Pernille', 'Pindsle', 'schmekuk@gmail.com', '22225555', 0, 'Why do Java programmers have to wear glasses? ...Because they don’t C#'),
(2, 'tinahodepina', 'paracet', 'Tina', 'Rambo', '-', '-', 0, 'Always code as if the guy who ends up maintaining your code will be a violent psychopath who knows where you live'),
(3, 'benjamin', '1234', 'Benjamin', 'Kløw', '-', '-', 1, 'The best thing about a boolean is even if you are wrong, you are only off by a bit'),
(4, 'simen', '1234', 'Simen', 'SL', '-', '-', 0, 'How many programmers does it take to change a light bulb? ...none, that’s a hardware problem');


INSERT INTO Project VALUES
(1, 'ProjectOne', 'Test project 1', '2019.06.01', '2019.06.14', NULL, 1, 0),
(2, 'ProjectTwo', 'Test project 2', '2019.06.01', '2019.06.14', '2019.06.02', 2, 1),
(3, 'ProjectThree', 'Test project 3', '2019.05.10', '2019.06.01', NULL, 4, 0),
(4, 'ProjectFour', 'Test project 4', '2019.05.01', '2019.05.03', NULL, 1, 0);


INSERT INTO Report (ProjectId, Comment) VALUES
(2, "My fucking GOD, that's efficient!");


INSERT INTO TaskList VALUES
(1, 1, 'ListOne'),
(2, 1, 'ListTwo');


INSERT INTO PTask (TaskId, TaskName, Description, Priority, TaskDeadline, CompletionDate, TaskProjectId, TaskListId) VALUES
(1, 'TaskOne', 'Test task 1', 'low', '2019.03.14', NULL, 1, 1),
(2, 'TaskTwo', 'Test task 2', 'high', '2019.04.14', '2019.04.13', 2, 2),
(3, 'TaskThree', 'Test task 3', 'normal', '2019.06.01', NULL, 1, 1);


INSERT INTO ProjectParticipant VALUES
(1, 1),
(2, 2),
(1, 3),
(1, 4);


INSERT INTO AssignedTask (ProjectId, UserId, TaskId) VALUES
(1, 1, 1);


INSERT INTO EmployeeLeave (UserId, FromDate, ToDate) VALUES
(4,  '2019.03.14',  '2019.04.14');



-- SELECT * FROM User;
-- SELECT * FROM Project;
-- SELECT * FROM Report;
-- SELECT * FROM TaskList;
-- SELECT * FROM PTask;
-- SELECT * FROM ProjectParticipant;
-- SELECT * FROM AssignedTask;
-- SELECT * FROM EmployeeLeave;

-- dotnet ef dbcontext scaffold "server=127.0.0.1;port=3306;user=app;password=app2000;database=Workflow" MySql.Data.EntityFrameworkCore -o Models --data-annotations -f
